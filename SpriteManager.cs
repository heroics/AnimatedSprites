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

        public SpriteManager(Game game): base(game)
        {
            
        }

        protected override void LoadContent()
        {
            //Intialize the spriteBatch
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            //Intialize the player object 
            player = new PlayerSprite(Game.Content.Load<Texture2D>(@"image/threerings"),
                Vector2.Zero, new Point(75, 75), 10, new Point(0, 0), new Point(6, 8), new Vector2(6, 6));

            //Add Bomb Sprites
            

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
