using SAE401_API_Vinted.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SAE401_API_Vinted.Models.EntityFramework;
using System.Security.Cryptography;

namespace SAE401_API_Vinted.Controllers
{

    public class VintieDTO
    {
        public string Pseudo { get; set; }
        public string Pwd { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly VintedDBContext _context;

        public LoginController(IConfiguration config, VintedDBContext dBContext)
        {
            _config = config;
            _context = dBContext;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] VintieDTO login)
        {
            IActionResult response = Unauthorized();
            Vintie user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
        private Vintie AuthenticateUser(VintieDTO vintie)
        {
           return _context.Vinties.SingleOrDefault(x => x.Pseudo.ToUpper() == vintie.Pseudo.ToUpper() &&
           x.Pwd == ComputeSha256Hash(vintie.Pwd));
        }

        private string GenerateJwtToken(Vintie userInfo)
        {
            var securityKey = new
           SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, userInfo.Pseudo),
                 new Claim("role","User"),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        static public string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
