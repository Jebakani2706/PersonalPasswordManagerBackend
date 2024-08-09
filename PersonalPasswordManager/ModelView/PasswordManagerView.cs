namespace PersonalPasswordManager.ModelView
{
    public class PasswordManagerView
    {
        public int PasswordManagerId { get; set; }
        public string? Category { get; set; }
        public string? App { get; set; }
        public string? UserName { get; set; }
        public string? DecryptedPassword { get; set; }
        public string? EncryptedPassword { get; set; }
    }
}
