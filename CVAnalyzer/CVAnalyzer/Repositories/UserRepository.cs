using System.Data.Entity;
using CVAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVAnalyzer.Repositories
{
    public class UserRepository
    {
        private readonly AppContext _appContext;

        public UserRepository(AppContext appContext)
        {
            _appContext = appContext;
        }

        public bool Add(User user)
        {
            _appContext.Users.Add(user);
            return _appContext.SaveChanges() > 0;
        }

        public bool Delete(User user)
        {
            _appContext.Users.Remove(user);
            return _appContext.SaveChanges() > 0;
        }

        public bool Update(User user)
        {
            _appContext.Entry(user).State = EntityState.Modified;
            return _appContext.SaveChanges() > 0;
        }

        public User getByEmail(String email)
        {
            try
            {
              return _appContext.Users.SingleOrDefault(u => u.Email.Equals(email));

            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}