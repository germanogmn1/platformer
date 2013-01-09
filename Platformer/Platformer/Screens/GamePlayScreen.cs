using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Foundation;
using Platformer.Physics;
using Foundation.Camera;

namespace Platformer.Screens
{
    class GamePlayScreen : BaseScreen
    {
        private SpriteFont font;
        
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Andy");
        }

        public override void Enter()
        {
            Game.Components.Add(new PhysicsComponent(Game));
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHelper.IsKeyPressed(Keys.Escape))
                Game.Exit();

            Vector2 mouse = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Debugger.Debug("Mouse Screen", mouse);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            Game.SpriteBatch.Begin();
            Game.SpriteBatch.DrawString(font, "Gameplay - ESC para sair", new Vector2(10, 680), Color.White);
            Game.SpriteBatch.End();
        }
    }
}
