using Microsoft.EntityFrameworkCore;
using PersonalPasswordManager.ModelView;
using System.Collections.Generic;

namespace PersonalPasswordManager.DBContext
{
    public class PasswordManagerDBContext : DbContext
    {
        public PasswordManagerDBContext(DbContextOptions<PasswordManagerDBContext> options) : base(options)
        { }
        public DbSet<PasswordManager> PasswordManager { get; set; }
    }
}
