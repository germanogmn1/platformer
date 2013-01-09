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
        /// View matrix with screen units
        /// </summary>
        public Matrix ScreenView;

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

        private Vector3 translateCenterScreen;
        private Vector3 translateCenterWorld;

        public Camera2D(Game game) : base(game)
        {
            WorldView = Matrix.Identity;
            ScreenView = Matrix.Identity;
            Scale = 1f;
            Position = Vector2.Zero;
        }

        public override void Initialize()
        {
            var viewport = Game.GraphicsDevice.Viewport;
            translateCenterScreen = new Vector3(viewport.Width / 2, viewport.Height / 2, 0);
            translateCenterWorld = ConvertUnits.ToWorld(translateCenterScreen);

            float worldWidth = ConvertUnits.ToWorld(viewport.Width);
            float worldHeight = ConvertUnits.ToWorld(viewport.Height);
            WorldProjection = Matrix.CreateOrthographicOffCenter(0f, worldWidth, worldHeight, 0f, 0f, 1f); 
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public override void Update(GameTime gameTime)
        {
            Vector3 pos = new Vector3(Position, 0);
            Matrix zoomMat = Matrix.CreateScale(Scale);

            WorldView = Matrix.CreateTranslation(pos) *
                        Matrix.CreateTranslation(translateCenterWorld) *
                        zoomMat;

            ScreenView = Matrix.CreateTranslation(ConvertUnits.ToScreen(pos)) *
                         Matrix.CreateTranslation(translateCenterScreen) *
                         zoomMat;
        }
    }
}
