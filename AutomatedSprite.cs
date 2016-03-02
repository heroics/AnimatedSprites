using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    class AutomatedSprite : Sprite
    {
      //Testinng Test  
        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffSet,
            Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffSet, currentFrame, sheetSize, speed)
        {

        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffSet,
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

            base.Update(gameTime, clientsbounds);
        }
    }
}