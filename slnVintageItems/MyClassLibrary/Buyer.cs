using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slnVintageItems
{
    public class Buyer:Person
    {
        public Buyer(int id, string login, string firstname, string lastname, string gender) : base(id, login, firstname,lastname, gender)
        {
            user_roleName = "seller";
        }
    }
}
