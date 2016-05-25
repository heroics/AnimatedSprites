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
            //Allow the player to quit the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This method will check if sprites collide with one another.
        /// </summary>

        //protected bool Collide()
        //{
        //    //Collision Rectangle Rings
        //    Rectangle ringsRect = new Rectangle((int)ringsPosition.X, (int)ringsPosition.Y,
        //        ringsFrameSize.X - (ringsCollisionRectOffset * 2), ringsFrameSize.Y - (ringsCollisionRectOffset * 2));

        //    //Collision Rectangle Skull
        //    Rectangle skullRect = new Rectangle((int)skullPosition.X, (int)skullPosition.Y,
        //        skullFrameSize.X - (skullCollisionRectOffset * 2), skullFrameSize.Y - (skullCollisionRectOffset * 2));

        //    //Collision Rectangle Plus
        //    Rectangle plusRect = new Rectangle((int)plusPosition.X, (int)plusPosition.Y,
        //        plusFrameSize.X - (plusCollisionRectOffset * 2), plusFrameSize.Y - (plusCollisionRectOffset * 2));

        //    return ringsRect.Intersects(skullRect) || ringsRect.Intersects(plusRect);

        //}


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Azure);


            base.Draw(gameTime);
        }
    }
}
