using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class FileVisitor : IVisitor
    {
        public StringBuilder StringBuilder { get; } = new StringBuilder();
        private string _prefix = string.Empty;

        public void Visit(Rectangle rectangle)
        {
            DoVisit(rectangle);
        }

        public void Visit(Ellipse ellipse)
        {
            DoVisit(ellipse);
        }

        private void DoVisit(DrawableItem item)
        {
            StringBuilder.AppendLine($"{_prefix}{item.ToString()} {item.X} {item.Y} {item.Width} {item.Height}");
        }

        public void BeforeGroup(Group group)
        {
            StringBuilder.AppendLine($"{_prefix}group {group.Items.Count}");
            _prefix += "\t";
        }

        public void AfterGroup(Group group)
        {
            _prefix = _prefix.Remove(_prefix.Length - 1);
        }
    }
}
