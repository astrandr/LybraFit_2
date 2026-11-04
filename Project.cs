using DataFit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFit
{
    [Serializable]
    public class Project
    {
        public string Title;
        public bool HasErrorY;
        public List<DataRow> Data;
        public string DataXName = "X";
        public string DataYName = "Y";
        public string ErrDataName = "Err";
        public ModelFitInfo[] Models;
        public ColumnSettings[] GridColumnSettings;
    }

    [Serializable]
    public class DataRow
    {
        public float X, Y, ErrY;
    }
}
