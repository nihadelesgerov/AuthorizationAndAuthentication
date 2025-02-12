using HubCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace HubCommerce.AuthServices
{
    public class RegistrationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public RegistrationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<SignInResult> RegisterUser(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email
            };
            string RegistrationDate = Convert.ToString(DateTime.UtcNow);
            var claims = new List<Claim>
            {
                new Claim("IsNormalUser","true"),
                new Claim("IsAdmin","false"),
                new Claim("IsBanned","false"),
                new Claim("UserName",model.Email),
                new Claim("Email",model.Email),
                new Claim("RegistrationDate",RegistrationDate),
                new Claim("IsVipCustomer","false")
            };
            await userManager.CreateAsync(user,model.Password);
            await userManager.AddClaimsAsync(user, claims);
            await signInManager.SignInAsync(user, isPersistent: true, null);
            return SignInResult.Success;
        }
    }
}
