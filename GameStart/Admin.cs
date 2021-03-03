using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStart
{
    public class Admin : Person
    {
        public Admin() { }
        public Admin(int i, string n, string e, string u, string p) : base(i, n, e, u, p)
        {
            type = "admin";
        }
    }
}
