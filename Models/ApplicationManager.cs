using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Eagles_Website.Models
{
    public class ApplicationManager : UserManager<ApplicationUser>
    {
        Context Context { get; set; }
        public ApplicationManager(Context context,IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.Context = context;
        }

        public void GiveTheUserCart(int UserId)
        {
            Cart cart = new Cart()
            {
                UserID = UserId,
                TotalPrice = 0
            };
            Context.Carts.Add(cart);
        }
    }
}
