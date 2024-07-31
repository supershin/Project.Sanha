using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Project.Sanha.Web.Common;
using QRCoder;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Sanha.Web.Controllers
{
    public class DecodingController : BaseController
    {
        private readonly IHostEnvironment _hosting;
        public DecodingController(IHostEnvironment hostEnvironment)
        {
            _hosting = hostEnvironment;
        }

        public IActionResult Index(string param)
        {

            //string value = HashHelper.DecodeFrom64(param);
            //var Array = value.Split(':');

            string baseUrl = _hosting.ContentRootPath;
            string url = baseUrl + Url.Action("Index", "Information", new { param = param });

            string qrUri = genQR(url);

            ViewBag.QrUri = qrUri;
            return View();
        }

        public IActionResult Encrypt(string param)
        {
            string md5Hash = HashHelper.Encrypt(param);

            return View();
        }

        public IActionResult GenQR(string param)
        {
            //string qrUri = genQR(param);

            //ViewBag.QrUri = qrUri;
            return View();
        }

        //public ActionResult GenerateQRCode(String qrText)
        //{
        //    // generating a barcode here. Code is taken from QrCode.Net library
        //    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
        //    QrCode qrCode = new QrCode();
        //    qrEncoder.TryEncode(qrText, out qrCode);
        //    GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);

        //    Stream memoryStream = new MemoryStream();
        //    renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);

        //    // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
        //    memoryStream.Position = 0;

        //    var resultStream = new FileStreamResult(memoryStream, "image/png");
        //    resultStream.FileDownloadName = String.Format("{0}.png", qrText);

        //    return resultStream;
        //}

        #region Gen QR
        private string genQR(string token)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(token, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(qrCodeData);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = ImageToByte(QrBitmap);
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));

            return QrUri;
        }
        public static byte[] GenerateByteArray(string url)
        {
            var image = GenerateImage(url);
            return ImageToByte(image);
        }

        public static System.Drawing.Bitmap GenerateImage(string url)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            //using (QRCode qrCode = new QRCode(qrCodeData))
            //{
            //    Bitmap qrCodeImage = qrCode.GetGraphic(20);
            //}
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10);
            return qrCodeImage;
        }

        private static byte[] ImageToByte(System.Drawing.Image img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
        #endregion
    }
}

