using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using OnlineShop.Domain.Concrete;
using OnlineShop.Domain.Entities;
using OnlineShop.WebUI.Models;

namespace OnlineShop.WebUI
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }


    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<EFDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = false;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
        public async Task<IdentityResult> ChangePersonalData(IndexViewModel model, string userId)
        {
            try
            {
                using (EFDbContext context = new EFDbContext())
                {
                    var cUser = context.Users.Find(userId);

                    var address = context.Addresses.FirstOrDefault(x => 
                                      x.City == model.Address.City &&
                                      x.Country == model.Address.Country &&
                                      x.Line1 == model.Address.Line1 &&
                                      x.Line2 == model.Address.Line2 &&
                                      x.Line3 == model.Address.Line3 &&
                                      x.State == model.Address.State &&
                                      x.Zip == model.Address.Zip
                                  ) ?? model.Address;

                    cUser.Address = address;
                    cUser.FirstName = model.FirstName;
                    cUser.LastName = model.LastName;
                    cUser.PhoneNumber = model.PhoneNumber;

                    await context.SaveChangesAsync();
                }
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public string GetFirstName(string userId)
        {
            try
            {
                using (EFDbContext context = new EFDbContext())
                {
                    return context.Users.Find(userId).FirstName;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public string GetLastName(string userId)
        {
            try
            {
                using (EFDbContext context = new EFDbContext())
                {
                    return context.Users.Find(userId).LastName;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public Address GetAddress(string userId)
        {
            try
            {
                using (EFDbContext context = new EFDbContext())
                {
                    var user = context.Users.Find(userId);
                    return context.Addresses.Find(user.AddressId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
