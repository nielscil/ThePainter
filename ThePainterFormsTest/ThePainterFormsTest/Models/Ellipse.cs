﻿//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using ThePainterFormsTest.Visitors;

//namespace ThePainterFormsTest.Models
//{
//    public class Ellipse : DrawableItem
//    {

//        public Ellipse(int x, int y, int width, int height) : base(x, y, width, height) { }

//        public override DrawableItem Clone()
//        {
//            Ellipse ellipse = new Ellipse(X, Y, Width, Height);
//            ellipse.Color = Color;
//            return ellipse;
//        }

//        public override void Accept(IVisitor visitor)
//        {
//            visitor.Visit(this);
//        }
//    }
//}
