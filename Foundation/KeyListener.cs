using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Foundation
{
    public class KeyListener : GameComponent
    {
        #region Static

        private enum PressStatus { None, New, Expired }

        private static Dictionary<Keys, PressStatus> keysStatus = new Dictionary<Keys, PressStatus>();

        static KeyListener()
        {
            ResetKeyState();
        }

        /// <summary>
        /// Reset pressed keys state.
        /// </summary>
        public static void ResetKeyState()
        {
            // OPTIMIZE: Keys enum has 160 entries, it's better to filter it and check only relevant keys.
            foreach (var key in System.Enum.GetValues(typeof(Keys)).Cast<Keys>())
                keysStatus[key] = PressStatus.None;
        }

        /// <summary>
        /// Check if a specific key has been pressed since the last time
        /// this method was called for the key.
        /// </summary>
        public static bool IsKeyPressed(Keys key)
        {
            if (keysStatus[key] == PressStatus.New)
            {
                keysStatus[key] = PressStatus.Expired;
                return true;
            }

            return false;
        }

        #endregion

        #region Game Component

        public KeyListener(Game game) : base(game) { }
        
        public override void Update(GameTime gameTime)
        {
            var downKeys = Keyboard.GetState().GetPressedKeys();

            foreach (var key in keysStatus.Keys.ToArray())
            {
                PressStatus status = keysStatus[key];

                bool isExpired = (status == PressStatus.Expired);
                bool isDown = downKeys.Contains(key);

                if (isDown && !isExpired)
                    keysStatus[key] = PressStatus.New;
                else if (isExpired && !isDown)
                    keysStatus[key] = PressStatus.None;
            }
        }

        #endregion
    }
}
