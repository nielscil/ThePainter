using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThePainterFormsTest.Models
{
    public interface ICanvasItem
    {
        int X { get; }

        int Y { get; }

        int Width { get; }

        int Height { get; }

        string Name { get; }

        Color Color { get; set; }

        void Resize(Point begin, Point end);

        void Resize(int width, int height);

        void Move(Point begin, Point end);

        void Move(int x, int y);

        void Draw(Graphics graphics);

        bool IsOnLocation(Point point);

        void Select();

        void Deselect();

        string Serialize();

        ICanvasItem Clone();
    }
}
