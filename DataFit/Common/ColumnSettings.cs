//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace LybraFit.Common
{
    [Serializable]
    public class ColumnSettings : ISerializable
    {
        public string Name;
        public string Alignment;
        public string Format;
        public uint Digits;
        public bool Visible;
        public string CustomFormat;
        public bool AutoWidth;
        public bool ReadOnly;
        public Font TableFont;

        public static string[] Formats = new string[] { "No Formatting", "Numeric", "Scientific", "Custom" }; //TODO Replace this with enum

        public override string ToString()
        {
            return Name;
        }

        public ColumnSettings()
        {

        }

        public ColumnSettings(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("Name");
            this.Alignment = info.GetString("Alignment");
            this.Format = info.GetString("Format");
            this.Digits = info.GetUInt16("Digits");
            this.Visible = info.GetBoolean("Visible");
            this.CustomFormat = info.GetString("CustomFormat");
            this.AutoWidth = info.GetBoolean("AutoWidth");
            this.ReadOnly = info.GetBoolean("ReadOnly");
            this.TableFont = (Font)info.GetValue("TableFont", typeof(Font));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ColumnSettings_Format_Version", 1);
            info.AddValue("Name",Name);
            info.AddValue("Alignment",Alignment);
            info.AddValue("Format",Format);
            info.AddValue("Digits",Digits);
            info.AddValue("Visible",Visible);
            info.AddValue("CustomFormat",CustomFormat);
            info.AddValue("AutoWidth",AutoWidth);
            info.AddValue("ReadOnly", ReadOnly);
            info.AddValue("TableFont", TableFont);
        }
    }
}
