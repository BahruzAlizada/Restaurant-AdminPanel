using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewsModel
{
    public class ResetPasswordVM
    {
        public string Id { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string CheckPassword { get; set; }   
    }
}
