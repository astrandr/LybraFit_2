//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class GraphicalReport : Form
    {
        private Bitmap image;
        private PrintDocument printDocument = null;
        public Point LeftTopPos;

        public GraphicalReport()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(Bitmap reportImage)
        {
            this.image = reportImage;

            this.pictView.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictView.Image = image;
            return this.ShowDialog();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
#if DEMO
            InfoBox.ShowMessage("Demo version does not support Copy of the report", "Warning", this);
#else
            Clipboard.SetImage(image);
#endif
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
#if DEMO
            InfoBox.ShowMessage("Demo version does not support Save of the report", "Warning", this);
#else
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(saveImageDialog.FileName);
            }
#endif
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
#if DEMO
            InfoBox.ShowMessage("Demo version does not support Print of the report", "Warning", this);
#else
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.Print();
            }
#endif
        }

        private void PrintPage(object sender, PrintPageEventArgs args)
        {
            args.Graphics.DrawImage(image, 0, 0);
        }

        private void GraphicalReport_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
        }

        private void GraphicalReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }

            if (e.KeyCode == Keys.F1)
            {
                Help.ShowHelp(this, "LybraFitHelp.chm");
            }
        }
    }
}
