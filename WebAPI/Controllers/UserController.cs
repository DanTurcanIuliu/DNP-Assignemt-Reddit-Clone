using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Logic_Interfaces;
using Application.Provider_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Controllers;


    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserLogic userLogic;
        private readonly IUserProvider userProvider;
        private readonly IConfiguration config;

        public UsersController(IUserLogic userLogic, IUserProvider userProvider, IConfiguration config)
        {
            this.userLogic = userLogic;
            this.userProvider = userProvider;
            this.config = config;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
        {
            try
            {
                User user = await userLogic.CreateAsync(dto);
                return Created($"/users/{user.Id}", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
        {
            try
            {
                SearchUserParametersDto parameters = new(username);
                IEnumerable<User> users = await userProvider.GetAsync(parameters);
                return Ok(users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        private List<Claim> GenerateClaims(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("ID", user.Id.ToString()),
            };
            return claims.ToList();
        }
        
        private string GenerateJwt(User user)
        {
            List<Claim> claims = GenerateClaims(user);
    
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
            JwtHeader header = new JwtHeader(signIn);
    
            JwtPayload payload = new JwtPayload(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims, 
                null,
                DateTime.UtcNow.AddMinutes(60));
    
            JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
            string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return serializedToken;
        }
        
        [HttpPost, Route("login")]
        public async Task<ActionResult> Login([FromBody] UserCreationDto userLoginDto)
        {
            try
            {
                User user = await userLogic.ValidateUser(userLoginDto.UserName, userLoginDto.Password);
                string token = GenerateJwt(user);
    
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }