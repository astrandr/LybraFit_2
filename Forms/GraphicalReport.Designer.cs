using DataFit.Common;
namespace DataFit.Forms
{
    partial class GraphicalReport
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
            this.btnClose = new DataFit.StylishButton();
            this.btnPrint = new DataFit.StylishButton();
            this.btnSave = new DataFit.StylishButton();
            this.btnCopy = new DataFit.StylishButton();
            this.pnlView = new System.Windows.Forms.Panel();
            this.pnlViewBackGround = new System.Windows.Forms.Panel();
            this.pictView = new System.Windows.Forms.PictureBox();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.pnlButtons.SuspendLayout();
            this.pnlView.SuspendLayout();
            this.pnlViewBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnCopy);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 613);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(755, 42);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(469, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(370, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(271, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(172, 9);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 0;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // pnlView
            // 
            this.pnlView.AutoScroll = true;
            this.pnlView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlView.Controls.Add(this.pnlViewBackGround);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlView.Location = new System.Drawing.Point(0, 0);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(755, 613);
            this.pnlView.TabIndex = 1;
            // 
            // pnlViewBackGround
            // 
            this.pnlViewBackGround.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlViewBackGround.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnlViewBackGround.Controls.Add(this.pictView);
            this.pnlViewBackGround.Location = new System.Drawing.Point(12, 12);
            this.pnlViewBackGround.Name = "pnlViewBackGround";
            this.pnlViewBackGround.Size = new System.Drawing.Size(731, 585);
            this.pnlViewBackGround.TabIndex = 0;
            // 
            // pictView
            // 
            this.pictView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictView.Location = new System.Drawing.Point(0, 0);
            this.pictView.Name = "pictView";
            this.pictView.Size = new System.Drawing.Size(731, 585);
            this.pictView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictView.TabIndex = 1;
            this.pictView.TabStop = false;
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Bitmap |*.bmp; Image|*.png;JPEG|*.jpg";
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // GraphicalReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 655);
            this.Controls.Add(this.pnlView);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GraphicalReport";
            this.Text = "Graphical Report";
            this.Load += new System.EventHandler(this.GraphicalReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphicalReport_KeyDown);
            this.pnlButtons.ResumeLayout(false);
            this.pnlView.ResumeLayout(false);
            this.pnlViewBackGround.ResumeLayout(false);
            this.pnlViewBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel pnlView;
        private StylishButton btnClose;
        private StylishButton btnPrint;
        private StylishButton btnSave;
        private StylishButton btnCopy;
        private System.Windows.Forms.Panel pnlViewBackGround;
        private System.Windows.Forms.PictureBox pictView;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.PrintDialog printDialog;
    }
}