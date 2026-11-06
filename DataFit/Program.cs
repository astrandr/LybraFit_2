//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using FuncLib;
using GraphControlLib;
using LybraFit.Common;
using LybraFit.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace LybraFit
{
    public static class Program
    {
        private static string CUSTOM_MODELS_FILE_NAME = "CustomModels.txt";
        private static string CUSTOM_MODELS_PARAMETERS_FILE_NAME = "CustomModelsParameters.txt";
        private static string DEFAULT_GRAPH_SETTINGS_FILE_NAME = "GraphSettings.txt";
        public static string PROJECT_FILE_EXTENSION = ".lfw";
        public static string APPLICATION_DEMO_NAME = "LybraFit Demo";
        public static string APPLICATION_NAME = "LybraFit";
        private static string RECENT_FILE_NAMES = "RecentFiles.txt";
        private static string RECENT_PROJECTS_NAMES = "RecentProjects.txt";

        public static GraphSettings DefaultGraphSettings;
        public static bool HasDefaultGraphSettings = false;
        public static Dictionary<string, UserFunction> UserModels = new Dictionary<string, UserFunction>();
        public static Dictionary<string, FitFunction> LibraryModels = new Dictionary<string, FitFunction>();

        public static Dictionary<string, double[]> UserModelsInitialParameter = new Dictionary<string, double[]>();

        public static Dictionary<string, string> RecentFiles = new Dictionary<string, string>(7);
        public static Dictionary<string, string> RecentProjects = new Dictionary<string, string>(7);

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
#if !DEBUG
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
#endif
            GetAvailableFitFunctions();
            LoadUserFunctions();
            LoadUserFunctionParameters();
            LoadRecentFiles();
            LoadRecentProjects();
            LoadDefaultGraphSettings();
            
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

        private static void GetAvailableFitFunctions()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
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
            catch
            {
                MessageBox.Show("Internal error occured while initializing library models", "Initialization error!");
            }
        }

        private static void LoadUserFunctions()
        {
            if (!File.Exists(Path.Combine(GetApplicationDataFolder(), CUSTOM_MODELS_FILE_NAME)))
            {
                return;
            }

            StreamReader reader = File.OpenText(Path.Combine(GetApplicationDataFolder(), CUSTOM_MODELS_FILE_NAME));
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
            StreamWriter writer = File.CreateText(Path.Combine(GetApplicationDataFolder(), CUSTOM_MODELS_FILE_NAME));
            foreach (UserFunction function in UserModels.Values)
            {
                writer.WriteLine(function.Name + "\t" + function.Description);
            }
            writer.Close();
        }

        public static void SaveRecentFiles()
        {
            try
            {
                StreamWriter writer = File.CreateText(Path.Combine(GetApplicationDataFolder(), RECENT_FILE_NAMES));
                foreach (var fileName in RecentFiles.Values)
                {
                    writer.WriteLine(fileName);
                }
                writer.Close();
            }
            catch
            {
            }
        }

        public static void LoadRecentFiles()
        {
            try
            {
                var recentFiles = Path.Combine(GetApplicationDataFolder(), RECENT_FILE_NAMES);
                if (!File.Exists(recentFiles)) return;
                StreamReader reader = File.OpenText(recentFiles);
                while (!reader.EndOfStream)
                {
                    string filePath = reader.ReadLine();
                    RecentFiles.Add(Path.GetFileName(filePath), filePath);
                }
                reader.Close();
            }
            catch
            {
            }

        }

        public static void SaveRecentProjects()
        {
            try
            {
                StreamWriter writer = File.CreateText(Path.Combine(GetApplicationDataFolder(), RECENT_PROJECTS_NAMES));
                foreach (var fileName in RecentProjects.Values)
                {
                    writer.WriteLine(fileName);
                }
                writer.Close();
            }
            catch
            {
            }
        }

        public static void LoadRecentProjects()
        {
            try
            {
                var recentProjects = Path.Combine(GetApplicationDataFolder(), RECENT_PROJECTS_NAMES);
                if (!File.Exists(recentProjects)) return;
                StreamReader reader = File.OpenText(recentProjects);
                while (!reader.EndOfStream)
                {
                    string filePath = reader.ReadLine();
                    RecentProjects.Add(Path.GetFileName(filePath), filePath);
                }
                reader.Close();
            }
            catch
            {
            }

        }

        public static string GetApplicationDataFolder()
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }

        public static void SaveUserFunctionParameters()
        {
            StreamWriter writer = File.CreateText(Path.Combine(GetApplicationDataFolder(), CUSTOM_MODELS_PARAMETERS_FILE_NAME));
            foreach (string functionName in UserModelsInitialParameter.Keys)
            {
                writer.Write(functionName);
                double[] lastParamatersValues = UserModelsInitialParameter[functionName];
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
            string path = Path.Combine(GetApplicationDataFolder(), CUSTOM_MODELS_PARAMETERS_FILE_NAME);
            if (File.Exists(path))
            {
                StreamReader reader = File.OpenText(path);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] tokens = line.Split('\t');
                    if (tokens.Length > 0 && UserModels.Keys.Contains(tokens[0]))
                    {
                        double[] parameters = new double[tokens.Length - 1];
                        for (int i = 1; i < tokens.Length; i++)
                        {
                            double v;
                            if (!double.TryParse(tokens[i], out v))
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

        public static void SaveDefaultGraphSettings()
        {
            try
            {
                using (var writer = File.CreateText(Path.Combine(GetApplicationDataFolder(), DEFAULT_GRAPH_SETTINGS_FILE_NAME)))
                {
                    writer.WriteLine(DefaultGraphSettings.AxisColor.Serialize());
                    writer.WriteLine(DefaultGraphSettings.AxisLabelsColor.Serialize());
                    writer.WriteLine(DefaultGraphSettings.AxisLabelsFont.Serialize());
                    writer.WriteLine(DefaultGraphSettings.AxisThickness.ToString());
                    writer.WriteLine(DefaultGraphSettings.BackgroundColor.Serialize());
                    writer.WriteLine(DefaultGraphSettings.ConnectDataPoints.ToString());
                    writer.WriteLine(DefaultGraphSettings.DataColor.Serialize());
                    writer.WriteLine(DefaultGraphSettings.DataLineStyle.ToString());
                    writer.WriteLine(DefaultGraphSettings.DataLineWidth.ToString());
                    writer.WriteLine(DefaultGraphSettings.DataPointSize.ToString());
                    writer.WriteLine(DefaultGraphSettings.DataPointType.ToString());
                    writer.WriteLine(DefaultGraphSettings.FitLineColor.Serialize());
                    writer.WriteLine(DefaultGraphSettings.FitLineStyle.ToString());
                    writer.WriteLine(DefaultGraphSettings.FitLineWidth.ToString());
                    writer.WriteLine(DefaultGraphSettings.GridColor.Serialize());
                    writer.WriteLine(DefaultGraphSettings.GridStyle.ToString());
                    writer.WriteLine(DefaultGraphSettings.GridThickness.ToString());
                    writer.WriteLine(DefaultGraphSettings.PlotAreaColor.Serialize());
                    writer.WriteLine(DefaultGraphSettings.ShowGridLines.ToString());
                    writer.WriteLine(DefaultGraphSettings.ShowModel.ToString());
                    writer.WriteLine(DefaultGraphSettings.ShowRegressionResult.ToString());
                    writer.WriteLine(DefaultGraphSettings.ShowTitle.ToString());
                    writer.WriteLine(DefaultGraphSettings.TickLabelsColor.Serialize());
                    writer.WriteLine(DefaultGraphSettings.TickLabelsFont.Serialize());
                    writer.WriteLine(DefaultGraphSettings.TitleFont.Serialize());
                    writer.WriteLine(DefaultGraphSettings.XLimitAuto.ToString());
                    writer.WriteLine(DefaultGraphSettings.XMin.ToString());
                    writer.WriteLine(DefaultGraphSettings.XMax.ToString());
                    writer.WriteLine(DefaultGraphSettings.XPow.ToString());
                    writer.WriteLine(DefaultGraphSettings.XStep.ToString());
                    writer.WriteLine(DefaultGraphSettings.YLimitAuto.ToString());
                    writer.WriteLine(DefaultGraphSettings.YMin.ToString());
                    writer.WriteLine(DefaultGraphSettings.YMax.ToString());
                    writer.WriteLine(DefaultGraphSettings.YPow.ToString());
                    writer.WriteLine(DefaultGraphSettings.YStep.ToString());
                    writer.Close();
                    HasDefaultGraphSettings = true;
                }
            }
            catch
            {
                InfoBox.ShowMessage("Cannot save default graph settings", "Failure", null);
            }
        }

        public static void LoadDefaultGraphSettings()
        {
            try
            {
                var recentGraphSettings = Path.Combine(GetApplicationDataFolder(), DEFAULT_GRAPH_SETTINGS_FILE_NAME);
                if (!File.Exists(recentGraphSettings)) return;
                using (var reader = File.OpenText(recentGraphSettings))
                {
                    DefaultGraphSettings.AxisColor = DeserializeColor(reader.ReadLine());
                    DefaultGraphSettings.AxisLabelsColor = DeserializeColor(reader.ReadLine());
                    DefaultGraphSettings.AxisLabelsFont = DeserializeFont(reader.ReadLine());
                    DefaultGraphSettings.AxisThickness = uint.Parse(reader.ReadLine());
                    DefaultGraphSettings.BackgroundColor = DeserializeColor(reader.ReadLine());
                    DefaultGraphSettings.ConnectDataPoints = bool.Parse(reader.ReadLine());
                    DefaultGraphSettings.DataColor = DeserializeColor(reader.ReadLine());
                    DefaultGraphSettings.DataLineStyle = (DashStyle)Enum.Parse(typeof(DashStyle), reader.ReadLine());
                    DefaultGraphSettings.DataLineWidth = uint.Parse(reader.ReadLine());
                    DefaultGraphSettings.DataPointSize = uint.Parse(reader.ReadLine());
                    DefaultGraphSettings.DataPointType = (PlotDataPointType)Enum.Parse(typeof(PlotDataPointType), reader.ReadLine());
                    DefaultGraphSettings.FitLineColor = DeserializeColor(reader.ReadLine());
                    DefaultGraphSettings.FitLineStyle = (DashStyle)Enum.Parse(typeof(DashStyle), reader.ReadLine());
                    DefaultGraphSettings.FitLineWidth = uint.Parse(reader.ReadLine());
                    DefaultGraphSettings.GridColor = DeserializeColor(reader.ReadLine());
                    DefaultGraphSettings.GridStyle = (DashStyle)Enum.Parse(typeof(DashStyle), reader.ReadLine());
                    DefaultGraphSettings.GridThickness = uint.Parse(reader.ReadLine());
                    DefaultGraphSettings.PlotAreaColor = DeserializeColor(reader.ReadLine());
                    DefaultGraphSettings.ShowGridLines = bool.Parse(reader.ReadLine());
                    DefaultGraphSettings.ShowModel = bool.Parse(reader.ReadLine());
                    DefaultGraphSettings.ShowRegressionResult = bool.Parse(reader.ReadLine());
                    DefaultGraphSettings.ShowTitle = bool.Parse(reader.ReadLine());
                    DefaultGraphSettings.TickLabelsColor = DeserializeColor(reader.ReadLine());
                    DefaultGraphSettings.TickLabelsFont = DeserializeFont(reader.ReadLine());
                    DefaultGraphSettings.TitleFont = DeserializeFont(reader.ReadLine());
                    DefaultGraphSettings.XLimitAuto = bool.Parse(reader.ReadLine());
                    DefaultGraphSettings.XMin = float.Parse(reader.ReadLine());
                    DefaultGraphSettings.XMax = float.Parse(reader.ReadLine());
                    DefaultGraphSettings.XPow = int.Parse(reader.ReadLine());
                    DefaultGraphSettings.XStep = float.Parse(reader.ReadLine());
                    DefaultGraphSettings.YLimitAuto = bool.Parse(reader.ReadLine());
                    DefaultGraphSettings.YMin = float.Parse(reader.ReadLine());
                    DefaultGraphSettings.YMax = float.Parse(reader.ReadLine());
                    DefaultGraphSettings.YPow = int.Parse(reader.ReadLine());
                    DefaultGraphSettings.YStep = float.Parse(reader.ReadLine());
                    HasDefaultGraphSettings = true;
                }
            }
            catch
            {
                InfoBox.ShowMessage("Cannot read default graph settings", "Failure", null);
            }
        }

        private static string Serialize(this Color color)
        {
            return string.Format("{0},{1},{2},{3}", color.A, color.R, color.G, color.B);
        }

        private static Color DeserializeColor(string colorDescription)
        {
            int[] colorElements = colorDescription.Split(',').Select(s => int.Parse(s)).ToArray<int>();
            return Color.FromArgb(colorElements[0], colorElements[1], colorElements[2], colorElements[3]);
        }

        private static string Serialize(this Font font)
        {
            return string.Format("{0},{1},{2}", font.FontFamily.Name, font.Size, font.Style);
        }

        private static Font DeserializeFont(string fontDescription)
        {
            string[] fontElements = fontDescription.Split(',');
            var fontFamily = new FontFamily(fontElements[0]);
            var size = float.Parse(fontElements[1]);
            var fontStyle = (FontStyle)(Enum.Parse(typeof(FontStyle), fontElements[2]));
            return new Font(fontFamily, size, fontStyle);
        }
    }
}
