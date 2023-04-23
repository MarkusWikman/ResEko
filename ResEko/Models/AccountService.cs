using Microsoft.AspNetCore.Identity;
using ResEko.Views.Home;

namespace ResEko.Models
{
    public class AccountService
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        public async Task<SignInResult> LoginAsync(LoginVM loginVM)
        {
            // Check if the user is an admin
            var adminUser = await userManager.FindByNameAsync(loginVM.Email);
            if (adminUser == null || !await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                return SignInResult.Failed;
            }

            // Sign in the user
            var result = await signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, false);
            return result;
        }
    }
}
