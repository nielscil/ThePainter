using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Strategy
{
    class EllipseStrategyObject : IStrategy
    {

        private static EllipseStrategyObject _instance;
        public static EllipseStrategyObject Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new EllipseStrategyObject();
                }

                return _instance;
            }
        }

        private EllipseStrategyObject() { }

        public void Draw(Graphics graphics, BasicFigure figure)
        {
            using (Pen pen = new Pen(figure.Color))
            {
                graphics.DrawEllipse(pen, figure.X, figure.Y, figure.Width, figure.Height);
            }
        }

        public string GetName()
        {
            return "ellipse";
        }
    }
}
