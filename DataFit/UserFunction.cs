//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MathLib;
using Microsoft.CSharp;

namespace FuncLib
{
    [Serializable]
    public class UserFunction : FitFunction
    {
        private string expression;

        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }


        private CompilerResults compilerResults;

        //Default constructor is needed for serialization
        public UserFunction()
        {

        }
        private UserFunction(string name, string description, string expression, CompilerResults compilerResults, string[] parameters) : base()
        {
            this.name = name;
            this.description = description;
            this.expression = expression;
            this.compilerResults = compilerResults;
            this.parameters = parameters;
        }

        public static UserFunction Create(string name, string description, out string compileErrors)
	    {
            compileErrors = String.Empty;
            string expression = ""; 
            string[] parameters;
            try
            {
                Parser.ParseFuncDescription(description, out expression, out parameters);                
            }
            catch
            {                
                return null;
            }
            string piCorrectedExpression = expression.Replace("\u03c0", "Math.PI");
            CompilerResults compilerResults = Compile(piCorrectedExpression);
            // Check errors
            if (compilerResults.Errors.HasErrors)
            {
                StringBuilder builder = new StringBuilder();
                foreach (CompilerError error in compilerResults.Errors)
                {
                    if (!builder.ToString().Contains(error.ErrorText))
                    {
                        builder.AppendLine(error.ErrorText);
                    }
                }

                compileErrors = builder.ToString();

                return null;
            }


            return new UserFunction(name, description, expression, compilerResults, parameters);            
	    }

        private static CompilerResults Compile(string funcString)
        {
            string SourceString =
                 @" using System;
                    public class CustomFunction
                   {
                    public static double GetValue(double X, double[] P)
                    {
                        return (double)(" + funcString + @");
                    }

                    public static double[] GetValues(double[] arrX, double[] P)
                    {
                        double[] Y = new double[arrX.Length];
                        for(int i = 0; i < arrX.Length; i++)
                        {
                            double X = arrX[i];
                            Y[i] = (double)(" + funcString +@");
                        }
                        return Y;
                    }
                  }";

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            ICodeCompiler icc = codeProvider.CreateCompiler();
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;

            return icc.CompileAssemblyFromSource(parameters, SourceString);
        }

        public double Calculate(double X, double[] P)
        {            
            // Execute
            Assembly assembly = compilerResults.CompiledAssembly;
            Type program = assembly.GetType("CustomFunction");
            MethodInfo execute = program.GetMethod("GetValue");
            double Y = (double)execute.Invoke(this, new object[] { X, P });
            return Y;
        }

        public double[] Calculate(double[] X, double[] P)
        {
            // Execute
            Assembly assembly = compilerResults.CompiledAssembly;
            Type program = assembly.GetType("CustomFunction");
            MethodInfo execute = program.GetMethod("GetValues");
            double[] Y = (double[])execute.Invoke(this, new object[] { X, P });
            return Y;
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = Calculate(X[i], P);
            }
        }
    }
}