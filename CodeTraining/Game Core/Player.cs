using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CodeTraining.Game_Core
{
    internal class Player
    {
        private Texture2D[] playerFrames;
        private Rectangle playerRectangle;
        private int currentFrame;
        private double frameTimer;
        private double frameInterval = 100;

        public void LoadContent(ContentManager contentManager)
        {
            playerFrames = new Texture2D[10];

            for (int i = 0; i < playerFrames.Length; i++)
            {
                playerFrames[i] = contentManager.Load<Texture2D>($"playerTextures/ryu-ts-stance-{i}");
            }
            playerRectangle = new Rectangle(20, 320, playerFrames[0].Width*2, playerFrames[0].Height*2);
        }

        public void Update(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer > frameInterval)
            {
                currentFrame = (currentFrame + 1) % playerFrames.Length;
                frameTimer = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerFrames[currentFrame], playerRectangle, Color.White);
        }
    }
}
