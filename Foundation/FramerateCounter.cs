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
using System.Resources;


namespace Foundation
{
    public class FramerateCounter : DrawableGameComponent
    {
        private ContentManager content;
        private SpriteBatch spriteBatch;

        private SpriteFont font;
        private Vector2 position;

        private int frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        
        public FramerateCounter(Game game, Vector2? screenPosition=null) : base(game)
        {
            position = screenPosition ?? new Vector2(10, 10);

            content = new ResourceContentManager(Game.Services, Resources.ResourceManager);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Font");
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            frameCounter++;

            string fps = string.Format("{0} FPS", frameRate);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, fps, position, Color.White);
            spriteBatch.End();
        }
    }
}
