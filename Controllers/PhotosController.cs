using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using PdfSharpCore.Drawing;
using System.Drawing;

namespace apiPhotos.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PhotosController : ControllerBase
    {
        private readonly ILogger<PhotosController> _logger;

        public PhotosController(ILogger<PhotosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        async public Task<ActionResult> DownloadPhotos()
        {
            using(var webClient = new WebClient())
            {
                var url = "https://i.pinimg.com/564x/64/5d/70/645d70745390ff2b08280ace9a650d77.jpg";
                byte[] imageUrl = webClient.DownloadData(url);

                return File(imageUrl, "image/png", "teste.png");
            }

            
        }

        [HttpGet]
        async public Task<ActionResult> DownloadPhotos2()
        {
            using(var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                using(var webClient = new WebClient())
                {
                    var page = doc.AddPage();
                    page.Size =  PdfSharpCore.PageSize.A4;
                    page.Orientation = PdfSharpCore.PageOrientation.Portrait;

                    var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                    var corFonte = PdfSharpCore.Drawing.XBrushes.Black;

                    var textFormatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                    var fonteOrganzacao = new PdfSharpCore.Drawing.XFont("Arial", 10);

                    String url = "https://i.pinimg.com/564x/64/5d/70/645d70745390ff2b08280ace9a650d77.jpg";
                    byte[] imagemUrl = webClient.DownloadData(url); 
            
                    XImage imagem = XImage.FromStream(() => new MemoryStream(imagemUrl));
                    graphics.DrawImage(imagem, 20, 20, 300, 700);


                    using (MemoryStream stream = new MemoryStream())
                    {
                        var contantType = "application/pdf";
                        doc.Save(stream, false);

                        var nomeArquivo = "RelatorioTeste.pdf";

                        return File(stream.ToArray(), contantType, nomeArquivo);
                    }
                }
            }
        }
    }
}
