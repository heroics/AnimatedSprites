using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace AnimatedSprites
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



        //Class Level Variables
        Texture2D ringsTexture;
        const float ringsSpeed = 6;
        Vector2 skullSpeed = new Vector2(4, 1);
        Vector2 plusSpeed = new Vector2(5, 2);

        Vector2 ringsPosition = Vector2.Zero;

        //Image Data
        Point ringsFrameSize = new Point(75, 75);
        Point ringsCurrentFrame = new Point(0, 0);
        Point ringsSheetSize = new Point(6, 8);

        //Framerate Data
        int ringsTimeSinceLastFrame = 0;
        int ringsMillisecondsPerFrame = 50;


        //SkullBall Class Level Variables
        Texture2D skullTexture;
        Point skullFrameSize = new Point(75, 75);
        Point skullCurrentFrame = new Point(0, 0);
        Point skullSheetSize = new Point(6, 8);
        Vector2 skullPosition = new Vector2(100, 100);
        int skullTimeSinceLastFrame = 0;
        const int skullMillisecondsPerFrame = 50;

        //Plus Class Level Variables 
        Texture2D plusTexture;
        Point plusFrameSize = new Point(75, 75);
        Point plusCurrentFrame = new Point(0, 0);
        Point plusSheetSize = new Point(6, 3);
        Vector2 plusPosition = new Vector2(260, 260);
        int plusTimeSinceLastFrame = 0;
        const int plusMillisecondsPerFrame = 50;

        //MouseState Variable
        MouseState prevMouseState = Mouse.GetState();

        //Collisions Rectangle Offsets
        int ringsCollisionRectOffset = 10;
        int skullCollisionRectOffset = 10;
        int plusCollisionRectOffset = 10;

        //SpriteManager
        SpriteManager spriteManager;

        //Audio File
        SoundEffect wallBounce;
        SoundEffectInstance wallBounceInstance;

        //Game music
        private Song gameMusic;

        //Random Number Generator 
        public Random random
        {
            get;
            private set;
        }

        //Randomly Spawned Sprites
        int skullSpawnMinMilliseconds = 1000;
        int skullSpawnMaxMilliseconds = 2205;
        int skullSpawnMinSpeed = 2;
        int skullSpawnMaxSpeed = 6;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.random = new Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ringsTexture = Content.Load<Texture2D>(@"Images\threerings");
            skullTexture = Content.Load<Texture2D>(@"Images\skullball");
            plusTexture = Content.Load<Texture2D>(@"Images\plus");

            wallBounce = Content.Load<SoundEffect>(@"Audio\bounceSound");
            wallBounceInstance = wallBounce.CreateInstance();

            gameMusic = Content.Load<Song>(@"Audio\intro");
            MediaPlayer.Play(gameMusic);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            wallBounceInstance.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update time since last frame and only change
            //animation if framerate expired
            ringsTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (ringsTimeSinceLastFrame > ringsMillisecondsPerFrame)
            {
                ringsTimeSinceLastFrame -= ringsMillisecondsPerFrame;

                // Update the Rings
                ringsCurrentFrame.X++;


                if (ringsCurrentFrame.X >= ringsSheetSize.X)
                {
                    ringsCurrentFrame.X = 0;
                    ringsCurrentFrame.Y++;
                    if (ringsCurrentFrame.Y >= ringsSheetSize.Y)
                    {
                        ringsCurrentFrame.Y = 0;
                    }
                }
            }

            //Move the current frame through the sequence of frames on the skullball spritesheet
            skullTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (skullTimeSinceLastFrame > skullMillisecondsPerFrame)
            {
                skullTimeSinceLastFrame -= skullMillisecondsPerFrame;
                skullCurrentFrame.X++;

                if (skullCurrentFrame.X >= skullSheetSize.X)
                {
                    skullCurrentFrame.X = 0;
                    skullCurrentFrame.Y++;
                    if (skullCurrentFrame.Y >= skullSheetSize.Y)
                    {
                        skullCurrentFrame.Y = 0;
                    }
                }

            }

            //Move the current frame throught the sequence of frames on the plus spritesheet
            plusTimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (plusTimeSinceLastFrame > plusMillisecondsPerFrame)
            {
                plusTimeSinceLastFrame -= plusMillisecondsPerFrame;

                plusCurrentFrame.X++;

                if (plusCurrentFrame.X >= plusSheetSize.X)
                {
                    plusCurrentFrame.X = 0;
                    plusCurrentFrame.Y++;
                    if (plusCurrentFrame.Y >= plusSheetSize.Y)
                    {
                        plusCurrentFrame.Y = 0;
                    }
                }
            }

            //Keep the sprite in the bounds of the screen
            if (ringsPosition.X > Window.ClientBounds.Width - ringsFrameSize.X || ringsPosition.X < 0)
            {
                ringsPosition.X = Window.ClientBounds.Width - ringsFrameSize.X;
                ringsPosition.Y = Window.ClientBounds.Height - ringsFrameSize.Y;
            }

            if (ringsPosition.Y > Window.ClientBounds.Height - ringsFrameSize.Y || ringsPosition.Y < 0)
            {
                ringsPosition.X = Window.ClientBounds.Width - ringsFrameSize.X;
                ringsPosition.Y = Window.ClientBounds.Height - ringsFrameSize.Y;
            }

            // Have the Sprite Move based on Keyboard input

            // Have the Sprite Move based on Keyboard input
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ringsPosition.X -= ringsSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ringsPosition.X += ringsSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                ringsPosition.Y -= ringsSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                ringsPosition.Y += ringsSpeed;
            }

            //Have the Sprite Move based on Mouse Input
            MouseState mouseState = Mouse.GetState();

            if (mouseState.X != prevMouseState.X ||
                mouseState.Y != prevMouseState.Y)
            {
                ringsPosition = new Vector2(mouseState.X, mouseState.Y);

            }
            prevMouseState = mouseState;

            //Gamepad Input
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            ringsPosition.X += ringsSpeed * gamepadState.ThumbSticks.Left.X;
            ringsPosition.Y -= ringsSpeed * gamepadState.ThumbSticks.Left.Y;

            //Keep the Sprite in the Game window
            if (ringsPosition.X < 0)
            {
                ringsPosition.X = 0;
            }

            if (ringsPosition.Y < 0)
            {
                ringsPosition.Y = 0;
            }

            if (ringsPosition.X > Window.ClientBounds.Width - ringsFrameSize.X)
            {
                ringsPosition.X = Window.ClientBounds.Width - ringsFrameSize.X;
            }

            if (ringsPosition.Y > Window.ClientBounds.Height - ringsFrameSize.Y)
            {
                ringsPosition.Y = Window.ClientBounds.Height - ringsFrameSize.Y;
            }


            //Move the Skull Sprite
            skullPosition += skullSpeed;

            if (skullPosition.X > Window.ClientBounds.Width - skullFrameSize.X ||
                skullPosition.X < 0)
            {
                skullSpeed.X *= -1;
                wallBounceInstance.Play();
            }

            if (skullPosition.Y >
                Window.ClientBounds.Height - skullFrameSize.Y ||
                skullPosition.Y < 0)
            {
                skullSpeed.Y *= -1;
                wallBounceInstance.Play();
            }



            //Move the Plus Sprite
            plusPosition += plusSpeed;

            if (plusPosition.X > Window.ClientBounds.Width - plusFrameSize.X ||
    plusPosition.X < 0)
            {
                plusSpeed.X *= -1;
            }

            if (plusPosition.Y >
                Window.ClientBounds.Height - plusFrameSize.Y ||
                plusPosition.Y < 0)
            {
                plusSpeed.Y *= -1;
            }




            if (Collide())
            {
                Exit();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This method will check if sprites collide with one another.
        /// </summary>

        protected bool Collide()
        {
            //Collision Rectangle Rings
            Rectangle ringsRect = new Rectangle((int)ringsPosition.X, (int)ringsPosition.Y,
                ringsFrameSize.X - (ringsCollisionRectOffset * 2), ringsFrameSize.Y - (ringsCollisionRectOffset * 2));

            //Collision Rectangle Skull
            Rectangle skullRect = new Rectangle((int)skullPosition.X, (int)skullPosition.Y,
                skullFrameSize.X - (skullCollisionRectOffset * 2), skullFrameSize.Y - (skullCollisionRectOffset * 2));

            //Collision Rectangle Plus
            Rectangle plusRect = new Rectangle((int)plusPosition.X, (int)plusPosition.Y,
                plusFrameSize.X - (plusCollisionRectOffset * 2), plusFrameSize.Y - (plusCollisionRectOffset * 2));

            return ringsRect.Intersects(skullRect) || ringsRect.Intersects(plusRect);

        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Azure);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            //Draw The Three Rings
            spriteBatch.Draw(ringsTexture, ringsPosition,
                new Rectangle(ringsCurrentFrame.X * ringsFrameSize.X,
                ringsCurrentFrame.Y * ringsFrameSize.Y, ringsFrameSize.X, ringsFrameSize.Y),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            //Draw The Skullball
            spriteBatch.Draw(skullTexture, skullPosition, new Rectangle(skullCurrentFrame.X * skullFrameSize.X,
                skullCurrentFrame.Y * skullFrameSize.Y, skullFrameSize.X, skullFrameSize.Y),
            Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            //Draw the plus
            spriteBatch.Draw(plusTexture, plusPosition, new Rectangle(plusCurrentFrame.X * plusFrameSize.X,
     plusCurrentFrame.Y * plusFrameSize.Y, plusFrameSize.X, plusFrameSize.Y),
 Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
