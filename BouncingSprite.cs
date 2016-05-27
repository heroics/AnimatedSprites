using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimatedSprites
{
    class BouncingSprite : AutomatedSprite
    {

        public BouncingSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffSet,
            Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffSet, currentFrame, sheetSize, speed)
        {

        }

        public BouncingSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffSet,
               Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame)
               : base(textureImage, position, frameSize, collisionOffSet, currentFrame, sheetSize, speed, millisecondsPerFrame)
        {

        }

        public override Vector2 direction
        {
            get
            {
                return speed;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientsbounds)
        {

            position += direction;

            //Make sure the ball stays within the clientbounds 
            if (position.X < 0 || position.X > clientsbounds.Width - frameSize.X)
            {
                   speed.X *= -1;
            }

            if (position.Y > clientsbounds.Height - frameSize.Y || position.Y < 0)

            {
                speed.Y *= -1;
            }


            base.Update(gameTime, clientsbounds);
        }
    }
}
