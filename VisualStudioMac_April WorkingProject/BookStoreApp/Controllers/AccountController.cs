using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountRepo _accountRepo;

    public AccountController(IAccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel){
        var result = await _accountRepo.SignUpAsync(signUpModel);

        if (result.Succeeded){
            return Ok(result.Succeeded);
        }
        // return BadRequest(result.Errors);
        return Unauthorized();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel){

        var result = await _accountRepo.LoginAsync(loginModel);

        if (string.IsNullOrEmpty(result)){
            return Unauthorized();
        }

        return Ok(result);
    }

}

