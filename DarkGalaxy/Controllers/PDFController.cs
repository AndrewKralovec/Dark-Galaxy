using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Text;
using System.Net;

namespace DarkDarkGalaxy.Controllers
{
    public class PdfViewController : Controller
    {
        public ActionResult DownloadPdf()
        {
            WebClient wc = new WebClient();          
            string htmlText = wc.DownloadString("http://localhost:56188/Home/Calendar");
            string textpath = "e:/SIUC-201560/Dark Galaxy/DarkDarkGalaxy/DarkDarkGalaxy/DarkDarkGalaxy/misc/TextFile1.txt";
            byte[] pdfFile = this.ConvertHtmlTextToPDF(htmlText);
            string title = HttpContext.Request.Url.AbsolutePath;
            // string title = Request
            string pdfname = title + ".pdf";
            return File(pdfFile, "application/pdf", pdfname);
            if (System.IO.File.Exists(textpath))
            {
                using (StreamWriter sw = new StreamWriter(textpath, false, Encoding.UTF8))
                {
                    //sw.WriteLine(htmlText);
                    sw.WriteLine(pdfFile);
                }
            }
            return null;
        }

        public byte[] ConvertHtmlTextToPDF(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
            {
                return null;
            }
            //htmlText = htmlText + "</p>";
            

            MemoryStream outputStream = new MemoryStream();
            byte[] data = Encoding.UTF8.GetBytes(htmlText);
            MemoryStream msInput = new MemoryStream(data);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            doc.Open();

            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.Default);
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8);

            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            outputStream.Close();

            return outputStream.ToArray();

        }

    }
}