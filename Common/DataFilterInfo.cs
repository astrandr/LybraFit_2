//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFit.Common
{
    public class DataFilterInfo
    {
        public static List<DataFilterInfo> Items = new List<DataFilterInfo>();

        public string ColumnName;
        public bool Selected;
        public float Min;
        public float Max;

        public override string ToString() { return ColumnName; }
    }
}
