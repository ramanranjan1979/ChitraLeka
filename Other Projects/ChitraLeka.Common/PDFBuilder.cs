using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PdfSharp.Charting;
using PdfSharp.Internal;
using PdfSharp.Fonts;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;

namespace ChitraLeka.Common.PDF
{
    public class PDFBuilder
    {

        private PdfDocument document;
        private List<PdfPage> page = new List<PdfPage>();
        private List<XGraphics> graphics = new List<XGraphics>();

        public List<PdfPage> Page
        {
            get
            {
                return page;
            }
        }

        public List<XGraphics> Graphics
        {
            get
            {
                return graphics;
            }
        }

        public PdfDocument Document
        {
            get
            {
                return document;
            }
        }

        public void Setup(string name)
        {
            document = new PdfDocument();
            document.Info.Title = name;
            page.Add(document.AddPage());
            graphics.Add(XGraphics.FromPdfPage(page[0]));
            graphics[0].MUH = PdfFontEncoding.Unicode;
            graphics[0].MFEH = PdfFontEmbedding.None;

        }

        public void PageSizeToA5(int pageNumber)
        {
            page[pageNumber].Size = PdfSharp.PageSize.A5;
        }

        public void PageLandScape(int pageNumber)
        {
            page[pageNumber].Orientation = PdfSharp.PageOrientation.Landscape;
        }

        public void AddPage(int pageNumber)
        {
            page.Add(document.AddPage());
            graphics.Add(XGraphics.FromPdfPage(page[pageNumber]));
            graphics[pageNumber].MUH = PdfFontEncoding.Unicode;
            graphics[pageNumber].MFEH = PdfFontEmbedding.None;
        }

        public void AddText(string text, double x, double y, XFont font, XSolidBrush colour, int pageNumber)
        {
            graphics[pageNumber].DrawString(text, font, colour, x, y);
        }

        public void DrawImage(string imageAddress, double x, double y, double width, double height, int pageNumber)
        {
            if (File.Exists(imageAddress))
            {
                XImage image = XImage.FromFile(imageAddress);
                graphics[pageNumber].DrawImage(image, x, y, width, height);
            }
        }

        public void Save(string address)
        {
            document.Save(address);
        }

        public void Send(HttpContextBase page, string fileName)
        {
            try
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream, false);
                    page.Response.Clear();
                    page.Response.AddHeader("Content-Disposition", "filename=" + fileName + ".pdf");
                    page.Response.ContentType = "application/pdf";
                    page.Response.AddHeader("contrnt-length", stream.Length.ToString());
                    page.Response.BinaryWrite(stream.ToArray());
                    //page.Response.Flush();
                    page.Response.End();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void EndGraphics()
        {
            foreach (XGraphics thisGraphics in graphics)
            {
                thisGraphics.Dispose();
            }
        }

    }
}