using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    class Sprite
    {
        /*
        Class Variables to be used for each instance of Sprite, and sub-class
        */

        Texture2D textureImage;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;
        int collisionOffset;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 0;
        const int defaultMillisecondsPerFrame = 16;
        protected Vector2 speed;
        protected Vector2 position;


        // Constructors

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffSet,
            Point currentFrame, Point sheetSize, Vector2 speed) : this(textureImage, position, frameSize, collisionOffSet,
                currentFrame, sheetSize, speed, defaultMillisecondsPerFrame)
        {
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,int millisecondsPerFrame)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        //Update the sprite by looping through the sprite sheet
        public virtual void Update(GameTime gameTime, Rectangle clientsbounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if(timeSinceLastFrame >= millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                currentFrame.X++;
                if(currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;
                    if( currentFrame.Y >= sheetSize.Y)
                    {
                        currentFrame.Y = 0;
                    }
                }
            }
        }

        //Drawing The Sprite To the screen.
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage, position,
                new Rectangle(currentFrame.X * frameSize.X,
                currentFrame.Y * frameSize.Y,
                frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, 0);
        }

        public abstract Vector2 direction
        {
            get;
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset, (int)position.Y + collisionOffset, frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));

            }
        }

    }
}
