using System.ComponentModel.DataAnnotations;

namespace PersonalPasswordManager.ModelView
{
    public class PasswordManager
    {
        [Key]
        public int PasswordManagerId { get; set; }
        public string? Category { get; set; }
        public string? App { get; set; }
        public string? UserName { get; set; }
        public string? EncryptedPassword { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get;set; }
    }
}
