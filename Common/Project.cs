//Designed and Implemented by Andrey Batchvarov 2018
using DataFit.Common;
using DataFit.Forms;
using FuncLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataFit
{
    [Serializable]
    public class Project
    {
        public string Title;
        public bool HasErrorY;
        public List<DataRow> Data;
        public string DataXName = "X";
        public string DataYName = "Y";
        public string ErrDataName = "Err";
        public ModelFitInfo[] Models;
        public ColumnSettings[] GridColumnSettings;

        [XmlIgnore]
        public Dictionary<string, List<DerivedValueInfo>> DerivedValues;

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
            
            FileStream stream  = File.Create(fileName);
            XmlSerializer serializer = new XmlSerializer(typeof(Project), modelTypes.ToArray());
            serializer.Serialize(stream, this);
            stream.Close();
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
            catch (Exception exc)
            {

                return;
            }
        }

        public static Project Load(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = File.Create(fileName);
                BinaryFormatter serializer = new BinaryFormatter();
                object b = serializer.Deserialize(stream);
                stream.Close();

                return (Project)b;
            }
            catch (Exception exc)
            {
                return null;
            }
        }
    }

    [Serializable]
    public class DataRow
    {
        public float X, Y, ErrY;
    }
}
