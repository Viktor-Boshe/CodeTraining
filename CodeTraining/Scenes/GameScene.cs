// GameScene
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTraining.Game_Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CodeTraining.GameManager;
namespace CodeTraining.Scenes
{
    internal class GameScene : Component
    {
        Player player;
        Input input;
        private Texture2D[] backgroundFrames;

        private int currentFrame;
        private double frameTimer;
        private const double frameInterval = 140;
        private Rectangle windowRectangle;
        public override void Init(ContentManager contentManager)
        {
            player = new Player();
            input = new Input();
            player.LoadContent(contentManager);
            backgroundFrames = new Texture2D[8];
            for(int i = 0;i < backgroundFrames.Length; i++)
            {
                backgroundFrames[i] = contentManager.Load<Texture2D>($"GameBackground/back-{i}");
            }
            windowRectangle = new Rectangle(0, 0, Data.ScreenWidth, Data.ScreenHeight);
        }

        public override void Update(GameTime gameTime)
        {

            frameTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer > frameInterval)
            {
                currentFrame = (currentFrame + 1) % backgroundFrames.Length;
                frameTimer = 0;
            }

            input.UpdateKeyboard(gameTime);
            player.Update(gameTime, input);
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(backgroundFrames[currentFrame], windowRectangle, Color.White);
            player.Draw(_spriteBatch);
        }

    }
}
