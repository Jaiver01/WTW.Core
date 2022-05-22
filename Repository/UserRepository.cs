using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WTW.Core.Models;

namespace WTW.Core.Repository
{
    public interface IUserRepository
    {
        IEnumerable<Person> GetPersons();
        IEnumerable<Person> GetPersonsStoredProcedure();
        IEnumerable<User> GetUsers();
        User GetUser(long id);
        User GetUserByEmail(string email);
        User CreateUser(User user);
        Person CreatePerson(Person person);
    }

    public class UserRepository : IUserRepository
    {
        private CoreContext _dbContext;

        public UserRepository(CoreContext coreContext)
        {
            this._dbContext = coreContext;
        }

        public IEnumerable<Person> GetPersons()
        {
            return this._dbContext.Persons;
        }

        public IEnumerable<Person> GetPersonsStoredProcedure()
        {
            return this._dbContext.Persons.FromSqlRaw("Exec SP_GetAllPersons");
        }

        public IEnumerable<User> GetUsers()
        {
            return this._dbContext.Users.Include(u => u.Data);
        }

        public User GetUser(long id)
        {
            return this._dbContext.Users.Include(u => u.Data).FirstOrDefault(p => p.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return this._dbContext.Users.Include(u => u.Data).FirstOrDefault(p => p.Data.Email == email);
        }

        public User CreateUser(User user)
        {
            this._dbContext.Users.Add(user);
            this._dbContext.SaveChanges();

            return user;
        }

        public Person CreatePerson(Person person)
        {
            this._dbContext.Persons.Add(person);
            this._dbContext.SaveChanges();

            return person;
        }
    }
}
