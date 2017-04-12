using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class SelectVisitor : IVisitor
    {
        private static SelectVisitor _selectVisitor;
        public static SelectVisitor Instance
        {
            get
            {
                if(_selectVisitor == null)
                {
                    _selectVisitor = new SelectVisitor();
                }
                return _selectVisitor;
            }
        }

        private SelectVisitor() { }

        public void AfterGroup(Group group)
        {
            //Do nothing
        }

        public void BeforeGroup(Group group)
        {
            DoVisit(group);
        }

        public void Visit(BasicFigure figure)
        {
            DoVisit(figure);
        }

        private void DoVisit(DrawableItem item)
        {
            item.Color = Color.Red;
        }

        public void BeforeOrnament(Ornament ornament)
        {
            DoVisit(ornament);
        }

        public void AfterOrnament(Ornament ornament)
        {
            //Do nothing I guess??
        }
    }
}
