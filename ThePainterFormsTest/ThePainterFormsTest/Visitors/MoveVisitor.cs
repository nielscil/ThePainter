using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    /// <summary>
    /// Move item visitor
    /// </summary>
    public class MoveVisitor : IVisitor
    {
        private int _x;
        private int _y;

        private int _isInOrnamentCount = 0;
        private bool isInOrnament
        {
            get
            {
                return _isInOrnamentCount > 0;
            }
            set
            {
                if (value)
                    _isInOrnamentCount++;
                else
                    _isInOrnamentCount--;
            }
        }

        public MoveVisitor(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void BeforeGroup(Group group)
        {
            //DO nothing
        }

        public void AfterGroup(Group group)
        {
            //Do nothing
        }

        public void Visit(BasicFigure figure)
        {
            if(!isInOrnament)
            {
                figure.X += _x;
                figure.Y += _y;

                //figure.NotifyPositionChangeToParent();
            }
        }

        public void BeforeOrnament(Ornament ornament)
        {
            //ornament.X += _x;
            //ornament.Y += _y;
            isInOrnament = true;

        }

        public void AfterOrnament(Ornament ornament)
        {
            isInOrnament = false;
        }
    }
}
