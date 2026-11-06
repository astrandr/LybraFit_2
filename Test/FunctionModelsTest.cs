using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Test.Common;
using Test.Dialogs;
using TestStack.White.UIItems.WindowItems;

namespace Test
{
    [TestClass]
    public class FunctionModelsTest
    {
        TestStack.White.Application app = null;
        Window mainWindow = null;
        private NewDataDialog newDataDialog;
        private ModelsDialog modelsDialog;
        private ExpressionEditorDialog expressionDialog;

        [TestInitialize]
        public void InitializeTest()
        {
            app = TestStack.White.Application.Launch(@"LybraFit.exe");
            mainWindow = app.GetWindow("LybraFit");
            BaseTest.DelayTime = 1000;
        }

        [TestCleanup]
        public void FinalizeTest()
        {
            if (mainWindow != null)
            {
                try
                {
                    app.ApplicationSession.Save();
                    mainWindow.Close();
                    mainWindow.Dispose();
                }
                finally
                {
                    mainWindow = null;
                }
            }

            if (app != null)
            {
                try
                {
                    app.Kill();
                    app.Dispose();
                }
                finally
                {
                    app = null;
                }
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void LibraryLinearFunction()
        {
            try
            {

                NewDataDialog newDataDialog;
                ModelsDialog modelsDialog;

                //Open, set precalculated data and function
                newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("LibraryLinearFunction")
                    .ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                    .ClickAutoCalculateY().SetExpression("2*x+3")
                    .ClickOk();

                MainForm main = MainForm.FromMainMenu(mainWindow);

                //TEst filter for the function with Linear function
                modelsDialog = ModelsDialog.FromMainMenu(mainWindow);
                string[] filteredFunctions = modelsDialog.SetFilter("Linear").GetFilteredLibFunctionNames();
                Assert.IsNotNull(filteredFunctions, "No functions match the filter");
                Assert.AreEqual(1, filteredFunctions.Length, "More than one functions found for filter 'Linear'");
                Assert.AreEqual("Linear", filteredFunctions[0], "Returned function is not as expected");

                //Close dialog
                modelsDialog.CheckLibFunction("Linear").ClickOk().Delay();
                Assert.IsTrue(modelsDialog.GetWindow().IsClosed);

                //Verify the function is added to the document and is only one
                string[] documentModels = main.GetDocumentModels();
                Assert.IsNotNull(documentModels, "No functions were added to document");
                Assert.AreEqual(1, documentModels.Length, "More than one functions found for document");
                Assert.AreEqual("Linear", documentModels[0], "The function in the model is not as expected");

                //Verify Linear function initialization checking the paramters
                var parameters = main.GetModelParameters();

                Assert.IsNotNull(parameters);
                Assert.AreEqual(2, parameters.Length, "Count of parameters not as expected");
                Assert.AreEqual("a", parameters[0].Item1, "Wrong name for the first parameter");
                Assert.AreEqual("b", parameters[1].Item1, "Wrong name for the second parameter");
                Assert.AreEqual("3", parameters[0].Item2, "Wrong value for the first parameter");
                Assert.AreEqual("2", parameters[1].Item2, "Wrong value for the second parameter");

            }
            catch (Exception e)
            {
                Assert.Fail(e.ToString());
                throw;
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void SeveralLibraryFunction()
        {
            NewDataDialog newDataDialog;
            ModelsDialog modelsDialog;

            //Open ne data dialog and set calculated data
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("SeveralLibraryFunction")
                .ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("0.2")
                .ClickAutoCalculateY().SetExpression("2*Sqrt(2*x+1)/(x+4)*Exp(-0.2*x)")
                .ClickOk();

            MainForm main = MainForm.FromMainMenu(mainWindow);
            //Open models dialog and set filter. Verify the found functions
            modelsDialog = ModelsDialog.FromMainMenu(mainWindow);
            string[] filteredFunctions = modelsDialog.SetFilter("Exp,pse").GetFilteredLibFunctionNames();
            Assert.IsNotNull(filteredFunctions, "No functions match the filter");
            Assert.AreEqual(4, filteredFunctions.Length, "More than one functions found for filter 'Linear'");
            Assert.AreEqual("Exponent", filteredFunctions[0], "Returned function is not as expected");
            Assert.AreEqual("Exponential Decline", filteredFunctions[1], "Returned function is not as expected");
            Assert.AreEqual("Pseudo-Voigt", filteredFunctions[2], "Returned function is not as expected");
            Assert.AreEqual("Reciprocal Exponential", filteredFunctions[3], "Returned function is not as expected");

            //Select the three library functions and close with ok
            modelsDialog.CheckLibFunction("Exponent").CheckLibFunction("Exponential Decline").CheckLibFunction("Pseudo-Voigt").ClickOk().Delay();
            Assert.IsTrue(modelsDialog.GetWindow().IsClosed);

            //Verify the functions were added to the document
            string[] documentModels = main.GetDocumentModels();
            Assert.IsNotNull(documentModels, "No functions were added to document");
            Assert.AreEqual(3, documentModels.Length, "Count of functions in the document not as expected document");
            Assert.AreEqual("Pseudo-Voigt", documentModels[0], "The function in the model is not as expected");
            Assert.AreEqual("Exponent", documentModels[1], "The function in the model is not as expected");
            Assert.AreEqual("Exponential Decline", documentModels[2], "The function in the model is not as expected");

            var parameters = main.GetModelParameters();
            //Verify initialization of the parameters
            Assert.IsNotNull(parameters);
            Assert.AreEqual(6, parameters.Length, "Count of parameters not as expected");
            Assert.AreEqual("a", parameters[0].Item1, "Wrong name for the first parameter");
            Assert.AreEqual("b", parameters[1].Item1, "Wrong name for the second parameter");
            Assert.AreEqual("c", parameters[2].Item1, "Wrong name for the third parameter");
            Assert.AreEqual("d", parameters[3].Item1, "Wrong name for the first parameter");
            Assert.AreEqual("e", parameters[4].Item1, "Wrong name for the second parameter");
            Assert.AreEqual("f", parameters[5].Item1, "Wrong name for the third parameter");
            Assert.AreEqual("0.503365043690562", parameters[0].Item2, "Wrong value for the first parameter");
            Assert.AreEqual("-7.90972929084951", parameters[1].Item2, "Wrong value for the second parameter");
            Assert.AreEqual("8.02946832474354", parameters[2].Item2, "Wrong value for the third parameter");
            Assert.AreEqual("0.269567356157551", parameters[3].Item2, "Wrong value for the first parameter");
            Assert.AreEqual("1.02962454943947", parameters[4].Item2, "Wrong value for the second parameter");
            Assert.AreEqual("4.0483604971376", parameters[5].Item2, "Wrong value for the third parameter");
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void AddRemoveCustomFunction()
        {
            string mathExpression = "2*Sqrt(2*x+1)/(x+4)*Exp(-0.2*x)";
            string newFunctionName = "Custom Function 1";

            //Open and type in new data autoincremented and precalculated
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("SeveralLibraryFunction")
                .ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("0.2")
                .ClickAutoCalculateY().SetExpression(mathExpression)
                .ClickOk();

            MainForm main = MainForm.FromMainMenu(mainWindow);

            //Open models dialog and verify buttons text
            modelsDialog = ModelsDialog.FromMainMenu(mainWindow);
            Assert.AreEqual("Edit", modelsDialog.BtnEdit.Text, "Wrong text for edit button.");
            Assert.AreEqual("New", modelsDialog.BtnNew.Text, "Wrong text for edit button.");
            Assert.AreEqual("Remove", modelsDialog.BtnRemove.Text, "Wrong text for edit button.");
            Bitmap mathImage = null;

            //Make sure there is no function with the name, necessary for the rest of the test to be successful
            if (IsFunctionPresent(newFunctionName))
            {
                RemoveFunction(newFunctionName);
            }
            //Search for the new expression 
            VerifyFunctionIsNotPresent(newFunctionName, mathExpression);

            //Enter new model in the math expression editor dialog and exit with cancel, verify the new function is not added
            AddNewCustomFunctionCancel(newFunctionName, mathExpression);

            //Enter new model in the math expression editor dialog and close with Ok, verify new the function is added
            AddNewCustomFunctionOk(newFunctionName, mathExpression);
            Assert.IsTrue(modelsDialog.GetExpressionImage(ref mathImage).CheckImages(mathImage, "AddRemoveCustomFunction_Step1"), "Math expression not displayed as expected");

            //Cancel and close the models dialog, open again and verifiy the new expression is there
            modelsDialog.ClickCancel().Delay();
            modelsDialog = ModelsDialog.FromMainMenu(mainWindow);
            VerifyFunctionIsPresent(newFunctionName, mathExpression);

            //Select the function and remove it, verify it is removed
            RemoveFunction(newFunctionName);
            Assert.IsTrue(modelsDialog.GetExpressionImage(ref mathImage).CheckImages(mathImage, "AddRemoveCustomFunction_Step2"), "Math expression not displayed as expected step 1");

            //Enter new model in the math expression editor dialog
            AddNewCustomFunctionOk(newFunctionName, mathExpression);
            Assert.IsTrue(modelsDialog.GetExpressionImage(ref mathImage).CheckImages(mathImage, "AddRemoveCustomFunction_Step3"), "Math expression not displayed as expected step 2");

            //close the models dialog with Ok, open again and verifiy the new expression is there
            modelsDialog.ClickOk().Delay();
            modelsDialog = ModelsDialog.FromMainMenu(mainWindow);
            VerifyFunctionIsPresent(newFunctionName, mathExpression);
            Assert.IsTrue(modelsDialog.GetExpressionImage(ref mathImage).CheckImages(mathImage, "AddRemoveCustomFunction_Step3"), "Math expression not displayed as expected  step 3");

            //Remove the new function and verify it is removed
            RemoveFunction(newFunctionName);
            Assert.IsTrue(modelsDialog.GetExpressionImage(ref mathImage).CheckImages(mathImage, "AddRemoveCustomFunction_Step4"), "Math expression not displayed as expected step 4");

        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void AddRemoveCustomFunctionWhichExistsInLibrary()
        {
            string mathExpressionData = "0.1*Sin(0.2*x+0.3)+0.4";
            string mathExpressionFunction = "q*Sin(w*x+e)+r";
            string newFunctionName = "Custom Function 1";

            //Open and type in new data autoincremented and precalculated
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("SeveralLibraryFunction")
                .ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("0.2")
                .ClickAutoCalculateY().SetExpression(mathExpressionData)
                .ClickOk();

            MainForm main = MainForm.FromMainMenu(mainWindow);

            //Open models dialog and verify buttons text
            modelsDialog = ModelsDialog.FromMainMenu(mainWindow);
            Assert.AreEqual("Edit", modelsDialog.BtnEdit.Text, "Wrong text for edit button.");
            Assert.AreEqual("New", modelsDialog.BtnNew.Text, "Wrong text for edit button.");
            Assert.AreEqual("Remove", modelsDialog.BtnRemove.Text, "Wrong text for edit button.");

            RemoveFunction(newFunctionName);

            //Enter new model in the math expression editor dialog and exit with cancel, verify the new function is not added
            AddNewCustomFunctionCancel(newFunctionName, mathExpressionFunction);

            //Enter new model in the math expression editor dialog and close with Ok, verify new the function is added
            AddCustomLibraryExistingFunctionOk(newFunctionName, mathExpressionFunction);
        }

        /// <summary>
        /// AddNewCustomFunctionOk - Enter new model in the math expression editor dialog and close with Ok, verify new the function is added
        /// </summary>
        /// <param name="newFunctionName"></param>
        /// <param name="mathExpresion"></param>
        private void AddNewCustomFunctionOk(string newFunctionName, string mathExpression)
        {

            modelsDialog.ClickNew().Delay();

            expressionDialog = ExpressionEditorDialog.FromMainWindow(mainWindow);

            //Verify name and expression boxes are empty, and set new name and expresion
            expressionDialog.IfNameIs("").IfExpressionIs("").SetName(newFunctionName).SetExpression(mathExpression).Delay().ClickOk().Delay();

            modelsDialog = ModelsDialog.FromMainWindow(mainWindow);

            string[] customFunctions = modelsDialog.Delay().GetFilteredCustomFunctionNames();

            //Search for and verify the new expression
            bool found = false;
            for (int i = 0; i < customFunctions.Length && !found; i++)
            {
                found |= customFunctions[i] == newFunctionName;
            }

            Assert.IsTrue(found, "Entered new math function not found in the user functions table.");
            string returnedMathExpressionReturned = modelsDialog.GetFilteredCustomFunctionExpression(newFunctionName);
            Assert.AreEqual(mathExpression, returnedMathExpressionReturned, "Math expression for new function is not as expected.");
        }

        /// <summary>
        /// AddNewCustomFunctionCancel - Enter new model in the math expression editor dialog and close with Cancel, verify new the function was not added
        /// </summary>
        /// <param name="newFunctionName"></param>
        /// <param name="mathExpresion"></param>
        private void AddNewCustomFunctionCancel(string newFunctionName, string mathExpression)
        {

            modelsDialog.ClickNew().Delay();

            expressionDialog = ExpressionEditorDialog.FromMainWindow(mainWindow);

            expressionDialog.SetName(newFunctionName).SetExpression(mathExpression).Delay().ClickCancel().Delay();

            modelsDialog = ModelsDialog.FromMainWindow(mainWindow);

            string[] customFunctions = modelsDialog.Delay().GetFilteredCustomFunctionNames();

            //Search for and verify the new expression
            bool found = false;
            for (int i = 0; i < customFunctions.Length && !found; i++)
            {
                found |= customFunctions[i] == newFunctionName;
            }

            Assert.IsFalse(found, "Entered new math function was added thought expression dialog was closed with cancel.");
        }

        /// <summary>
        /// AddNewCustomFunctionOk - Enter new model in the math expression editor dialog and close with Ok, verify new the function is added
        /// </summary>
        /// <param name="newFunctionName"></param>
        /// <param name="mathExpresion"></param>
        private void AddCustomLibraryExistingFunctionOk(string newFunctionName, string mathExpression)
        {

            modelsDialog.ClickNew().Delay();

            expressionDialog = ExpressionEditorDialog.FromMainWindow(mainWindow);

            //Verify name and expression boxes are empty, and set new name and expresion
            expressionDialog.IfNameIs("").IfExpressionIs("").SetName(newFunctionName).SetExpression(mathExpression).Delay().ClickOk().Delay();

            var informationDialog = InformationDialog.FromMainWindow(mainWindow);

            informationDialog.ClickClose();
        }

        /// <summary>
        /// RemoveFunction - Remove model and verify it is not in the custom functions list
        /// </summary>
        /// <param name="functionName"></param>
        private void RemoveFunction(string functionName)
        {
            modelsDialog.SelectFunction(functionName);

            if (modelsDialog.HasFunction(functionName))
            {
                modelsDialog.ClickRemove().Delay();
            }

            modelsDialog = ModelsDialog.FromMainWindow(mainWindow);

            string[] customFunctions = modelsDialog.Delay().GetFilteredCustomFunctionNames();

            //Search for and verify the new expression
            bool found = false;
            for (int i = 0; i < customFunctions.Length && !found; i++)
            {
                found |= customFunctions[i] == functionName;
            }

            Assert.IsFalse(found, "Entered new math function was added thought expression dialog was closed with cancel.");
        }

        private void VerifyFunctionIsPresent(string functionName, string mathExpression)
        {
            modelsDialog = ModelsDialog.FromMainWindow(mainWindow);
            string[] customFunctions = modelsDialog.Delay().Delay().GetFilteredCustomFunctionNames();
            var found = false;
            for (int i = 0; i < customFunctions.Length && !found; i++)
            {
                found |= customFunctions[i] == functionName;
            }
            Assert.IsTrue(found, "Function was not found");
            string expression = modelsDialog.GetFilteredCustomFunctionExpression(functionName);

            Assert.AreEqual(mathExpression, expression, "Math expression not as expected");
        }

        private void VerifyFunctionIsNotPresent(string functionName, string mathExpression)
        {
            modelsDialog = ModelsDialog.FromMainWindow(mainWindow);
            string[] customFunctions = modelsDialog.Delay().Delay().GetFilteredCustomFunctionNames();
            var found = false;
            for (int i = 0; i < customFunctions.Length && !found; i++)
            {
                found |= customFunctions[i] == functionName;
            }
            Assert.IsFalse(found, "Function was present in the list");
        }

        private bool IsFunctionPresent(string functionName)
        {
            modelsDialog = ModelsDialog.FromMainWindow(mainWindow);
            string[] customFunctions = modelsDialog.Delay().Delay().GetFilteredCustomFunctionNames();
            var found = false;
            for (int i = 0; i < customFunctions.Length && !found; i++)
            {
                found |= customFunctions[i] == functionName;
            }
            return found;
        }

    }
}
