using UserApi.Model;
using UserApi.Repository;
using UserApi.Service;

namespace UserApiTest
{
    public class UserApiServiceTest:IClassFixture<UserDbFixture>
    {
        readonly IUserRepository _userRepository;
        readonly IUserService _userService;

        public UserApiServiceTest(UserDbFixture userDbFixture)
        {
            _userRepository = new UserRepository(userDbFixture._userDbContext);
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public void RegisterUser_ShouldReturnTrueIfUserRegistrationIsSuccessful()
        {
            var expected = true;
            User user = new User() { Id = 3, Name = "aayush", Email = "aayush@gmail.com", Password = "123", Location = "Pune" };
            var actual = _userService.RegisterUser(user);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LoginUser_ShouldReturnTheUserObjectIfSuccessfulLogin()
        {
            User user = new User() { Id = 1, Name = "vishal", Password = "1234", Email = "vishal110500@gmail.com", Location = "Mumbai"};
            var expected = user;
            LogInUser loginUser = new LogInUser() { Name = "vishal", Password = "1234" };
            var actual = _userService.LogIn(loginUser);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Password, actual.Password);
        }
    }
}