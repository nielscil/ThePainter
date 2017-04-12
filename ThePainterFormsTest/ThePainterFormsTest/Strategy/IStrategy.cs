using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Strategy
{
    public interface IStrategy
    {
        void Draw(Graphics graphics,BasicFigure figure);
        string GetName();
    }
}
