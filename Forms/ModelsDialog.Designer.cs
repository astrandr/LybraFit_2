using DataFit.Common;
namespace DataFit.Forms
{
    partial class ModelsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelsDialog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new DataFit.StylishButton();
            this.btnOk = new DataFit.StylishButton();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.grpFunctionPreview = new System.Windows.Forms.GroupBox();
            this.pnlMathPreview = new System.Windows.Forms.Panel();
            this.pictFunctionPreview = new System.Windows.Forms.PictureBox();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pictureSearch = new System.Windows.Forms.PictureBox();
            this.txtModelFilter = new System.Windows.Forms.TextBox();
            this.pnlModels = new System.Windows.Forms.Panel();
            this.tabCtrlModels = new System.Windows.Forms.TabControl();
            this.tabPageLibraryModels = new System.Windows.Forms.TabPage();
            this.dataGridFitFunctions = new System.Windows.Forms.DataGridView();
            this.Checked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LibName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageCustomModels = new System.Windows.Forms.TabPage();
            this.pnlCustomFunctionsGrid = new System.Windows.Forms.Panel();
            this.dataGridCustomFunctions = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MathExpression = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEditUserFunction = new DataFit.StylishButton();
            this.btnNewUserFunction = new DataFit.StylishButton();
            this.btnRemoveUserFunction = new DataFit.StylishButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlButtons.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.grpFunctionPreview.SuspendLayout();
            this.pnlMathPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictFunctionPreview)).BeginInit();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSearch)).BeginInit();
            this.pnlModels.SuspendLayout();
            this.tabCtrlModels.SuspendLayout();
            this.tabPageLibraryModels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFitFunctions)).BeginInit();
            this.tabPageCustomModels.SuspendLayout();
            this.pnlCustomFunctionsGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCustomFunctions)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 487);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(473, 49);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(128, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(268, 14);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // pnlPreview
            // 
            this.pnlPreview.Controls.Add(this.grpFunctionPreview);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPreview.Location = new System.Drawing.Point(0, 326);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(473, 161);
            this.pnlPreview.TabIndex = 1;
            // 
            // grpFunctionPreview
            // 
            this.grpFunctionPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.grpFunctionPreview.Controls.Add(this.pnlMathPreview);
            this.grpFunctionPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFunctionPreview.Location = new System.Drawing.Point(0, 0);
            this.grpFunctionPreview.Name = "grpFunctionPreview";
            this.grpFunctionPreview.Size = new System.Drawing.Size(473, 161);
            this.grpFunctionPreview.TabIndex = 2;
            this.grpFunctionPreview.TabStop = false;
            // 
            // pnlMathPreview
            // 
            this.pnlMathPreview.Controls.Add(this.pictFunctionPreview);
            this.pnlMathPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMathPreview.Location = new System.Drawing.Point(3, 16);
            this.pnlMathPreview.Name = "pnlMathPreview";
            this.pnlMathPreview.Size = new System.Drawing.Size(467, 142);
            this.pnlMathPreview.TabIndex = 0;
            // 
            // pictFunctionPreview
            // 
            this.pictFunctionPreview.BackColor = System.Drawing.SystemColors.Info;
            this.pictFunctionPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictFunctionPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictFunctionPreview.Location = new System.Drawing.Point(0, 0);
            this.pictFunctionPreview.Name = "pictFunctionPreview";
            this.pictFunctionPreview.Size = new System.Drawing.Size(467, 142);
            this.pictFunctionPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictFunctionPreview.TabIndex = 1;
            this.pictFunctionPreview.TabStop = false;
            this.pictFunctionPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictFunctionPreview_Paint);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlSearch.Controls.Add(this.pictureSearch);
            this.pnlSearch.Controls.Add(this.txtModelFilter);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(473, 37);
            this.pnlSearch.TabIndex = 0;
            // 
            // pictureSearch
            // 
            this.pictureSearch.Image = ((System.Drawing.Image)(resources.GetObject("pictureSearch.Image")));
            this.pictureSearch.Location = new System.Drawing.Point(432, 8);
            this.pictureSearch.Name = "pictureSearch";
            this.pictureSearch.Size = new System.Drawing.Size(21, 20);
            this.pictureSearch.TabIndex = 1;
            this.pictureSearch.TabStop = false;
            // 
            // txtModelFilter
            // 
            this.txtModelFilter.Location = new System.Drawing.Point(7, 9);
            this.txtModelFilter.Name = "txtModelFilter";
            this.txtModelFilter.Size = new System.Drawing.Size(417, 20);
            this.txtModelFilter.TabIndex = 0;
            this.txtModelFilter.TextChanged += new System.EventHandler(this.txtModelFilter_TextChanged);
            this.txtModelFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModelFilter_KeyDown);
            // 
            // pnlModels
            // 
            this.pnlModels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlModels.Controls.Add(this.tabCtrlModels);
            this.pnlModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlModels.Location = new System.Drawing.Point(0, 37);
            this.pnlModels.Name = "pnlModels";
            this.pnlModels.Size = new System.Drawing.Size(473, 289);
            this.pnlModels.TabIndex = 0;
            // 
            // tabCtrlModels
            // 
            this.tabCtrlModels.Controls.Add(this.tabPageLibraryModels);
            this.tabCtrlModels.Controls.Add(this.tabPageCustomModels);
            this.tabCtrlModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlModels.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlModels.Name = "tabCtrlModels";
            this.tabCtrlModels.SelectedIndex = 0;
            this.tabCtrlModels.Size = new System.Drawing.Size(473, 289);
            this.tabCtrlModels.TabIndex = 1;
            this.tabCtrlModels.SelectedIndexChanged += new System.EventHandler(this.tabCtrlModels_SelectedIndexChanged);
            // 
            // tabPageLibraryModels
            // 
            this.tabPageLibraryModels.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageLibraryModels.Controls.Add(this.dataGridFitFunctions);
            this.tabPageLibraryModels.Location = new System.Drawing.Point(4, 22);
            this.tabPageLibraryModels.Name = "tabPageLibraryModels";
            this.tabPageLibraryModels.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLibraryModels.Size = new System.Drawing.Size(465, 263);
            this.tabPageLibraryModels.TabIndex = 0;
            this.tabPageLibraryModels.Text = "Library";
            // 
            // dataGridFitFunctions
            // 
            this.dataGridFitFunctions.AllowUserToAddRows = false;
            this.dataGridFitFunctions.AllowUserToDeleteRows = false;
            this.dataGridFitFunctions.AllowUserToResizeRows = false;
            this.dataGridFitFunctions.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridFitFunctions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFitFunctions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checked,
            this.LibName,
            this.Description});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridFitFunctions.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridFitFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridFitFunctions.Location = new System.Drawing.Point(3, 3);
            this.dataGridFitFunctions.MultiSelect = false;
            this.dataGridFitFunctions.Name = "dataGridFitFunctions";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridFitFunctions.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridFitFunctions.RowHeadersVisible = false;
            this.dataGridFitFunctions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dataGridFitFunctions.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridFitFunctions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridFitFunctions.Size = new System.Drawing.Size(459, 257);
            this.dataGridFitFunctions.TabIndex = 0;
            this.dataGridFitFunctions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridFitFunctions_DataError);
            this.dataGridFitFunctions.SelectionChanged += new System.EventHandler(this.dataGridFitFunctions_SelectionChanged);
            this.dataGridFitFunctions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridFitFunctions_KeyDown);
            // 
            // Checked
            // 
            this.Checked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Checked.HeaderText = "";
            this.Checked.Name = "Checked";
            this.Checked.Width = 5;
            // 
            // LibName
            // 
            this.LibName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            this.LibName.DefaultCellStyle = dataGridViewCellStyle1;
            this.LibName.HeaderText = "Name";
            this.LibName.Name = "LibName";
            this.LibName.ReadOnly = true;
            this.LibName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LibName.Width = 60;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Description.DefaultCellStyle = dataGridViewCellStyle2;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tabPageCustomModels
            // 
            this.tabPageCustomModels.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCustomModels.Controls.Add(this.pnlCustomFunctionsGrid);
            this.tabPageCustomModels.Controls.Add(this.panel1);
            this.tabPageCustomModels.Location = new System.Drawing.Point(4, 22);
            this.tabPageCustomModels.Name = "tabPageCustomModels";
            this.tabPageCustomModels.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCustomModels.Size = new System.Drawing.Size(465, 263);
            this.tabPageCustomModels.TabIndex = 1;
            this.tabPageCustomModels.Text = "Custom";
            // 
            // pnlCustomFunctionsGrid
            // 
            this.pnlCustomFunctionsGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlCustomFunctionsGrid.Controls.Add(this.dataGridCustomFunctions);
            this.pnlCustomFunctionsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomFunctionsGrid.Location = new System.Drawing.Point(3, 3);
            this.pnlCustomFunctionsGrid.Name = "pnlCustomFunctionsGrid";
            this.pnlCustomFunctionsGrid.Size = new System.Drawing.Size(459, 230);
            this.pnlCustomFunctionsGrid.TabIndex = 3;
            // 
            // dataGridCustomFunctions
            // 
            this.dataGridCustomFunctions.AllowUserToAddRows = false;
            this.dataGridCustomFunctions.AllowUserToDeleteRows = false;
            this.dataGridCustomFunctions.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridCustomFunctions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCustomFunctions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.Name,
            this.MathExpression});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridCustomFunctions.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridCustomFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridCustomFunctions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridCustomFunctions.Location = new System.Drawing.Point(0, 0);
            this.dataGridCustomFunctions.MultiSelect = false;
            this.dataGridCustomFunctions.Name = "dataGridCustomFunctions";
            this.dataGridCustomFunctions.RowHeadersVisible = false;
            this.dataGridCustomFunctions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridCustomFunctions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridCustomFunctions.Size = new System.Drawing.Size(459, 230);
            this.dataGridCustomFunctions.TabIndex = 0;
            this.dataGridCustomFunctions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridCustomFunctions_CellValueChanged);
            this.dataGridCustomFunctions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridCustomFunctions_DataError);
            this.dataGridCustomFunctions.SelectionChanged += new System.EventHandler(this.dataGridCustomFunctions_SelectionChanged);
            this.dataGridCustomFunctions.Enter += new System.EventHandler(this.dataGridCustomFunctions_Enter);
            this.dataGridCustomFunctions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridCustomFunctions_KeyDown);
            this.dataGridCustomFunctions.Leave += new System.EventHandler(this.dataGridCustomFunctions_Leave);
            // 
            // Selected
            // 
            this.Selected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Selected.HeaderText = "";
            this.Selected.Name = "Selected";
            this.Selected.Width = 5;
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Info;
            this.Name.DefaultCellStyle = dataGridViewCellStyle6;
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.Width = 60;
            // 
            // MathExpression
            // 
            this.MathExpression.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.MathExpression.DefaultCellStyle = dataGridViewCellStyle7;
            this.MathExpression.HeaderText = "Math Expression";
            this.MathExpression.Name = "MathExpression";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panel1.Controls.Add(this.btnEditUserFunction);
            this.panel1.Controls.Add(this.btnNewUserFunction);
            this.panel1.Controls.Add(this.btnRemoveUserFunction);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 27);
            this.panel1.TabIndex = 1;
            // 
            // btnEditUserFunction
            // 
            this.btnEditUserFunction.Location = new System.Drawing.Point(102, 3);
            this.btnEditUserFunction.Name = "btnEditUserFunction";
            this.btnEditUserFunction.Size = new System.Drawing.Size(75, 20);
            this.btnEditUserFunction.TabIndex = 1;
            this.btnEditUserFunction.Text = "Edit";
            this.btnEditUserFunction.UseVisualStyleBackColor = true;
            this.btnEditUserFunction.Click += new System.EventHandler(this.btnEditUserFunction_Click);
            // 
            // btnNewUserFunction
            // 
            this.btnNewUserFunction.Location = new System.Drawing.Point(187, 3);
            this.btnNewUserFunction.Name = "btnNewUserFunction";
            this.btnNewUserFunction.Size = new System.Drawing.Size(75, 20);
            this.btnNewUserFunction.TabIndex = 2;
            this.btnNewUserFunction.Text = "New";
            this.btnNewUserFunction.UseVisualStyleBackColor = true;
            this.btnNewUserFunction.Click += new System.EventHandler(this.btnNewUserFunction_Click);
            // 
            // btnRemoveUserFunction
            // 
            this.btnRemoveUserFunction.Enabled = false;
            this.btnRemoveUserFunction.Location = new System.Drawing.Point(272, 3);
            this.btnRemoveUserFunction.Name = "btnRemoveUserFunction";
            this.btnRemoveUserFunction.Size = new System.Drawing.Size(75, 20);
            this.btnRemoveUserFunction.TabIndex = 3;
            this.btnRemoveUserFunction.Text = "Remove";
            this.btnRemoveUserFunction.UseVisualStyleBackColor = true;
            this.btnRemoveUserFunction.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ModelsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 536);
            this.Controls.Add(this.pnlModels);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlPreview);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;

            this.Text = "Models";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModelsDialog_FormClosing);
            this.Load += new System.EventHandler(this.ModelsDialog_Load);
            this.Shown += new System.EventHandler(this.ModelsDialog_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModelsDialog_KeyDown);
            this.pnlButtons.ResumeLayout(false);
            this.pnlPreview.ResumeLayout(false);
            this.grpFunctionPreview.ResumeLayout(false);
            this.pnlMathPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictFunctionPreview)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSearch)).EndInit();
            this.pnlModels.ResumeLayout(false);
            this.tabCtrlModels.ResumeLayout(false);
            this.tabPageLibraryModels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFitFunctions)).EndInit();
            this.tabPageCustomModels.ResumeLayout(false);
            this.pnlCustomFunctionsGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCustomFunctions)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtModelFilter;
        private System.Windows.Forms.Panel pnlModels;
        private System.Windows.Forms.TabControl tabCtrlModels;
        private System.Windows.Forms.TabPage tabPageLibraryModels;
        private System.Windows.Forms.DataGridView dataGridFitFunctions;
        private System.Windows.Forms.TabPage tabPageCustomModels;
        private StylishButton btnOk;
        private StylishButton btnNewUserFunction;
        private System.Windows.Forms.GroupBox grpFunctionPreview;
        private StylishButton btnCancel;
        private System.Windows.Forms.Panel panel1;
        private StylishButton btnRemoveUserFunction;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel pnlCustomFunctionsGrid;
        private System.Windows.Forms.DataGridView dataGridCustomFunctions;
        private System.Windows.Forms.Panel pnlMathPreview;
        private System.Windows.Forms.PictureBox pictFunctionPreview;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checked;
        private System.Windows.Forms.DataGridViewTextBoxColumn LibName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn MathExpression;
        private StylishButton btnEditUserFunction;
        private System.Windows.Forms.PictureBox pictureSearch;
    }
}