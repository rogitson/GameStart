using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStart
{
    public abstract class Person
    {
        int id;
        string name;
        string email;
        string username;
        string password;
        protected string type;

        public Person() { }
        public Person(int i, string n, string e, string u, string p)
        {
            id = i;
            name = n;
            email = e;
            username = u;
            password = p;
        }

        public int ID
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
    }
}
