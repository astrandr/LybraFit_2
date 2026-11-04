//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using FuncLib;

namespace LybraFit.Common
{
    public class NewDataSettings
    {
        public string Title = "";

        public string DataXName = "X";
        public string DataYName = "Y";
        public string ErrDataName = "Err";

        public bool HasErrData = false;

        public bool DataXAutoIncrement = false;
        public float DataXMin = 0;
        public float DataXMax = 0;
        public float DataXIncrement = 0;

        public bool ErrDataCalculatedStd;

        public bool CalculateYData = false;
        public UserFunction YFunction = null;
    }
}
