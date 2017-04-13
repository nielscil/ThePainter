using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.States
{
    /// <summary>
    /// Ornament Right State
    /// </summary>
    public class OrnamentRight : IOrnamentState
    {

        private static OrnamentRight _instance;
        public static OrnamentRight Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new OrnamentRight();
                }

                return _instance;
            }
        }

        private OrnamentRight() { }

        public string State
        {
            get
            {
                return "right";
            }
        }

        /// <summary>
        /// Gets drawing position
        /// </summary>
        /// <param name="ornament">ornament</param>
        /// <returns>position</returns>
        public PointF GetPosition(Ornament ornament)
        {
            int x = ornament.X + ornament.Width + 20;
            int y = ornament.Height / 2 + ornament.Y - 5;
            return new PointF(x, y);
        }

        public override string ToString()
        {
            return State;
        }
    }
}
