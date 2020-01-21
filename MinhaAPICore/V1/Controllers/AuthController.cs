using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Business.Servicos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinhaAPICore.Controllers;
using MinhaAPICore.Extensoes;
using MinhaAPICore.ViewModels;

namespace MinhaAPICore.V1.Controllers
{    
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(INotificador notificador,
                              SignInManager<IdentityUser> signInManager, 
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appsettings,
                              IUser appuser) : base(notificador, appuser)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appsettings.Value;
        }

        //[HttpPost("nova-conta")]
        //public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        //{
        //    if (!ModelState.IsValid) return CustomResponse(ModelState);

        //    var user = new IdentityUser() {
        //        UserName = registerUser.Email,
        //        Email = registerUser.Email,
        //        EmailConfirmed = true
        //    };

        //    IdentityResult resultado = await _userManager.CreateAsync(user, registerUser.Password);

        //    if (resultado.Succeeded)
        //    {
        //        await _signInManager.SignInAsync(user, isPersistent: false);
        //        return CustomResponse( await GerarJwt(user.Email));
        //    }

        //    foreach (var error in resultado.Errors)
        //    {
        //        NotificarErro(error.Description);
        //    }
            
        //    return CustomResponse(registerUser);
        //}

        //[HttpPost("entrar")]
        //public async Task<ActionResult> Login(LoginUserViewModel loginUserViewModel)
        //{
        //    if (!ModelState.IsValid) return CustomResponse(ModelState);

        //    var resultado = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, 
        //                                                             loginUserViewModel.Password, 
        //                                                             isPersistent: false, 
        //                                                             lockoutOnFailure: true
        //                                                            );

        //    if (resultado.Succeeded)
        //    {
        //        return CustomResponse(await GerarJwt(loginUserViewModel.Email));
        //    }

        //    if (resultado.IsLockedOut)
        //    {
        //        NotificarErro(msg: "Usuário bloqueado");
        //        return CustomResponse(loginUserViewModel);
        //    }

        //    NotificarErro("Usuário ou senha incorretos");
        //    return CustomResponse(loginUserViewModel);
        //}

        //private string GerarJwt()
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key          = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        //    {
        //        Issuer             = _appSettings.Emissor,
        //        Audience           = _appSettings.ValidoEm,
        //        Expires            = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    });

        //    var encodedToken = tokenHandler.WriteToken(token);

        //    return encodedToken;
        //}

        //private async Task<LoginResponseViewModel> GerarJwt(string email)
        //{
        //    var usuario = await _userManager.FindByEmailAsync(email);
        //    var claims = await _userManager.GetClaimsAsync(usuario);
        //    var userRoles = await _userManager.GetRolesAsync(usuario);

        //    claims.Add(new Claim(type: JwtRegisteredClaimNames.Sub, value: usuario.Id));
        //    claims.Add(new Claim(type: JwtRegisteredClaimNames.Email, value: usuario.Email));
        //    claims.Add(new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()));
        //    claims.Add(new Claim(type: JwtRegisteredClaimNames.Nbf, value: ToUnixEpochDate(DateTime.UtcNow).ToString()));
        //    claims.Add(new Claim(type: JwtRegisteredClaimNames.Iat, value: ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));


        //    foreach (var userRole in userRoles)
        //    {
        //        claims.Add(new Claim(type: "role", value: userRole));
        //    }

        //    var identityClaims = new ClaimsIdentity();
        //    identityClaims.AddClaims(claims);

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        //    {
        //        Issuer = _appSettings.Emissor,
        //        Audience = _appSettings.ValidoEm,
        //        Subject = identityClaims,
        //        Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    });

        //    var encodedToken = tokenHandler.WriteToken(token);

        //    var response = new LoginResponseViewModel
        //    {
        //         AccessToken = encodedToken,
        //         ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
        //         UserToken = new UserTokenViewModel {
        //              Id = usuario.Id,
        //              Email = usuario.Email,
        //              Claims = claims.Select(c => new ClaimViewModel
        //              {
        //                Type = c.Type,
        //                Value = c.Value
        //              })
        //         }
        //    };

        //    return response;
        //}

        //private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}