//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using LybraFit.Common;
using FuncLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace LybraFit
{
    [Serializable]
    public class Project : ISerializable
    {
        public string Title;
        public bool HasErrorY;
        public List<double[]> Data;
        public string DataXName = "X";
        public string DataYName = "Y";
        public string ErrDataName = "Err";
        public ModelFitInfo[] Models;
        public ColumnSettings[] GridColumnSettings;

        public Project()
        {

        }

        //This method is unused
        public void SaveAsXml(string fileName)
        {
            List<Type> modelTypes = new List<Type>();

            if (Models != null)
            {
                foreach (ModelFitInfo modelFitInfo in Models)
                {
                    modelTypes.Add(modelFitInfo.ModelFunction.GetType());
                }
            }

            modelTypes.Add(typeof(UserFunction));

            FileStream stream = File.Create(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(Project), modelTypes.ToArray());
            serializer.Serialize(stream, this);
            stream.Close();
        }

        public static Project LoadFromXml(string fileName, Type[] extraTypes = null)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(Project), extraTypes);
                Project p = (Project)serializer.Deserialize(stream);
                stream.Close();
                return p;
            }
            catch
            {
                return null;
            }
        }

        public void Save(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = File.Create(fileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(stream, this);
                stream.Close();

            }
            catch
            {

                return;
            }
        }

        public static Project Load(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(fileName);
                BinaryFormatter serializer = new BinaryFormatter();
                object b = serializer.Deserialize(stream);
                stream.Close();

                return (Project)b;
            }
            catch
            {
                return null;
            }
        }

        //Constructor used for deserialization
        public Project(SerializationInfo info, StreamingContext context)
        {
            this.Title = info.GetString("Title");
            this.HasErrorY = info.GetBoolean("HasErrorY");
            this.DataXName = info.GetString("DataXName");
            this.DataYName = info.GetString("DataYName");
            this.ErrDataName = info.GetString("ErrDataName");
            this.Data = (List<double[]>)info.GetValue("Data", typeof(List<double[]>));
            this.GridColumnSettings = (ColumnSettings[])info.GetValue("GridColumnSettings", typeof(ColumnSettings[]));
            this.Models = (ModelFitInfo[])info.GetValue("Models", typeof(ModelFitInfo[]));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Project_Format_Version", 1);
            info.AddValue("Title", Title);
            info.AddValue("HasErrorY", HasErrorY);
            info.AddValue("DataXName", DataXName);
            info.AddValue("DataYName", DataYName);
            info.AddValue("ErrDataName", ErrDataName);
            info.AddValue("Data", Data);
            info.AddValue("GridColumnSettings", GridColumnSettings);
            info.AddValue("Models", Models);

        }
    }

    [Serializable]
    public class DataRow
    {
        public double X, Y, ErrY;
    }
}
