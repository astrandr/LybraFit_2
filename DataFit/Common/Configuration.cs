//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System.Drawing;

namespace LybraFit.Common
{
    public class Configuration
    {
        public static GraphSettings     GraphSettings;
        public static NewDataSettings   NewDataSetting = new NewDataSettings();
        public static ImportSettings    ImportSettings = new ImportSettings();
        public static Color BackColor = Color.FromArgb(255, 247, 247, 247);
        public static int RegressionDataSmoothPoints = 0;
        public static Font MathFont = new Font("Book Antiqua", 16, FontStyle.Italic);
        public static bool AutoSort = false;
    }
}
