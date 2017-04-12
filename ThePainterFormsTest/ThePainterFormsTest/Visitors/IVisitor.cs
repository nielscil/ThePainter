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
        void BeforeGroup(Group group);
        void AfterGroup(Group group);
        void Visit(BasicFigure figure);
    }
}
