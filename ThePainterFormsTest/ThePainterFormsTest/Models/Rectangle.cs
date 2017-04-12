//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ThePainterFormsTest.Visitors;

//namespace ThePainterFormsTest.Models
//{
//    public class Rectangle : DrawableItem
//    {
//        public override string Name
//        {
//            get
//            {
//                return "rectangle";
//            }
//        }

//        public Rectangle(int x, int y, int width, int height) : base(x, y, width, height) { }

//        public override DrawableItem Clone()
//        {
//            Rectangle rect = new Rectangle(X, Y, Width, Height);
//            rect.Color = Color;
//            return rect;
//        }

//        public override void Accept(IVisitor visitor)
//        {
//            visitor.Visit(this);
//        }
//    }
//}
