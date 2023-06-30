using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewsModel
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "Email can not be null")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
