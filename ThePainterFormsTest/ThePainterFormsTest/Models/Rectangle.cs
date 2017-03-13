using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePainterFormsTest.Models
{
    public class Rectangle : DrawableItem
    {
        public override string Name
        {
            get
            {
                return "rectangle";
            }
        }

        public Rectangle(int x, int y, int width, int height) : base(x, y, width, height) { }

        public override void Draw(Graphics graphics)
        {
            using (Pen p = new Pen(Color))
            {
                graphics.DrawLine(p, _x, _y, _x + _width, _y);
                graphics.DrawLine(p, _x, _y, _x, _y + _height);
                graphics.DrawLine(p, _x + _width, _y, _x + _width, _y + _height);
                graphics.DrawLine(p, _x, _y + _height, _x + _width, _y + _height);
            }
                
        }
    }
}
