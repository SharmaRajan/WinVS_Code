using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApp;

public class AccountRepository : IAccountRepo
{

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly IConfiguration _configuration;

    public AccountRepository(UserManager<ApplicationUser> userManager, 
                    SignInManager<ApplicationUser> signInManager,
                    IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> SignUpAsync(SignUpModel model){

        var user = new ApplicationUser(){
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email
        };

        return await _userManager.CreateAsync(user,model.Password);
    }

    public async Task<string> LoginAsync(LoginModel loginModel){
        var result = await _signInManager.PasswordSignInAsync(loginModel.Email,loginModel.Password,false,false);

        if(!result.Succeeded){
            return null;
        }

        var authClaims = new List<Claim>{
            new Claim(ClaimTypes.Name,loginModel.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };

        var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer : _configuration["JWT:ValidIssuer"],
            audience : _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims : authClaims,
            signingCredentials: new SigningCredentials(authSignInKey,SecurityAlgorithms.HmacSha256Signature)
        );

         
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
