using DataFit.Common;
namespace DataFit.Forms
{
    partial class ColumnSettingsDialog
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
            this.btnCancel = new DataFit.StylishButton();
            this.btnOk = new DataFit.StylishButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpColumnList = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listColumns = new System.Windows.Forms.ListBox();
            this.grpColumn5 = new System.Windows.Forms.GroupBox();
            this.chkAutoWidth = new System.Windows.Forms.CheckBox();
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.txtCustomFormat = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.nmrDigits = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grpColumnList.SuspendLayout();
            this.grpColumn5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDigits)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(82, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(194, 219);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpColumnList);
            this.panel1.Controls.Add(this.grpColumn5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 195);
            this.panel1.TabIndex = 9;
            // 
            // grpColumnList
            // 
            this.grpColumnList.Controls.Add(this.label1);
            this.grpColumnList.Controls.Add(this.listColumns);
            this.grpColumnList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpColumnList.Location = new System.Drawing.Point(0, 0);
            this.grpColumnList.Name = "grpColumnList";
            this.grpColumnList.Size = new System.Drawing.Size(151, 195);
            this.grpColumnList.TabIndex = 0;
            this.grpColumnList.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Columns";
            // 
            // listColumns
            // 
            this.listColumns.BackColor = System.Drawing.SystemColors.Control;
            this.listColumns.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listColumns.FormattingEnabled = true;
            this.listColumns.Location = new System.Drawing.Point(3, 32);
            this.listColumns.Name = "listColumns";
            this.listColumns.Size = new System.Drawing.Size(145, 160);
            this.listColumns.TabIndex = 0;
            this.listColumns.SelectedIndexChanged += new System.EventHandler(this.listColumns_SelectedIndexChanged);
            this.listColumns.Enter += new System.EventHandler(this.listColumns_Enter);
            this.listColumns.Leave += new System.EventHandler(this.listColumns_Leave);
            // 
            // grpColumn5
            // 
            this.grpColumn5.Controls.Add(this.chkAutoWidth);
            this.grpColumn5.Controls.Add(this.chkVisible);
            this.grpColumn5.Controls.Add(this.txtCustomFormat);
            this.grpColumn5.Controls.Add(this.label21);
            this.grpColumn5.Controls.Add(this.nmrDigits);
            this.grpColumn5.Controls.Add(this.label22);
            this.grpColumn5.Controls.Add(this.cboFormat);
            this.grpColumn5.Controls.Add(this.label23);
            this.grpColumn5.Controls.Add(this.txtName);
            this.grpColumn5.Controls.Add(this.label25);
            this.grpColumn5.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpColumn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpColumn5.Location = new System.Drawing.Point(151, 0);
            this.grpColumn5.Name = "grpColumn5";
            this.grpColumn5.Size = new System.Drawing.Size(202, 195);
            this.grpColumn5.TabIndex = 1;
            this.grpColumn5.TabStop = false;
            // 
            // chkAutoWidth
            // 
            this.chkAutoWidth.AutoSize = true;
            this.chkAutoWidth.Location = new System.Drawing.Point(83, 12);
            this.chkAutoWidth.Name = "chkAutoWidth";
            this.chkAutoWidth.Size = new System.Drawing.Size(101, 17);
            this.chkAutoWidth.TabIndex = 1;
            this.chkAutoWidth.Text = "Automatic width";
            this.chkAutoWidth.UseVisualStyleBackColor = true;
            this.chkAutoWidth.CheckedChanged += new System.EventHandler(this.chkAutoWidth_CheckedChanged);
            // 
            // chkVisible
            // 
            this.chkVisible.AutoSize = true;
            this.chkVisible.Checked = true;
            this.chkVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisible.Location = new System.Drawing.Point(28, 12);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(56, 17);
            this.chkVisible.TabIndex = 0;
            this.chkVisible.Text = "Visible";
            this.chkVisible.UseVisualStyleBackColor = true;
            this.chkVisible.CheckedChanged += new System.EventHandler(this.chkVisible_CheckedChanged);
            // 
            // txtCustomFormat
            // 
            this.txtCustomFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomFormat.Location = new System.Drawing.Point(86, 138);
            this.txtCustomFormat.Name = "txtCustomFormat";
            this.txtCustomFormat.Size = new System.Drawing.Size(91, 20);
            this.txtCustomFormat.TabIndex = 5;
            this.txtCustomFormat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCustomFormat.Validating += new System.ComponentModel.CancelEventHandler(this.txtCustomFormat_Validating);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 141);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 13);
            this.label21.TabIndex = 8;
            this.label21.Text = "Custom Fromat:";
            // 
            // nmrDigits
            // 
            this.nmrDigits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrDigits.Location = new System.Drawing.Point(86, 104);
            this.nmrDigits.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmrDigits.Name = "nmrDigits";
            this.nmrDigits.Size = new System.Drawing.Size(45, 20);
            this.nmrDigits.TabIndex = 4;
            this.nmrDigits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmrDigits.ValueChanged += new System.EventHandler(this.nmrDigits_Validating);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(46, 108);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(36, 13);
            this.label22.TabIndex = 6;
            this.label22.Text = "Digits:";
            // 
            // cboFormat
            // 
            this.cboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFormat.FormattingEnabled = true;
            this.cboFormat.Location = new System.Drawing.Point(86, 69);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Size = new System.Drawing.Size(91, 21);
            this.cboFormat.TabIndex = 3;
            this.cboFormat.Validating += new System.ComponentModel.CancelEventHandler(this.cboFormat_Validating);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(40, 73);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(42, 13);
            this.label23.TabIndex = 4;
            this.label23.Text = "Format:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(86, 35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(91, 20);
            this.txtName.TabIndex = 2;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(44, 38);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(38, 13);
            this.label25.TabIndex = 0;
            this.label25.Text = "Name:";
            // 
            // ColumnSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(353, 265);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnSettingsDialog";
            this.Text = "Table Columns Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColumnSettingsDialog_FormClosing);
            this.Load += new System.EventHandler(this.ColumnSettingsDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ColumnSettingsDialog_KeyDown);
            this.panel1.ResumeLayout(false);
            this.grpColumnList.ResumeLayout(false);
            this.grpColumnList.PerformLayout();
            this.grpColumn5.ResumeLayout(false);
            this.grpColumn5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrDigits)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private StylishButton btnCancel;
        private StylishButton btnOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpColumnList;
        private System.Windows.Forms.ListBox listColumns;
        private System.Windows.Forms.GroupBox grpColumn5;
        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.TextBox txtCustomFormat;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown nmrDigits;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cboFormat;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckBox chkAutoWidth;
        private System.Windows.Forms.Label label1;
    }
}