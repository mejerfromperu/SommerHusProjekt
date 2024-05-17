using SommerHusProjekt.Model07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07
{
    public interface IUserRepository
    {
        User Add(User m);
        User Delete(int id);
        List<User> GetAll();
        List<User> GetSomething();
        User GetById(int id);
        string? ToString();
        User Update(int id, User User);
        User GetByEmailAndPassword(string email, string password);
        User GetByEmail(string email);
        string GeneratePasswordResetToken(User user);
        User GetUserByPasswordResetToken(string token);
        void ResetPassword(User user, string newPassword);
        public List<User> Search(int? id, string? firstName, string? lastName, string? phone, string? email);
        List<User> SortId();
        List<User> SortFirstName();
        List<User> SortLastName();
    }
}
