//Designed and Implemented by Andrey Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;

namespace FuncLib
{
    [Serializable]
    public abstract class BaseFunction
    {
        protected string description;
        protected string name;
        protected string[] parameters;

        protected BaseFunction() { }

        public string Description { get { return description; } set { description = value; } }

        public string Name { get { return name; } set { name = value; } }

        public string[] Parameters { get { return parameters; } set { parameters = value; } }

        public abstract void CalcProcedure(double[] X, double[] P, ref double[] Y);
    }
}
