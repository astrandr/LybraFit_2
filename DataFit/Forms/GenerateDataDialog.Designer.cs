using LybraFit.Common;
namespace LybraFit.Forms
{
    partial class GenerateDataDialog
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
            this.txtCol1Name = new System.Windows.Forms.TextBox();
            this.txtDataXMin = new System.Windows.Forms.TextBox();
            this.grpMainDataX = new System.Windows.Forms.GroupBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDataXIncrement = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDataXMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkMainDataXAuto = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkErrDataCalculated = new System.Windows.Forms.CheckBox();
            this.txtErrDataName = new System.Windows.Forms.TextBox();
            this.chkErrData = new System.Windows.Forms.CheckBox();
            this.grpMainDataY = new System.Windows.Forms.GroupBox();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.lblYCalcFunction = new System.Windows.Forms.Label();
            this.chkMainDataYCalculated = new System.Windows.Forms.CheckBox();
            this.txtCol2Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.btnCancel = new LybraFit.StylishButton();
            this.btnOk = new LybraFit.StylishButton();
            this.grpMainDataX.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpMainDataY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Column 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Min:";
            // 
            // txtCol1Name
            // 
            this.txtCol1Name.Location = new System.Drawing.Point(79, 54);
            this.txtCol1Name.MaxLength = 42;
            this.txtCol1Name.Name = "txtCol1Name";
            this.txtCol1Name.Size = new System.Drawing.Size(100, 20);
            this.txtCol1Name.TabIndex = 1;
            this.txtCol1Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDataXMin
            // 
            this.txtDataXMin.Enabled = false;
            this.txtDataXMin.Location = new System.Drawing.Point(79, 103);
            this.txtDataXMin.Name = "txtDataXMin";
            this.txtDataXMin.Size = new System.Drawing.Size(100, 20);
            this.txtDataXMin.TabIndex = 3;
            this.txtDataXMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpMainDataX
            // 
            this.grpMainDataX.Controls.Add(this.txtTitle);
            this.grpMainDataX.Controls.Add(this.label4);
            this.grpMainDataX.Controls.Add(this.txtDataXIncrement);
            this.grpMainDataX.Controls.Add(this.label7);
            this.grpMainDataX.Controls.Add(this.txtDataXMax);
            this.grpMainDataX.Controls.Add(this.label6);
            this.grpMainDataX.Controls.Add(this.chkMainDataXAuto);
            this.grpMainDataX.Controls.Add(this.txtCol1Name);
            this.grpMainDataX.Controls.Add(this.txtDataXMin);
            this.grpMainDataX.Controls.Add(this.label1);
            this.grpMainDataX.Controls.Add(this.label2);
            this.grpMainDataX.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpMainDataX.Location = new System.Drawing.Point(0, 0);
            this.grpMainDataX.Name = "grpMainDataX";
            this.grpMainDataX.Size = new System.Drawing.Size(277, 186);
            this.grpMainDataX.TabIndex = 0;
            this.grpMainDataX.TabStop = false;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(17, 25);
            this.txtTitle.MaxLength = 120;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(241, 20);
            this.txtTitle.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Title:";
            // 
            // txtDataXIncrement
            // 
            this.txtDataXIncrement.Enabled = false;
            this.txtDataXIncrement.Location = new System.Drawing.Point(132, 154);
            this.txtDataXIncrement.Name = "txtDataXIncrement";
            this.txtDataXIncrement.Size = new System.Drawing.Size(47, 20);
            this.txtDataXIncrement.TabIndex = 5;
            this.txtDataXIncrement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(64, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Increment:";
            // 
            // txtDataXMax
            // 
            this.txtDataXMax.Enabled = false;
            this.txtDataXMax.Location = new System.Drawing.Point(79, 129);
            this.txtDataXMax.Name = "txtDataXMax";
            this.txtDataXMax.Size = new System.Drawing.Size(100, 20);
            this.txtDataXMax.TabIndex = 4;
            this.txtDataXMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Max:";
            // 
            // chkMainDataXAuto
            // 
            this.chkMainDataXAuto.AutoSize = true;
            this.chkMainDataXAuto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMainDataXAuto.Location = new System.Drawing.Point(55, 80);
            this.chkMainDataXAuto.Name = "chkMainDataXAuto";
            this.chkMainDataXAuto.Size = new System.Drawing.Size(109, 19);
            this.chkMainDataXAuto.TabIndex = 2;
            this.chkMainDataXAuto.Text = "Auto Increment";
            this.chkMainDataXAuto.UseVisualStyleBackColor = true;
            this.chkMainDataXAuto.CheckedChanged += new System.EventHandler(this.chkMainDataXAuto_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkErrDataCalculated);
            this.groupBox1.Controls.Add(this.txtErrDataName);
            this.groupBox1.Controls.Add(this.chkErrData);
            this.groupBox1.Location = new System.Drawing.Point(0, 308);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 100);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Column 3:";
            // 
            // chkErrDataCalculated
            // 
            this.chkErrDataCalculated.AutoSize = true;
            this.chkErrDataCalculated.Enabled = false;
            this.chkErrDataCalculated.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkErrDataCalculated.Location = new System.Drawing.Point(17, 73);
            this.chkErrDataCalculated.Name = "chkErrDataCalculated";
            this.chkErrDataCalculated.Size = new System.Drawing.Size(102, 19);
            this.chkErrDataCalculated.TabIndex = 15;
            this.chkErrDataCalculated.Text = "Calculated Std";
            this.chkErrDataCalculated.UseVisualStyleBackColor = true;
            // 
            // txtErrDataName
            // 
            this.txtErrDataName.Enabled = false;
            this.txtErrDataName.Location = new System.Drawing.Point(79, 43);
            this.txtErrDataName.MaxLength = 42;
            this.txtErrDataName.Name = "txtErrDataName";
            this.txtErrDataName.Size = new System.Drawing.Size(100, 20);
            this.txtErrDataName.TabIndex = 14;
            this.txtErrDataName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkErrData
            // 
            this.chkErrData.AutoSize = true;
            this.chkErrData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkErrData.Location = new System.Drawing.Point(17, 20);
            this.chkErrData.Name = "chkErrData";
            this.chkErrData.Size = new System.Drawing.Size(77, 19);
            this.chkErrData.TabIndex = 13;
            this.chkErrData.Text = "Error data";
            this.chkErrData.UseVisualStyleBackColor = true;
            this.chkErrData.CheckedChanged += new System.EventHandler(this.chkErrData_CheckedChanged);
            // 
            // grpMainDataY
            // 
            this.grpMainDataY.Controls.Add(this.txtExpression);
            this.grpMainDataY.Controls.Add(this.lblYCalcFunction);
            this.grpMainDataY.Controls.Add(this.chkMainDataYCalculated);
            this.grpMainDataY.Controls.Add(this.txtCol2Name);
            this.grpMainDataY.Controls.Add(this.label5);
            this.grpMainDataY.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpMainDataY.Location = new System.Drawing.Point(0, 186);
            this.grpMainDataY.Name = "grpMainDataY";
            this.grpMainDataY.Size = new System.Drawing.Size(277, 120);
            this.grpMainDataY.TabIndex = 6;
            this.grpMainDataY.TabStop = false;
            // 
            // txtExpression
            // 
            this.txtExpression.Enabled = false;
            this.txtExpression.Font = new System.Drawing.Font("Calisto MT", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpression.Location = new System.Drawing.Point(17, 68);
            this.txtExpression.MaxLength = 120;
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(241, 46);
            this.txtExpression.TabIndex = 9;
            this.txtExpression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpression_KeyDown);
            // 
            // lblYCalcFunction
            // 
            this.lblYCalcFunction.AutoSize = true;
            this.lblYCalcFunction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYCalcFunction.Location = new System.Drawing.Point(100, 47);
            this.lblYCalcFunction.Name = "lblYCalcFunction";
            this.lblYCalcFunction.Size = new System.Drawing.Size(29, 15);
            this.lblYCalcFunction.TabIndex = 12;
            this.lblYCalcFunction.Text = "Y(X)";
            // 
            // chkMainDataYCalculated
            // 
            this.chkMainDataYCalculated.AutoSize = true;
            this.chkMainDataYCalculated.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMainDataYCalculated.Location = new System.Drawing.Point(18, 46);
            this.chkMainDataYCalculated.Name = "chkMainDataYCalculated";
            this.chkMainDataYCalculated.Size = new System.Drawing.Size(82, 19);
            this.chkMainDataYCalculated.TabIndex = 8;
            this.chkMainDataYCalculated.Text = "Calculated";
            this.chkMainDataYCalculated.UseVisualStyleBackColor = true;
            this.chkMainDataYCalculated.CheckedChanged += new System.EventHandler(this.chkMainDataYCalculated_CheckedChanged);
            // 
            // txtCol2Name
            // 
            this.txtCol2Name.Location = new System.Drawing.Point(79, 18);
            this.txtCol2Name.MaxLength = 42;
            this.txtCol2Name.Name = "txtCol2Name";
            this.txtCol2Name.Size = new System.Drawing.Size(100, 20);
            this.txtCol2Name.TabIndex = 7;
            this.txtCol2Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Column 2:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(49, 421);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(152, 421);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // GenerateDataDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(277, 456);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpMainDataY);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpMainDataX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateDataDialog";
            this.Text = "Generate Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewDataDialog_FormClosing);
            this.Load += new System.EventHandler(this.NewDataDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewDataDialog_KeyDown);
            this.grpMainDataX.ResumeLayout(false);
            this.grpMainDataX.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpMainDataY.ResumeLayout(false);
            this.grpMainDataY.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCol1Name;
        private System.Windows.Forms.TextBox txtDataXMin;
        private System.Windows.Forms.GroupBox grpMainDataX;
        private System.Windows.Forms.CheckBox chkMainDataXAuto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkErrDataCalculated;
        private System.Windows.Forms.TextBox txtErrDataName;
        private System.Windows.Forms.CheckBox chkErrData;
        private System.Windows.Forms.GroupBox grpMainDataY;
        private System.Windows.Forms.CheckBox chkMainDataYCalculated;
        private System.Windows.Forms.TextBox txtCol2Name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDataXIncrement;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDataXMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private StylishButton btnOk;
        private StylishButton btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblYCalcFunction;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.HelpProvider helpProvider;
    }
}