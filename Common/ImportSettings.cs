//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFit.Common
{
    public class ImportSettings
    {
        public string LastFile = "";
        public int HeaderLinesCount = 0;
        public int Column1Index = 0;
        public string Column1Name = "X";
        public int Column2Index = 1;
        public string Column2Name = "Y";
        public int Column3Index = 2;
        public string Column3Name = "Err";
        public bool HasErrColumn = false;
        public bool UseStandardDelimiters = true;
        public bool SkipLinesWithError = false;
    }
}
