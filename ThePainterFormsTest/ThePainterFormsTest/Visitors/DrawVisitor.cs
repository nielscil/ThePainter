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

        public void BeforeGroup(Group group)
        {
            //Do nothing I guess??
        }

        public void AfterGroup(Group group)
        {
            //Do nothing I guess??
        }

        public void Visit(BasicFigure figure)
        {
            figure.Draw(_graphics);
        }
    }
}
