﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    /// <summary>
    /// Resize item visitor
    /// </summary>
    public class ResizeVisitor : IVisitor
    {
        private int _width;
        private int _height;

        public ResizeVisitor(int width, int heigth)
        {
            _width = width;
            _height = heigth;
        }

        public void BeforeGroup(Group group)
        {
            //Do nothing here ??
        }

        public void AfterGroup(Group group)
        {
            //Do nothing here ??
        }

        public void Visit(BasicFigure figure)
        {
            figure.Width += _width;
            figure.Height += _height;

            figure.NotifyPositionChangeToParent();
        }

        public void BeforeOrnament(Ornament ornament)
        {
            //DO nothing
        }

        public void AfterOrnament(Ornament ornament)
        {
            //Do nothing I guess??
        }
    }
}
