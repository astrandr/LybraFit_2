//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using MathLib;
using LybraFit.Common;
using FuncLib;
using System.Text.RegularExpressions;

namespace LybraFit
{
    public class Document
    {
        public ColumnSettings[] GridColumnSettings;
        public string DataFile = "";
        public List<double[]> Data = null;
        private List<double[]> unfilteredData = null;
        public string[] ColumnNames = new string[] { "X", "Y", "Err", "Model", "Difference" };
        public int ColumnCount = 5;
        public string ErrMessage = "";
        public bool HasErrorY = false;
        public bool YCalculated = false;
        public UserFunction yCalculationFunction = null;
        public double[] Parameters;

        public double[] FitDataX;
        public double[] FitDataY;
        public string XDataName = "X";
        public string YDataName = "Y";
        public Dictionary<string, ModelFitInfo> Models = new Dictionary<string, ModelFitInfo>();
        public ModelFitInfo CurrentModel = null;

        public static char[] StandardDelimiters = new char[] { '\t', '\r', '\n', ',', ':', ' ', ';', '|', '/', '\\' };

        public Document(int coumnCount = 5, string[] columnNames = null)
        {
            Data = new List<double[]>();

            ColumnCount = coumnCount;
            if (columnNames != null) ColumnNames = columnNames;

            //TODO: Move this initialization outside document so values can be taken from some configuration and the logic is not inside the document
            GridColumnSettings = new ColumnSettings[ColumnCount];
            for (int i = 0; i < ColumnCount; i++)
            {
                GridColumnSettings[i] = new ColumnSettings();
                GridColumnSettings[i].Name = ColumnNames[i];
                GridColumnSettings[i].Format = ColumnSettings.Formats[2]; //Default format is scientific with 2 digits
                GridColumnSettings[i].Digits = 2;
                GridColumnSettings[i].CustomFormat = ""; 
                GridColumnSettings[i].AutoWidth = false;
                GridColumnSettings[i].Visible = true;
                GridColumnSettings[i].ReadOnly = true;
                GridColumnSettings[i].TableFont = new System.Drawing.Font("Arial", 10);
            }
            GridColumnSettings[0].Visible = true;
            GridColumnSettings[1].Visible = true;
            GridColumnSettings[0].ReadOnly = false;
            GridColumnSettings[1].ReadOnly = false;
            GridColumnSettings[2].ReadOnly = false;
        }

        public double[] X
        {
            //TODO this can be optimized with dirty flag for the data
            get
            {
                double[] X = new double[Data.Count];
                for (int i = 0; i < X.Length; i++)
                {
                    X[i] = Data[i][0];
                }
                return X;
            }
        }

        public double[] Y
        {
            //TODO this can be optimized with dirty flag for the data
            get
            {
                double[] Y = new double[Data.Count];
                for (int i = 0; i < Y.Length; i++)
                {
                    Y[i] = Data[i][1];
                }
                return Y;
            }
        }

        public double[] ErrY
        {
            //TODO this can be optimized with dirty flag for the data
            get
            {
                if (!HasErrorY) return null;
                double[] errY = new double[Data.Count];
                for (int i = 0; i < Y.Length; i++)
                {
                    errY[i] = Data[i][2];
                }
                return errY;
            }
        }

        public bool LoadFromFile(string fileName)
        {
            Debug.Assert(File.Exists(fileName), "File does not exist:" + fileName);
            ErrMessage = "";
            char[] splitChars = StandardDelimiters;
            StreamReader r = File.OpenText(fileName);
            string line;
            string[] items;
            int lineNumber = 1;
            bool hasHeader = false;
            int columnCount = 2;
            line = r.ReadLine();
            items = line.Trim().Split(splitChars);
            //Test for column/data names/captions
            foreach (string item in items)
            {
                double v;
                if (!double.TryParse(item, out v))
                {
                    //Some item is not number but probably column header
                    hasHeader = true;
                    break;
                }
            }
            if (hasHeader)
            {
                ColumnNames = items;
            }
            columnCount = items.Length;
            List<float[]> newData = new List<float[]>();
            unfilteredData = null;
            HasErrorY = false;
            bool skipLine = !hasHeader;

            while (!r.EndOfStream)
            {
                if (!skipLine) line = r.ReadLine();
                skipLine = false;
                float[] lineData = new float[5];
                lineNumber++;
                items = line.Trim().Split(splitChars);

                for (int i = 0; i < columnCount; i++)
                {
                    float f;
                    if (i >= items.Length)
                    {

                        ErrMessage = "Missing value on line:" + lineNumber;
                        r.Close();
                        return false;
                    }
                    if (!float.TryParse(items[i], out f))
                    {

                        ErrMessage = "Cannot parse value on line:" + lineNumber + "  :" + items[i];
                        r.Close();
                        return false;
                    }
                    lineData[i] = f;
                }

                newData.Add(lineData);
            }
            r.Close();
            DataFile = fileName;
            Data = newData;
            return true;
        }

        public bool LoadFromMultilineText(string text, ImportSettings importSettings)
        {
            ErrMessage = "";
            char[] splitChars = StandardDelimiters;
            string[] lines = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string line;
            string[] items;
            int lineNumber = importSettings.HeaderLinesCount;;
            int columnCount = importSettings.HasErrColumn ? 3 : 2;

            List<float[]> newData = new List<float[]>();
            unfilteredData = null;
            HasErrorY = false; //TODO These initializations are to be moved in a separate mathod Initialize or this method is to be made static and return document as all others form file and text creation
            float f;
            while (lineNumber < lines.Length)
            {
                line = lines[lineNumber];
                float[] lineData = new float[5];
                lineNumber++;

                if (importSettings.UseStandardDelimiters)
                {
                    items = line.Trim().Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    Regex regex = new Regex(Constants.NUMBER_MATCH_EXPRESSION);
                    MatchCollection matches = regex.Matches(line);
                    items = new string[matches.Count];
                    for (int i = 0; i < matches.Count; i++)
                    {
                        items[i] = matches[i].ToString();
                    }
                }

                if (items.Length > importSettings.Column1Index)
                {
                    if (!float.TryParse(items[importSettings.Column1Index], out f))
                    {
                        if (importSettings.SkipLinesWithError) continue;

                        ErrMessage = "Cannot parse value on line:" + lineNumber + "  :" + items[importSettings.Column1Index];
                        return false;
                    }
                    lineData[0] = f;
                }
                else
                {
                    if (importSettings.SkipLinesWithError) continue;
                    ErrMessage = "Missing value for 1st column on line:" + lineNumber;
                    return false;
                }

                if (items.Length > importSettings.Column2Index)
                {
                    if (!float.TryParse(items[importSettings.Column2Index], out f))
                    {
                        if (importSettings.SkipLinesWithError) continue;
                        ErrMessage = "Cannot parse value on line:" + lineNumber + "  :" + items[importSettings.Column2Index];
                        return false;
                    }
                    lineData[1] = f;
                }
                else
                {
                    if (importSettings.SkipLinesWithError) continue;
                    ErrMessage = "Missing value for 2nd column on line:" + lineNumber;
                    return false;
                }

                if (Configuration.ImportSettings.HasErrColumn)
                {
                    if (items.Length > importSettings.Column3Index)
                    {
                        if (!float.TryParse(items[importSettings.Column3Index], out f))
                        {
                            if (importSettings.SkipLinesWithError) continue;
                            ErrMessage = "Cannot parse value on line:" + lineNumber + "  :" + items[importSettings.Column3Index];
                            return false;
                        }
                        lineData[2] = f;
                    }
                    else
                    {
                        if (importSettings.SkipLinesWithError) continue;
                        ErrMessage = "Missing value for 3rd column on line:" + lineNumber;
                        return false;
                    }
                }
                newData.Add(lineData);
            }

            ColumnNames[0] = importSettings.Column1Name;
            ColumnNames[1] = importSettings.Column2Name;
            ColumnNames[2] = importSettings.Column3Name;

            GridColumnSettings[0].Name = importSettings.Column1Name;
            GridColumnSettings[1].Name = importSettings.Column2Name;
            GridColumnSettings[2].Name = importSettings.Column3Name; 

            Data = newData;
            return true;
        }

        public bool LoadFromText(string text, ImportSettings settings)
        {
            ErrMessage = "";
            char[] splitChars = StandardDelimiters;
            string[] lines = text.Trim().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] items;

            items = lines[0].Trim().Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length < 2)
            {
                ErrMessage = String.Format("Cannot find two columns in the data to paste");
                return false;
            }

            Data = new List<float[]>();
            for (int columnIndex = 0; columnIndex < items.Length; columnIndex++)
            {
                ColumnNames[columnIndex] = items[columnIndex];
            }
           

            int lineIndex = 1;
            List<float[]> newData = new List<float[]>();
            HasErrorY = false;
            unfilteredData = null;
            while (lineIndex < lines.Length)
            {
                items = lines[lineIndex].Trim().Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                float[] lineData = new float[this.ColumnCount];
                if (items.Length < 2)
                {
                    ErrMessage = String.Format("Cannot find two columns values at line index {0}", lineIndex);
                    return false;
                }
                for (int columnIndex = 0; columnIndex < items.Length; columnIndex++)
                {
                    float f = 0;

                    if (!float.TryParse(items[columnIndex], out f))
                    {
                        ErrMessage = String.Format("Cannot parse value '{0}' at index {1}", items[columnIndex], lineIndex);
                        return false;
                    }
                    lineData[columnIndex] = f;
                }
                lineIndex++;
                newData.Add(lineData);
            }

            Data = newData;
            return true;
        }

        public bool InsertFromText(string text, int dataIndex, char[] splitChars = null)
        {
            ErrMessage = "";
            if (splitChars == null) splitChars = StandardDelimiters;
            string[] items;
            int index = 0;
            items = text.Trim().Split(splitChars).Where(s => !String.IsNullOrEmpty(s)).ToArray<string>();

            while (index < items.Length)
            {
                float[] lineData = new float[5];
                float f = 0;
                while (index < items.Length && string.IsNullOrEmpty(items[index])) index++;
                if (index < items.Length)
                {
                    if (!float.TryParse(items[index], out f))
                    {
                        ErrMessage = String.Format("Cannot parse value '{0}' at index {1}", items[index], index);
                        return false;
                    }
                }
                lineData[0] = f;
                index++;

                f = 0;
                while (index < items.Length && string.IsNullOrEmpty(items[index])) index++;
                if (index < items.Length)
                {
                    if (!float.TryParse(items[index], out f))
                    {

                        ErrMessage = String.Format("Cannot parse value {0} at index {1}", items[index], index);
                        return false;
                    }
                }
                lineData[1] = f;
                index++;

                if (dataIndex < Data.Count)
                {
                    Data.Insert(dataIndex, lineData);
                }
                else
                {
                    Data.Add(lineData);
                }

                dataIndex++;
            }

            return true;
        }

        public void UpdateX(double[] newX)
        {
            for (int i = 0; i < newX.Length; i++)
            {
                Data[i][0] = newX[i];
            }
        }

        public void UpdateY(double[] newY)
        {
            for (int i = 0; i < newY.Length; i++)
            {
                Data[i][1] = newY[i];
            }
        }

        public void InitializeParameters(int smootPoints = 0)
        {
            Debug.Assert(CurrentModel != null);
            if (CurrentModel.ModelFunction.CanInitParameters)
            {
                if (smootPoints == 0 || Y.Length < smootPoints * 4)
                {
                    CurrentModel.LastFitParameters = CurrentModel.ModelFunction.InitParameters(X, Y);
                }
                else
                {
                    CurrentModel.LastFitParameters = CurrentModel.ModelFunction.InitParameters(X, DataSmooth.MovingAverage(Y, smootPoints));
                }

                this.Parameters = this.CurrentModel.LastFitParameters;
            }
        }

        public void ReInitializeParameters()
        {
            Debug.Assert(CurrentModel != null);
            foreach(ModelFitInfo fitInfo in Models.Values)
            {
                try
                {
                    fitInfo.LastFitParameters = fitInfo.ModelFunction.InitParameters(X, Y);
                    LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(fitInfo.ModelFunction.CalcProcedure);
                    LeastSquare ls = new LeastSquare(X, Y, fitInfo.LastFitParameters, new int[0], calcFunc, null);
                    fitInfo.FitResultStdError = ls.CalculateStdError();
                }
                catch { } //TODO Add some logging or handling here
            }
        }

        public void MoveRow(int fromIndex, int toIndex)
        {
            var dataRow = Data[fromIndex];
            Data.RemoveAt(fromIndex);
            Data.Insert(toIndex, dataRow);
        }

        private bool DataFilterPredicate(float[] row)
        {
            for (int i = 0; i < DataFilterInfo.Items.Count; i++)
            {
                if (DataFilterInfo.Items[i].Selected)
                {
                    if (row[i] < DataFilterInfo.Items[i].Min || row[i] > DataFilterInfo.Items[i].Max)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Sort()
        {
            Data = Data.OrderBy( r => r[0]).ToList<float[]>();
        }

        public void FilterData(List<DataFilterInfo> filterItems)
        {
            //Restore to original data and filter it
            if (unfilteredData == null) 
            {
                unfilteredData = new List<float[]>();
                foreach (float[] row in Data)
                {
                    unfilteredData.Add(row);
                }
            }
            else
            {
                Data = new List<float[]>();
                foreach (float[] row in unfilteredData)
                {
                    Data.Add(row);
                }
            }

            Data.RemoveAll(new Predicate<float[]>(DataFilterPredicate));
        }
    }
}
