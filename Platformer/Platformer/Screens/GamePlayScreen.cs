using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Foundation;

namespace Platformer.Screens
{
    class GamePlayScreen : BaseScreen
    {
        private SpriteFont font;
        private Texture2D sprite;
        private Vector2 position;
        private bool reverse;

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Andy");
            sprite = content.Load<Texture2D>("AwesomeFace");
        }

        public override void Initialize()
        {
            position = new Vector2(100, 250);
            reverse = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyListener.IsKeyPressed(Keys.Escape))
                Game.Exit();

            int move = gameTime.ElapsedGameTime.Milliseconds;

            if (reverse)
                position.X -= move;
            else
                position.X += move;

            if (position.X >= 1280 - sprite.Width)
                reverse = true;
            else if (position.X <= 0)
                reverse = false;
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            Game.SpriteBatch.Begin();
            Game.SpriteBatch.DrawString(font, "Gameplay - ESC para sair", new Vector2(10, 680), Color.White);
            Game.SpriteBatch.Draw(sprite, position, Color.White);
            Game.SpriteBatch.End();
        }
    }
}
