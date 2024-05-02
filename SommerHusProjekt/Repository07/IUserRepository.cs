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
        User GetById(int id);
        string? ToString();
        User Update(int id, User User);

        User GetByEmailAndPassword(string email, string password);
        public List<User> Search(int? id, string? name, string? team);
    }
}
