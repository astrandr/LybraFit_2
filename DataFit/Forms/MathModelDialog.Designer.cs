namespace LybraFit.Forms
{
    partial class MathModelDialog
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
            this.btnCancel = new LybraFit.StylishButton();
            this.btnOk = new LybraFit.StylishButton();
            this.pictFunctionPreview = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.contextMenuExpressionEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolExpressionText = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolExpressionText = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictFunctionPreview)).BeginInit();
            this.contextMenuExpressionEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(622, 266);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(713, 266);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // pictFunctionPreview
            // 
            this.pictFunctionPreview.BackColor = System.Drawing.SystemColors.Info;
            this.pictFunctionPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictFunctionPreview.Location = new System.Drawing.Point(12, 96);
            this.pictFunctionPreview.Name = "pictFunctionPreview";
            this.pictFunctionPreview.Size = new System.Drawing.Size(776, 161);
            this.pictFunctionPreview.TabIndex = 2;
            this.pictFunctionPreview.TabStop = false;
            this.pictFunctionPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictFunctionPreview_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Expression  Y(x) =";
            // 
            // txtExpression
            // 
            this.txtExpression.ContextMenuStrip = this.contextMenuExpressionEdit;
            this.txtExpression.Font = new System.Drawing.Font("Calisto MT", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpression.Location = new System.Drawing.Point(116, 45);
            this.txtExpression.MaxLength = 255;
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(672, 45);
            this.txtExpression.TabIndex = 1;
            this.txtExpression.TextChanged += new System.EventHandler(this.txtExpression_TextChanged);
            this.txtExpression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpression_KeyDown);
            // 
            // contextMenuExpressionEdit
            // 
            this.contextMenuExpressionEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolExpressionText,
            this.PasteToolExpressionText});
            this.contextMenuExpressionEdit.Name = "contextMenuExpressionEdit";
            this.contextMenuExpressionEdit.Size = new System.Drawing.Size(103, 48);
            // 
            // copyToolExpressionText
            // 
            this.copyToolExpressionText.Name = "copyToolExpressionText";
            this.copyToolExpressionText.Size = new System.Drawing.Size(102, 22);
            this.copyToolExpressionText.Text = "Copy";
            this.copyToolExpressionText.Click += new System.EventHandler(this.copyToolExpressionText_Click);
            // 
            // PasteToolExpressionText
            // 
            this.PasteToolExpressionText.Name = "PasteToolExpressionText";
            this.PasteToolExpressionText.Size = new System.Drawing.Size(102, 22);
            this.PasteToolExpressionText.Text = "Paste";
            this.PasteToolExpressionText.Click += new System.EventHandler(this.PasteToolExpressionText_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(116, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(293, 26);
            this.txtName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Model name:";
            // 
            // MathModelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(800, 301);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExpression);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictFunctionPreview);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MathModelDialog";
            this.Text = "Expression Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MathModelDialog_FormClosing);
            this.Load += new System.EventHandler(this.MathModelDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MathModelDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictFunctionPreview)).EndInit();
            this.contextMenuExpressionEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StylishButton btnCancel;
        private StylishButton btnOk;
        private System.Windows.Forms.PictureBox pictFunctionPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuExpressionEdit;
        private System.Windows.Forms.ToolStripMenuItem copyToolExpressionText;
        private System.Windows.Forms.ToolStripMenuItem PasteToolExpressionText;
    }
}