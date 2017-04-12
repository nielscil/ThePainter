using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class DeselectVisitor : IVisitor
    {

        private static DeselectVisitor _deselectVisitor;
        public static DeselectVisitor Instance
        {
            get
            {
                if (_deselectVisitor == null)
                {
                    _deselectVisitor = new DeselectVisitor();
                }
                return _deselectVisitor;
            }
        }

        private DeselectVisitor() { }

        public void AfterGroup(Group group)
        {
            //Do nothing
        }

        public void BeforeGroup(Group group)
        {
            DoVisit(group);
        }

        private void DoVisit(DrawableItem item)
        {
            item.Color = Color.Black;
        }

        public void Visit(BasicFigure figure)
        {
            DoVisit(figure);
        }
    }
}
