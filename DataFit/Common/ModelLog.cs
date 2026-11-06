//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LybraFit.Common
{
    [Serializable]
    public class ModelLog : ISerializable
    {
        private string identifier;
        private List<LogRecord> records;
        private int valuesCount;
        private string[] valueNames;

        /// <summary>
        /// Identifier - uniquely identifies model log by name and set of parameters and derived values names separated by ":"
        /// </summary>
        public string Identifier { get { return identifier; } set { identifier = value; } }
        public string Name { get; set; }

        public List<LogRecord> Records { get { return this.records; } set { records = value; } }
        public string[] ValueNames { get { return this.valueNames; } set { valueNames = value; } }
        public ModelLog() { }
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

        public void AddRecord(string key, double[] parametersValues, double[] derivedValues, double stdError = 0)
        {
            if (valuesCount != parametersValues.Length + derivedValues.Length)
            {
                throw new ArgumentException("Cannot insert log record, different values count");
            }

            double[] values = new double[this.valuesCount];
            int i = 0;
            for (; i < parametersValues.Length; i++)
            {
                values[i] = parametersValues[i];
            }
            for (; i < valuesCount; i++)
            {
                values[i] = derivedValues[i - parametersValues.Length];
            }

            this.Records.Add(new LogRecord(key, values, stdError));
        }

        public ModelLog(SerializationInfo info, StreamingContext context)
        {
            identifier = info.GetString("Identifier");
            Name = info.GetString("Name");
            valuesCount = info.GetInt16("valuesCount");
            ValueNames = (string[])info.GetValue("ValueNames", typeof(string[]));
            records = (List<LogRecord>)info.GetValue("Records", typeof(List<LogRecord>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ModelLog_Format_Version", 1);
            info.AddValue("Identifier", Identifier);
            info.AddValue("Name", Name);
            info.AddValue("valuesCount", 1);
            info.AddValue("ValueNames", ValueNames);
            info.AddValue("Records", Records);
        }
    }
}
