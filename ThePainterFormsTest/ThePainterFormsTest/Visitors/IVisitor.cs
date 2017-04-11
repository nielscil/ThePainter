using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public interface IVisitor
    {
        void Visit(Group group);
        void Visit(Ellipse ellipse);
        void Visit(Rectangle rectangle);
    }
}
