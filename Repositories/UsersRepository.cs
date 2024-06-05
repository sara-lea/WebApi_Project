using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace Repositories
{
    public class UsersRepository : IUsersRepository
    {

       private AdoNetUsers326077351Context _UsersContext;

        public UsersRepository(AdoNetUsers326077351Context UsersContext)
        {
            _UsersContext = UsersContext;
        }
        string filePath = "./Users.txt";

        public async Task<User> GetById(int id)
        {
            return await _UsersContext.Users.FindAsync(id);   
        }

        public async Task<User> Register(User user)
        {
           await _UsersContext.Users.AddAsync(user);
           await _UsersContext.SaveChangesAsync();
            return user;
        }

        public async Task< User> Login(UserLogin userLogin)
        {
            return await _UsersContext.Users.Where(user => user.Email == userLogin.Email && user.Password == userLogin.Password).FirstOrDefaultAsync();
        }


        public async Task<User> Update(int id, User user)
        {
            //User userToUpdate = await _UsersContext.Users.FindAsync(id);
            //if (userToUpdate == null)
            //    return null;
            //user.UserId = id;
            //_UsersContext.Entry(userToUpdate).CurrentValues.SetValues(user);
            //await _UsersContext.SaveChangesAsync();
            //return user;

            user.UserId = id;
            _UsersContext.Update(user);
            await _UsersContext.SaveChangesAsync();
            return user;
        }

    }
}
