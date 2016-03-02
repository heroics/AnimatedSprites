using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimatedSprites
{
    class PlayerSprite : Sprite
    {

        //Constructors
        public PlayerSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisonOffset
        , Point sheetSize, Point currentFrame, Vector2 speed) : base(textureImage, position, frameSize, collisonOffset, currentFrame, sheetSize, speed)
        {

        }

        public PlayerSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisonOffset
, Point sheetSize, Point currentFrame, Vector2 speed, int millisecondsPerFrame) : base(textureImage, position, frameSize, collisonOffset, currentFrame, sheetSize, speed, millisecondsPerFrame)
        {

        }

        //Class Level Variables
        MouseState prevMouseState;

        public override void Update(GameTime gameTime, Rectangle clientsbounds)
        {
            //Move the sprite based on the direction
            position += direction;

            //If player moved the mouse, move the sprite
            MouseState currentMouseState = Mouse.GetState();

            if (currentMouseState.X != prevMouseState.Y || currentMouseState.Y != prevMouseState.Y)
            {
                position = new Vector2(currentMouseState.X, currentMouseState.Y);
            }

            //If the sprite is off the screenn, movie it back within the game window 
            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
            }

            if (position.X > clientsbounds.Width - frameSize.X)
            {
                position.X = clientsbounds.Width - frameSize.X;
            }

            if (position.Y > clientsbounds.Height - frameSize.Y)
            {
                position.Y = clientsbounds.Height - frameSize.Y;
            }



            base.Update(gameTime, clientsbounds);
        }
        public override Vector2 direction
        {
            get
            {
                //Get The Direction of User Input
                Vector2 inputDirection = Vector2.Zero;


                //Allow Keyboard Input
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    inputDirection.X -= 1;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    inputDirection.X += 1;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    inputDirection.Y -= 1;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    inputDirection.Y += 1;
                }

                //Allow GamePad Input
                GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                if (gamePadState.ThumbSticks.Left.X != 0)
                {
                    inputDirection.X += gamePadState.ThumbSticks.Left.X;
                }

                if (gamePadState.ThumbSticks.Left.Y != 0)
                {
                    inputDirection.Y -= gamePadState.ThumbSticks.Left.Y;
                }

                return inputDirection * speed;

            }
        }
    }
}
