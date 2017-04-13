using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    /// <summary>
    /// Visitor interface for the items
    /// </summary>
    public interface IVisitor
    {
        void BeforeGroup(Group group);
        void AfterGroup(Group group);
        void Visit(BasicFigure figure);
        void BeforeOrnament(Ornament ornament);
        void AfterOrnament(Ornament ornament);
    }
}
