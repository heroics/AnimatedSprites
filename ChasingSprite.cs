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

            //First, move the sprite along its direction vector
            position += speed;

            //Find the player using spriteManager
            Vector2 playerPosition = spriteManager.GetPlayerPosition();

            //If the player is moving vectically, chase horizontally
            if (speed.X == 0)
            {
                if(playerPosition.X < position.X)
                {
                    position.X -= Math.Abs(speed.Y);
                }
                else if(playerPosition.X > position.X)
                {
                    position.X += Math.Abs(speed.Y);
                }
            }

            //If the player is moving vertically, chase vertically
            if (speed.Y == 0)
            {
                if (playerPosition.Y < position.Y)
                {
                    position.Y -= Math.Abs(speed.X);
                }
                else if (playerPosition.Y > position.Y)
                {
                    position.Y += Math.Abs(speed.X);
                }

            }
            base.Update(gameTime, clientsbounds);
        }
    }
}
