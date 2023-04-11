using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DragonBall.Objects
{
    public class Player
    {       
        public Player() { }

        public int Height { get; set; } = 200;
        public int Width { get; set; } = 200;
        public int Speed { get; set; } = 10;

        public Image Image { get; set; } 

        public List<string> imageMovements = new List<string>();

        public int form { get; set; }
        
    }
}
