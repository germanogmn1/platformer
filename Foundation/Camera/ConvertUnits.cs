using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Foundation.Camera
{
    /// <summary>
    /// Convert units between physics world and display
    /// </summary>
    public static class ConvertUnits
    {
        public static readonly float PixelsPerMeter = 100f;

        #region ToWorld
        public static float ToWorld(float display)
        {
            return display / PixelsPerMeter;
        }

        public static Vector2 ToWorld(Vector2 display)
        {
            return display / PixelsPerMeter;
        }

        public static Vector3 ToWorld(Vector3 display)
        {
            return display / PixelsPerMeter;
        }
        #endregion

        #region ToDisplay
        public static float ToDisplay(float world)
        {
            return world * PixelsPerMeter;
        }

        public static Vector2 ToDisplay(Vector2 world)
        {
            return world * PixelsPerMeter;
        }

        public static Vector3 ToDisplay(Vector3 world)
        {
            return world * PixelsPerMeter;
        }
        #endregion
    }
}
