using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class DrawVisitor : IVisitor
    {
        private Graphics _graphics;

        public DrawVisitor(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void Visit(Models.Rectangle rectangle)
        {
            using (Pen p = new Pen(rectangle.Color))
            {
                _graphics.DrawLine(p, rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y);
                _graphics.DrawLine(p, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + rectangle.Height);
                _graphics.DrawLine(p, rectangle.X + rectangle.Width, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
                _graphics.DrawLine(p, rectangle.X, rectangle.Y + rectangle.Height, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            }
        }

        public void Visit(Ellipse ellipse)
        {
            using (Pen pen = new Pen(ellipse.Color))
            {
                _graphics.DrawEllipse(pen, ellipse.X, ellipse.Y, ellipse.Width, ellipse.Height);
            }
        }

        public void BeforeGroup(Group group)
        {
            //Do nothing I guess??
        }

        public void AfterGroup(Group group)
        {
            //Do nothing I guess??
        }
    }
}
