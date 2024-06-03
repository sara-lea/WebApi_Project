using Entities;
using Repositories;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly AdoNetUsers326077351Context _dbContext;
        private readonly UsersRepository _userRepository;

        public UserRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepository = new UsersRepository(_dbContext);
        }

        [Fact]
        public async Task GetById_ExistingUserId_ReturnsUser()
        {
            // Arrange
            var user = new User { Firstname = "Test", Lastname = "Test", Email = "test2@example.com", Password = "password" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            int userId = user.UserId;

            // Act
            var result = await _userRepository.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task Register_ValidUser_SavesAndReturnsUser()
        {
            // Arrange
            var user = new User { Firstname = "New", Lastname = "User", Email = "newuser@example.com", Password = "password" };

            // Act
            var result = await _userRepository.Register(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Firstname, result.Firstname);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var email = "login@ex.com";
            var password = "password";
            User user = new User { Firstname = "Login", Lastname = "User", Email = email, Password = password };
            var userLogin = new UserLogin { Email = email, Password = password };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userRepository.Login(userLogin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Update_ValidIdAndUser_UpdatesAndReturnsUser()
        {
            // Arrange
            var user = new User { Firstname = "Update", Lastname = "User", Email = "update@ex.com", Password = "password" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            int userId = user.UserId;

            var updatedUser = new User { Firstname = "Updated", Lastname = "User", Email = "updated@ex.com", Password = "npassword" };

            // Act
            var result = await _userRepository.Update(userId, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Firstname, result.Firstname);
            Assert.Equal(updatedUser.Email, result.Email);
        }
    }
}

