using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.ViewsModel
{
    public class UpdateVM
    {  
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
