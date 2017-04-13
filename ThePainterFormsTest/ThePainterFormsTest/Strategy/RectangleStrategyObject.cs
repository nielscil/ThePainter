using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Strategy
{
    /// <summary>
    /// Rectangle stragety object
    /// </summary>
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

        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="graphics">grahpics</param>
        /// <param name="figure">figure</param>
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

        /// <summary>
        /// Gets the name of the figure
        /// </summary>
        /// <returns>name</returns>
        public string GetName()
        {
            return "rectangle";
        }
    }
}
