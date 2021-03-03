using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStart
{
    public class Game
    {
        int id;
        string title;
        string genre;
        decimal price;
        int rating;
        public Game() { }
        public Game(int i, string t, string g, decimal p, int r)
        {
            id = i;
            title = t;
            genre = g;
            rating = r;
            price = p;
        }
        public int ID
        {
            get
            {
                return id;
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
            }
        }
        
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public int Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
            }
        }
    }
}
