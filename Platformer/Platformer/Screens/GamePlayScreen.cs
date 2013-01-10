using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Foundation;
using Foundation.Camera;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;

namespace Platformer.Screens
{
    class GamePlayScreen : BaseScreen
    {
        private Physics physics;
        private Camera2D camera;

        private SpriteFont font;
        private Body ball1;
        private Body ball2;
        private Body ball3;
        private Sprite ball1Sprite;
        private Sprite ball2Sprite;
        private Sprite ball3Sprite;

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Andy");
        }

        public override void Initialize()
        {
            camera = new Camera2D(Game);
            physics = new Physics(Game, camera);

            CreateBodies();
        }

        private void CreateBodies()
        {
            var ballTexture = Game.Content.Load<Texture2D>("Ball");
            ball1Sprite = new Sprite(ballTexture, 1.5f);
            ball2Sprite = new Sprite(ballTexture, 2f);
            ball3Sprite = new Sprite(ballTexture, 1f);
            
            // Create ground

            Body ground = BodyFactory.CreateBody(physics.World, new Vector2(0, 0));
            ground.BodyType = BodyType.Static;

            PolygonShape box = new PolygonShape(0f);
            box.SetAsBox(10f, 0.5f);

            ground.CreateFixture(box);

            // Create ball

            ball1 = CreateBall(0.75f, new Vector2(0f, -10f));
            ball2 = CreateBall(1f, new Vector2(-0.5f, -7f));
            ball3 = CreateBall(0.5f, new Vector2(1f, -4f));
        }

        private Body CreateBall(float radius, Vector2 position)
        {
            Body ball = BodyFactory.CreateBody(physics.World, position);
            ball.BodyType = BodyType.Dynamic;

            Fixture fix = ball.CreateFixture(new CircleShape(radius, 1f));
            fix.Restitution = 0.6f;

            return ball;
        }

        public override void Enter()
        {
            Game.Components.Add(physics);
            Game.Components.Add(camera);
        }

        public override void Update(GameTime gameTime)
        {
            UpdateInput();
        }

        private void UpdateInput()
        {
            if (InputHelper.KeyPressed(Keys.Escape))
                Game.Exit();

            if (InputHelper.KeyPressed(Keys.F12))
            {
                bool toDebug = !physics.ShowDebug;
                physics.ShowDebug = toDebug;
                Debugger.ShowWindow = toDebug;
            }

            int scroll = InputHelper.MouseScrolled();
            if (scroll != 0)
                camera.Scale += scroll / 1200f;
            if (InputHelper.MouseButtonPressed(MouseButtons.Middle))
                camera.Position = camera.ScreenToWorld(InputHelper.MousePosition);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            Game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None,
                RasterizerState.CullCounterClockwise, null, camera.DisplayView);
            Game.SpriteBatch.Draw(ball1Sprite.Texture, ConvertUnits.ToDisplay(ball1.Position), null, Color.White, ball1.Rotation, ball1Sprite.Origin, ball1Sprite.Scale, SpriteEffects.None, 0f);
            Game.SpriteBatch.Draw(ball2Sprite.Texture, ConvertUnits.ToDisplay(ball2.Position), null, Color.White, ball2.Rotation, ball2Sprite.Origin, ball2Sprite.Scale, SpriteEffects.None, 0f);
            Game.SpriteBatch.Draw(ball3Sprite.Texture, ConvertUnits.ToDisplay(ball3.Position), null, Color.White, ball3.Rotation, ball3Sprite.Origin, ball3Sprite.Scale, SpriteEffects.None, 0f);
            Game.SpriteBatch.End();

            Game.SpriteBatch.Begin();
            Game.SpriteBatch.DrawString(font, "Gameplay - ESC para sair", new Vector2(10, 680), Color.White);
            Game.SpriteBatch.End();
        }
    }
}
