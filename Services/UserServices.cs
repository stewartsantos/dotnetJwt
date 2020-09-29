
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ApiJwt.Models;
using ApiJwt.Repositories;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace ApiJwt.Services
{
    public class UserService
    {
        public User Authenticate(string username, string password)
        {
            var user = UserRepository.ValidadeLogin(username, password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"appsettings.json")));
            var key = Encoding.ASCII.GetBytes(jAppSettings["Secret"].ToString());
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("Store", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }
    }
}