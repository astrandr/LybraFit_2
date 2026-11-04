namespace LybraFit.Forms
{
    partial class ExportDialog
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
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new LybraFit.StylishButton();
            this.btnOk = new LybraFit.StylishButton();
            this.pnlFile = new System.Windows.Forms.Panel();
            this.btnBrowse = new LybraFit.StylishButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.chkColumns = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDelimiters = new System.Windows.Forms.ComboBox();
            this.chkDifference = new System.Windows.Forms.CheckBox();
            this.chkModel = new System.Windows.Forms.CheckBox();
            this.chkErrors = new System.Windows.Forms.CheckBox();
            this.chkFormat = new System.Windows.Forms.CheckBox();
            this.chkHeader = new System.Windows.Forms.CheckBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.pnlButtons.SuspendLayout();
            this.pnlFile.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 330);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(584, 50);
            this.pnlButtons.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(193, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(319, 15);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // pnlFile
            // 
            this.pnlFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlFile.Controls.Add(this.btnBrowse);
            this.pnlFile.Controls.Add(this.label2);
            this.pnlFile.Controls.Add(this.txtOutputFile);
            this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFile.Location = new System.Drawing.Point(0, 287);
            this.pnlFile.Name = "pnlFile";
            this.pnlFile.Size = new System.Drawing.Size(584, 43);
            this.pnlFile.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(513, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(58, 23);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output file:";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFile.Location = new System.Drawing.Point(74, 11);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(430, 21);
            this.txtOutputFile.TabIndex = 7;
            // 
            // pnlOptions
            // 
            this.pnlOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlOptions.Controls.Add(this.chkColumns);
            this.pnlOptions.Controls.Add(this.label1);
            this.pnlOptions.Controls.Add(this.cboDelimiters);
            this.pnlOptions.Controls.Add(this.chkDifference);
            this.pnlOptions.Controls.Add(this.chkModel);
            this.pnlOptions.Controls.Add(this.chkErrors);
            this.pnlOptions.Controls.Add(this.chkFormat);
            this.pnlOptions.Controls.Add(this.chkHeader);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOptions.Location = new System.Drawing.Point(0, 0);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(584, 58);
            this.pnlOptions.TabIndex = 0;
            // 
            // chkColumns
            // 
            this.chkColumns.AutoSize = true;
            this.chkColumns.Checked = true;
            this.chkColumns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColumns.Location = new System.Drawing.Point(380, 21);
            this.chkColumns.Name = "chkColumns";
            this.chkColumns.Size = new System.Drawing.Size(66, 17);
            this.chkColumns.TabIndex = 5;
            this.chkColumns.Text = "Columns";
            this.chkColumns.UseVisualStyleBackColor = true;
            this.chkColumns.CheckedChanged += new System.EventHandler(this.chkFormat_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(449, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Delimiters:";
            // 
            // cboDelimiters
            // 
            this.cboDelimiters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDelimiters.FormattingEnabled = true;
            this.cboDelimiters.Items.AddRange(new object[] {
            "Tabs",
            "Space",
            ";",
            ":",
            ","});
            this.cboDelimiters.Location = new System.Drawing.Point(507, 19);
            this.cboDelimiters.Name = "cboDelimiters";
            this.cboDelimiters.Size = new System.Drawing.Size(63, 21);
            this.cboDelimiters.TabIndex = 6;
            this.cboDelimiters.SelectedIndexChanged += new System.EventHandler(this.cboDelimiters_SelectedIndexChanged);
            // 
            // chkDifference
            // 
            this.chkDifference.AutoSize = true;
            this.chkDifference.Location = new System.Drawing.Point(295, 21);
            this.chkDifference.Name = "chkDifference";
            this.chkDifference.Size = new System.Drawing.Size(80, 17);
            this.chkDifference.TabIndex = 4;
            this.chkDifference.Text = "Differences";
            this.chkDifference.UseVisualStyleBackColor = true;
            this.chkDifference.CheckedChanged += new System.EventHandler(this.chkFormat_CheckedChanged);
            // 
            // chkModel
            // 
            this.chkModel.AutoSize = true;
            this.chkModel.Location = new System.Drawing.Point(235, 21);
            this.chkModel.Name = "chkModel";
            this.chkModel.Size = new System.Drawing.Size(55, 17);
            this.chkModel.TabIndex = 3;
            this.chkModel.Text = "Model";
            this.chkModel.UseVisualStyleBackColor = true;
            this.chkModel.CheckedChanged += new System.EventHandler(this.chkFormat_CheckedChanged);
            // 
            // chkErrors
            // 
            this.chkErrors.AutoSize = true;
            this.chkErrors.Location = new System.Drawing.Point(157, 21);
            this.chkErrors.Name = "chkErrors";
            this.chkErrors.Size = new System.Drawing.Size(73, 17);
            this.chkErrors.TabIndex = 2;
            this.chkErrors.Text = "Data error";
            this.chkErrors.UseVisualStyleBackColor = true;
            this.chkErrors.CheckedChanged += new System.EventHandler(this.chkFormat_CheckedChanged);
            // 
            // chkFormat
            // 
            this.chkFormat.AutoSize = true;
            this.chkFormat.Location = new System.Drawing.Point(79, 21);
            this.chkFormat.Name = "chkFormat";
            this.chkFormat.Size = new System.Drawing.Size(73, 17);
            this.chkFormat.TabIndex = 1;
            this.chkFormat.Text = "Formatted";
            this.chkFormat.UseVisualStyleBackColor = true;
            this.chkFormat.CheckedChanged += new System.EventHandler(this.chkFormat_CheckedChanged);
            // 
            // chkHeader
            // 
            this.chkHeader.AutoSize = true;
            this.chkHeader.Location = new System.Drawing.Point(13, 21);
            this.chkHeader.Name = "chkHeader";
            this.chkHeader.Size = new System.Drawing.Size(61, 17);
            this.chkHeader.TabIndex = 0;
            this.chkHeader.Text = "Header";
            this.chkHeader.UseVisualStyleBackColor = true;
            this.chkHeader.CheckedChanged += new System.EventHandler(this.chkHeader_CheckedChanged);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlHeader.Controls.Add(this.txtHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 58);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(584, 100);
            this.pnlHeader.TabIndex = 1;
            // 
            // txtHeader
            // 
            this.txtHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHeader.Enabled = false;
            this.txtHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader.Location = new System.Drawing.Point(0, 0);
            this.txtHeader.MaxLength = 255;
            this.txtHeader.Multiline = true;
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHeader.Size = new System.Drawing.Size(584, 100);
            this.txtHeader.TabIndex = 6;
            // 
            // pnlPreview
            // 
            this.pnlPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlPreview.Controls.Add(this.txtPreview);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(0, 158);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(584, 129);
            this.pnlPreview.TabIndex = 4;
            // 
            // txtPreview
            // 
            this.txtPreview.BackColor = System.Drawing.SystemColors.Info;
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreview.Location = new System.Drawing.Point(0, 0);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.Size = new System.Drawing.Size(584, 129);
            this.txtPreview.TabIndex = 0;
            this.txtPreview.TabStop = false;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.Filter = "Text files (*.txt)|*.txt|CSV (*.csv)|*.csv|All files (*.*)|*.*";
            this.saveFileDialog.Title = "Export file name";
            // 
            // ExportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 380);
            this.Controls.Add(this.pnlPreview);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.pnlFile);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportDialog";
            this.Text = "Export Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExportDialog_FormClosing);
            this.Load += new System.EventHandler(this.ExportDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExportDialog_KeyDown);
            this.pnlButtons.ResumeLayout(false);
            this.pnlFile.ResumeLayout(false);
            this.pnlFile.PerformLayout();
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlPreview.ResumeLayout(false);
            this.pnlPreview.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel pnlFile;
        private StylishButton btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDelimiters;
        private System.Windows.Forms.CheckBox chkDifference;
        private System.Windows.Forms.CheckBox chkModel;
        private System.Windows.Forms.CheckBox chkErrors;
        private System.Windows.Forms.CheckBox chkFormat;
        private System.Windows.Forms.CheckBox chkHeader;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.TextBox txtPreview;
        private StylishButton btnCancel;
        private StylishButton btnOk;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox chkColumns;
    }
}