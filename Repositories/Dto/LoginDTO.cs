using System.ComponentModel.DataAnnotations;

namespace Repositories.Dto
{
    public class LoginDTO
    {
		public int UserID { get; set; }
		public string? UserName { get; set; }
        public string? Password { get; set; }
		public string? Email { get; set; }
		public int RoleID { get; set; }
        public bool ISBLOCKED { get; set; }
        public bool ISACTIVED { get; set; }
        public bool ISDELETED { get; set; }
    }
    public class AccountDTO
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
		
		public string? Password { get; set; }
	
		public string? Email { get; set; }
	
		public string? FullName { get; set; }
		
		public string? Address { get; set; }
		
		public string? Phone { get; set; }
		
		public DateTime DateOfBirth { get; set; }
		public string? Image;
	}
	public class ContactDTO
	{

		public string? YourName { get;set; }
		
		public string? Youremail { get; set; }
	
		public string? Subject { get; set; }
		
		public string? Yourmessage { get; set; }
	}

	public class ForgotPasswordDTO
	{
		public string? Email { get;set; }
		public int UserExists { get; set; }
		public string? Password { get; set; }
	}
}
