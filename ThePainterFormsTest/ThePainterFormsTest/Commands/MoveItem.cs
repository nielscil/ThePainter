﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Commands
{
    class MoveItem : ICommand
    {
        private int _x, _y, _oldX, _oldY;
        private DrawableItem _item;

        public MoveItem(DrawableItem item, Point begin, Point end)
        {
            _x = end.X - begin.X;
            _y = end.Y - begin.Y;
            _oldX = item.X;
            _oldY = item.Y;
            _item = item;
        }

        public void Execute(Canvas canvas)
        {
            canvas.SelectedItem = null;

            _item.Accept(new MoveVisitor(_x, _y));

            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }

        public void Undo(Canvas canvas)
        {
            canvas.SelectedItem = null;

            _item.Accept(new MoveVisitor(_oldX, _oldY));

            canvas.SelectedItem = _item;

            Controller.Instance.InvalidateCanvas();
        }
    }
}
