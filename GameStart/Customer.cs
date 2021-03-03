using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStart
{
    public class Customer : User
    {
        string address, phone;
        Game order;
        public Customer() { }
        public Customer(int i, string n, string e, string u, string p) : base(i, n, e, u, p)
        {

        }
        public Customer(int i, string n, string e, string u, string p, Game o) : base(i, n, e, u, p)
        {
            order = o;
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }
        public string Game
        {
            get
            {
                return order.Title;
            }
            set
            {
                order.Title = value;
            }
        }
        public decimal Due
        {
            get
            {
                return order.Price;
            }
            set
            {
                order.Price = value;
            }
        }
        public Game Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }
    }
}
