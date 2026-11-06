//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System.Collections.Generic;

namespace LybraFit.Common
{
    public class DataFilterInfo
    {
        public static List<DataFilterInfo> Items = new List<DataFilterInfo>();

        public string ColumnName;
        public bool Selected;
        public double Min;
        public double Max;
        public bool Visible;

        public override string ToString() { return ColumnName; }
    }
}
