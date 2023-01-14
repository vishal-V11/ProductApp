using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Context;
using UserApi.Model;

namespace UserApiTest
{
    public class UserDbFixture
    {
        internal readonly UserDbContext _userDbContext;
        public UserDbFixture()
        {
            var userDbContextOptions = new DbContextOptionsBuilder<UserDbContext>().UseInMemoryDatabase("UserDb").Options;
            _userDbContext = new UserDbContext(userDbContextOptions);
            _userDbContext.Add(new User() { Id = 1, Name = "vishal", Email = "vishal110500@gmail.com", Password = "1234", Location = "Mumbai" });
            _userDbContext.Add(new User() { Id = 2, Name = "tejas", Email = "tejas@gmail.com", Password = "123", Location = "Pune" });
            _userDbContext.SaveChanges();
        }
    }
}
