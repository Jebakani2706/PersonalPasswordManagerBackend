using PersonalPasswordManager.DBContext;
using PersonalPasswordManager.ModelView;

namespace PersonalPasswordManager.Repository
{
    public interface IPasswordManagerRepository
    {
        List<PasswordManager> GetAllPassword();
        PasswordManager? GetPassword(int passwordManagerId);
        Task<int> AddOrUpdatePassword(PasswordManagerView passwordManager);
        Task<bool> DeletePassword(int passwordManagerId);
    }
    public class PasswordManagerRepository : IPasswordManagerRepository
    {
        private readonly PasswordManagerDBContext _dbContext;
        public PasswordManagerRepository(PasswordManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PasswordManager> GetAllPassword()
        {
            return _dbContext.PasswordManager.ToList();
        }
        public PasswordManager? GetPassword(int passwordManagerId)
        {
            return _dbContext.PasswordManager.FirstOrDefault(x => x.PasswordManagerId == passwordManagerId);
        }

        public async Task<int> AddOrUpdatePassword(PasswordManagerView passwordManager)
        {
            PasswordManager? password = passwordManager.PasswordManagerId == 0 ? new PasswordManager() : GetPassword(passwordManager.PasswordManagerId);
            if (password != null)
            {
                password.App = passwordManager.App;
                password.Category = passwordManager.Category;
                password.UserName = passwordManager.UserName;
                password.EncryptedPassword = passwordManager.EncryptedPassword;
                if (password.PasswordManagerId == 0)
                {
                    password.CreatedOn = DateTime.UtcNow;
                    await _dbContext.PasswordManager.AddAsync(password);
                }
                else
                {
                    password.ModifiedOn = DateTime.UtcNow;
                    _dbContext.Update(password);
                }
                await _dbContext.SaveChangesAsync();
                return password.PasswordManagerId;
            }
            return -1;
        }

        public async Task<bool> DeletePassword(int passwordManagerId)
        {
            PasswordManager? password = GetPassword(passwordManagerId);
            if (password == null)
            {
                return false;
            }
            _dbContext.PasswordManager.Remove(password);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
