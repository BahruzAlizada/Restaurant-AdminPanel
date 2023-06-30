using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class QRCodeModel
    {
        public string QrCodeText { get; set; }
        public string returnQRUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
