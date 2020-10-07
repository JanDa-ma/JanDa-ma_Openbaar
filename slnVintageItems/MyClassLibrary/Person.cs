using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slnVintageItems
{
    public class  Person
    {
        public Person(int id, string login, string firstname, string lastname, string gender)
        {
            user_id = id;
            user_login = login;
            user_firstname = firstname;
            user_gender = gender;
        }
        public Person()
        {
        
        }

        public int user_id { get;}
        public string user_login { get;}
        public string user_roleName { get; set;}
        public string user_firstname { get;}
        public string user_gender { get; }
    }
}
