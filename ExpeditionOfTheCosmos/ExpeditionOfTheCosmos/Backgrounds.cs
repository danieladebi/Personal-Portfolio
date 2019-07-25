using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpeditionOfTheCosmos
{
    class Backgrounds
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public void Draw(SpriteBatch batch) {
            batch.Draw(texture, rectangle, Color.LavenderBlush);
        }
    }

    class Scrolling : Backgrounds 
    {
        public Scrolling(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.X -= 2;
        }
    }
}
