//Designed and Implemented by Andrey Batchvarov 2018
using FuncLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFit.Common
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
