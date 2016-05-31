using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        PlayerSprite player;
        List<Sprite> spriteList = new List<Sprite>();

        //Randomly Spawned Sprites
        int skullSpawnMinMilliseconds = 1000;
        int skullSpawnMaxMilliseconds = 2005;
        int skullSpawnMinSpeed = 2;
        int skullSpawnMaxSpeed = 6;
        int nextSpawnTime = 0;

        Random myRandom = new Random();
        int randomSpeedMin = -5;
        int randomSpeedMax = 5;


        public SpriteManager(Game game) : base(game)
        {

        }

        private void ResetSpawnTime()
        {
            nextSpawnTime = ((Game1)Game).random.Next(skullSpawnMinMilliseconds,
                skullSpawnMaxMilliseconds);
        }

        protected override void LoadContent()
        {
            //Intialize the spriteBatch
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            //Intialize the player object 
            player = new PlayerSprite(Game.Content.Load<Texture2D>(@"images/threerings"),
                Vector2.Zero, new Point(75, 75), 10, new Point(0, 0), new Point(6, 8), new Vector2(6, 6));


            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            //Draw the player
            player.Draw(gameTime, spriteBatch);

            //Draw all the other sprites
            foreach (Sprite sprite in spriteList)
            {
                sprite.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //Update the player
            player.Update(gameTime, Game.Window.ClientBounds);

            //Update the other sprites
            foreach (Sprite sprite in spriteList)
            {
                sprite.Update(gameTime, Game.Window.ClientBounds);

                if (sprite.getCollisionRect.Intersects(player.getCollisionRect))
                {
                    Game.Exit();
                    break; 
                }

            }

            //Spawn the enemys

            //When timer is less than zero spawn an enemy sprite
            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;

            if (nextSpawnTime < 0)
            {
                SpawnEnemy();

                //Reset the spawn timer creating an enemy
                ResetSpawnTime();
            }

            base.Update(gameTime);
        }

        private void SpawnEnemy()
        {
            Vector2 speed = Vector2.Zero;
            Vector2 position = Vector2.Zero;

            //Default frame size
            Point frameSize = new Point(75, 75);

            /* Randomly choose which side of the sceen to place enemy
             * then randomly create position along that side of the screen
             * and randomly choose a speed/direction for the enemy
             */

            //Randomly pick a number between 0 & 3
            switch (((Game1)Game).random.Next(4))
            {
                //Move LEFT to RIGHT
                case 0:
                    position = new Vector2(-frameSize.X, ((Game1)Game).random.Next(
                        0, Game.GraphicsDevice.PresentationParameters.BackBufferHeight - frameSize.Y));
                    speed = new Vector2(((Game1)Game).random.Next(
                        skullSpawnMinSpeed, skullSpawnMaxSpeed), 0);
                    break;

                //Move RIGHT to LEFT
                case 1:
                    position = new Vector2(Game.GraphicsDevice.PresentationParameters.BackBufferWidth, (
                        (Game1)Game).random.Next(0, Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));
                    speed = new Vector2(-((Game1)Game).random.Next(skullSpawnMinSpeed, skullSpawnMaxSpeed), 0);
                    break;

                //Move BOTTOM to TOP
                case 2:
                    position = new Vector2(((Game1)Game).random.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth - frameSize.X),
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight);

                    speed = new Vector2(0, -((Game1)Game).random.Next(
                        skullSpawnMinSpeed, skullSpawnMaxSpeed));
                    break;

                //Move TOP to BOTTOM
                case 3:
                    position = new Vector2(((Game1)Game).random.Next(
                        0, Game.GraphicsDevice.PresentationParameters.BackBufferWidth - frameSize.X), -frameSize.Y);

                    speed = new Vector2(((Game1)Game).random.Next(0, Game.GraphicsDevice.PresentationParameters
                        .BackBufferWidth - frameSize.X), -frameSize.Y);
                    break;

            }

            //Create the sprite
            spriteList.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"images\skullball"),
                position, new Point(75, 75), 10, new Point(0, 0), new Point(6, 8), speed));
        }

        public override void Initialize()
        {
            ResetSpawnTime();
            base.Initialize();
        }


    }
}
