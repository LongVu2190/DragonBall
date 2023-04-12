using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Xml.Xsl;
using DragonBall.Enums;

namespace DragonBall.Objects
{
    internal class Player
    {       
        public Player() 
        {
            Height = 200;
            Width = 200;
            Speed = 10;
            X = 0;
            Y = 0;
            form = 1;
            stepFrame = 0;
            delayShootTime = 15;
            maxSlowDownFPS = 0;
            maxSlowDownFPS = 6;
            imageMovements = Directory.GetFiles("assets/player0", "*.png").ToList();
            Image = Image.FromFile(imageMovements[10]);
        }

        public int X { get; set; }  
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public int stepFrame { get; set; }
        public int startFrame { get; set; }
        public int endFrame { get; set; }
        public int delayShootTime { get; set; }
        public int slowDownFPS { get; set; }
        public int maxSlowDownFPS { get; set; }
        public int form { get; set; }
        public Image Image { get; set; } 

        public List<string> imageMovements = new List<string>();

        public void SetFrame(bool isShot, bool isTransform, Move status)
        {
            if (isShot)
            {
                startFrame = 6;
                endFrame = 8;
            }
            else if (form == 0 && isTransform)
            {
                startFrame = 10;
                endFrame = 16;
            }
            else if (form == 0 && status == Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 0 && status == Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (form == 1 && isTransform)
            {
                startFrame = 10;
                endFrame = 17;
            }
            else if (form == 1 && status == Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 1 && status == Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (form == 2 && isTransform)
            {
                startFrame = 10;
                endFrame = 20;
            }
            else if (form == 2 && status == Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 2 && status == Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (form == 3 && isTransform)
            {
                startFrame = 10;
                endFrame = 27;
            }
            else if (form == 3 && status == Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 3 && status == Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (form == 4 && isTransform)
            {
                startFrame = 10;
                endFrame = 22;
            }
            else if (form == 4 && status == Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 4 && status == Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }
        }

        ~Player() { }
    }
}
