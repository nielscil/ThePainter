﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    /// <summary>
    /// Temp resizing visitor for resizing animation
    /// </summary>
    public class TempResizeVisitor : IVisitor
    {

        private Point _begin;
        private Point _end;

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

        public TempResizeVisitor(Point begin, Point end)
        {
            _begin = begin;
            _end = end;
        }

        public void Visit(BasicFigure figure)
        {
            if(!isInOrnament)
            {
                figure.Height += _end.Y - _begin.Y;
                figure.Width += _end.X - _begin.X;
            }
        }

        public void BeforeGroup(Group group)
        {
            //Do nothing I guess??
        }

        public void AfterGroup(Group group)
        {
            //do nothing I guess??
        }

        public void BeforeOrnament(Ornament ornament)
        {
            isInOrnament = true;

        }

        public void AfterOrnament(Ornament ornament)
        {
            isInOrnament = false;
        }
    }
}
