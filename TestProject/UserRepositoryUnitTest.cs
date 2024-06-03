using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserRepositoryUnitTest
    {
        [Fact]

        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            var user = new User { Email = "test@ex.com", Password = "password" };

            var mockContext= new Mock<AdoNetUsers326077351Context>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UsersRepository(mockContext.Object);

            var userLogin = new UserLogin { Email = user.Email, Password = user.Password };

            var result = await userRepository.Login(userLogin);

            Assert.Equal(user, result);

        }

        [Fact]
        public async Task GetById_ExistingUser_ReturnsUser()
        {
            // Arrange
            int userId = 1;
            var user = new User { UserId = userId, Email = "test@ex.com", Password = "password" };

            var mockContext = new Mock<AdoNetUsers326077351Context>();
            mockContext.Setup(x => x.Users.FindAsync(userId)).ReturnsAsync(user);

            var userRepository = new UsersRepository(mockContext.Object);

            // Act
            var result = await userRepository.GetById(userId);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsUser()
        {
            // Arrange
            var user = new User { Firstname = "Test", Lastname = "Test", Email = "test2@ex.com", Password = "password" };

            var users = new List<User>();
            var mockContext = new Mock<AdoNetUsers326077351Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UsersRepository(mockContext.Object);

            // Act
            var result = await userRepository.Register(user);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
          public async Task Update_UserExists_ShouldUpdateUserAndReturnUpdatedUser()
        {
            // Arrange
            var userId = 1;
            var existingUser = new User { Firstname = "Test", Lastname = "Test", Email = "test2@ex.com", Password = "password" };
            var updatedUser = new User { Firstname = "updated", Lastname = "updated", Email = "test2@ex.com", Password = "password" };

            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<AdoNetUsers326077351Context>();
            mockContext.Setup(m => m.Users.FindAsync(userId)).ReturnsAsync(existingUser);
            mockContext.Setup(m => m.Entry(It.IsAny<User>()).CurrentValues.SetValues(It.IsAny<object>()));

            var userRepository = new UsersRepository(mockContext.Object);

            // Act
            var result = await userRepository.Update(userId, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal("Updated", result.Firstname);
            mockContext.Verify(m => m.Users.FindAsync(userId), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
            mockContext.Verify(m => m.Entry(existingUser).CurrentValues.SetValues(updatedUser), Times.Once());
        }


    }
}
