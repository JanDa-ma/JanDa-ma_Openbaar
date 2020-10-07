using System;

namespace ClassLibraryVintage
{
    public class User : Person
    {
        public User(int id, string login, string firstname, string lastname, string gender, string password, DateTime birthdate, string email, string telephone) : base(id, login, firstname, lastname, gender, password, birthdate, email, telephone)
        {
            
        }
    }
}
