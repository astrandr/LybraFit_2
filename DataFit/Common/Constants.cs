//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

namespace LybraFit.Common
{
    public class Constants
    {
        public static string ERROR_INVALID_NUMBER = "Invalid number";
        public static string NUMBER_MATCH_EXPRESSION = @"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?";
#if DEMO
        public static string PROGRAM_NAME = @"LybraFit Demo";
#else
        public static string PROGRAM_NAME = @"LybraFit";
#endif
        public static int STANDARD_DPI = 96; //96 dpi is the default resolution when the bitmap initialized
    }
}
