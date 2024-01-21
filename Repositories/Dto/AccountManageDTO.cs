namespace Repositories.Dto
{
    public class AccountManageDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? TimeStamp { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
    public class BlockAccount
    {
        public int UserId { get; set; }
        public int TimeBlock { get; set; }
        public bool IsBlocked { get; set; }
    }
}
