using System.Collections.Generic;
using System.Linq;
using ApiJwt.Models;

namespace ApiJwt.Repositories
{
  public class UserRepository
  {
    public static User ValidadeLogin(string username, string password)
    {
      var users = UserRepository.All();
      return users.Where(u => u.Username.ToLower() == username.ToLower() && u.Password.ToLower() == password.ToLower()).First();
    }

    public static List<User> All()
    {
      var users = new List<User>();
      users.Add(new User(){Id = 1, Username = "didox", Password = "123456", Role="Administrador"});
      users.Add(new User(){Id = 2, Username = "bruno", Password = "123456", Role="Editor"});
      return users;
    }
  }
}