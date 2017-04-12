using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.States
{
    public class OrnamentLeft : IOrnamentState
    {
        private static OrnamentLeft _instance;
        public static OrnamentLeft Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrnamentLeft();
                }

                return _instance;
            }
        }

        private OrnamentLeft() { }


        public string State
        {
            get
            {
                return "left";
            }
        }

        public PointF GetPosition(Ornament ornament)
        {
            int x = ornament.X - 20;
            int y = ornament.Height / 2 + ornament.Y - 5;
            return new PointF(x, y);
        }

        public override string ToString()
        {
            return State;
        }
    }
}
