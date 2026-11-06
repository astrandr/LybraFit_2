//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Runtime.Serialization;
namespace LybraFit.Common
{
    [Serializable()]
    public class LogRecord : ISerializable
    {
        public string Key { get; set; }
        public double[] Values { get; set; }
        public double StdError { get; set; }
        public LogRecord(){}

        public LogRecord(string key, double[] values, double stdError)
        {
            this.Key = key;
            this.Values = values;
            this.StdError = stdError;
        }

        public LogRecord(SerializationInfo info, StreamingContext context)
        {
            Key = info.GetString("Key");
            Values = (double[])info.GetValue("Values", typeof(float[]));
            StdError = (double)info.GetValue("StdError", typeof(float));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LogRecord_Format_Version", 1); //This is intended to switch to call corresponding function based on the version, below is default for version 1
            info.AddValue("Key", Key);
            info.AddValue("Values", Values);
            info.AddValue("StdError", StdError);
        }
    }
}
