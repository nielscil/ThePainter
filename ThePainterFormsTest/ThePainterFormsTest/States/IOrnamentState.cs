using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.States
{
    public interface IOrnamentState
    {
        string State { get; }

        PointF GetPosition(Ornament ornament);
    }
}
