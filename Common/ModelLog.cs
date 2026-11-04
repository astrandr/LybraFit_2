using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFit.Common
{
    public class ModelLog
    {
        private string identifier;
        private List<LogRecord> records;
        private int valuesCount;
        private string[] valueNames;

        /// <summary>
        /// Identifier - uniquely identifies model log by name and set of parameters and derived values names separated by ":"
        /// </summary>
        public string Identifier { get { return identifier; } }
        public string Name { get; set; }

        public List<LogRecord> Records { get { return this.records; } set { records = value; } } //TODO those can be set because of serialization, refactor it to readonly and solve problems with serialization
        public string[] ValueNames { get { return this.valueNames; } set { valueNames = value; } } //TODO those can be set because of serialization, refactor it to readonly and solve problems with serialization
        public ModelLog(){}
        public ModelLog(string name, string[] parameterNames, string[] derivedValueNames)
        {
            if (derivedValueNames == null)
            {
                derivedValueNames = new string[0];
            }
            this.Name = name;
            this.valuesCount = parameterNames.Length + derivedValueNames.Length;
            this.valueNames = new string[valuesCount];

            int j = 0;
            for (int i = 0; i < parameterNames.Length; i++)
            {
                identifier += parameterNames[i] + ":";
                valueNames[j++] = parameterNames[i];
            }
            for (int i = 0; i < derivedValueNames.Length; i++)
            {
                identifier += derivedValueNames[i] + ":";
                valueNames[j++] = derivedValueNames[i];
            }

            records = new List<LogRecord>();
        }

        public void AddRecord(string key, float[] parametersValues, float[] derivedValues)
        {
            if (valuesCount != parametersValues.Length+derivedValues.Length)
            {
                throw new ArgumentException("Cannot insert log record, different values count");
            }

            float[] values = new float[this.valuesCount];
            int i = 0;
            for (; i < parametersValues.Length; i++)
            {
                values[i] = parametersValues[i];
            }
            for (; i < valuesCount; i++)
            {
                values[i] = derivedValues[i - parametersValues.Length];
            }

            this.Records.Add(new LogRecord(key, values));
        }
    }
}
