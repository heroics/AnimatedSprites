using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    class EvadingSprite : Sprite
    {

        //Save a reference to the sprite manager to use
        //to gget the player positionn
        SpriteManager spriteManager;

        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisonOffSet, Point currentFrame,
            Point sheetSize, Vector2 speed, SpriteManager spriteManager)
            : base(textureImage, position, frameSize, collisonOffSet, currentFrame,
                  sheetSize, speed)

        {
            this.spriteManager = spriteManager;
        }

        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffSet, Point currentFrame,
            Point sheetSize, Vector2 speed, int millisecondsPerFrame, SpriteManager spriteManager)
            : base(textureImage, position, frameSize, collisionOffSet, currentFrame,
                  sheetSize, speed, millisecondsPerFrame)
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

            //First, move the sprite along its current direction vector
            position += speed;

            //Use the player's position to move the sprite closer in
            // the X and/or directions
            Vector2 currentPlayerPosition = spriteManager.GetPlayerPosition();

            //if the plyaer is moving vertically, evade horizontally
            if (speed.X == 0)
            {
                if (currentPlayerPosition.X < position.X)
                {
                    position.X += Math.Abs(speed.Y);
                }
                else if (currentPlayerPosition.X > position.X)
                {
                    position.X -= Math.Abs(speed.Y);
                }
            }

            if (speed.Y == 0)
            {
                if (currentPlayerPosition.Y < position.Y)
                {
                    position.Y += Math.Abs(speed.X);
                }
                else if (currentPlayerPosition.Y > position.Y)
                {
                    position.Y -= Math.Abs(speed.X);
                }
            }


            base.Update(gameTime, clientsbounds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage, position,
                new Rectangle(currentFrame.X * frameSize.X,
                currentFrame.Y * frameSize.Y,
                frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, 0); 
            base.Draw(gameTime, spriteBatch);
        }
    }
}