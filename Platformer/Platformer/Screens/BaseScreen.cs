using Foundation.ScreenManager;

namespace Platformer.Screens
{
    /// <summary>
    /// Utility class provides an acessor for the concrete game type.
    /// Inherit this class for
    /// </summary>
    public abstract class BaseScreen : Screen
    {
        public Game1 Game
        {
            get { return (Game1)Manager.Game; }
        }
    }
}
