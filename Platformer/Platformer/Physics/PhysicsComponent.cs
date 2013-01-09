using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics;
using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;
using Foundation;
using Foundation.Camera;


namespace Platformer.Physics
{
    public class PhysicsComponent : DrawableGameComponent
    {
        public World World { get; private set; }
        private DebugViewXNA debugView;
        
        private Camera2D camera;

        private Sprite ballSprite;
        
        private Body ball1;
        private Body ball2;
        private Body ball3;
        private Sprite bigBallSprite;

        public PhysicsComponent(Game game) : base(game)
        {
            World = new World(new Vector2(0f, 10f));
            debugView = new DebugViewXNA(World);
            debugView.AppendFlags(DebugViewFlags.Shape);
            debugView.AppendFlags(DebugViewFlags.CenterOfMass);

            camera = new Camera2D(game);
            game.Components.Add(camera);
            camera.Position = new Vector2(0f, 5f);
            camera.Scale = 0.5f;
        }

        public override void Initialize()
        {
            base.Initialize();

            // Create ground

            Body ground = BodyFactory.CreateBody(World, new Vector2(0, 0));
            ground.BodyType = BodyType.Static;

            PolygonShape box = new PolygonShape(0f);
            box.SetAsBox(10f, 0.5f);

            ground.CreateFixture(box);

            // Create ball

            ball1 = CreateBall(0.5f, new Vector2(0f, -10f));
            ball2 = CreateBall(1f, new Vector2(-0.5f, -7f));
            ball3 = CreateBall(0.5f, new Vector2(1f, -4f));
        }

        private Body CreateBall(float radius, Vector2 position)
        {
            Body ball = BodyFactory.CreateBody(World, position);
            ball.BodyType = BodyType.Dynamic;

            Fixture fix = ball.CreateFixture(new CircleShape(radius, 1f));
            fix.Restitution = 0.6f;

            return ball;
        }

        protected override void LoadContent()
        {
            debugView.LoadContent(GraphicsDevice, Game.Content);
            
            ballSprite = new Sprite(Game.Content.Load<Texture2D>("Ball"));
            bigBallSprite = ballSprite;
            bigBallSprite.Scale = 2f;
        }

        public override void Update(GameTime gameTime)
        {
            var seconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            World.Step(seconds);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1 game = (Game1)Game;

            game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None,
                RasterizerState.CullCounterClockwise, null, camera.ScreenView);

            game.SpriteBatch.Draw(ballSprite.Texture, ConvertUnits.ToScreen(ball1.Position), null, Color.White, ball1.Rotation, ballSprite.Origin, ballSprite.Scale, SpriteEffects.None, 0f);
            game.SpriteBatch.Draw(bigBallSprite.Texture, ConvertUnits.ToScreen(ball2.Position), null, Color.White, ball2.Rotation, bigBallSprite.Origin, bigBallSprite.Scale, SpriteEffects.None, 0f);
            game.SpriteBatch.Draw(ballSprite.Texture, ConvertUnits.ToScreen(ball3.Position), null, Color.White, ball3.Rotation, ballSprite.Origin, ballSprite.Scale, SpriteEffects.None, 0f);

            game.SpriteBatch.End();

            debugView.RenderDebugData(ref camera.WorldProjection, ref camera.WorldView);
        }

    }
}
