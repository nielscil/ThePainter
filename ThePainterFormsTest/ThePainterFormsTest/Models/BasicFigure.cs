using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Strategy;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Models
{
    public class BasicFigure : DrawableItem
    {
        private IStrategy _strategyObject;

        public BasicFigure(int x, int y, int width, int height, IStrategy strategyObject) :base(x, y, width, height)
        {
            _strategyObject = strategyObject;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override DrawableItem Clone()
        {
            BasicFigure basicFigure = new BasicFigure(X, Y, Width, Height,_strategyObject);
            basicFigure.Color = Color;
            return basicFigure;
        }

        public void Draw(Graphics graphics)
        {
            _strategyObject.Draw(graphics, this);
        }

        public override string ToString()
        {
            return _strategyObject.GetName();
        }
    }
}
