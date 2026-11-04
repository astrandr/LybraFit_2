//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

namespace LybraFit.Common
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
