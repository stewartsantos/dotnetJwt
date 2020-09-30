
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

            user.Token = TokenService.GenerateToken(user);

            user.Password = null;

            return user;
        }
    }
}