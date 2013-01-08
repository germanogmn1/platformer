using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Foundation.ScreenManager
{
    public abstract class Screen
    {
        public ScreenManager Manager { set; protected get; }

        /// <summary>
        /// Called when entering the screen
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        /// Called when leaving the screen
        /// </summary>
        public virtual void Leave() { }

        // GameComponent methods
        public abstract void LoadContent(ContentManager content);
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
