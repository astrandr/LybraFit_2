using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;

namespace Test.Common
{
    public class MainForm : BaseTest
    {
        private MenuBar menu;
        private Window mainWindow;
        private Table modelsGrid;
        private Table modelParametersGrid;

        public static MainForm FromMainMenu(Window mainWindow)
        {
            return new MainForm(mainWindow);
        }

        public MainForm(Window window) : base(window)
        {
            this.mainWindow = window;
            menu = mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu"));

        }

        private Table GetModelsGrid()
        {
            if (modelsGrid == null)
            {
                SetPlotView();
                modelsGrid = mainWindow.Get<Table>(SearchCriteria.ByAutomationId("dataGridModels"));
            }

            return modelsGrid;
        }

        private Table GetModelParametersGrid()
        {
            if (modelsGrid == null)
            {
                SetPlotView(); Delay();
                modelParametersGrid = mainWindow.Get<Table>(SearchCriteria.ByAutomationId("dataGridParameters"));
            }
            var i = 0;
            while(modelParametersGrid == null && i++ < 5 )
            {
                modelParametersGrid = mainWindow.Get<Table>(SearchCriteria.ByAutomationId("dataGridParameters"));
            }
            Assert.IsNotNull(modelParametersGrid, "Cannot find the parameters grid UI element");
            return modelParametersGrid;
        }

        public MainForm SetPlotView()
        {
            Menu plotMenuItem = menu.MenuItem("Data", "Graph view");
            plotMenuItem.Click();
            return this;
        }

        public string[] GetDocumentModels()
        {
            var rows = GetModelsGrid().Rows;
            return rows.Select(row => row.Cells["Name"].Value.ToString()).ToArray<string>();
        }

        public Tuple<string, string>[] GetModelParameters()
        {
            var rows = GetModelParametersGrid().Rows;
            return rows.Select(row => new Tuple<string, string>(row.Cells["Parameter"].Value.ToString(), row.Cells["Value"].Value.ToString())).ToArray();
        }
    }
}
