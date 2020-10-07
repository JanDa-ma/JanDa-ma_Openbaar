
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryVintage
{
    public class Seller:Person
    {
        public Seller(int id, string login, string firstname, string lastname, string gender, string password, DateTime birthdate, string email, string telephone) : base(id, login, firstname, lastname, gender, password, birthdate, email, telephone)
        {
            person_roleName = "seller";
        }
    }
}
