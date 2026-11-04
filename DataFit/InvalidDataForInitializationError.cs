//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    public class InvalidDataForInitializationError : Exception
    {
        public InvalidDataForInitializationError() : base ("Data is not suitable for initialization")
        {
        }
    }
}
