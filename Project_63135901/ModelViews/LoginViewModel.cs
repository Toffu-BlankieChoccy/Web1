using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_63135901.ModelViews
{
	public class LoginViewModel
	{
		[Key]
		[MaxLength(100)]
		[Required(ErrorMessage = "Vui lòng nhập Email")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		[Display(Name = "Email")]
		public string UserName { get; set; }

		[Display(Name = "Mật khẩu")]
		[Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
		[MinLength(5, ErrorMessage = "Bạn cần đặt mật khẩu tối thiếu 5 kí tự")]
		public string Password { get; set; }	
	}
}
