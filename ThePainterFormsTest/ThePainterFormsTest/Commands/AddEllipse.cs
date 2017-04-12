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
    class AddEllipse : ICommand
    {
        private int _x, _y, _width, _heigth;
        private BasicFigure _ellipse;

        public AddEllipse(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _heigth = height;
            _ellipse = new BasicFigure(_x, _y, _width, _heigth, EllipseStrategyObject.Instance);
        }

        public void Execute(Canvas canvas)
        {
            canvas.AddItem(_ellipse);

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            canvas.RemoveItem(_ellipse);

            Controller.Instance.InvalidateCanvas();
        }
    }
}
