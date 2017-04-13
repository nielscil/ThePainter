using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;
using ThePainterFormsTest.Strategy;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Add ellipse commandclass
    /// </summary>
    public class AddEllipse : ICommand
    {
        private int _x, _y, _width, _heigth;
        private BasicFigure _ellipse;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="width">width of ellipse</param>
        /// <param name="height">heigth of ellipse</param>
        public AddEllipse(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _heigth = height;
            _ellipse = new BasicFigure(_x, _y, _width, _heigth, EllipseStrategyObject.Instance);
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.AddItem(_ellipse);

            Controller.Instance.InvalidateCanvas();
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            canvas.RemoveItem(_ellipse);

            Controller.Instance.InvalidateCanvas();
        }
    }
}
