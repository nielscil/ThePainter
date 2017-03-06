using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePainterFormsTest.Models
{
    class Ellipse : DrawableItem
    {
        public Ellipse(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(Color))
            {
                graphics.DrawEllipse(pen, _x, _y, _width, _height);
            }  
        }

        public override string ToString()
        {
            return "Ellipse " + base.ToString();
        }
    }
}
