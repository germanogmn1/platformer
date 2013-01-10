using Microsoft.Xna.Framework;

namespace Foundation.Camera
{
    /// <summary>
    /// This camera operates with physics world coordinates
    /// </summary>
    public class Camera2D : GameComponent
    {
        /// <summary>
        /// View matrix with physics world units
        /// </summary>
        public Matrix WorldView;

        /// <summary>
        /// View matrix with display units
        /// </summary>
        public Matrix DisplayView;

        /// <summary>
        /// Projection matrix with world units
        /// </summary>
        public Matrix WorldProjection;

        private float _scale;
        /// <summary>
        /// Zoom scale
        /// </summary>
        public float Scale
        {
            get { return _scale; }
            set { _scale = (value > 0.1f) ? value : 0.1f; }
        }

        /// <summary>
        /// Position in world units
        /// </summary>
        public Vector2 Position;

        private Vector3 translateCenterDisplay;
        private Vector3 translateCenterWorld;

        public Camera2D(Game game) : base(game)
        {
            WorldView = Matrix.Identity;
            DisplayView = Matrix.Identity;
            Scale = 1f;
            Position = Vector2.Zero;
        }

        public override void Initialize()
        {
            var viewport = Game.GraphicsDevice.Viewport;
            translateCenterDisplay = new Vector3(viewport.Width / 2, viewport.Height / 2, 0);
            translateCenterWorld = ConvertUnits.ToWorld(translateCenterDisplay);
            
            float screenWidth = ConvertUnits.ToWorld(viewport.Width);
            float screenHeight = ConvertUnits.ToWorld(viewport.Height);
            WorldProjection = Matrix.CreateOrthographicOffCenter(0f, screenWidth, screenHeight, 0f, 0f, 1f);

            UpdateMatrices();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateMatrices();
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        /// <summary>
        /// Convert from screen position to physics world position
        /// </summary>
        /// <param name="screen">Position on screen</param>
        /// <returns>Position on physics world</returns>
        public Vector2 ScreenToWorld(Vector2 screen)
        {
            Vector3 pos = new Vector3(screen, 0);
            pos = Game.GraphicsDevice.Viewport.Unproject(pos, WorldProjection, WorldView, Matrix.Identity);
            return new Vector2(pos.X, pos.Y);
        }

        /// <summary>
        /// Convert from physics world position to screen position
        /// </summary>
        /// <param name="screen">Position on screen</param>
        /// <returns>Position on physics world</returns>
        public Vector2 WorldToScreen(Vector2 world)
        {
            Vector3 pos = new Vector3(world, 0);
            pos = Game.GraphicsDevice.Viewport.Unproject(pos, WorldProjection, DisplayView, Matrix.Identity);
            return new Vector2(pos.X, pos.Y);
        }

        private void UpdateMatrices()
        {
            Vector3 pos = new Vector3(-Position, 0);
            Matrix zoomMat = Matrix.CreateScale(Scale);

            WorldView = Matrix.CreateTranslation(pos) *
                        Matrix.CreateTranslation(translateCenterWorld / Scale) *
                        zoomMat;

            DisplayView = Matrix.CreateTranslation(ConvertUnits.ToDisplay(pos)) *
                          Matrix.CreateTranslation(translateCenterDisplay / Scale) *
                          zoomMat;
        }
    }
}
