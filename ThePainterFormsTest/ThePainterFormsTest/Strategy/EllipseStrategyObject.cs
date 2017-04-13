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
    /// Ellipse strategy object
    /// </summary>
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

        /// <summary>
        /// Draw ellipse
        /// </summary>
        /// <param name="graphics">grahpics</param>
        /// <param name="figure">figure</param>
        public void Draw(Graphics graphics, BasicFigure figure)
        {
            using (Pen pen = new Pen(figure.Color))
            {
                graphics.DrawEllipse(pen, figure.X, figure.Y, figure.Width, figure.Height);
            }
        }

        /// <summary>
        /// Gets the name of the figure
        /// </summary>
        /// <returns>name</returns>
        public string GetName()
        {
            return "ellipse";
        }
    }
}
