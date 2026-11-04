//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathLib
{
    public class MathParser
    {

        private static string[] mathLibOperations;

        private static string[] standardOperations = new string[] { "+", "-", "*", "/", "^" };

        private int nestedLevels;

        public int NestedLevels
        {
            get { return nestedLevels; }
        }

        public MathParser()
        {
            StringBuilder sb = new StringBuilder();
            List<string> tempList = new List<string>();

            MethodInfo[] mathMethods = typeof(Math).GetMethods();

            foreach (MethodInfo methodInfo in mathMethods)
            {
                if (!tempList.Contains(methodInfo.Name))
                {
                    tempList.Add(methodInfo.Name);
                    sb.Append(" "+methodInfo.Name + ",");
                }
            }
            string s = sb.ToString();
            tempList.Add("Ln");

            mathLibOperations = tempList.ToArray();
        }

        private string[] GetExpressionInTokens(string expression)
        {

            //Regex for floating point number
            Regex reg = new Regex(@"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?");

            //Clean from spaces and returns
            expression = expression.Replace(" ", "");
            expression = expression.Replace("\r", "");
            expression = expression.Replace("\n", "");

            foreach (string mathMethod in mathLibOperations)
            {
                expression = expression.Replace(mathMethod, ":;:M:" + mathMethod + ":");
            }

            foreach (string standardOperation in standardOperations)
            {
                expression = expression.Replace(standardOperation, ":;:O:" + standardOperation + ":");
            }

            expression = expression.Replace("(", ":;:(:");
            expression = expression.Replace(")", ":;:):");

            expression = ":" + expression + ":";

            expression = expression.Replace("::", ":");

            return expression.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private Node BuildTree(Stack<Node> rpnSequence)
        {
            if (rpnSequence.Count == 1)
            {
                //this is just single symbol
                return rpnSequence.Pop();
            }
            Node node = null;
            Node currentNode = rpnSequence.Pop();

            while (rpnSequence.Count > 0)
            {
                node = rpnSequence.Pop();

                if (currentNode is OperationNode)
                {
                    while (!currentNode.CanAppendChild)
                    {
                        currentNode = currentNode.Parent;
                    }
                    currentNode.AppendChild(node);
                }

                if (node is OperationNode)
                {
                    currentNode = (OperationNode)node;
                }
            }

            //return the root
            while (currentNode.Parent != null)
            {
                currentNode = currentNode.Parent;
            }

            return currentNode;
        }

        private Stack<Node> GetRPN(string[] tokens, int startIndex, ref int endIndex)
        {
            Stack<Node> rpn = new Stack<Node>(); //Reverse Polish Notation of the math expression
            Stack<Node> operationsStack = new Stack<Node>();
            Node currentNode = new Node();
            Node newNode = null;

            //Build RPN using Shunting-yrad algorithm of Dijkstra
            for (int i = startIndex; i <= endIndex; i++)
            {
                string s;
                if (tokens[i] == ";")
                {
                    i++; s = tokens[i];
                    if (s == "O")
                    {
                        i++;
                        switch (tokens[i])
                        {
                            case "+": newNode = new AdditionNode(); break;
                            case "-": newNode = new SubtractionNode(); break;
                            case "*": newNode = new MultiplicationNode(); break;
                            case "/": newNode = new DivisionNode(); break;
                            case "^": newNode = new PowerNode(); break;
                        }

                        //If precedence is greater than top of the stack move everything from it to rpn stack.
                        while (operationsStack.Count > 0 && newNode.PrecedencePos < operationsStack.Peek().PrecedencePos)
                        {
                            rpn.Push(operationsStack.Pop());
                        }

                        if (newNode is BinaryOperationNode)
                        {
                            operationsStack.Push(newNode);
                        }
                    }
                    else
                    {
                        switch (tokens[i])
                        {
                            case "(":
                                {
                                    newNode = new BracketNode();
                                    int newIndex = tokens.Length - 1;
                                    ((BracketNode)newNode).Body = new MathParser().BuildTree(GetRPN(tokens, i + 1, ref newIndex));
                                    rpn.Push(newNode);
                                    i = newIndex;
                                    break;
                                }
                            case ")": endIndex = i; break;
                            case "M":
                                {
                                    i++;
                                    BracketNode bracketNode = new BracketNode();
                                    int newIndex = tokens.Length - 1;
                                    Stack<Node> subRpn = GetRPN(tokens, i + 3, ref newIndex);
                                    bool hasDivisionOrPower = subRpn.Any<Node>(n => n is DivisionNode || n is PowerNode);
                                    bracketNode.Body = new MathParser().BuildTree(subRpn);

                                    //Special handling of exponent 
                                    if (tokens[i] == "Exp" && !hasDivisionOrPower)
                                    {
                                        newNode = new ExponentNode();
                                        newNode.AppendChild(bracketNode); //First append child is right child
                                        newNode.AppendChild(new SymbolNode("e"));
                                    }
                                    else
                                    {
                                        //Special handling of square root
                                        if (tokens[i] == "Sqrt")
                                        {
                                            newNode = new SquareRootNode("|");
                                        }
                                        else
                                        {
                                            newNode = new FunctionNode(tokens[i]);
                                        }
                                        ((FunctionNode)newNode).Body = bracketNode;
                                    }
                                    rpn.Push(newNode);
                                    i = newIndex;
                                };
                                break;
                        }
                    }
                }
                else
                {
                    newNode = new SymbolNode(tokens[i]);
                    rpn.Push(newNode);
                }
            }

            while (operationsStack.Count > 0)
            {
                rpn.Push(operationsStack.Pop());
            }
            return rpn;
        }


        /// <summary>
        /// SetNestedLevels - this calculates how many times we run into power so the font must be decreased
        /// </summary>
        /// <param name="node"></param>
        private void SetNestedLevels(Node node, int level)
        {
            //TODO : refactor this, nested level should be part of the node so we can avoid that complicated if
            if (node is PowerNode)
            {
                level += (node as PowerNode).SmallerFontDistance;
                if (level > nestedLevels)
                {
                    nestedLevels = level;
                }
            }
            if (node is BracketNode)
            {
                SetNestedLevels((node as BracketNode).Body, level);
            }
            if (node is BinaryOperationNode)
            {
                if ((node as BinaryOperationNode).LeftChild != null) SetNestedLevels((node as BinaryOperationNode).LeftChild, level);
                if ((node as BinaryOperationNode).RightChild != null) SetNestedLevels((node as BinaryOperationNode).RightChild, level);
            }
        }

        public Node BuildTree(string mathExpression)
        {
            Node expressionTree = null;
            try
            {
                string[] tokens = GetExpressionInTokens(mathExpression);
                int endIndex = tokens.Length - 1;
                Stack<Node> rpn = GetRPN(tokens, 0, ref endIndex);
                nestedLevels = 0;
                expressionTree = BuildTree(rpn);
                SetNestedLevels(expressionTree, 0);
            }
            catch
            {
            }

            return expressionTree;
        }
    }
}

