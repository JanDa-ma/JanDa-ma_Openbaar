using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClassLibraryVintage
{
    public class Buyer : Person
    {
        public Buyer(int id, string login, string firstname, string lastname, string gender, string password, DateTime birthdate, string email, string telephone) : base(id, login, firstname, lastname, gender, password, birthdate, email, telephone)
        {
            person_roleName = "buyer";
        }
    }
}

