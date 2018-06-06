using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFTools
{
    public class Utility
    {
        public string ErrorMessage { get; set; }
        public PdfDocument UnlockPDF(string path)
        {
            PdfDocument result = null;
            try
            {
                using (var pdf = PdfReader.Open(path, PdfDocumentOpenMode.Import))
                using (result = new PdfDocument())
                {
                    foreach (PdfPage page in pdf.Pages)
                    {
                        result.AddPage(page);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
