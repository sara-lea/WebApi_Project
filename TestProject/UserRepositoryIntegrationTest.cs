using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;

namespace TestProject
{
    public class UserRepositoryIntegrationTest:IClassFixture<DatabaseFixture>
    {
        private readonly AdoNetUsers326077351Context _dbContext;

        private readonly UsersRepository _userRepository;
        
        public UserRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepository = new UsersRepository(_dbContext);
        }

        [Fact]
        public async Task GetUser_ValidCredentails_ReturnsUser()
        {
            var email = "test@example.com";
            var password = "password";

            var user = new User { Email = email, Password = password, Firstname = "Test", Lastname = "Test" };
            var userLogin = new UserLogin { Email = email, Password = password };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var result = await _userRepository.Login(userLogin);

            Assert.NotNull(result);
        }
    }
}
