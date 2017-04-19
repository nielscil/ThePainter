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
    /// <summary>
    /// Ornament class
    /// </summary>
    public class Ornament : DrawableItem, IDisposable
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
                _child.SizeChanged += Item_SizeChanged;
                SetPosition();
            }
        }
        private IOrnamentState _state;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="child">child of ornament</param>
        /// <param name="state">state of ornament</param>
        public Ornament(string text, DrawableItem child, IOrnamentState state) : base()
        {
            _state = state;
            Text = text;
            Child = child;
        }

        /// <summary>
        /// Set position
        /// </summary>
        private void SetPosition()
        {
            X = Child.X;
            Y = Child.Y;
            Width = Child.Width;
            Height = Child.Height;
        }

        private void Item_SizeChanged()
        {
            SetPosition();
        }

        /// <summary>
        /// Draw ornament
        /// </summary>
        /// <param name="graphics">graphics</param>
        public void Draw(Graphics graphics)
        {
            PointF point = _state.GetPosition(this);
            graphics.DrawString(Text, SystemFonts.DefaultFont, GetBrush(), point.X, point.Y);
        }

        /// <summary>
        /// Get brush
        /// </summary>
        /// <returns>brush color</returns>
        private Brush GetBrush()
        {
            return Color == Color.Red ? Brushes.Red : Brushes.Black;
        }

        /// <summary>
        /// Get state text
        /// </summary>
        /// <returns>state text</returns>
        public string GetState()
        {
            return _state.State;
        }

        /// <summary>
        /// Accept visitor
        /// </summary>
        /// <param name="visitor">visitor</param>
        public override void Accept(IVisitor visitor)
        {
            visitor.BeforeOrnament(this);

            Child.Accept(visitor);

            visitor.AfterOrnament(this);
        }

        /// <summary>
        /// Clones ornament
        /// </summary>
        /// <returns>clone</returns>
        public override DrawableItem Clone()
        {
            Ornament ornament = new Ornament(Text, Child, _state);
            ornament.Color = Color;
            ornament.X = X;
            ornament.Y = Y;
            return ornament;
        }

        ///// <summary>
        ///// Notify position changed
        ///// </summary>
        //public override void NotifyPositionChangeToParent()
        //{
        //    SetPosition();

        //    base.NotifyPositionChangeToParent();
        //}

        /// <summary>
        /// To string override
        /// </summary>
        /// <returns>treeview text</returns>
        public override string ToString()
        {
            return $"Ornament({GetState()}) \"{Text}\"";
        }

        public void Dispose()
        {
            Child.SizeChanged -= Item_SizeChanged;
        }
    }
}
