using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Physics
{
    public struct Sprite
    {
        public Vector2 Origin;
        public Texture2D Texture;
        public float Scale;

        public Sprite(Texture2D texture, Vector2 origin, float scale=1f)
        {
            this.Texture = texture;
            this.Origin = origin;
            this.Scale = scale;
        }

        public Sprite(Texture2D texture, float scale = 1f)
        {
            this.Texture = texture;
            this.Origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            this.Scale = scale;
        }
    }
}
