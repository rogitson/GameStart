using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStart
{
    public class User : Person
    {
        public User() { }
        public User(int i, string n, string e, string u, string p) : base(i, n, e, u, p)
        {
            type = "user";
        }
    }
}
