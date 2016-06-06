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
        int randomSpeedMin = 1;
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
            for(int i = 0; i < spriteList.Count; i++)
            {
                Sprite sprite = spriteList[i];

                sprite.Update(gameTime, Game.Window.ClientBounds);

                if (sprite.GetCollisionRect.Intersects(player.GetCollisionRect))
                {
                    Game.Exit();
                    break; 
                }

                //Remove if object if it is out of bounds
                if (sprite.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    spriteList.Remove(sprite);
                }


            }

            //Spawn the enemys

            //When timer is less than zero spawn an enemy sprite
            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds - 1;

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

            //Randomly Spawn Chasing Sprites
            int chasingSpriteFlag = myRandom.Next(10);

            if (chasingSpriteFlag % 2 == 0)
            {
                spriteList.Add(new ChasingSprite(Game.Content.Load<Texture2D>(@"images\plus"),
                    position, new Point(75, 75), 10, new Point(0, 0), new Point(6, 4), speed, this));

            }
            else
            {
                spriteList.Add(new EvadingSprite(Game.Content.Load<Texture2D>(@"images\star"),
                    position, new Point(200, 200), 10, new Point(0, 0), new Point(1, 1), speed, this));
            }


        }

        //Find the player's position on the board
        public Vector2 GetPlayerPosition()
        {
            return player.GetPosition;
        }

        public override void Initialize()
        {
            ResetSpawnTime();
            base.Initialize();
        }


    }
}
