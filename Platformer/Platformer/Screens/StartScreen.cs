using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Foundation;

namespace Platformer.Screens
{
    public class StartScreen : BaseScreen
    {
        private SpriteFont font;

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Andy");
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyListener.IsKeyPressed(Keys.Space))
                Manager.EnterScreen<GamePlayScreen>();
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            Game.SpriteBatch.Begin();
            Game.SpriteBatch.DrawString(font, "Tela Incial - Aperte Espaco", new Vector2(450, 300), Color.White);
            Game.SpriteBatch.End();
        }
    }
}
