using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class AddRectangle : ICommand
    {
        private int _x, _y, _width, _heigth;
        private Rectangle _rectangle;

        public AddRectangle(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _heigth = height;
            _rectangle = new Rectangle(_x, _y, _width, _heigth);
        }

        public void Execute(Canvas canvas)
        {
            canvas.AddItem(_rectangle);

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            canvas.RemoveItem(_rectangle);

            Controller.Instance.InvalidateCanvas();
        }
    }
}
