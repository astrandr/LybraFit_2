using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Common
{
    public class ControlNotFoundException : Exception
    {
        public string ControlName { get; set; }

        public ControlNotFoundException(string controlName)
        {
            this.ControlName = controlName;
        }
    }
}
