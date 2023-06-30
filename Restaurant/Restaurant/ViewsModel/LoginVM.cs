using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewsModel
{
	public class LoginVM
	{
		[Required(ErrorMessage ="Bu xana boş ola bilməz")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Password { get; set; }
		public bool IsRemember { get; set; }
	}
}
