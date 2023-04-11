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

        public int Height = 200, Width = 200, Speed = 10;

        public Image Image;

        public List<string> imageMovements = new List<string>();

        public int form;
        
    }
}
