﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Visitors
{
    public class MoveVisitor : IVisitor
    {
        private int _x;
        private int _y;
            
        public MoveVisitor(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void BeforeGroup(Group group)
        {
            //DO nothing
        }

        public void AfterGroup(Group group)
        {
            //Do nothing
        }

        public void Visit(BasicFigure figure)
        {
            figure.X += _x;
            figure.Y += _y;

            figure.NotifyPositionChangeToParent();
        }
    }
}
