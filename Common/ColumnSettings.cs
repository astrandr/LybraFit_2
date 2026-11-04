//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFit.Common
{
    [Serializable]
    public class ColumnSettings
    {
        public string Name;
        public string Alignment;
        public string Format;
        public uint Digits;
        public bool Visible;
        public string CustomFormat;
        public bool AutoWidth;
        public bool ReadOnly;

        public static string[] Formats = new string[] { "No Formatting", "Numeric", "Scientific", "Custom" }; //TODO Replace this with enum

        public override string ToString()
        {
            return Name;
        }
    }
}
