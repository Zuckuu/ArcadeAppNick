using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ArcadeAppNick.Models
{
    [Table("cards")]
    public class Cards
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Image { get; set; }
        public int Attack { get; set; }

        public int Hitpoint { get; set; }

        public string Rarity { get; set; }

    }
}
