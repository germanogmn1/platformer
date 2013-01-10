using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Foundation
{
    public enum MouseButtons { Left, Middle, Right, Extra1, Extra2 }

    public class InputHelper : GameComponent
    {
        private static MouseState lastMouseState;
        private static MouseState currentMouseState;

        private static KeyboardState lastKeyboardState;
        private static KeyboardState currentKeyboardState;

        static InputHelper()
        {
            lastMouseState = new MouseState();
            currentMouseState = new MouseState();

            lastKeyboardState = new KeyboardState();
            currentKeyboardState = new KeyboardState();
        }
        
        /// <summary>
        /// Check if the keyboard key was pressed since the last game loop
        /// </summary>
        /// <param name="key">Wich key to check</param>
        public static bool KeyPressed(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key) &&
                    lastKeyboardState.IsKeyUp(key));
        }

        /// <summary>
        /// Check if the mouse button was pressed since the last game loop
        /// </summary>
        /// <param name="button">Which button to check</param>
        public static bool MouseButtonPressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return (currentMouseState.LeftButton == ButtonState.Pressed &&
                            lastMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.Middle:
                    return (currentMouseState.MiddleButton == ButtonState.Pressed &&
                            lastMouseState.MiddleButton == ButtonState.Released);
                case MouseButtons.Right:
                    return (currentMouseState.RightButton == ButtonState.Pressed &&
                            lastMouseState.RightButton == ButtonState.Released);
                case MouseButtons.Extra1:
                    return (currentMouseState.XButton1 == ButtonState.Pressed &&
                            lastMouseState.XButton1 == ButtonState.Released);
                case MouseButtons.Extra2:
                    return (currentMouseState.XButton2 == ButtonState.Pressed &&
                            lastMouseState.XButton2 == ButtonState.Released);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Current mouse position on screen
        /// </summary>
        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(currentMouseState.X, currentMouseState.Y);
            }
        }

        /// <summary>
        /// Get the amount of mouse scroll since the last game loop
        /// </summary>
        /// <returns>Zero if no scroll, positive if scrolled up, negative if scrolled down</returns>
        public static int MouseScrolled()
        {
            return currentMouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue;
        }

        // Non static

        public InputHelper(Game game) : base(game)
        {
            // This component needs to be updated first in order
            // to others get the correct result from static methods
            UpdateOrder = -1;
        }

        public override void Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
    }
}
