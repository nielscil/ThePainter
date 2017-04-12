using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.States;
using ThePainterFormsTest.Strategy;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Models
{
    public class Ornament : DrawableItem
    {
        public string Text { get; private set; }

        private DrawableItem _child;
        public DrawableItem Child
        {
            get
            {
                return _child;
            }
            private set
            {
                _child = value;
                Parent = _child.Parent;
                SetPosition();
            }
        }
        private IOrnamentState _state;

        public Ornament(string text, DrawableItem child, IOrnamentState state) : base()
        {
            _state = state;
            Text = text;
            Child = child;
        }


        private void SetPosition()
        {
            X = Child.X;
            Y = Child.Y;
            Width = Child.Width;
            Height = Child.Height;
        }

        public void Draw(Graphics graphics)
        {
            PointF point = _state.GetPosition(this);
            graphics.DrawString(Text, SystemFonts.DefaultFont, GetBrush(), point.X, point.Y);
        }

        private Brush GetBrush()
        {
            return Color == Color.Red ? Brushes.Red : Brushes.Black;
        }

        public string GetState()
        {
            return _state.State;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.BeforeOrnament(this);

            Child.Accept(visitor);

            visitor.AfterOrnament(this);
        }

        public override DrawableItem Clone()
        {
            Ornament ornament = new Ornament(Text, Child, _state);
            ornament.Color = Color;
            return ornament;
        }

        public override void NotifyPositionChangeToParent()
        {
            X = Child.X;
            Y = Child.Y;
            Width = Child.Width;
            Height = Child.Height;

            base.NotifyPositionChangeToParent();
        }

        public override string ToString()
        {
            return $"Ornament({GetState()}) \"{Text}\"";
        }
    }
}
