//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Drawing;


namespace DataFit.Common
{
    public class Configuration
    {
        public static GraphSettings     GraphSettings;
        public static NewDataSettings   NewDataSetting = new NewDataSettings();
        public static ImportSettings    ImportSettings = new ImportSettings();
        public static Color BackColor = Color.FromArgb(255, 247, 247, 247);
        public static int RegressionDataSmoothPoints = 0;
        public static Font MathFont = new Font("Book Antiqua", 16, FontStyle.Italic);
    }
}
