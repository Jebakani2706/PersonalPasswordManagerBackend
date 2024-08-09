using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalPasswordManager.ModelView;
using PersonalPasswordManager.Service;
using EncryptionDecryption = PersonalPasswordManager.CommonFunctions.EncryptionDecryption;

namespace PersonalPasswordManager.Controller
{
    [Route("api/[controller]")]
    [ ApiController]
    public class PasswordManagerController : ControllerBase
    {
        private readonly PasswordManagerService _passwordManagerService;

        public PasswordManagerController(PasswordManagerService passwordManagerService) { 
            _passwordManagerService = passwordManagerService;
        }

        #region Get all the passwords
        /// <summary>
        ///  Get all the passwords that is managed
        /// </summary>
        /// <returns>
        ///  returns the list of password with all the details like user name , encrypted password etc..
        /// </returns>
        [HttpGet]
        [Route("GetAllPassword")]
        public IActionResult GetAllPassword()
        {
            try
            {
                List<PasswordManager> passwordList = _passwordManagerService.GetAllPassword();
                return Ok(new
                {
                    StatusCode = "SUCCESS",
                    StatusText = "SUCCESS",
                    Data = passwordList
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    StatusCode = "FAILURE",
                    StatusText = "Something went wrong",
                });
            }
        }
        #endregion

        #region Get the password with the unique id
        /// <summary>
        ///  Get the password with the unique id
        /// </summary>
        /// <param name="id">Unique Id - PasswordManagerId</param>
        /// <param name="decrypt">Boolean value to check the type of password to be returned (encrypted = false / decrypted = true)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPasswordById")]
        public IActionResult GetPasswordById(int id , bool decrypt)
        {
            try
            {
                PasswordManagerView? password = _passwordManagerService.GetPassword(id);
                if(decrypt == true && password != null)
                {
                    password.DecryptedPassword = EncryptionDecryption.DecryptString(password.EncryptedPassword ?? "");
                }
                return Ok(new
                {
                    StatusCode = "SUCCESS",
                    StatusText = "SUCCESS",
                    Data = password
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    StatusCode = "FAILURE",
                    StatusText = "Something went wrong",
                });
            }
        }
        #endregion

        #region Add the password data
        /// <summary>
        /// Api to add the password data
        /// </summary>
        /// <param name="passwordManagerView">Password manager view contains the data such as  
        /// Eg:
        /// {
        ///  "category": "Work",
        ///  "app": "Messenger",
        ///  "userName": "testuser@gmail.com",
        ///  "encryptedPassword": "test@123$"
        ///  }</param>
        /// <returns>returns the stored password id</returns>
        [HttpPost]
        [Route("AddPassword")]
        public async Task<IActionResult> AddPassword(PasswordManagerView passwordManagerView)
        {
            try
            {
                passwordManagerView.EncryptedPassword = EncryptionDecryption.EncryptString(passwordManagerView.DecryptedPassword);
                int passwordId = await _passwordManagerService.AddPassword(passwordManagerView);
                if(passwordId > 0)
                {
                    return Ok(new
                    {
                        StatusCode = "SUCCESS",
                        StatusText = "Password Added Successfully",
                        Data = passwordId
                    });
                }
                return Ok(new
                {
                    StatusCode = "FAILURE",
                    StatusText = "Something went wrong",
                    Data = 0
                }) ;
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    StatusCode = "FAILURE",
                    StatusText = "Something went wrong",
                });
            }
        }
        #endregion

        #region update the password data
        /// <summary>
        /// Api to update the password data
        /// </summary>
        /// <param name="passwordManagerView">Password manager view contains the data such as  
        /// Eg:
        /// {
        ///  "passwordManagerId":1
        ///  "category": "Work",
        ///  "app": "Messenger",
        ///  "userName": "testuser@gmail.com",
        ///  "encryptedPassword": "test@123$"
        ///  }</param>
        /// <returns>returns the stored password id</returns>
        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(PasswordManagerView passwordManagerView)
        {
            try
            {
                passwordManagerView.EncryptedPassword = EncryptionDecryption.EncryptString(passwordManagerView.DecryptedPassword);
                int passwordId = await _passwordManagerService.UpdatePassword(passwordManagerView);
                if (passwordId > 0)
                {
                    return Ok(new
                    {
                        StatusCode = "SUCCESS",
                        StatusText = "Password Updated Successfully",
                        Data = passwordId
                    });
                }
                return Ok(new
                {
                    StatusCode = "FAILURE",
                    StatusText = "Password Not Found",
                    Data = 0
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    StatusCode = "FAILURE",
                    StatusText = "Something went wrong",
                });
            }
        }
        #endregion

        #region Delete the password
        /// <summary>
        ///  Delete the password record from the database.
        /// </summary>
        /// <param name="id">Unique Id - PasswordManagerId</param>
        /// <returns>returns the bool value. If true then deletion process completed successfully , else failed</returns>
        [HttpDelete]
        [Route("DeletePassword")]
        public async Task<IActionResult> DeletePassword(int id)
        {
            try
            {
                bool deleteResult = await _passwordManagerService.DeletePassword(id);
                if (deleteResult)
                {
                    return Ok(new
                    {
                        StatusCode = "SUCCESS",
                        StatusText = "Password Deleted Successfully",
                        Data = deleteResult
                    });
                }
                return Ok(new
                {
                    StatusCode = "FAILURE",
                    StatusText = "Password Not Found",
                    Data = false
                });
            }
            catch (Exception)
            {
                return Ok(new
                {
                    StatusCode = "FAILURE",
                    StatusText = "Something went wrong",
                });
            }
        }
        #endregion
    }
}
