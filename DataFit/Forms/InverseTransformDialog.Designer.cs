namespace LybraFit.Forms
{
    partial class InverseTransformDialog
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
            this.lblMinY = new System.Windows.Forms.Label();
            this.lblMaxY = new System.Windows.Forms.Label();
            this.txtMinY = new System.Windows.Forms.TextBox();
            this.txtMaxY = new System.Windows.Forms.TextBox();
            this.txtStepY = new System.Windows.Forms.TextBox();
            this.lblStepY = new System.Windows.Forms.Label();
            this.lblMaxYV = new System.Windows.Forms.Label();
            this.lblMinYV = new System.Windows.Forms.Label();
            this.btnCancel = new LybraFit.StylishButton();
            this.btnOk = new LybraFit.StylishButton();
            this.txtPrecision = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMinY
            // 
            this.lblMinY.AutoSize = true;
            this.lblMinY.Location = new System.Drawing.Point(47, 25);
            this.lblMinY.Name = "lblMinY";
            this.lblMinY.Size = new System.Drawing.Size(41, 13);
            this.lblMinY.TabIndex = 0;
            this.lblMinY.Text = "lblMinY";
            this.lblMinY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMaxY
            // 
            this.lblMaxY.AutoSize = true;
            this.lblMaxY.Location = new System.Drawing.Point(44, 56);
            this.lblMaxY.Name = "lblMaxY";
            this.lblMaxY.Size = new System.Drawing.Size(44, 13);
            this.lblMaxY.TabIndex = 1;
            this.lblMaxY.Text = "lblMaxY";
            this.lblMaxY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMinY
            // 
            this.txtMinY.Location = new System.Drawing.Point(94, 22);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Size = new System.Drawing.Size(84, 20);
            this.txtMinY.TabIndex = 0;
            this.txtMinY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMaxY
            // 
            this.txtMaxY.Location = new System.Drawing.Point(94, 53);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(84, 20);
            this.txtMaxY.TabIndex = 1;
            this.txtMaxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStepY
            // 
            this.txtStepY.Location = new System.Drawing.Point(94, 84);
            this.txtStepY.Name = "txtStepY";
            this.txtStepY.Size = new System.Drawing.Size(84, 20);
            this.txtStepY.TabIndex = 2;
            this.txtStepY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblStepY
            // 
            this.lblStepY.AutoSize = true;
            this.lblStepY.Location = new System.Drawing.Point(42, 87);
            this.lblStepY.Name = "lblStepY";
            this.lblStepY.Size = new System.Drawing.Size(46, 13);
            this.lblStepY.TabIndex = 4;
            this.lblStepY.Text = "lblStepY";
            this.lblStepY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMaxYV
            // 
            this.lblMaxYV.AutoSize = true;
            this.lblMaxYV.Location = new System.Drawing.Point(194, 56);
            this.lblMaxYV.Name = "lblMaxYV";
            this.lblMaxYV.Size = new System.Drawing.Size(51, 13);
            this.lblMaxYV.TabIndex = 7;
            this.lblMaxYV.Text = "lblMaxYV";
            this.lblMaxYV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMinYV
            // 
            this.lblMinYV.AutoSize = true;
            this.lblMinYV.Location = new System.Drawing.Point(194, 25);
            this.lblMinYV.Name = "lblMinYV";
            this.lblMinYV.Size = new System.Drawing.Size(48, 13);
            this.lblMinYV.TabIndex = 6;
            this.lblMinYV.Text = "lblMinYV";
            this.lblMinYV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(45, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(164, 158);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // txtPrecision
            // 
            this.txtPrecision.Location = new System.Drawing.Point(94, 117);
            this.txtPrecision.Name = "txtPrecision";
            this.txtPrecision.Size = new System.Drawing.Size(84, 20);
            this.txtPrecision.TabIndex = 3;
            this.txtPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "precision:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // InverseTransformDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(282, 198);
            this.Controls.Add(this.txtPrecision);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblMaxYV);
            this.Controls.Add(this.lblMinYV);
            this.Controls.Add(this.txtStepY);
            this.Controls.Add(this.lblStepY);
            this.Controls.Add(this.txtMaxY);
            this.Controls.Add(this.txtMinY);
            this.Controls.Add(this.lblMaxY);
            this.Controls.Add(this.lblMinY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InverseTransformDialog";
            this.Text = "Inverse Transform";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InverseTransformDialog_FormClosing);
            this.Load += new System.EventHandler(this.InverseTransformDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InverseTransformDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMinY;
        private System.Windows.Forms.Label lblMaxY;
        private System.Windows.Forms.TextBox txtMinY;
        private System.Windows.Forms.TextBox txtMaxY;
        private System.Windows.Forms.TextBox txtStepY;
        private System.Windows.Forms.Label lblStepY;
        private System.Windows.Forms.Label lblMaxYV;
        private System.Windows.Forms.Label lblMinYV;
        private StylishButton btnCancel;
        private StylishButton btnOk;
        private System.Windows.Forms.TextBox txtPrecision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}