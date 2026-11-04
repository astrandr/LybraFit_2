//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFit.Common
{
    public class Command
    {
        protected Document document;
        private string name;

        public string Name
        {
            get { return name; }
        }

        public Command(string name, Document doc)
        {
            this.document = doc;
            this.name = name;
        }

        public virtual void Execute() { }
    }
}
