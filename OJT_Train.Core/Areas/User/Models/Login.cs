using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OJT_Train.Core.Areas.User.Models
{
	public class Login
	{
		public int UserID { get; set; }
		[Required]
		[DisplayName("User Name")]
		[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Please do not input special character")]
		public string? UserName { get; set; }
		[Required]
		[DisplayName("Password")]
		public string? Password { get; set; }


		[ValidateNever]
		public int RoleID { get; set; }
		[ValidateNever]
		public bool ISBLOCKED { get; set; }
		[ValidateNever]
		public bool ISACTIVED { get; set; }
		[ValidateNever]
		public bool ISDELETED { get; set; }
	}
	public class Account
	{
		public int UserID { get; set; }
		[Required]
		[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Please do not input special character")]
		public string? UserName { get; set; }
		[Required]
		public string? Password { get; set; }
		[Required]
		[Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
		public string? ComfirmPassword { get; set; }
		[Required]
		public string? Email { get; set; }
		[Required]
		public string? FullName { get; set; }
		[Required]
		public string? Address { get; set; }
		[Required]
		[RegularExpression("^0[0-9]{9}$", ErrorMessage = "Phone must be exactly 10 digits.")]
		public string? Phone { get; set; }
		[Required]
		public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }
    }
	public class Contact
	{
		[Required]
		public string? YourName { get; set; }
        [Required]
        public string? Youremail { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Yourmessage { get; set; }
	}
	public class ForgotPassword
	{
		[Required]
		public string? Email { get; set; }
		public int UserExists { get; set; }
		public string? Password { get; set; }
	}

	public class UserInfoPayment
	{

	}
}
