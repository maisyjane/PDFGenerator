
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using PDF_Generator.Utility;
using PDFGenerator.Models;
using System;
using System.IO;

namespace PDF_Generator.Controllers
{
    [Route("api/pdfcreator")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {

        private IConverter _converter;
        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpPost]
        public IActionResult CreatePDF([FromBody] PDFData data)
        {
            var documentId = Guid.NewGuid();

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                
                DocumentTitle = "PDF Report",
                Out = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/" + documentId.ToString() + ".pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(data),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Created On: " + DateTime.Now.ToShortDateString() }

            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);

            return Ok("Successfully created PDF document " + documentId.ToString() + ".pdf");
        }

    }
}
