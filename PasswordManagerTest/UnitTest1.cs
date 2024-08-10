using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalPasswordManager.Controller;
using PersonalPasswordManager.DBContext;
using PersonalPasswordManager.ModelView;
using PersonalPasswordManager.Repository;
using PersonalPasswordManager.Service;


namespace PasswordManagerTest
{
    public class GetAllPasswordTest
    {
        PasswordManagerController passwordManagerController;
        PasswordManagerService passwordManagerService;
        IPasswordManagerRepository passwordManagerRepository;
        private readonly PasswordManagerDBContext _context;

        public GetAllPasswordTest()
        {
            var options = new DbContextOptionsBuilder<PasswordManagerDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            _context = new PasswordManagerDBContext(options);
            passwordManagerRepository = new PasswordManagerRepository(_context);
            passwordManagerService = new PasswordManagerService(passwordManagerRepository);
            SeedDatabase();
        }
        private void SeedDatabase()
        {
            _context.PasswordManager.Add(new PasswordManager
            {
                Category = "work",
                App = "outlook",
                UserName = "testuser@mytest.com",
                EncryptedPassword = "encryptedPassword"
            });
            _context.SaveChanges();
        }
        [Fact] 
        public void GetAllPasswordTest1()
        {
            //Arrange
            //Act
            var result = passwordManagerService.GetAllPassword();
            //Assert
            Assert.IsType<List<PasswordManager>>(result);
            var listPassword= result as List<PasswordManager>;
            Assert.Equal(1, listPassword?.Count);
        }
    }
}