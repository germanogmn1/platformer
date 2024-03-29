using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Screens;
using Foundation.ScreenManager;
using Foundation;

namespace Platformer
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        
        private ScreenManager screenManager;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;

            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            Components.Add(new InputHelper(this));
            Components.Add(new FramerateCounter(this));
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            screenManager.AddScreen(new StartScreen());
            screenManager.AddScreen(new GamePlayScreen());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
