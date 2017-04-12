using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Strategy
{
    public class RectangleStrategyObject : IStrategy
    {

        private static RectangleStrategyObject _instance;
        public static RectangleStrategyObject Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new RectangleStrategyObject();
                }

                return _instance;
            }
        }

        private RectangleStrategyObject() { }

        public void Draw(Graphics graphics, BasicFigure figure)
        {
            using (Pen p = new Pen(figure.Color))
            {
                graphics.DrawLine(p, figure.X, figure.Y, figure.X + figure.Width, figure.Y);
                graphics.DrawLine(p, figure.X, figure.Y, figure.X, figure.Y + figure.Height);
                graphics.DrawLine(p, figure.X + figure.Width, figure.Y, figure.X + figure.Width, figure.Y + figure.Height);
                graphics.DrawLine(p, figure.X, figure.Y + figure.Height, figure.X + figure.Width, figure.Y + figure.Height);
            }
        }

        public string GetName()
        {
            return "rectangle";
        }
    }
}
