using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Foundation.Camera
{
    /// <summary>
    /// Convert units between physics world and screen
    /// </summary>
    public static class ConvertUnits
    {
        public static readonly float PixelsPerMeter = 100f;

        #region ToWorld
        public static float ToWorld(float screen)
        {
            return screen / PixelsPerMeter;
        }

        public static Vector2 ToWorld(Vector2 screen)
        {
            return screen / PixelsPerMeter;
        }

        public static Vector3 ToWorld(Vector3 screen)
        {
            return screen / PixelsPerMeter;
        }
        #endregion

        #region ToScreen
        public static float ToScreen(float world)
        {
            return world * PixelsPerMeter;
        }

        public static Vector2 ToScreen(Vector2 world)
        {
            return world * PixelsPerMeter;
        }

        public static Vector3 ToScreen(Vector3 world)
        {
            return world * PixelsPerMeter;
        }
        #endregion
    }
}
