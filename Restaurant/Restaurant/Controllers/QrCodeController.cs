using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Restaurant.Helpers;
using Restaurant.Models;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class QrCodeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public QrCodeController(IWebHostEnvironment env)
        {
            _env=env;
        }

		#region Create
		[HttpGet]
		public IActionResult Create()
		{
			QRCodeModel code = new QRCodeModel();
			return View(code);
		}

		[HttpPost]
		public IActionResult Create(QRCodeModel code)
		{
			QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
			QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(code.QrCodeText, QRCodeGenerator.ECCLevel.Q);
			QRCode qRCode = new QRCode(qRCodeData);
			Bitmap qrbitmap = qRCode.GetGraphic(60);
			byte[] bitMapArray = qrbitmap.BitmapToByteArray();
			string Url = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bitMapArray));
			code.returnQRUrl = Url;
			return View(code);
		}
		#endregion

		#region Download
		[HttpPost]
		public async Task<IActionResult> QrCodeImgDownload(IFormFile qrCodeFile)
		{
			if (qrCodeFile != null && qrCodeFile.Length > 0)
			{
				string folder = Path.Combine(_env.WebRootPath, "assets", "img", "qrcode");
				string filePath = Path.Combine(folder, "qrcode.png");

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await qrCodeFile.CopyToAsync(fileStream);
				}
			}

			return View();
		}
		#endregion

	}
}
