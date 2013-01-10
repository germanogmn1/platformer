using Microsoft.Xna.Framework;
using FarseerPhysics;
using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using Foundation.Camera;


namespace Platformer
{
    public class Physics : DrawableGameComponent
    {
        public World World { get; private set; }
        private Camera2D camera;

        private DebugViewXNA debugView;
        public bool ShowDebug;

        public Physics(Game game, Camera2D camera)
            : base(game)
        {
            World = new World(new Vector2(0f, 10f));
            debugView = new DebugViewXNA(World);
            debugView.AppendFlags(DebugViewFlags.Shape);
            debugView.AppendFlags(DebugViewFlags.ContactNormals);
            debugView.AppendFlags(DebugViewFlags.PolygonPoints);

            this.camera = camera;

            ShowDebug = false;
        }

        protected override void LoadContent()
        {
            debugView.LoadContent(GraphicsDevice, Game.Content);
        }

        public override void Update(GameTime gameTime)
        {
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            World.Step(seconds);
        }

        public override void Draw(GameTime gameTime)
        {
            if (ShowDebug)
                debugView.RenderDebugData(ref camera.WorldProjection, ref camera.WorldView);
        }

    }
}
