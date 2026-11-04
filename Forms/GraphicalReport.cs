//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataFit.Forms
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
            Clipboard.SetImage(image);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(saveImageDialog.FileName);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.Print();
            }
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
