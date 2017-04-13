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
    /// Ornament Top state
    /// </summary>
    public class OrnamentTop : IOrnamentState
    {
        private static OrnamentTop _instance;
        public static OrnamentTop Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrnamentTop();
                }

                return _instance;
            }
        }

        private OrnamentTop() { }


        public string State
        {
            get
            {
                return "top";
            }
        }

        /// <summary>
        /// Gets drawing position
        /// </summary>
        /// <param name="ornament">ornament</param>
        /// <returns>position</returns>
        public PointF GetPosition(Ornament ornament)
        {
            int x = ornament.Width / 2 + ornament.X - 30;
            int y = ornament.Y - 25;
            return new PointF(x, y);
        }

        public override string ToString()
        {
            return State;
        }
    }
}
