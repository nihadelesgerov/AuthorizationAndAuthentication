using HubCommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace HubCommerce.AuthServices
{
    public class LoginService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly Logger<LoginService> logger;

        public LoginService(SignInManager<ApplicationUser> signInManager,Logger<LoginService> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }
        public async Task<SignInResult> LoginUser(LoginModel model)
        {
            await signInManager.PasswordSignInAsync(model.Email, model.Password,isPersistent:true,lockoutOnFailure:true);
            logger.LogInformation($"User with email : {model.Email} logged in succesfully");
            return SignInResult.Success;
        }
    }
}
