
namespace DataFit.Common
{
    public class LogRecord
    {
        public string Key { get; set; }
        public float[] Values { get; set; }

        public LogRecord(){}

        public LogRecord(string key, float[] values)
        {
            this.Key = key;
            this.Values = values;
        }
    }
}
