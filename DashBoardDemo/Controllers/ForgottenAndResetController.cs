using DashBoardDemo.Interface;
using DashBoardDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DashBoardDemo.Controllers
{
    public class ForgottenAndResetController : Controller
    {


        private readonly ILogger<ForgottenAndResetController> _logger;
        private readonly ILoginDetailsService loginDetailsService;
        private readonly IEncryptionService encryptionService ;




        public ForgottenAndResetController(ILogger<ForgottenAndResetController> logger, ILoginDetailsService loginDetailsService, IEncryptionService encryptionService)
        {
            _logger = logger;
            this.loginDetailsService = loginDetailsService;
            this.encryptionService = encryptionService;

        }

        /*//Encrypt & Decrypt
        [HttpGet]
        public IActionResult EncryptNumber(int number)
        {
            string encryptedValue = encryptionService.Encrypt(number);
            // Use the encrypted value as needed
            return Ok(encryptedValue);
        }


        [HttpGet]
        public IActionResult DecryptNumber(string encryptedValue)
        {
            int decryptedValue = encryptionService.Decrypt(encryptedValue);
            return Ok(decryptedValue);
        }*/


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult OtpForPassword()
        {
            return View();
        }

       
        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            var emailExists = await loginDetailsService.VerifyEmail(email);

            if (emailExists != null)
            {
                // Email is valid
                // Perform further actions or redirect to the next step
                return Ok(emailExists);
            }
            else
            {
                // Email is not valid
                // Display an error message or redirect to an error page
                return Ok(null);
            }
        }

       


        [HttpPost]
        public async Task<IActionResult> VerifyOTP(int userId,int otp)
        {
            var isOTPValid = await loginDetailsService.VerifyOTP(userId,otp);

            if (isOTPValid != null)
            {
                // Email is valid
                // Perform further actions or redirect to the next step
                return Ok(isOTPValid);
            }
            else
            {
                // Email is not valid
                // Display an error message or redirect to an error page
                return Ok(null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePasswordForpatient(int userId, string newPassword)
        {
            bool UserExist = await loginDetailsService.UpdatePassword(userId, newPassword);

            if (UserExist)
            {
                // Email is valid
                // Perform further actions or redirect to the next step
                return Ok(true);
            }
            else
            {
                // Email is not valid
                // Display an error message or redirect to an error page
                return Ok(false);
            }
        }

    }
}
