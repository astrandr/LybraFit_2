//Designed and Implemented by Andrey Batchvarov 2018
using FuncLib;
using MathLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DataFit
{
    static class Program
    {
        private static string CUSTOM_MODELS_FILE_NAME = "CustomModels.txt";
        private static string CUSTOM_MODELS_PARAMETERS_FILE_NAME = "CustomModelsParameters.txt";
        public static string PROJECT_FILE_EXTENSION = ".prj";
        public static string APPLICATION_NAME = "LybraFit";
        public static Dictionary<string, UserFunction> UserModels = new Dictionary<string, UserFunction>();
        public static Dictionary<string, FitFunction> LibraryModels = new Dictionary<string, FitFunction>();

        public static Dictionary<string, float[]> UserModelsInitialParameter = new Dictionary<string, float[]>();

        public static bool IsFromUser
        {
            get { return enterFromCode == 0; }
            set { if (value) enterFromCode--; else enterFromCode++; }
        }
        private static int enterFromCode = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            //Application.ThreadException += ApplicationThreadException;
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            GetAvailableFitFunctions(@"FuncLib.dll");
            LoadUserFunctions();
            LoadUserFunctionParameters();
            Application.Run(new MainForm());

        }

        /// <summary>
        /// Global exceptions in Non User Interfarce(other thread) antipicated error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("LybraFit encountered internal error and needs to close.", "Unexpected error");
            Application.Exit();
        }

        /// <summary>
        /// Global exceptions in User Interfarce antipicated error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("LybraFit encountered internal error and needs to close.", "Unexpected error");
            Application.Exit();
        }

        private static void GetAvailableFitFunctions(string assemblyFile)
        {
            if (!File.Exists(assemblyFile))
            {
                MessageBox.Show("Cannot find library models: " + assemblyFile);
                return;
            }
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), assemblyFile);
                Assembly assembly = Assembly.LoadFile(path);
                Type[] types = assembly.GetTypes();
                Array.Sort(types, new TypeComparer());
                foreach (Type type in types)
                {
                    //This one is set for automation tests
                    if (type.BaseType.ToString().Contains("FitFunction") && !type.ToString().Contains("UserFunction"))
                    {
                        FitFunction bFunc = (FitFunction)assembly.CreateInstance(type.FullName);
                        bFunc.SetUniqueDescription();
                        LibraryModels.Add(bFunc.Name, bFunc);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Internal error occured while initializing library models", "Initialization error!");
            }
        }

        private static void LoadUserFunctions()
        {
            if (!File.Exists(CUSTOM_MODELS_FILE_NAME))
            {
                //MessageBox.Show("Cannot find file 'CustomModels.txt'.", "Initialization error!"); TODO: Reconsider removing this message
                return;
            }

            StreamReader reader = File.OpenText(CUSTOM_MODELS_FILE_NAME);
            StringBuilder errors = new StringBuilder();
            int lineIndex = 0;
            bool hasErrors = false;
            while (!reader.EndOfStream)
            {
                lineIndex++;
                string line = reader.ReadLine();
                string[] tokens = line.Split(new char[] { '\t' });
                if (tokens.Length < 2)
                {
                    errors.Append("No name and math expression provided on line " + lineIndex);
                    hasErrors = true;
                    continue;
                }

                string name = tokens[0];
                string expression = tokens[1];

                try
                {
                    string compilerErrors;
                    UserFunction userFunction = UserFunction.Create(name, expression, out compilerErrors);
                    if (userFunction == null)
                    {
                        errors.Append("Invalid math expression on line:" + lineIndex);
                        hasErrors = true;
                        continue;
                    }
                    else
                    {
                        userFunction.SetUniqueDescription();
                        Program.UserModels.Add(userFunction.Name, userFunction);
                    }
                }
                catch
                {
                    errors.Append("Internal error has occured while tryping to create the custom model on line:" + lineIndex);
                    hasErrors = true;
                    continue;
                }
            }

            if (hasErrors)
            {
                string errorText = "Error where found while reading CustomModels file:+\t\n" + errors.ToString();
                MessageBox.Show(errorText, "Initialization error!");
            }

            reader.Close();
        }

        public static void SaveUserFunctions()
        {
            StreamWriter writer = File.CreateText(CUSTOM_MODELS_FILE_NAME);
            foreach (UserFunction function in UserModels.Values)
            {
                writer.WriteLine(function.Name + "\t" + function.Description);
            }
            writer.Close();
        }

        public static void SaveUserFunctionParameters()
        {
            StreamWriter writer = File.CreateText(CUSTOM_MODELS_PARAMETERS_FILE_NAME);
            foreach (string functionName in UserModelsInitialParameter.Keys)
            {
                writer.Write(functionName);
                float[] lastParamatersValues = UserModelsInitialParameter[functionName];
                for (int i = 0; i < lastParamatersValues.Length; i++)
                {
                    writer.Write("\t" + lastParamatersValues[i].ToString());
                }
                writer.WriteLine();
            }
            writer.Close();
        }

        public static void LoadUserFunctionParameters()
        {
            if (File.Exists(CUSTOM_MODELS_PARAMETERS_FILE_NAME))
            {
                StreamReader reader = File.OpenText(CUSTOM_MODELS_PARAMETERS_FILE_NAME);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] tokens = line.Split('\t');
                    if (tokens.Length > 0 && UserModels.Keys.Contains(tokens[0]))
                    {
                        float[] parameters = new float[tokens.Length - 1];
                        for (int i = 1; i < tokens.Length; i++)
                        {
                            float v;
                            if (!float.TryParse(tokens[i], out v))
                            {
                                break;
                            }
                            parameters[i - 1] = v;
                        }

                        UserModelsInitialParameter.Add(tokens[0], parameters);
                    }
                }
                reader.Close();
            }
        }

    }
}
