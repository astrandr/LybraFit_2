//Designed and Implemented by Andrey Batchvarov 2018
//Copyright Andrey Borissov Batchvarov 2018

using MathLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FuncLib
{
    public class Parser
    {
        public static Dictionary<string, string> greekSymbols = new Dictionary<string, string>()
        {
            {"alpha",   "\u03b1"},
            {"beta",    "\u03b2"},
            {"gamma",   "\u03b3"},
            {"delta",   "\u03b4"},
            {"epsilon", "\u03b5"},
            {"zeta",    "\u03b6"},
            {"eta",     "\u03b7"},
            {"theta",   "\u03b8"},
            {"iota",   "\u03b9"},
            {"lambda",  "\u03bb"},
            {"mu",      "\u03bc"},
            {"nu",      "\u03bd"},
            {"xi",      "\u03be"},
            {"pi",      "\u03c0"},
            {"PI",      "\u03c0"},
            {"rho",     "\u03c1"},
            {"sigma",   "\u03c3"},
            {"tau",     "\u03c4"},
            {"phi",     "\u03c6"},
            {"chi",     "\u03c7"},
            {"psi",     "\u03c8"},
            {"omega",   "\u03c9"}
        };

        static string[] mathLibOperations;

        static string[] standardOperations = new string[] { "(", ")", "+", "-", "*", "/", "^", "," };
        
        //Regex for floating point number
        static Regex reg = new Regex(@"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?");


        static Parser()
        {

            List<string> tempList = new List<string>();

            MethodInfo[] mathMethods = typeof(Math).GetMethods();

            foreach (MethodInfo methodInfo in mathMethods)
            {
                if (!tempList.Contains(methodInfo.Name))
                {
                    tempList.Add(methodInfo.Name);
                }
            }

            tempList.Add("PI");
            tempList.Add("Pi");
            mathLibOperations = tempList.ToArray();
        }

        public static string ReplaceParametersWithValues(string description, string[] parameters, float[] values, string[] formattedValues)
        {
            string expression = description.Replace(" ", "");

            foreach (string mathMethod in mathLibOperations)
            {
                expression = expression.Replace(mathMethod, ":" + mathMethod + ":");
            }

            foreach (string standardOperation in standardOperations)
            {
                expression = expression.Replace(standardOperation, ":" + standardOperation + ":");
            }

            expression = ":" + expression + ":";

            for (int parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                expression = expression.Replace(":" + parameters[parameterIndex] + ":", formattedValues[parameterIndex]);
            }

            //Remove the splitter ':'
            expression = expression.Replace(":", "");

            //Remove artifacts '+-"
            expression = expression.Replace("+-", "-");

            //Remove artifacts '--"
            expression = expression.Replace("--", "");

            return expression;

        }

        public static string ReplaceParametersWithGreekSymbols(string description, string[] parameters)
        {
            string expression = description.Replace(" ", "");

            foreach (string mathMethod in mathLibOperations)
            {
                expression = expression.Replace(mathMethod, ":" + mathMethod + ":");
            }

            foreach (string standardOperation in standardOperations)
            {
                expression = expression.Replace(standardOperation, ":" + standardOperation + ":");
            }

            expression = ":" + expression + ":";

            foreach (string symbolKey in greekSymbols.Keys)
            {
                expression = expression.Replace(":" + symbolKey + ":", ":" + greekSymbols[symbolKey] + ":");
            }

            //Remove the splitter ':'
            expression = expression.Replace(":", "");

            //Remove artifacts '+-"
            expression = expression.Replace("+-", "-");

            //Remove artifacts '--"
            expression = expression.Replace("--", "");

            return expression;

        }

        public static string GetUniqueExpression(string description, string[] parameters)
        {
            string expression = description.Replace(" ", "");

            foreach (string mathMethod in mathLibOperations)
            {
                expression = expression.Replace(mathMethod, ":" + mathMethod + ":");
            }

            foreach (string standardOperation in standardOperations)
            {
                expression = expression.Replace(standardOperation, ":" + standardOperation + ":");
            }

            expression = ":" + expression + ":";

            for (int parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                expression = expression.Replace(":" + parameters[parameterIndex] + ":", "P");
            }

            expression = expression.Replace(":x:", ":X:");

            //Remove the splitter ':'
            expression = expression.Replace(":", "");

            //Remove artifacts '+-"
            expression = expression.Replace("+-", "-");

            //Remove artifacts '--"
            expression = expression.Replace("--", "");

            return expression;
        }

        /// <summary>
        /// ParseFuncDescription2 - uses math tree parser from MathControl
        /// </summary>
        /// <param name="description"></param>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        public static void ParseFuncDescription(string description, out string expression, out string[] parameters)
        {

            var prms = new List<string>();

            Node mathTree = new MathParser().BuildTree(description);

            expression = GetExpressionFromTree(mathTree, ref prms);

            parameters = prms.ToArray();

        }


        //TODO: Refactor this to Builder pattern using same parser to build tree but supplying nodes specific for this processing and nodes specific for math control visualization
        /// <summary>
        /// GetExpressionFromTree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetExpressionFromTree(Node node, ref List<string> parameters)
        {
            string expression = "";
            if (node is ExponentNode)
            {
                expression = "Math.Exp" + GetExpressionFromTree((node as ExponentNode).RightChild, ref parameters);
            }
            else
                if (node is PowerNode)
                {
                    expression = "Math.Pow(" + GetExpressionFromTree((node as BinaryOperationNode).LeftChild, ref parameters) + "," + GetExpressionFromTree((node as BinaryOperationNode).RightChild, ref parameters) + ")";
                }
                else
                    if (node is MultiplicationNode)
                    {
                        expression = GetExpressionFromTree((node as BinaryOperationNode).LeftChild, ref parameters) + "*" + GetExpressionFromTree((node as BinaryOperationNode).RightChild, ref parameters);
                    }
                    else                                            
                        if (node is SubtractionNode)
                        {
                            if ((node as BinaryOperationNode).LeftChild != null)
                            {
                                expression = GetExpressionFromTree((node as BinaryOperationNode).LeftChild, ref parameters) + node.Symbol + GetExpressionFromTree((node as BinaryOperationNode).RightChild, ref parameters);
                            }
                            else
                            {
                                //Just negative sign infront of the remaining math expression
                                expression = node.Symbol + GetExpressionFromTree((node as BinaryOperationNode).RightChild, ref parameters);
                            }
                        }
                        else
                        if (node is BinaryOperationNode)
                        {
                            expression = GetExpressionFromTree((node as BinaryOperationNode).LeftChild, ref parameters) + node.Symbol + GetExpressionFromTree((node as BinaryOperationNode).RightChild, ref parameters);
                        }
                        else
                            if (node is SquareRootNode)
                            {
                                expression = "Math.Sqrt" + GetExpressionFromTree((node as FunctionNode).Body, ref parameters);
                            }
                            else
                                if (node is FunctionNode)
                                {
                                    expression = "Math." + node.Symbol + GetExpressionFromTree((node as FunctionNode).Body, ref parameters);
                                }
                                else
                                    if (node is BracketNode)
                                    {
                                        expression = "(" + GetExpressionFromTree((node as BracketNode).Body, ref parameters) + ")";
                                    }
                                    else
                                    {
                                        //Symbol
                                        if (node.Symbol.ToUpper() == "X")
                                        {
                                            expression = node.Symbol == "x" ? "X" : node.Symbol;
                                        }
                                        else
                                        {
                                            if (node.Symbol != "\u03c0" && !reg.IsMatch(node.Symbol))
                                            {
                                                //Not a number but a paramater
                                                if (!parameters.Contains(node.Symbol))
                                                {
                                                    parameters.Add(node.Symbol);
                                                }

                                                expression = "P[" + parameters.IndexOf(node.Symbol) + "]";
                                            }
                                            else
                                            {
                                                expression = node.Symbol;
                                            }
                                        }
                                    }

            return expression;
        }

    }
}

