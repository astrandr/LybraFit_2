using System.Drawing;
namespace LybraFit
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileImportASCII = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importASCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportASCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRecentProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.copyStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPasteInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInverseTransform = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tableViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.modelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCalculateFit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddAllModels = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuUpdateStdErrors = new System.Windows.Forms.ToolStripMenuItem();
            this.derivedValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAddToLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuReprots = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuTextReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuGraphicalReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuLogReport = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatPlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuViewHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpenProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripProjectSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNewData = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPasteAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteRows = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripDataSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonColumnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGraphSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSettingsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAddModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddAllModels = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPlot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFit = new System.Windows.Forms.ToolStripButton();
            this.toolStripModelSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGraphicalReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripHelpSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tabPagePlot = new System.Windows.Forms.TabPage();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.graph = new GraphControlLib.GraphControl();
            this.contextMenuPlot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.plotMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.plotMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFunctionParameters = new System.Windows.Forms.Panel();
            this.panelModelParameters = new System.Windows.Forms.Panel();
            this.btnStop = new LybraFit.StylishButton();
            this.btnInitParameters = new LybraFit.StylishButton();
            this.lblLastModel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStdError = new System.Windows.Forms.Label();
            this.btnPlot = new LybraFit.StylishButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCalcBestFit = new LybraFit.StylishButton();
            this.dataGridParameters = new System.Windows.Forms.DataGridView();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vary = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterSigma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuParametersGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuCopyParameters = new System.Windows.Forms.ToolStripMenuItem();
            this.logResultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.derivedValuesPopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelModels = new System.Windows.Forms.Panel();
            this.pictureBoxModelPreview = new System.Windows.Forms.PictureBox();
            this.dataGridModels = new System.Windows.Forms.DataGridView();
            this.ModelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelFitStdError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuModelsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuRemoveModel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuRemoveAllModels = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuReInit = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.savePlotImageFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveProjectDialog = new System.Windows.Forms.SaveFileDialog();
            this.openProjectDialog = new System.Windows.Forms.OpenFileDialog();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mainMenu.SuspendLayout();
            this.contextMenuDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabPagePlot.SuspendLayout();
            this.panelGraph.SuspendLayout();
            this.contextMenuPlot.SuspendLayout();
            this.panelFunctionParameters.SuspendLayout();
            this.panelModelParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParameters)).BeginInit();
            this.contextMenuParametersGrid.SuspendLayout();
            this.panelModels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModelPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridModels)).BeginInit();
            this.contextMenuModelsGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileImportASCII,
            this.dataMenu,
            this.toolStripMenuItem1,
            this.toolStripMenuReprots,
            this.settingsStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(1081, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileImportASCII
            // 
            this.fileImportASCII.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.importASCIIToolStripMenuItem,
            this.exportASCIIToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuRecentFiles,
            this.toolStripMenuItemRecentProjects,
            this.toolStripSeparator4,
            this.toolStripMenuItem2});
            this.fileImportASCII.Name = "fileImportASCII";
            this.fileImportASCII.Size = new System.Drawing.Size(37, 20);
            this.fileImportASCII.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // importASCIIToolStripMenuItem
            // 
            this.importASCIIToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importASCIIToolStripMenuItem.Image")));
            this.importASCIIToolStripMenuItem.Name = "importASCIIToolStripMenuItem";
            this.importASCIIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importASCIIToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.importASCIIToolStripMenuItem.Tag = "K0";
            this.importASCIIToolStripMenuItem.Text = "&Import ASCII";
            this.importASCIIToolStripMenuItem.Click += new System.EventHandler(this.importASCIIToolStripMenuItem_Click);
            // 
            // exportASCIIToolStripMenuItem
            // 
            this.exportASCIIToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportASCIIToolStripMenuItem.Image")));
            this.exportASCIIToolStripMenuItem.Name = "exportASCIIToolStripMenuItem";
            this.exportASCIIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportASCIIToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exportASCIIToolStripMenuItem.Tag = "K2";
            this.exportASCIIToolStripMenuItem.Text = "Export ASCII";
            this.exportASCIIToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveToolStripMenuItem.Tag = "K2";
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveAsToolStripMenuItem.Tag = "K2";
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(176, 6);
            // 
            // toolStripMenuRecentFiles
            // 
            this.toolStripMenuRecentFiles.Name = "toolStripMenuRecentFiles";
            this.toolStripMenuRecentFiles.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuRecentFiles.Text = "Recent Files";
            // 
            // toolStripMenuItemRecentProjects
            // 
            this.toolStripMenuItemRecentProjects.Name = "toolStripMenuItemRecentProjects";
            this.toolStripMenuItemRecentProjects.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItemRecentProjects.Text = "Recent projects";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(176, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem2.Text = "&Exit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // dataMenu
            // 
            this.dataMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator5,
            this.copyStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripPasteInsert,
            this.toolStripSeparator1,
            this.insertToolStripMenuItem,
            this.deleteSelectedStripMenuItem,
            this.toolStripMenuItemUndo,
            this.toolStripMenuItem6,
            this.transformToolStripMenuItem,
            this.toolStripMenuItemInverseTransform,
            this.filterToolStripMenuItem,
            this.toolStripSeparator6,
            this.tableViewToolStripMenuItem,
            this.graphViewToolStripMenuItem});
            this.dataMenu.Name = "dataMenu";
            this.dataMenu.Size = new System.Drawing.Size(43, 20);
            this.dataMenu.Text = "&Data";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(227, 6);
            // 
            // copyStripMenuItem
            // 
            this.copyStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyStripMenuItem.Image")));
            this.copyStripMenuItem.Name = "copyStripMenuItem";
            this.copyStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.copyStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.copyStripMenuItem.Tag = "K2";
            this.copyStripMenuItem.Text = "&Export to clipboard";
            this.copyStripMenuItem.Click += new System.EventHandler(this.copyStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.pasteToolStripMenuItem.Tag = "K0";
            this.pasteToolStripMenuItem.Text = "Import from c&lipboard";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripPasteInsert
            // 
            this.toolStripPasteInsert.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPasteInsert.Image")));
            this.toolStripPasteInsert.Name = "toolStripPasteInsert";
            this.toolStripPasteInsert.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.toolStripPasteInsert.Size = new System.Drawing.Size(230, 22);
            this.toolStripPasteInsert.Tag = "K1";
            this.toolStripPasteInsert.Text = "&Paste";
            this.toolStripPasteInsert.Click += new System.EventHandler(this.toolStripPasteInsert_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(227, 6);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("insertToolStripMenuItem.Image")));
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.insertToolStripMenuItem.Tag = "K1";
            this.insertToolStripMenuItem.Text = "&Insert";
            this.insertToolStripMenuItem.Click += new System.EventHandler(this.insertToolStripMenuItem_Click);
            // 
            // deleteSelectedStripMenuItem
            // 
            this.deleteSelectedStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteSelectedStripMenuItem.Image")));
            this.deleteSelectedStripMenuItem.Name = "deleteSelectedStripMenuItem";
            this.deleteSelectedStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteSelectedStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.deleteSelectedStripMenuItem.Tag = "K3";
            this.deleteSelectedStripMenuItem.Text = "&Delete Selected";
            this.deleteSelectedStripMenuItem.Click += new System.EventHandler(this.deleteSelectedStripMenuItem1_Click);
            // 
            // toolStripMenuItemUndo
            // 
            this.toolStripMenuItemUndo.Enabled = false;
            this.toolStripMenuItemUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemUndo.Image")));
            this.toolStripMenuItemUndo.Name = "toolStripMenuItemUndo";
            this.toolStripMenuItemUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.toolStripMenuItemUndo.Size = new System.Drawing.Size(230, 22);
            this.toolStripMenuItemUndo.Tag = "K9";
            this.toolStripMenuItemUndo.Text = "&Undo";
            this.toolStripMenuItemUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(227, 6);
            // 
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("transformToolStripMenuItem.Image")));
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.transformToolStripMenuItem.Tag = "K2";
            this.transformToolStripMenuItem.Text = "Transform";
            this.transformToolStripMenuItem.Click += new System.EventHandler(this.transformToolStripMenuItem_Click);
            // 
            // toolStripMenuItemInverseTransform
            // 
            this.toolStripMenuItemInverseTransform.Enabled = false;
            this.toolStripMenuItemInverseTransform.Name = "toolStripMenuItemInverseTransform";
            this.toolStripMenuItemInverseTransform.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.toolStripMenuItemInverseTransform.Size = new System.Drawing.Size(230, 22);
            this.toolStripMenuItemInverseTransform.Text = "In&verse transform";
            this.toolStripMenuItemInverseTransform.Click += new System.EventHandler(this.toolStripMenuItemInverseTransform_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.filterToolStripMenuItem.Tag = "K2";
            this.filterToolStripMenuItem.Text = "&Filter";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(227, 6);
            // 
            // tableViewToolStripMenuItem
            // 
            this.tableViewToolStripMenuItem.Name = "tableViewToolStripMenuItem";
            this.tableViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tableViewToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.tableViewToolStripMenuItem.Tag = "K0";
            this.tableViewToolStripMenuItem.Text = "&Table view";
            this.tableViewToolStripMenuItem.Click += new System.EventHandler(this.tableViewToolStripMenuItem_Click);
            // 
            // graphViewToolStripMenuItem
            // 
            this.graphViewToolStripMenuItem.Name = "graphViewToolStripMenuItem";
            this.graphViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.graphViewToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.graphViewToolStripMenuItem.Tag = "K2";
            this.graphViewToolStripMenuItem.Text = "&Graph view";
            this.graphViewToolStripMenuItem.Click += new System.EventHandler(this.graphViewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItemCalculateFit,
            this.toolStripMenuAddAllModels,
            this.toolStripMenuUpdateStdErrors,
            this.derivedValuesToolStripMenuItem,
            this.toolStripMenuAddToLog});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(62, 20);
            this.toolStripMenuItem1.Tag = "K2";
            this.toolStripMenuItem1.Text = "&Analysis";
            // 
            // modelsToolStripMenuItem
            // 
            this.modelsToolStripMenuItem.Enabled = false;
            this.modelsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("modelsToolStripMenuItem.Image")));
            this.modelsToolStripMenuItem.Name = "modelsToolStripMenuItem";
            this.modelsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.modelsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.modelsToolStripMenuItem.Tag = "K2";
            this.modelsToolStripMenuItem.Text = "Add &Models";
            this.modelsToolStripMenuItem.Click += new System.EventHandler(this.modelsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.toolStripMenuItem3.Size = new System.Drawing.Size(229, 22);
            this.toolStripMenuItem3.Tag = "K5";
            this.toolStripMenuItem3.Text = "&Plot";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // toolStripMenuItemCalculateFit
            // 
            this.toolStripMenuItemCalculateFit.Enabled = false;
            this.toolStripMenuItemCalculateFit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCalculateFit.Image")));
            this.toolStripMenuItemCalculateFit.Name = "toolStripMenuItemCalculateFit";
            this.toolStripMenuItemCalculateFit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.toolStripMenuItemCalculateFit.Size = new System.Drawing.Size(229, 22);
            this.toolStripMenuItemCalculateFit.Tag = "K5";
            this.toolStripMenuItemCalculateFit.Text = "Calculate &Fit";
            this.toolStripMenuItemCalculateFit.Click += new System.EventHandler(this.btnCalcBestFit_Click);
            // 
            // toolStripMenuAddAllModels
            // 
            this.toolStripMenuAddAllModels.Enabled = false;
            this.toolStripMenuAddAllModels.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuAddAllModels.Image")));
            this.toolStripMenuAddAllModels.Name = "toolStripMenuAddAllModels";
            this.toolStripMenuAddAllModels.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.toolStripMenuAddAllModels.Size = new System.Drawing.Size(229, 22);
            this.toolStripMenuAddAllModels.Tag = "K2";
            this.toolStripMenuAddAllModels.Text = "&Add all library models";
            this.toolStripMenuAddAllModels.Click += new System.EventHandler(this.toolStripButtonFitAll_Click);
            // 
            // toolStripMenuUpdateStdErrors
            // 
            this.toolStripMenuUpdateStdErrors.Enabled = false;
            this.toolStripMenuUpdateStdErrors.Name = "toolStripMenuUpdateStdErrors";
            this.toolStripMenuUpdateStdErrors.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.toolStripMenuUpdateStdErrors.Size = new System.Drawing.Size(229, 22);
            this.toolStripMenuUpdateStdErrors.Tag = "K5";
            this.toolStripMenuUpdateStdErrors.Text = "&Update Std. Errors";
            this.toolStripMenuUpdateStdErrors.Click += new System.EventHandler(this.toolStripMenuUpdateStdErrors_Click);
            // 
            // derivedValuesToolStripMenuItem
            // 
            this.derivedValuesToolStripMenuItem.Enabled = false;
            this.derivedValuesToolStripMenuItem.Name = "derivedValuesToolStripMenuItem";
            this.derivedValuesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.derivedValuesToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.derivedValuesToolStripMenuItem.Tag = "K5";
            this.derivedValuesToolStripMenuItem.Text = "&Derived values";
            this.derivedValuesToolStripMenuItem.Click += new System.EventHandler(this.derivedValuesToolStripMenuItem_Click);
            // 
            // toolStripMenuAddToLog
            // 
            this.toolStripMenuAddToLog.Enabled = false;
            this.toolStripMenuAddToLog.Name = "toolStripMenuAddToLog";
            this.toolStripMenuAddToLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.toolStripMenuAddToLog.Size = new System.Drawing.Size(229, 22);
            this.toolStripMenuAddToLog.Tag = "K5";
            this.toolStripMenuAddToLog.Text = "&Log Fit Results";
            this.toolStripMenuAddToLog.Click += new System.EventHandler(this.toolStripMenuAddToLog_Click);
            // 
            // toolStripMenuReprots
            // 
            this.toolStripMenuReprots.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuTextReport,
            this.toolStripMenuGraphicalReport,
            this.toolStripMenuLogReport});
            this.toolStripMenuReprots.Enabled = false;
            this.toolStripMenuReprots.Name = "toolStripMenuReprots";
            this.toolStripMenuReprots.Size = new System.Drawing.Size(59, 20);
            this.toolStripMenuReprots.Tag = "K5";
            this.toolStripMenuReprots.Text = "Reports";
            // 
            // toolStripMenuTextReport
            // 
            this.toolStripMenuTextReport.Enabled = false;
            this.toolStripMenuTextReport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuTextReport.Image")));
            this.toolStripMenuTextReport.Name = "toolStripMenuTextReport";
            this.toolStripMenuTextReport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.toolStripMenuTextReport.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuTextReport.Tag = "K5";
            this.toolStripMenuTextReport.Text = "Generate &Text Report";
            this.toolStripMenuTextReport.Click += new System.EventHandler(this.toolStripButtonReport_Click);
            // 
            // toolStripMenuGraphicalReport
            // 
            this.toolStripMenuGraphicalReport.Enabled = false;
            this.toolStripMenuGraphicalReport.Name = "toolStripMenuGraphicalReport";
            this.toolStripMenuGraphicalReport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.toolStripMenuGraphicalReport.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuGraphicalReport.Tag = "K5";
            this.toolStripMenuGraphicalReport.Text = "&Graphical Report";
            this.toolStripMenuGraphicalReport.Click += new System.EventHandler(this.toolStripButtonGraphicalReport_Click);
            // 
            // toolStripMenuLogReport
            // 
            this.toolStripMenuLogReport.Enabled = false;
            this.toolStripMenuLogReport.Name = "toolStripMenuLogReport";
            this.toolStripMenuLogReport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
            this.toolStripMenuLogReport.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuLogReport.Tag = "K5";
            this.toolStripMenuLogReport.Text = "Fit &Log Report";
            this.toolStripMenuLogReport.Click += new System.EventHandler(this.toolStripMenuFitLogsReport_Click);
            // 
            // settingsStripMenuItem
            // 
            this.settingsStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatPlotToolStripMenuItem,
            this.formatColumnsToolStripMenuItem});
            this.settingsStripMenuItem.Name = "settingsStripMenuItem";
            this.settingsStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsStripMenuItem.Tag = "K1";
            this.settingsStripMenuItem.Text = "&Settings";
            // 
            // formatPlotToolStripMenuItem
            // 
            this.formatPlotToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("formatPlotToolStripMenuItem.Image")));
            this.formatPlotToolStripMenuItem.Name = "formatPlotToolStripMenuItem";
            this.formatPlotToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.formatPlotToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.formatPlotToolStripMenuItem.Tag = "K2";
            this.formatPlotToolStripMenuItem.Text = "&Graph";
            this.formatPlotToolStripMenuItem.Click += new System.EventHandler(this.formatGraphToolStripMenuItem_Click);
            // 
            // formatColumnsToolStripMenuItem
            // 
            this.formatColumnsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("formatColumnsToolStripMenuItem.Image")));
            this.formatColumnsToolStripMenuItem.Name = "formatColumnsToolStripMenuItem";
            this.formatColumnsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.formatColumnsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.formatColumnsToolStripMenuItem.Tag = "K1";
            this.formatColumnsToolStripMenuItem.Text = "&Table Columns";
            this.formatColumnsToolStripMenuItem.Click += new System.EventHandler(this.editColumnsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuViewHelp,
            this.toolStripSeparator2,
            this.toolStripMenuAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStripMenuViewHelp
            // 
            this.toolStripMenuViewHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuViewHelp.Image")));
            this.toolStripMenuViewHelp.Name = "toolStripMenuViewHelp";
            this.toolStripMenuViewHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.toolStripMenuViewHelp.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuViewHelp.Text = "View Help";
            this.toolStripMenuViewHelp.Click += new System.EventHandler(this.toolStripButtonHelp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // toolStripMenuAbout
            // 
            this.toolStripMenuAbout.Name = "toolStripMenuAbout";
            this.toolStripMenuAbout.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuAbout.Text = "About";
            this.toolStripMenuAbout.Click += new System.EventHandler(this.toolStripMenuAbout_Click);
            // 
            // contextMenuDataGrid
            // 
            this.contextMenuDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuPaste,
            this.insertToolStripMenuItem1,
            this.deleteSelectedStripMenuItem1});
            this.contextMenuDataGrid.Name = "contextMenuDataGrid";
            this.contextMenuDataGrid.Size = new System.Drawing.Size(155, 70);
            // 
            // toolStripMenuPaste
            // 
            this.toolStripMenuPaste.Name = "toolStripMenuPaste";
            this.toolStripMenuPaste.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuPaste.Tag = "K1";
            this.toolStripMenuPaste.Text = "Paste";
            this.toolStripMenuPaste.Click += new System.EventHandler(this.toolStripPasteInsert_Click);
            // 
            // insertToolStripMenuItem1
            // 
            this.insertToolStripMenuItem1.Name = "insertToolStripMenuItem1";
            this.insertToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.insertToolStripMenuItem1.Tag = "K1";
            this.insertToolStripMenuItem1.Text = "Insert";
            this.insertToolStripMenuItem1.Click += new System.EventHandler(this.insertToolStripMenuItem_Click);
            // 
            // deleteSelectedStripMenuItem1
            // 
            this.deleteSelectedStripMenuItem1.Name = "deleteSelectedStripMenuItem1";
            this.deleteSelectedStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.deleteSelectedStripMenuItem1.Tag = "K3";
            this.deleteSelectedStripMenuItem1.Text = "Delete Selected";
            this.deleteSelectedStripMenuItem1.Click += new System.EventHandler(this.deleteSelectedStripMenuItem1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 24);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.toolStripMain);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelMain);
            this.splitContainerMain.Size = new System.Drawing.Size(1081, 721);
            this.splitContainerMain.TabIndex = 1;
            // 
            // toolStripMain
            // 
            this.toolStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenProject,
            this.toolStripButtonSaveProject,
            this.toolStripProjectSeparator,
            this.toolStripButtonImport,
            this.toolStripButtonExport,
            this.toolStripButtonNewData,
            this.toolStripButtonPasteAll,
            this.toolStripButtonCopy,
            this.toolStripButtonPaste,
            this.toolStripButtonDeleteRows,
            this.toolStripButtonUndo,
            this.toolStripDataSeparator,
            this.toolStripButtonColumnSettings,
            this.toolStripButtonGraphSettings,
            this.toolStripSettingsSeparator,
            this.toolStripButtonAddModel,
            this.toolStripButtonAddAllModels,
            this.toolStripButtonPlot,
            this.toolStripButtonFit,
            this.toolStripModelSeparator,
            this.toolStripButtonZoomOut,
            this.toolStripButtonReport,
            this.toolStripButtonGraphicalReport,
            this.toolStripHelpSeparator,
            this.toolStripButtonHelp});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1081, 50);
            this.toolStripMain.TabIndex = 0;
            // 
            // toolStripButtonOpenProject
            // 
            this.toolStripButtonOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenProject.Image")));
            this.toolStripButtonOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenProject.Name = "toolStripButtonOpenProject";
            this.toolStripButtonOpenProject.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonOpenProject.Tag = "K0";
            this.toolStripButtonOpenProject.Text = "Open project";
            this.toolStripButtonOpenProject.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripButtonSaveProject
            // 
            this.toolStripButtonSaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveProject.Image")));
            this.toolStripButtonSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveProject.Name = "toolStripButtonSaveProject";
            this.toolStripButtonSaveProject.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonSaveProject.Tag = "K2";
            this.toolStripButtonSaveProject.Text = "Save project";
            this.toolStripButtonSaveProject.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripProjectSeparator
            // 
            this.toolStripProjectSeparator.Name = "toolStripProjectSeparator";
            this.toolStripProjectSeparator.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButtonImport
            // 
            this.toolStripButtonImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImport.Image")));
            this.toolStripButtonImport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImport.Name = "toolStripButtonImport";
            this.toolStripButtonImport.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonImport.Tag = "K0";
            this.toolStripButtonImport.Text = "Import data from ASCII text file";
            this.toolStripButtonImport.ToolTipText = "Import data from ASCII text file";
            this.toolStripButtonImport.Click += new System.EventHandler(this.importASCIIToolStripMenuItem_Click);
            // 
            // toolStripButtonExport
            // 
            this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExport.Image")));
            this.toolStripButtonExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExport.Name = "toolStripButtonExport";
            this.toolStripButtonExport.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonExport.Tag = "K2";
            this.toolStripButtonExport.Text = "Export data to ASCII text file";
            this.toolStripButtonExport.ToolTipText = "Export data to ASCII text file";
            this.toolStripButtonExport.Click += new System.EventHandler(this.toolStripButtonExport_Click);
            // 
            // toolStripButtonNewData
            // 
            this.toolStripButtonNewData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewData.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewData.Image")));
            this.toolStripButtonNewData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonNewData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewData.Name = "toolStripButtonNewData";
            this.toolStripButtonNewData.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonNewData.Tag = "K0";
            this.toolStripButtonNewData.Text = "Generate Data";
            this.toolStripButtonNewData.ToolTipText = "Generate Data";
            this.toolStripButtonNewData.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripButtonPasteAll
            // 
            this.toolStripButtonPasteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPasteAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPasteAll.Image")));
            this.toolStripButtonPasteAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPasteAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPasteAll.Name = "toolStripButtonPasteAll";
            this.toolStripButtonPasteAll.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonPasteAll.Tag = "K0";
            this.toolStripButtonPasteAll.Text = "Import from clipboard";
            this.toolStripButtonPasteAll.ToolTipText = "Import from clipboard";
            this.toolStripButtonPasteAll.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopy.Image")));
            this.toolStripButtonCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonCopy.Tag = "K2";
            this.toolStripButtonCopy.Text = "Copy all data to clipboard";
            this.toolStripButtonCopy.ToolTipText = "Copy all data to clipboard";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.copyStripMenuItem_Click);
            // 
            // toolStripButtonPaste
            // 
            this.toolStripButtonPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPaste.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPaste.Image")));
            this.toolStripButtonPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPaste.Name = "toolStripButtonPaste";
            this.toolStripButtonPaste.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonPaste.Tag = "K1";
            this.toolStripButtonPaste.Text = "Paste from clipboard";
            this.toolStripButtonPaste.ToolTipText = "Paste from clipboard";
            this.toolStripButtonPaste.Click += new System.EventHandler(this.toolStripPasteInsert_Click);
            // 
            // toolStripButtonDeleteRows
            // 
            this.toolStripButtonDeleteRows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDeleteRows.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeleteRows.Image")));
            this.toolStripButtonDeleteRows.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonDeleteRows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteRows.Name = "toolStripButtonDeleteRows";
            this.toolStripButtonDeleteRows.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonDeleteRows.Tag = "K3";
            this.toolStripButtonDeleteRows.Text = "Delete selected rows";
            this.toolStripButtonDeleteRows.ToolTipText = "Delete selected rows";
            this.toolStripButtonDeleteRows.Click += new System.EventHandler(this.deleteSelectedStripMenuItem1_Click);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Enabled = false;
            this.toolStripButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndo.Image")));
            this.toolStripButtonUndo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonUndo.Tag = "K9";
            this.toolStripButtonUndo.Text = "Undo Delete/Paste";
            this.toolStripButtonUndo.ToolTipText = "Undo Delete/Paste";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // toolStripDataSeparator
            // 
            this.toolStripDataSeparator.Name = "toolStripDataSeparator";
            this.toolStripDataSeparator.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButtonColumnSettings
            // 
            this.toolStripButtonColumnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonColumnSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonColumnSettings.Image")));
            this.toolStripButtonColumnSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonColumnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonColumnSettings.Name = "toolStripButtonColumnSettings";
            this.toolStripButtonColumnSettings.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonColumnSettings.Tag = "K1";
            this.toolStripButtonColumnSettings.Text = "Data columns settings";
            this.toolStripButtonColumnSettings.ToolTipText = "Data columns settings";
            this.toolStripButtonColumnSettings.Click += new System.EventHandler(this.editColumnsToolStripMenuItem_Click);
            // 
            // toolStripButtonGraphSettings
            // 
            this.toolStripButtonGraphSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGraphSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGraphSettings.Image")));
            this.toolStripButtonGraphSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonGraphSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGraphSettings.Name = "toolStripButtonGraphSettings";
            this.toolStripButtonGraphSettings.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonGraphSettings.Tag = "K1";
            this.toolStripButtonGraphSettings.Text = "Graph settings";
            this.toolStripButtonGraphSettings.ToolTipText = "Graph settings";
            this.toolStripButtonGraphSettings.Click += new System.EventHandler(this.formatGraphToolStripMenuItem_Click);
            // 
            // toolStripSettingsSeparator
            // 
            this.toolStripSettingsSeparator.Name = "toolStripSettingsSeparator";
            this.toolStripSettingsSeparator.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButtonAddModel
            // 
            this.toolStripButtonAddModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddModel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddModel.Image")));
            this.toolStripButtonAddModel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonAddModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddModel.Name = "toolStripButtonAddModel";
            this.toolStripButtonAddModel.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonAddModel.Tag = "K2";
            this.toolStripButtonAddModel.Text = "Add models";
            this.toolStripButtonAddModel.ToolTipText = "Add models";
            this.toolStripButtonAddModel.Click += new System.EventHandler(this.modelsToolStripMenuItem_Click);
            // 
            // toolStripButtonAddAllModels
            // 
            this.toolStripButtonAddAllModels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddAllModels.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddAllModels.Image")));
            this.toolStripButtonAddAllModels.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonAddAllModels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddAllModels.Name = "toolStripButtonAddAllModels";
            this.toolStripButtonAddAllModels.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonAddAllModels.Tag = "K2";
            this.toolStripButtonAddAllModels.Text = "Add all library models";
            this.toolStripButtonAddAllModels.ToolTipText = "Add all library models";
            this.toolStripButtonAddAllModels.Click += new System.EventHandler(this.toolStripButtonFitAll_Click);
            // 
            // toolStripButtonPlot
            // 
            this.toolStripButtonPlot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPlot.Image")));
            this.toolStripButtonPlot.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonPlot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlot.Name = "toolStripButtonPlot";
            this.toolStripButtonPlot.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonPlot.Tag = "K5";
            this.toolStripButtonPlot.Text = "Plot with current model parameters";
            this.toolStripButtonPlot.ToolTipText = "Plot with current model parameters";
            this.toolStripButtonPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // toolStripButtonFit
            // 
            this.toolStripButtonFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFit.Image")));
            this.toolStripButtonFit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFit.Name = "toolStripButtonFit";
            this.toolStripButtonFit.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonFit.Tag = "K5";
            this.toolStripButtonFit.Text = "Find best fit for current model";
            this.toolStripButtonFit.ToolTipText = "Find best fit for current model";
            this.toolStripButtonFit.Click += new System.EventHandler(this.btnCalcBestFit_Click);
            // 
            // toolStripModelSeparator
            // 
            this.toolStripModelSeparator.Name = "toolStripModelSeparator";
            this.toolStripModelSeparator.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButtonZoomOut
            // 
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomOut.Image")));
            this.toolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            this.toolStripButtonZoomOut.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonZoomOut.Tag = "K8";
            this.toolStripButtonZoomOut.Text = "Zoom Out / Clear Filter";
            this.toolStripButtonZoomOut.Click += new System.EventHandler(this.toolStripButtonZoomOut_Click);
            // 
            // toolStripButtonReport
            // 
            this.toolStripButtonReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReport.Image")));
            this.toolStripButtonReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReport.Name = "toolStripButtonReport";
            this.toolStripButtonReport.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonReport.Tag = "K5";
            this.toolStripButtonReport.Text = "Generate results report";
            this.toolStripButtonReport.ToolTipText = "Generate results report";
            this.toolStripButtonReport.Click += new System.EventHandler(this.toolStripButtonReport_Click);
            // 
            // toolStripButtonGraphicalReport
            // 
            this.toolStripButtonGraphicalReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGraphicalReport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGraphicalReport.Image")));
            this.toolStripButtonGraphicalReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGraphicalReport.Name = "toolStripButtonGraphicalReport";
            this.toolStripButtonGraphicalReport.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonGraphicalReport.Tag = "K5";
            this.toolStripButtonGraphicalReport.Text = "toolStripButtonGraphicalReport";
            this.toolStripButtonGraphicalReport.ToolTipText = "Generate graphical report";
            this.toolStripButtonGraphicalReport.Click += new System.EventHandler(this.toolStripButtonGraphicalReport_Click);
            // 
            // toolStripHelpSeparator
            // 
            this.toolStripHelpSeparator.Name = "toolStripHelpSeparator";
            this.toolStripHelpSeparator.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHelp.Image")));
            this.toolStripButtonHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Size = new System.Drawing.Size(40, 47);
            this.toolStripButtonHelp.Text = "Help";
            this.toolStripButtonHelp.ToolTipText = "Help";
            this.toolStripButtonHelp.Click += new System.EventHandler(this.toolStripButtonHelp_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1081, 667);
            this.panelMain.TabIndex = 2;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPageData);
            this.tabMain.Controls.Add(this.tabPagePlot);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(2);
            this.tabMain.MinimumSize = new System.Drawing.Size(700, 500);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1081, 667);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabStop = false;
            this.tabMain.Tag = "";
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.dataGridView);
            this.tabPageData.Location = new System.Drawing.Point(4, 22);
            this.tabPageData.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageData.Size = new System.Drawing.Size(1073, 641);
            this.tabPageData.TabIndex = 0;
            this.tabPageData.Text = "Data";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ContextMenuStrip = this.contextMenuDataGrid;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.NullValue = "0";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(2, 2);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(1069, 637);
            this.dataGridView.StandardTab = true;
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting);
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError_1);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // tabPagePlot
            // 
            this.tabPagePlot.Controls.Add(this.panelGraph);
            this.tabPagePlot.Controls.Add(this.panelFunctionParameters);
            this.tabPagePlot.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlot.Margin = new System.Windows.Forms.Padding(2);
            this.tabPagePlot.Name = "tabPagePlot";
            this.tabPagePlot.Padding = new System.Windows.Forms.Padding(2);
            this.tabPagePlot.Size = new System.Drawing.Size(1073, 641);
            this.tabPagePlot.TabIndex = 1;
            this.tabPagePlot.Tag = "K2";
            this.tabPagePlot.Text = "Plot";
            this.tabPagePlot.UseVisualStyleBackColor = true;
            // 
            // panelGraph
            // 
            this.panelGraph.Controls.Add(this.graph);
            this.panelGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraph.Location = new System.Drawing.Point(2, 2);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(2);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(779, 637);
            this.panelGraph.TabIndex = 2;
            // 
            // graph
            // 
            this.graph.AxisLabelsColor = System.Drawing.Color.Black;
            this.graph.AxisTitlesColor = System.Drawing.Color.Black;
            this.graph.AxisTitlesFont = new System.Drawing.Font("Arial", 10F);
            this.graph.BackColor = System.Drawing.Color.WhiteSmoke;
            this.graph.ContextMenuStrip = this.contextMenuPlot;
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.Frame = true;
            this.graph.FrameColor = System.Drawing.Color.Black;
            this.graph.FrameThickness = ((uint)(1u));
            this.graph.GridColor = System.Drawing.Color.Black;
            this.graph.GridDashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.graph.GridLines = true;
            this.graph.GridThickness = ((uint)(1u));
            this.graph.InternalMargins = 10;
            this.graph.Location = new System.Drawing.Point(0, 0);
            this.graph.Margin = new System.Windows.Forms.Padding(2);
            this.graph.MinPlotHeigth = 80;
            this.graph.MinPlotWidth = 80;
            this.graph.MinSelectionSize = new System.Drawing.Size(5, 5);
            this.graph.Name = "graph";
            this.graph.PlotBkColor = System.Drawing.Color.White;
            this.graph.SelctionLineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.graph.SelectionColor = System.Drawing.Color.Black;
            this.graph.SelectionEnd = ((System.Drawing.PointF)(resources.GetObject("graph.SelectionEnd")));
            this.graph.SelectionPenWidth = 1;
            this.graph.SelectionStart = ((System.Drawing.PointF)(resources.GetObject("graph.SelectionStart")));
            this.graph.ShowTitle = false;
            this.graph.ShowXAxisLabels = true;
            this.graph.ShowYAxisLabels = true;
            this.graph.Size = new System.Drawing.Size(779, 637);
            this.graph.SubTitle = "";
            this.graph.SubTitleFont = new System.Drawing.Font("Arial", 10F);
            this.graph.TabIndex = 1;
            this.graph.TabStop = false;
            this.graph.Title = "";
            this.graph.TitleFont = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Italic);
            this.graph.XAxisLabelsDigits = 0;
            this.graph.XAxisLabelsFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graph.XAxisLabelsLength = 3;
            this.graph.XAxisLabelsMax = 0F;
            this.graph.XAxisLabelsMin = 0F;
            this.graph.XAxisLabelsPow = 0;
            this.graph.XAxisLabelsStep = 0F;
            this.graph.XAxisText = "";
            this.graph.YAxisLabelsDigits = 0;
            this.graph.YAxisLabelsFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graph.YAxisLabelsLength = 3;
            this.graph.YAxisLabelsMax = 0F;
            this.graph.YAxisLabelsMin = 0F;
            this.graph.YAxisLabelsPow = 0;
            this.graph.YAxisLabelsStep = 0F;
            this.graph.YAxisText = "";
            this.graph.YAxisTextVertical = false;
            // 
            // contextMenuPlot
            // 
            this.contextMenuPlot.Enabled = false;
            this.contextMenuPlot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plotMenuCopy,
            this.plotMenuSave});
            this.contextMenuPlot.Name = "contextMenuPlot";
            this.contextMenuPlot.Size = new System.Drawing.Size(103, 48);
            // 
            // plotMenuCopy
            // 
            this.plotMenuCopy.Enabled = false;
            this.plotMenuCopy.Name = "plotMenuCopy";
            this.plotMenuCopy.Size = new System.Drawing.Size(102, 22);
            this.plotMenuCopy.Text = "Copy";
            this.plotMenuCopy.Click += new System.EventHandler(this.plotMenuCopy_Click);
            // 
            // plotMenuSave
            // 
            this.plotMenuSave.Enabled = false;
            this.plotMenuSave.Name = "plotMenuSave";
            this.plotMenuSave.Size = new System.Drawing.Size(102, 22);
            this.plotMenuSave.Text = "Save";
            this.plotMenuSave.Click += new System.EventHandler(this.plotMenuSave_Click);
            // 
            // panelFunctionParameters
            // 
            this.panelFunctionParameters.Controls.Add(this.panelModelParameters);
            this.panelFunctionParameters.Controls.Add(this.panelModels);
            this.panelFunctionParameters.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelFunctionParameters.Location = new System.Drawing.Point(781, 2);
            this.panelFunctionParameters.Margin = new System.Windows.Forms.Padding(2);
            this.panelFunctionParameters.Name = "panelFunctionParameters";
            this.panelFunctionParameters.Size = new System.Drawing.Size(290, 637);
            this.panelFunctionParameters.TabIndex = 1;
            // 
            // panelModelParameters
            // 
            this.panelModelParameters.Controls.Add(this.btnStop);
            this.panelModelParameters.Controls.Add(this.btnInitParameters);
            this.panelModelParameters.Controls.Add(this.lblLastModel);
            this.panelModelParameters.Controls.Add(this.label2);
            this.panelModelParameters.Controls.Add(this.lblStdError);
            this.panelModelParameters.Controls.Add(this.btnPlot);
            this.panelModelParameters.Controls.Add(this.label1);
            this.panelModelParameters.Controls.Add(this.btnCalcBestFit);
            this.panelModelParameters.Controls.Add(this.dataGridParameters);
            this.panelModelParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModelParameters.Location = new System.Drawing.Point(0, 362);
            this.panelModelParameters.Name = "panelModelParameters";
            this.panelModelParameters.Size = new System.Drawing.Size(290, 275);
            this.panelModelParameters.TabIndex = 11;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(203, 172);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(56, 25);
            this.btnStop.TabIndex = 3;
            this.btnStop.Tag = "K7";
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnInitParameters
            // 
            this.btnInitParameters.Enabled = false;
            this.btnInitParameters.Location = new System.Drawing.Point(28, 216);
            this.btnInitParameters.Margin = new System.Windows.Forms.Padding(2);
            this.btnInitParameters.Name = "btnInitParameters";
            this.btnInitParameters.Size = new System.Drawing.Size(112, 25);
            this.btnInitParameters.TabIndex = 0;
            this.btnInitParameters.Tag = "K5";
            this.btnInitParameters.Text = "Init parameters";
            this.btnInitParameters.UseVisualStyleBackColor = true;
            this.btnInitParameters.Click += new System.EventHandler(this.btnInitParameters_Click);
            // 
            // lblLastModel
            // 
            this.lblLastModel.AutoSize = true;
            this.lblLastModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblLastModel.Location = new System.Drawing.Point(68, 156);
            this.lblLastModel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastModel.Name = "lblLastModel";
            this.lblLastModel.Size = new System.Drawing.Size(0, 13);
            this.lblLastModel.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 156);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Last Model:";
            // 
            // lblStdError
            // 
            this.lblStdError.AutoSize = true;
            this.lblStdError.Location = new System.Drawing.Point(68, 178);
            this.lblStdError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStdError.Name = "lblStdError";
            this.lblStdError.Size = new System.Drawing.Size(0, 13);
            this.lblStdError.TabIndex = 13;
            // 
            // btnPlot
            // 
            this.btnPlot.Enabled = false;
            this.btnPlot.Location = new System.Drawing.Point(144, 216);
            this.btnPlot.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(55, 25);
            this.btnPlot.TabIndex = 1;
            this.btnPlot.Tag = "K5";
            this.btnPlot.Text = "Plot";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 178);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Std. Error:";
            // 
            // btnCalcBestFit
            // 
            this.btnCalcBestFit.Enabled = false;
            this.btnCalcBestFit.Location = new System.Drawing.Point(203, 216);
            this.btnCalcBestFit.Margin = new System.Windows.Forms.Padding(2);
            this.btnCalcBestFit.Name = "btnCalcBestFit";
            this.btnCalcBestFit.Size = new System.Drawing.Size(56, 25);
            this.btnCalcBestFit.TabIndex = 2;
            this.btnCalcBestFit.Tag = "K5";
            this.btnCalcBestFit.Text = "Solve";
            this.btnCalcBestFit.UseVisualStyleBackColor = true;
            this.btnCalcBestFit.Click += new System.EventHandler(this.btnCalcBestFit_Click);
            // 
            // dataGridParameters
            // 
            this.dataGridParameters.AllowUserToAddRows = false;
            this.dataGridParameters.AllowUserToDeleteRows = false;
            this.dataGridParameters.AllowUserToResizeColumns = false;
            this.dataGridParameters.AllowUserToResizeRows = false;
            this.dataGridParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.Vary,
            this.Value,
            this.ParameterSigma});
            this.dataGridParameters.ContextMenuStrip = this.contextMenuParametersGrid;
            this.dataGridParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridParameters.Location = new System.Drawing.Point(0, 0);
            this.dataGridParameters.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridParameters.Name = "dataGridParameters";
            this.dataGridParameters.RowHeadersVisible = false;
            this.dataGridParameters.RowTemplate.Height = 24;
            this.dataGridParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.helpProvider.SetShowHelp(this.dataGridParameters, false);
            this.dataGridParameters.Size = new System.Drawing.Size(290, 137);
            this.dataGridParameters.TabIndex = 5;
            this.dataGridParameters.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridParameters_CellValueChanged);
            this.dataGridParameters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridParameters_KeyDown);
            this.dataGridParameters.Leave += new System.EventHandler(this.dataGridParameters_Leave);
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Name.DefaultCellStyle = dataGridViewCellStyle2;
            this.Name.HeaderText = "Parameter";
            this.Name.MinimumWidth = 30;
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            this.Name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Name.Width = 80;
            // 
            // Vary
            // 
            this.Vary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.NullValue = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Vary.DefaultCellStyle = dataGridViewCellStyle3;
            this.Vary.HeaderText = "Vary";
            this.Vary.MinimumWidth = 20;
            this.Vary.Name = "Vary";
            this.Vary.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Vary.Width = 34;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Value.DefaultCellStyle = dataGridViewCellStyle4;
            this.Value.HeaderText = "Value";
            this.Value.MaxInputLength = 25;
            this.Value.MinimumWidth = 40;
            this.Value.Name = "Value";
            this.Value.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Value.Width = 59;
            // 
            // ParameterSigma
            // 
            this.ParameterSigma.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ParameterSigma.DefaultCellStyle = dataGridViewCellStyle5;
            this.ParameterSigma.HeaderText = "Sigma";
            this.ParameterSigma.Name = "ParameterSigma";
            this.ParameterSigma.ReadOnly = true;
            this.ParameterSigma.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // contextMenuParametersGrid
            // 
            this.contextMenuParametersGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuCopyValue,
            this.toolStripMenuCopyParameters,
            this.logResultToolStripMenuItem,
            this.derivedValuesPopToolStripMenuItem});
            this.contextMenuParametersGrid.Name = "contextMenuParametersGrid";
            this.contextMenuParametersGrid.Size = new System.Drawing.Size(165, 92);
            // 
            // toolStripMenuCopyValue
            // 
            this.toolStripMenuCopyValue.Name = "toolStripMenuCopyValue";
            this.toolStripMenuCopyValue.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuCopyValue.Tag = "K5";
            this.toolStripMenuCopyValue.Text = "Copy Value";
            this.toolStripMenuCopyValue.Click += new System.EventHandler(this.toolStripMenuCopyValue_Click);
            // 
            // toolStripMenuCopyParameters
            // 
            this.toolStripMenuCopyParameters.Name = "toolStripMenuCopyParameters";
            this.toolStripMenuCopyParameters.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuCopyParameters.Tag = "K5";
            this.toolStripMenuCopyParameters.Text = "Copy Parameters";
            this.toolStripMenuCopyParameters.Click += new System.EventHandler(this.toolStripMenuCopyParameters_Click);
            // 
            // logResultToolStripMenuItem
            // 
            this.logResultToolStripMenuItem.Name = "logResultToolStripMenuItem";
            this.logResultToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.logResultToolStripMenuItem.Tag = "K5";
            this.logResultToolStripMenuItem.Text = "Log Result";
            this.logResultToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuAddToLog_Click);
            // 
            // derivedValuesPopToolStripMenuItem
            // 
            this.derivedValuesPopToolStripMenuItem.Name = "derivedValuesPopToolStripMenuItem";
            this.derivedValuesPopToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.derivedValuesPopToolStripMenuItem.Tag = "K5";
            this.derivedValuesPopToolStripMenuItem.Text = "Derived values";
            this.derivedValuesPopToolStripMenuItem.Click += new System.EventHandler(this.derivedValuesToolStripMenuItem_Click);
            // 
            // panelModels
            // 
            this.panelModels.Controls.Add(this.pictureBoxModelPreview);
            this.panelModels.Controls.Add(this.dataGridModels);
            this.panelModels.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelModels.Location = new System.Drawing.Point(0, 0);
            this.panelModels.Name = "panelModels";
            this.panelModels.Size = new System.Drawing.Size(290, 362);
            this.panelModels.TabIndex = 10;
            // 
            // pictureBoxModelPreview
            // 
            this.pictureBoxModelPreview.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBoxModelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxModelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxModelPreview.Location = new System.Drawing.Point(0, 245);
            this.pictureBoxModelPreview.Name = "pictureBoxModelPreview";
            this.pictureBoxModelPreview.Size = new System.Drawing.Size(290, 117);
            this.pictureBoxModelPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxModelPreview.TabIndex = 13;
            this.pictureBoxModelPreview.TabStop = false;
            this.pictureBoxModelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxModelPreview_Paint);
            // 
            // dataGridModels
            // 
            this.dataGridModels.AllowUserToAddRows = false;
            this.dataGridModels.AllowUserToDeleteRows = false;
            this.dataGridModels.AllowUserToResizeColumns = false;
            this.dataGridModels.AllowUserToResizeRows = false;
            this.dataGridModels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridModels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ModelName,
            this.ModelFitStdError});
            this.dataGridModels.ContextMenuStrip = this.contextMenuModelsGrid;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridModels.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridModels.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridModels.Location = new System.Drawing.Point(0, 0);
            this.dataGridModels.Name = "dataGridModels";
            this.dataGridModels.RowHeadersVisible = false;
            this.dataGridModels.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridModels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridModels.Size = new System.Drawing.Size(290, 245);
            this.dataGridModels.TabIndex = 4;
            this.dataGridModels.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridModels_CellDoubleClick);
            this.dataGridModels.SelectionChanged += new System.EventHandler(this.dataGridModels_SelectionChanged);
            this.dataGridModels.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridModels_SortCompare);
            this.dataGridModels.Enter += new System.EventHandler(this.dataGridModels_Enter);
            this.dataGridModels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridModels_KeyDown);
            this.dataGridModels.Leave += new System.EventHandler(this.dataGridModels_Leave);
            // 
            // ModelName
            // 
            this.ModelName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ModelName.DefaultCellStyle = dataGridViewCellStyle6;
            this.ModelName.HeaderText = "Name";
            this.ModelName.Name = "ModelName";
            this.ModelName.ReadOnly = true;
            this.ModelName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ModelFitStdError
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ModelFitStdError.DefaultCellStyle = dataGridViewCellStyle7;
            this.ModelFitStdError.HeaderText = "Std. Error";
            this.ModelFitStdError.Name = "ModelFitStdError";
            this.ModelFitStdError.ReadOnly = true;
            this.ModelFitStdError.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // contextMenuModelsGrid
            // 
            this.contextMenuModelsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuRemoveModel,
            this.toolStripMenuRemoveAllModels,
            this.toolStripMenuCopy,
            this.toolStripMenuReInit});
            this.contextMenuModelsGrid.Name = "contextMenuModelsGrid";
            this.contextMenuModelsGrid.Size = new System.Drawing.Size(135, 92);
            // 
            // toolStripMenuRemoveModel
            // 
            this.toolStripMenuRemoveModel.Name = "toolStripMenuRemoveModel";
            this.toolStripMenuRemoveModel.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuRemoveModel.Tag = "K5";
            this.toolStripMenuRemoveModel.Text = "Remove";
            this.toolStripMenuRemoveModel.Click += new System.EventHandler(this.toolStripMenuRemoveModel_Click);
            // 
            // toolStripMenuRemoveAllModels
            // 
            this.toolStripMenuRemoveAllModels.Name = "toolStripMenuRemoveAllModels";
            this.toolStripMenuRemoveAllModels.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuRemoveAllModels.Tag = "K5";
            this.toolStripMenuRemoveAllModels.Text = "Remove All";
            this.toolStripMenuRemoveAllModels.Click += new System.EventHandler(this.toolStripMenuRemoveAllModels_Click);
            // 
            // toolStripMenuCopy
            // 
            this.toolStripMenuCopy.Name = "toolStripMenuCopy";
            this.toolStripMenuCopy.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuCopy.Tag = "K5";
            this.toolStripMenuCopy.Text = "Copy";
            this.toolStripMenuCopy.Click += new System.EventHandler(this.toolStripMenuCopy_Click);
            // 
            // toolStripMenuReInit
            // 
            this.toolStripMenuReInit.Name = "toolStripMenuReInit";
            this.toolStripMenuReInit.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuReInit.Tag = "K5";
            this.toolStripMenuReInit.Text = "Initialize all";
            this.toolStripMenuReInit.Click += new System.EventHandler(this.toolStripMenuReInit_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // savePlotImageFileDialog
            // 
            this.savePlotImageFileDialog.Filter = "Bitmap|*.bmp; JPEG|*.jpg; Image|*.png";
            // 
            // saveProjectDialog
            // 
            this.saveProjectDialog.Filter = "Project| *.prj";
            // 
            // openProjectDialog
            // 
            this.openProjectDialog.Filter = "Project | *.prj";
            // 
            // helpProvider
            // 
            this.helpProvider.HelpNamespace = "C:\\Users\\ANDREY\\Documents\\Visual Studio 2013\\Projects\\LybraFit\\Help\\LybraFitHelp." +
    "chm";
            // 
            // timer
            // 
            this.timer.Interval = 2000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 745);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1097, 784);
            this.Name = "MainForm";
            this.Text = "LybraFit";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.contextMenuDataGrid.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabPagePlot.ResumeLayout(false);
            this.panelGraph.ResumeLayout(false);
            this.contextMenuPlot.ResumeLayout(false);
            this.panelFunctionParameters.ResumeLayout(false);
            this.panelModelParameters.ResumeLayout(false);
            this.panelModelParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParameters)).EndInit();
            this.contextMenuParametersGrid.ResumeLayout(false);
            this.panelModels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxModelPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridModels)).EndInit();
            this.contextMenuModelsGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileImportASCII;
        private System.Windows.Forms.ToolStripMenuItem dataMenu;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importASCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuDataGrid;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripPasteInsert;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuPaste;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatPlotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatColumnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem modelsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TabPage tabPagePlot;
        private System.Windows.Forms.Panel panelGraph;
        private GraphControlLib.GraphControl graph;
        private System.Windows.Forms.Panel panelFunctionParameters;
        private System.Windows.Forms.Panel panelModelParameters;
        private StylishButton btnPlot;
        private System.Windows.Forms.Label label1;
        private StylishButton btnCalcBestFit;
        private System.Windows.Forms.DataGridView dataGridParameters;
        private System.Windows.Forms.Panel panelModels;
        private System.Windows.Forms.DataGridView dataGridModels;
        private System.Windows.Forms.Label lblStdError;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuViewHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAbout;
        private System.Windows.Forms.ToolStripMenuItem exportASCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCalculateFit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddAllModels;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewData;
        private System.Windows.Forms.ToolStripButton toolStripButtonImport;
        private System.Windows.Forms.ToolStripButton toolStripButtonExport;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonPasteAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonPaste;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteRows;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripButton toolStripButtonColumnSettings;
        private System.Windows.Forms.ToolStripButton toolStripButtonGraphSettings;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlot;
        private System.Windows.Forms.ToolStripButton toolStripButtonFit;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddAllModels;
        private System.Windows.Forms.ToolStripButton toolStripButtonReport;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddModel;
        private System.Windows.Forms.Label lblLastModel;
        private System.Windows.Forms.Label label2;
        private StylishButton btnInitParameters;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyStripMenuItem;
        private LybraFit.StylishButton btnStop;
        private System.Windows.Forms.ContextMenuStrip contextMenuModelsGrid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuRemoveModel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCopy;
        private System.Windows.Forms.ContextMenuStrip contextMenuParametersGrid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCopyValue;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCopyParameters;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuReInit;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ContextMenuStrip contextMenuPlot;
        private System.Windows.Forms.ToolStripMenuItem plotMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem plotMenuSave;
        private System.Windows.Forms.SaveFileDialog savePlotImageFileDialog;
        private System.Windows.Forms.SaveFileDialog saveProjectDialog;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openProjectDialog;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuRemoveAllModels;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuUpdateStdErrors;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tableViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphViewToolStripMenuItem;
        private System.Windows.Forms.HelpProvider helpProvider;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInverseTransform;
        private System.Windows.Forms.PictureBox pictureBoxModelPreview;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelFitStdError;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Vary;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterSigma;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenProject;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveProject;
        private System.Windows.Forms.ToolStripMenuItem derivedValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAddToLog;
        private System.Windows.Forms.ToolStripMenuItem logResultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuReprots;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuTextReport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuGraphicalReport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuLogReport;
        private System.Windows.Forms.ToolStripMenuItem derivedValuesPopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuRecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRecentProjects;
        private System.Windows.Forms.ToolStripSeparator toolStripProjectSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripDataSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSettingsSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripModelSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripHelpSeparator;
        private System.Windows.Forms.ToolStripButton toolStripButtonGraphicalReport;
    }
}

