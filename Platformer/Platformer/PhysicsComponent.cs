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


namespace Platformer
{
    public class PhysicsComponent : DrawableGameComponent
    {
        public World World { get; private set; }
        
        private DebugViewXNA debugView;
        
        private Matrix projection;
        private Matrix view;

        private int PixelsPerMeter = 50;

        private float ToMeter(int pixels)
        {
            return pixels / (float)PixelsPerMeter;
        }

        public PhysicsComponent(Game game) : base(game)
        {
            World = new World(new Vector2(0f, 10f));
            debugView = new DebugViewXNA(World);
            debugView.AppendFlags(DebugViewFlags.Shape);
            debugView.AppendFlags(DebugViewFlags.CenterOfMass);
        }

        public override void Initialize()
        {
            base.Initialize();

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            projection = Matrix.CreateOrthographic(ToMeter(screenWidth), ToMeter(-screenHeight), 0, 1);
            view = Matrix.Identity;

            // Create ground

            Body ground = BodyFactory.CreateBody(World, new Vector2(0, 0));
            ground.BodyType = BodyType.Static;

            PolygonShape box = new PolygonShape(0f);
            box.SetAsBox(10f, 0.5f);

            ground.CreateFixture(box);

            // Create ball

            Body ball = BodyFactory.CreateBody(World, new Vector2(0, -10));
            ball.BodyType = BodyType.Dynamic;

            Fixture fix = ball.CreateFixture(new CircleShape(3f, 1f));
            fix.Restitution = 0.5f;
        }

        protected override void LoadContent()
        {
            debugView.LoadContent(GraphicsDevice, Game.Content);
        }

        public override void Update(GameTime gameTime)
        {
            var seconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
            var old = view.Translation;
            view.Translation += new Vector3(seconds, seconds, 0);

            World.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void Draw(GameTime gameTime)
        {
            debugView.RenderDebugData(ref projection, ref view);
        }
    }
}
