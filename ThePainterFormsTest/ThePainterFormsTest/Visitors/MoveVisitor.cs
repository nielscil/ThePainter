using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class MoveVisitor : IVisitor
    {
        private int _x;
        private int _y;
            
        public MoveVisitor(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Visit(Rectangle rectangle)
        {
            DoVisit(rectangle);
        }

        public void Visit(Ellipse ellipse)
        {
            DoVisit(ellipse);
        }

        public void BeforeGroup(Group group)
        {
            //DO nothing
        }

        private void DoVisit(DrawableItem item)
        {
                item.X += _x;
                item.Y += _y;

                item.NotifyPositionChangeToParent();
        }

        public void AfterGroup(Group group)
        {
            //Do nothing
        }
    }
}
