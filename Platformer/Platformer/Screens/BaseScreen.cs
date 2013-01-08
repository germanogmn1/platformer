using Foundation.ScreenManager;

namespace Platformer.Screens
{
    /// <summary>
    /// Utility class that provides an acessor for the concrete game instance.
    /// Inherit this class for the game screens.
    /// </summary>
    public abstract class BaseScreen : Screen
    {
        protected Game1 Game
        {
            get { return (Game1)Manager.Game; }
        }
    }
}
