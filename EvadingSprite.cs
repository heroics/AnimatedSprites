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
        //to gget the player position
        SpriteManager spriteManager;

        //Variables to improve evasion funtionality
        float evasionSpeedModifier;
        int evasionRange;
        bool isEvading = false;


        //Constructors
        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisonOffSet, Point currentFrame,
            Point sheetSize, Vector2 speed, SpriteManager spriteManager,
            float evasionSpeedModifier, int evasionRange)
            : base(textureImage, position, frameSize, collisonOffSet, currentFrame,
                  sheetSize, speed)

        {
            this.spriteManager = spriteManager;
            this.evasionSpeedModifier = evasionSpeedModifier;
            this.evasionRange = evasionRange;
        }

        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffSet, Point currentFrame,
            Point sheetSize, Vector2 speed, int millisecondsPerFrame, SpriteManager spriteManager,
            float evasionSpeedModifier, int evasionRange)
            : base(textureImage, position, frameSize, collisionOffSet, currentFrame,
                  sheetSize, speed, millisecondsPerFrame)
        {
            this.spriteManager = spriteManager;
            this.evasionSpeedModifier = evasionSpeedModifier;
            this.evasionRange = evasionRange;
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
            if (isEvading)
            {
                if (currentPlayerPosition.X < position.X)
                {
                    position.X += Math.Abs(speed.Y);
                }
                else if (currentPlayerPosition.X > position.X)
                {
                    position.X -= Math.Abs(speed.Y);
                }


                if (currentPlayerPosition.Y < position.Y)
                {
                    position.Y += Math.Abs(speed.X);
                }
                else if (currentPlayerPosition.Y > position.Y)
                {
                    position.Y -= Math.Abs(speed.X);
                }

            }
            else
            {
                if (Vector2.Distance(position, currentPlayerPosition) < evasionRange)
                {
                    //Player is within evasion range, reverse direction and modify speed
                    speed *= -evasionSpeedModifier;
                    isEvading = true;
                }
            }

            base.Update(gameTime, clientsbounds);
        }

    }
}