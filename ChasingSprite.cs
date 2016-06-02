using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    class ChasingSprite : Sprite
    {
        //Save a reference to the sprite manage to use to get the player Position
        SpriteManager spriteManager;

        //Constructors
        public ChasingSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            SpriteManager spriteManager)
            : base(textureImage, position, frameSize,
                 collisionOffset, currentFrame, sheetSize, speed)
        {
            this.spriteManager = spriteManager;

        }

        public ChasingSprite(Texture2D textureImage, Vector2 position, Point frameSize,
    int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
    int millisecondsPerFrame, SpriteManager spriteManager)
    : base(textureImage, position, frameSize,
         collisionOffset, currentFrame, sheetSize, speed, millisecondsPerFrame)
        {
            this.spriteManager = spriteManager;

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
            //Find the player using spriteManager
            Vector2 playerPosition = spriteManager.GetPlayerPosition();

            //Because the sprite may be moving in the X or Y direction
            //but not both, get the largest of the two and use it to as the
            //speed of the object

            float speedVal = Math.Max(Math.Abs(speed.X), Math.Abs(speed.Y));

            if (playerPosition.X < position.X)
            {
                position.X -= speedVal;
            }

            else if (playerPosition.X > position.X)
            {
                position.X += speedVal;
            }

            if (playerPosition.Y < position.Y)
            {
                position.Y -= speedVal;
            }

            else if (playerPosition.Y > position.Y)
            {
                position.Y += speedVal;
            }


            base.Update(gameTime, clientsbounds);
        }
    }
}
