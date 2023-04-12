using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonBall.Objects
{
    public class Enemy
    {
        public Enemy() { }

        public int Height { get; set; } = 200;
        public int Width { get; set; } = 200;
        public int Speed { get; set; } = 10;
        public int X { get; set; }
        public int Y { get; set; }
        public Image Image { get; set; }

        public List<string> imageMovements = new List<string>();

        public int form { get; set; }

        ~Enemy() { }
    }
}
