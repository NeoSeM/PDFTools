using PdfSharp.Pdf;
using PDFTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UnlockPDF
{
    public partial class UnlockPdf : Form
    {
        Utility pdfUtil = new Utility();
        public UnlockPdf()
        {
            InitializeComponent();
            this.Paint += UnlockPdf_Paint;

            browseBtn.FlatStyle = FlatStyle.Flat;

            unlockBtn.FlatStyle = FlatStyle.Flat;
            unlockBtn.FlatAppearance.BorderSize = 1;

            label1.BackColor = System.Drawing.Color.Transparent;
        }

        private void UnlockPdf_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            //the rectangle, the same size as our Form
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);

            //define gradient's properties
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(22, 59, 71), Color.FromArgb(57, 128, 227), 65f);

            //apply gradient         
            graphics.FillRectangle(b, gradient_rectangle);
        }

        private void SavePdfDocument(PdfDocument pdf, string fileName)
        {
            saveFileDialog1.FileName = fileName;
            saveFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                pdf.Save(saveFileDialog1.OpenFile(), true);
                pdf.Dispose();
            }
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                textBox1.Text = path;
            }
        }

        private void unlockBtn_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;

            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                var resultPdf = pdfUtil.UnlockPDF(path);
                if(resultPdf != null && MessageBox.Show("Unlocked!") == DialogResult.OK)
                {
                    SavePdfDocument(resultPdf, Path.GetFileName(path));
                }
            }
            else
            {
                MessageBox.Show($"Incorrect File or Path! (Path: {path})");
            }
        }
    }
}
