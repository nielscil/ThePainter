using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    /// <summary>
    /// File visitor for serializing the items
    /// </summary>
    public class FileVisitor : IVisitor
    {
        public StringBuilder StringBuilder { get; } = new StringBuilder();
        private string _prefix = string.Empty;

        private int _isInOrnamentCount = 0;
        private bool isInOrnament
        {
            get
            {
                return _isInOrnamentCount > 0;
            }
            set
            {
                if (value)
                    _isInOrnamentCount++;
                else
                    _isInOrnamentCount--;
            }
        }

        private void DoVisit(DrawableItem item)
        {
            if(!isInOrnament)
            {
                StringBuilder.AppendLine($"{_prefix}{item.ToString()} {item.X} {item.Y} {item.Width} {item.Height}");
            }
        }

        public void BeforeGroup(Group group)
        {
            if(!isInOrnament)
            {
                StringBuilder.AppendLine($"{_prefix}group {group.Count}");
                _prefix += "\t";
            }
        }

        public void AfterGroup(Group group)
        {
            if(!isInOrnament)
            {
                _prefix = _prefix.Remove(_prefix.Length - 1);
            }
        }

        public void Visit(BasicFigure figure)
        {
            DoVisit(figure);
        }

        public void BeforeOrnament(Ornament ornament)
        {
            if(!isInOrnament)
                StringBuilder.AppendLine($"{_prefix}ornament {ornament.GetState()} \"{ornament.Text}\"");
            isInOrnament = true;
        }

        public void AfterOrnament(Ornament ornament)
        {
            isInOrnament = false;
        }
    }
}
