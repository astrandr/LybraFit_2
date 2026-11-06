using LybraFit.Common;
namespace LybraFit.Forms
{
    partial class PlotExportDialog
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.btnOk = new LybraFit.StylishButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubTitle = new System.Windows.Forms.TextBox();
            this.chkSubTitle = new System.Windows.Forms.CheckBox();
            this.cboUnits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboDPI = new System.Windows.Forms.ComboBox();
            this.chkAspect = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboPaper = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.rdoPortrait = new System.Windows.Forms.RadioButton();
            this.Button1 = new LybraFit.StylishButton();
            this.txtMargins = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnApplyParametersLegend = new LybraFit.StylishButton();
            this.btnApplyParametersValues = new LybraFit.StylishButton();
            this.btnApplyDerivedValues = new LybraFit.StylishButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(135, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(237, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(178, 15);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(56, 20);
            this.txtWidth.TabIndex = 1;
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(284, 15);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(56, 20);
            this.txtHeight.TabIndex = 2;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(256, 207);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 23);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(74, 117);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(482, 20);
            this.txtTitle.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(270, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Title";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Line 1:";
            // 
            // txtSubTitle
            // 
            this.txtSubTitle.Enabled = false;
            this.txtSubTitle.Location = new System.Drawing.Point(74, 145);
            this.txtSubTitle.Name = "txtSubTitle";
            this.txtSubTitle.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSubTitle.Size = new System.Drawing.Size(482, 20);
            this.txtSubTitle.TabIndex = 12;
            // 
            // chkSubTitle
            // 
            this.chkSubTitle.AutoSize = true;
            this.chkSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSubTitle.Location = new System.Drawing.Point(16, 146);
            this.chkSubTitle.Name = "chkSubTitle";
            this.chkSubTitle.Size = new System.Drawing.Size(60, 19);
            this.chkSubTitle.TabIndex = 11;
            this.chkSubTitle.Text = "Line 2:";
            this.chkSubTitle.UseVisualStyleBackColor = true;
            this.chkSubTitle.CheckedChanged += new System.EventHandler(this.chkSubTitle_CheckedChanged);
            // 
            // cboUnits
            // 
            this.cboUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnits.FormattingEnabled = true;
            this.cboUnits.ItemHeight = 13;
            this.cboUnits.Items.AddRange(new object[] {
            "Pixel",
            "Inch",
            "cm"});
            this.cboUnits.Location = new System.Drawing.Point(73, 14);
            this.cboUnits.Name = "cboUnits";
            this.cboUnits.Size = new System.Drawing.Size(54, 21);
            this.cboUnits.TabIndex = 0;
            this.cboUnits.SelectedIndexChanged += new System.EventHandler(this.cboUnits_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Units:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(454, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "DPI:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDPI
            // 
            this.cboDPI.FormattingEnabled = true;
            this.cboDPI.ItemHeight = 13;
            this.cboDPI.Items.AddRange(new object[] {
            "300",
            "600",
            "1200"});
            this.cboDPI.Location = new System.Drawing.Point(485, 15);
            this.cboDPI.Name = "cboDPI";
            this.cboDPI.Size = new System.Drawing.Size(71, 21);
            this.cboDPI.TabIndex = 4;
            // 
            // chkAspect
            // 
            this.chkAspect.AutoSize = true;
            this.chkAspect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAspect.Location = new System.Drawing.Point(348, 18);
            this.chkAspect.Name = "chkAspect";
            this.chkAspect.Size = new System.Drawing.Size(109, 19);
            this.chkAspect.TabIndex = 3;
            this.chkAspect.Text = "Preserve Aspect";
            this.chkAspect.UseVisualStyleBackColor = true;
            this.chkAspect.CheckedChanged += new System.EventHandler(this.chkAspect_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Paper:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPaper
            // 
            this.cboPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaper.FormattingEnabled = true;
            this.cboPaper.ItemHeight = 13;
            this.cboPaper.Items.AddRange(new object[] {
            "Custom",
            "A4  (8.3\" x 11.7\")",
            "A5 (5.8\" x 8.3\")",
            "A6 (4.1\" x 5.8\")"});
            this.cboPaper.Location = new System.Drawing.Point(74, 55);
            this.cboPaper.Name = "cboPaper";
            this.cboPaper.Size = new System.Drawing.Size(164, 21);
            this.cboPaper.TabIndex = 5;
            this.cboPaper.SelectedIndexChanged += new System.EventHandler(this.cboPaper_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoLandscape);
            this.groupBox1.Controls.Add(this.rdoPortrait);
            this.groupBox1.Location = new System.Drawing.Point(280, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 30);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoLandscape.Location = new System.Drawing.Point(85, 9);
            this.rdoLandscape.Name = "rdoLandscape";
            this.rdoLandscape.Size = new System.Drawing.Size(81, 19);
            this.rdoLandscape.TabIndex = 8;
            this.rdoLandscape.Text = "Landscape";
            this.rdoLandscape.UseVisualStyleBackColor = true;
            // 
            // rdoPortrait
            // 
            this.rdoPortrait.AutoSize = true;
            this.rdoPortrait.Checked = true;
            this.rdoPortrait.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoPortrait.Location = new System.Drawing.Point(8, 9);
            this.rdoPortrait.Name = "rdoPortrait";
            this.rdoPortrait.Size = new System.Drawing.Size(64, 19);
            this.rdoPortrait.TabIndex = 7;
            this.rdoPortrait.TabStop = true;
            this.rdoPortrait.Text = "Portrait";
            this.rdoPortrait.UseVisualStyleBackColor = true;
            // 
            // Button1
            // 
            this.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button1.Location = new System.Drawing.Point(206, 207);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(68, 23);
            this.Button1.TabIndex = 27;
            this.Button1.Text = "Cancel";
            this.Button1.UseVisualStyleBackColor = true;
            // 
            // txtMargins
            // 
            this.txtMargins.Location = new System.Drawing.Point(512, 55);
            this.txtMargins.Name = "txtMargins";
            this.txtMargins.Size = new System.Drawing.Size(43, 20);
            this.txtMargins.TabIndex = 8;
            this.txtMargins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(459, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 15);
            this.label8.TabIndex = 29;
            this.label8.Text = "Margins:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnApplyParametersLegend
            // 
            this.btnApplyParametersLegend.Location = new System.Drawing.Point(73, 170);
            this.btnApplyParametersLegend.Name = "btnApplyParametersLegend";
            this.btnApplyParametersLegend.Size = new System.Drawing.Size(155, 23);
            this.btnApplyParametersLegend.TabIndex = 13;
            this.btnApplyParametersLegend.Text = "Parameters as legend";
            this.btnApplyParametersLegend.UseVisualStyleBackColor = true;
            this.btnApplyParametersLegend.Click += new System.EventHandler(this.btnApplyParametersLegend_Click);
            // 
            // btnApplyParametersValues
            // 
            this.btnApplyParametersValues.Location = new System.Drawing.Point(234, 171);
            this.btnApplyParametersValues.Name = "btnApplyParametersValues";
            this.btnApplyParametersValues.Size = new System.Drawing.Size(166, 23);
            this.btnApplyParametersValues.TabIndex = 14;
            this.btnApplyParametersValues.Text = "Explicit parameter values";
            this.btnApplyParametersValues.UseVisualStyleBackColor = true;
            this.btnApplyParametersValues.Click += new System.EventHandler(this.btnApplyParametersValues_Click);
            // 
            // btnApplyDerivedValues
            // 
            this.btnApplyDerivedValues.Enabled = false;
            this.btnApplyDerivedValues.Location = new System.Drawing.Point(406, 171);
            this.btnApplyDerivedValues.Name = "btnApplyDerivedValues";
            this.btnApplyDerivedValues.Size = new System.Drawing.Size(144, 23);
            this.btnApplyDerivedValues.TabIndex = 15;
            this.btnApplyDerivedValues.Text = "Derived values";
            this.btnApplyDerivedValues.UseVisualStyleBackColor = true;
            this.btnApplyDerivedValues.Click += new System.EventHandler(this.btnDerivedValues_Click);
            // 
            // PlotExportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(581, 242);
            this.Controls.Add(this.btnApplyDerivedValues);
            this.Controls.Add(this.btnApplyParametersValues);
            this.Controls.Add(this.btnApplyParametersLegend);
            this.Controls.Add(this.txtMargins);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboPaper);
            this.Controls.Add(this.chkAspect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboDPI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboUnits);
            this.Controls.Add(this.txtSubTitle);
            this.Controls.Add(this.chkSubTitle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlotExportDialog";
            this.Text = "Plot Export Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlotExportDialog_FormClosing);
            this.Load += new System.EventHandler(this.PlotExportDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlotExportDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private StylishButton btnOk;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSubTitle;
        private System.Windows.Forms.CheckBox chkSubTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboUnits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboDPI;
        private System.Windows.Forms.CheckBox chkAspect;
        private StylishButton Button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoLandscape;
        private System.Windows.Forms.RadioButton rdoPortrait;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPaper;
        private System.Windows.Forms.TextBox txtMargins;
        private System.Windows.Forms.Label label8;
        private StylishButton btnApplyParametersValues;
        private StylishButton btnApplyParametersLegend;
        private StylishButton btnApplyDerivedValues;
    }
}