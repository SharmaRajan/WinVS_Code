using Microsoft.AspNetCore.Identity;

namespace BookStoreApp;

public interface IAccountRepo
{
    Task<IdentityResult> SignUpAsync(SignUpModel model);

    Task<string> LoginAsync(LoginModel loginModel);
}
