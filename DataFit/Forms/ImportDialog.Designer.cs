using LybraFit.Common;
namespace LybraFit
{
    partial class ImportDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOriginal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numHeaderLinesCount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txt1stColumnName = new System.Windows.Forms.TextBox();
            this.txt2ndColumnName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt3rdColumnName = new System.Windows.Forms.TextBox();
            this.chkHasErrColumn = new System.Windows.Forms.CheckBox();
            this.listParsedData = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.num1stColumnIndex = new System.Windows.Forms.NumericUpDown();
            this.num2ndColumnIndex = new System.Windows.Forms.NumericUpDown();
            this.num3rdColumnIndex = new System.Windows.Forms.NumericUpDown();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkSkipErrors = new System.Windows.Forms.CheckBox();
            this.rdoStandardDelimiters = new System.Windows.Forms.RadioButton();
            this.rdoAnyNonNumericDelimiter = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new LybraFit.StylishButton();
            this.btnOk = new LybraFit.StylishButton();
            ((System.ComponentModel.ISupportInitialize)(this.numHeaderLinesCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1stColumnIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2ndColumnIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num3rdColumnIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original content";
            // 
            // txtOriginal
            // 
            this.txtOriginal.Location = new System.Drawing.Point(13, 30);
            this.txtOriginal.Multiline = true;
            this.txtOriginal.Name = "txtOriginal";
            this.txtOriginal.ReadOnly = true;
            this.txtOriginal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOriginal.Size = new System.Drawing.Size(592, 145);
            this.txtOriginal.TabIndex = 1;
            this.txtOriginal.TabStop = false;
            this.txtOriginal.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Parsed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "skip header lines:";
            // 
            // numHeaderLinesCount
            // 
            this.numHeaderLinesCount.Location = new System.Drawing.Point(119, 386);
            this.numHeaderLinesCount.Name = "numHeaderLinesCount";
            this.numHeaderLinesCount.Size = new System.Drawing.Size(44, 20);
            this.numHeaderLinesCount.TabIndex = 0;
            this.numHeaderLinesCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numHeaderLinesCount.ValueChanged += new System.EventHandler(this.numHeaderLinesCount_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 425);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "1st Column Name:";
            // 
            // txt1stColumnName
            // 
            this.txt1stColumnName.Location = new System.Drawing.Point(119, 421);
            this.txt1stColumnName.MaxLength = 52;
            this.txt1stColumnName.Name = "txt1stColumnName";
            this.txt1stColumnName.Size = new System.Drawing.Size(62, 20);
            this.txt1stColumnName.TabIndex = 2;
            this.txt1stColumnName.TextChanged += new System.EventHandler(this.txt1stColumnName_TextChanged);
            // 
            // txt2ndColumnName
            // 
            this.txt2ndColumnName.Location = new System.Drawing.Point(308, 421);
            this.txt2ndColumnName.MaxLength = 52;
            this.txt2ndColumnName.Name = "txt2ndColumnName";
            this.txt2ndColumnName.Size = new System.Drawing.Size(56, 20);
            this.txt2ndColumnName.TabIndex = 3;
            this.txt2ndColumnName.TextChanged += new System.EventHandler(this.txt2ndColumnName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, 425);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "2nd Column Name:";
            // 
            // txt3rdColumnName
            // 
            this.txt3rdColumnName.Enabled = false;
            this.txt3rdColumnName.Location = new System.Drawing.Point(524, 421);
            this.txt3rdColumnName.MaxLength = 52;
            this.txt3rdColumnName.Name = "txt3rdColumnName";
            this.txt3rdColumnName.Size = new System.Drawing.Size(60, 20);
            this.txt3rdColumnName.TabIndex = 5;
            this.txt3rdColumnName.TextChanged += new System.EventHandler(this.txt3rdColumnName_TextChanged);
            // 
            // chkHasErrColumn
            // 
            this.chkHasErrColumn.AutoSize = true;
            this.chkHasErrColumn.Location = new System.Drawing.Point(405, 423);
            this.chkHasErrColumn.Name = "chkHasErrColumn";
            this.chkHasErrColumn.Size = new System.Drawing.Size(113, 17);
            this.chkHasErrColumn.TabIndex = 4;
            this.chkHasErrColumn.Text = "3rd Column Name:";
            this.chkHasErrColumn.UseVisualStyleBackColor = true;
            this.chkHasErrColumn.CheckedChanged += new System.EventHandler(this.chkHasErrColumn_CheckedChanged);
            // 
            // listParsedData
            // 
            this.listParsedData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listParsedData.FullRowSelect = true;
            this.listParsedData.GridLines = true;
            this.listParsedData.Location = new System.Drawing.Point(16, 214);
            this.listParsedData.Name = "listParsedData";
            this.listParsedData.Size = new System.Drawing.Size(589, 156);
            this.listParsedData.TabIndex = 16;
            this.listParsedData.TabStop = false;
            this.listParsedData.UseCompatibleStateImageBehavior = false;
            this.listParsedData.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "X";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Y";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Err";
            this.columnHeader3.Width = 100;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(213, 462);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "2nd Column Index:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 462);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "1st Column Index:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(425, 462);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "3rd Column Index:";
            // 
            // num1stColumnIndex
            // 
            this.num1stColumnIndex.Location = new System.Drawing.Point(119, 458);
            this.num1stColumnIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num1stColumnIndex.Name = "num1stColumnIndex";
            this.num1stColumnIndex.Size = new System.Drawing.Size(44, 20);
            this.num1stColumnIndex.TabIndex = 6;
            this.num1stColumnIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num1stColumnIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num1stColumnIndex.ValueChanged += new System.EventHandler(this.num1stColumnIndex_ValueChanged);
            // 
            // num2ndColumnIndex
            // 
            this.num2ndColumnIndex.Location = new System.Drawing.Point(308, 458);
            this.num2ndColumnIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num2ndColumnIndex.Name = "num2ndColumnIndex";
            this.num2ndColumnIndex.Size = new System.Drawing.Size(44, 20);
            this.num2ndColumnIndex.TabIndex = 7;
            this.num2ndColumnIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num2ndColumnIndex.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num2ndColumnIndex.ValueChanged += new System.EventHandler(this.num1stColumnIndex_ValueChanged);
            // 
            // num3rdColumnIndex
            // 
            this.num3rdColumnIndex.Enabled = false;
            this.num3rdColumnIndex.Location = new System.Drawing.Point(524, 458);
            this.num3rdColumnIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num3rdColumnIndex.Name = "num3rdColumnIndex";
            this.num3rdColumnIndex.Size = new System.Drawing.Size(44, 20);
            this.num3rdColumnIndex.TabIndex = 8;
            this.num3rdColumnIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num3rdColumnIndex.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.num3rdColumnIndex.ValueChanged += new System.EventHandler(this.num1stColumnIndex_ValueChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(62, 527);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(522, 20);
            this.txtTitle.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 529);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Title:";
            // 
            // chkSkipErrors
            // 
            this.chkSkipErrors.AutoSize = true;
            this.chkSkipErrors.Location = new System.Drawing.Point(119, 496);
            this.chkSkipErrors.Name = "chkSkipErrors";
            this.chkSkipErrors.Size = new System.Drawing.Size(122, 17);
            this.chkSkipErrors.TabIndex = 9;
            this.chkSkipErrors.Text = "Skip lines with errors";
            this.chkSkipErrors.UseVisualStyleBackColor = true;
            // 
            // rdoStandardDelimiters
            // 
            this.rdoStandardDelimiters.AutoSize = true;
            this.rdoStandardDelimiters.Checked = true;
            this.rdoStandardDelimiters.Location = new System.Drawing.Point(6, 10);
            this.rdoStandardDelimiters.Name = "rdoStandardDelimiters";
            this.rdoStandardDelimiters.Size = new System.Drawing.Size(114, 17);
            this.rdoStandardDelimiters.TabIndex = 1;
            this.rdoStandardDelimiters.TabStop = true;
            this.rdoStandardDelimiters.Text = "Standard delimiters";
            this.rdoStandardDelimiters.UseVisualStyleBackColor = true;
            // 
            // rdoAnyNonNumericDelimiter
            // 
            this.rdoAnyNonNumericDelimiter.AutoSize = true;
            this.rdoAnyNonNumericDelimiter.Location = new System.Drawing.Point(193, 10);
            this.rdoAnyNonNumericDelimiter.Name = "rdoAnyNonNumericDelimiter";
            this.rdoAnyNonNumericDelimiter.Size = new System.Drawing.Size(152, 17);
            this.rdoAnyNonNumericDelimiter.TabIndex = 2;
            this.rdoAnyNonNumericDelimiter.Text = "Any non numeric character";
            this.rdoAnyNonNumericDelimiter.UseVisualStyleBackColor = true;
            this.rdoAnyNonNumericDelimiter.CheckedChanged += new System.EventHandler(this.rdoAnyNonNumericDelimiter_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoAnyNonNumericDelimiter);
            this.groupBox1.Controls.Add(this.rdoStandardDelimiters);
            this.groupBox1.Location = new System.Drawing.Point(212, 380);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 32);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(225, 572);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(315, 572);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // ImportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(617, 614);
            this.Controls.Add(this.chkSkipErrors);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.num3rdColumnIndex);
            this.Controls.Add(this.num2ndColumnIndex);
            this.Controls.Add(this.num1stColumnIndex);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listParsedData);
            this.Controls.Add(this.chkHasErrColumn);
            this.Controls.Add(this.txt3rdColumnName);
            this.Controls.Add(this.txt2ndColumnName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt1stColumnName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numHeaderLinesCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOriginal);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportDialog";
            this.Text = "Import and Paste Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportDialog_FormClosing);
            this.Load += new System.EventHandler(this.ImportDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImportDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numHeaderLinesCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1stColumnIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2ndColumnIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num3rdColumnIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOriginal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numHeaderLinesCount;
        private StylishButton btnOk;
        private StylishButton btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt1stColumnName;
        private System.Windows.Forms.TextBox txt2ndColumnName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt3rdColumnName;
        private System.Windows.Forms.CheckBox chkHasErrColumn;
        private System.Windows.Forms.ListView listParsedData;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown num1stColumnIndex;
        private System.Windows.Forms.NumericUpDown num2ndColumnIndex;
        private System.Windows.Forms.NumericUpDown num3rdColumnIndex;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkSkipErrors;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoAnyNonNumericDelimiter;
        private System.Windows.Forms.RadioButton rdoStandardDelimiters;
    }
}