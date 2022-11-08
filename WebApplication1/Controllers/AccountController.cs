using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data.Repository.Interfaces;
using WebApplication1.Dtos;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto userDto)
        {
            var user = await _unitOfWork.UserRepository.Authenticate(userDto.UserName, userDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid User ID or Password");
            }

            var loginRes = new LoginResDto
            {
                UserName = userDto.UserName,
                Token = CreateJWT(user)
            };

            return Ok(loginRes);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequestDto userDto)
        {
            //if (string.IsNullOrEmpty(userDto.UserName.Trim()) || string.IsNullOrEmpty(userDto.Password.Trim()))
            if (userDto.UserName.IsEmpty() || userDto.Password.IsEmpty())
            {
                return BadRequest("userName or password can't be blank!!");
            }

            if (await _unitOfWork.UserRepository.UserAlreadyExists(userDto.UserName))

                return BadRequest("User Already Exists, please try something else..");

            _unitOfWork.UserRepository.Register(userDto.UserName, userDto.Password);
            await _unitOfWork.SaveAsync();
            return Ok(201);
        }

        private string CreateJWT(User user)
        {
            //secret key
            var secretKey = _configuration.GetSection("AppSettings:Key").Value;

            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            //Claims[] for taking the user information.
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            //HmacSha256Signature => means it takes till 256 characters for the key..
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            //token information
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
