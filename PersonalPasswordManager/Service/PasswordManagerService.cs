using PersonalPasswordManager.ModelView;
using PersonalPasswordManager.Repository;

namespace PersonalPasswordManager.Service
{
    public class PasswordManagerService
    {
        private readonly IPasswordManagerRepository _passwordManagerRepository;

        public PasswordManagerService(IPasswordManagerRepository passwordManagerRepository)
        {
            _passwordManagerRepository = passwordManagerRepository;
        }

        public List<PasswordManager> GetAllPassword()
        {
            return _passwordManagerRepository.GetAllPassword();
        }

        public PasswordManagerView? GetPassword(int passwordManagerId)
        {
            PasswordManager? passwordManager = _passwordManagerRepository.GetPassword(passwordManagerId);
            PasswordManagerView? passwordManagerView = passwordManager == null ? null : new PasswordManagerView()
            {
                PasswordManagerId = passwordManagerId,
                EncryptedPassword = passwordManager.EncryptedPassword,
                UserName = passwordManager.UserName,
                Category = passwordManager.Category,
                App = passwordManager.App
            };
            return passwordManagerView;
        }

        public async Task<int> AddPassword(PasswordManagerView passwordManager)
        {
            return  await _passwordManagerRepository.AddOrUpdatePassword(passwordManager);
        }

        public async Task<int> UpdatePassword(PasswordManagerView passwordManager)
        {
            return await _passwordManagerRepository.AddOrUpdatePassword(passwordManager);
        }

        public async Task<bool> DeletePassword(int passwordManagerId)
        {
            return await _passwordManagerRepository.DeletePassword(passwordManagerId);
        }
    }
}
