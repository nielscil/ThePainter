using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    /// <summary>
    /// Resize item visitor
    /// </summary>
    public class ResizeVisitor : IVisitor
    {
        private int _width;
        private int _height;

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

        public ResizeVisitor(int width, int heigth)
        {
            _width = width;
            _height = heigth;
        }

        public void BeforeGroup(Group group)
        {
            //Do nothing here ??
        }

        public void AfterGroup(Group group)
        {
            //Do nothing here ??
        }

        public void Visit(BasicFigure figure)
        {

            if(!isInOrnament)
            {
                figure.Width += _width;
                figure.Height += _height;
            }

            //figure.NotifyPositionChangeToParent();
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
