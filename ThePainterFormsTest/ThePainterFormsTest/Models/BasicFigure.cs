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
    /// <summary>
    /// Basic figure class
    /// </summary>
    public class BasicFigure : DrawableItem
    {
        private IStrategy _strategyObject;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="width">width of figure</param>
        /// <param name="height">height of figure</param>
        /// <param name="strategyObject">strategy object</param>
        public BasicFigure(int x, int y, int width, int height, IStrategy strategyObject) :base(x, y, width, height)
        {
            _strategyObject = strategyObject;
        }

        /// <summary>
        /// Accepts the visitor
        /// </summary>
        /// <param name="visitor">visitor</param>
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Clones the item
        /// </summary>
        /// <returns>clone</returns>
        public override DrawableItem Clone()
        {
            BasicFigure basicFigure = new BasicFigure(X, Y, Width, Height,_strategyObject);
            basicFigure.Color = Color;
            return basicFigure;
        }

        /// <summary>
        /// Draws the item
        /// </summary>
        /// <param name="graphics">graphics</param>
        public void Draw(Graphics graphics)
        {
            _strategyObject.Draw(graphics, this);
        }

        /// <summary>
        /// To string function
        /// </summary>
        /// <returns>name of item</returns>
        public override string ToString()
        {
            return _strategyObject.GetName();
        }
    }
}
