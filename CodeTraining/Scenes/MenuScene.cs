// MenuScene.cs
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

namespace CodeTraining.Menu
{
    internal class MenuScene : Component
    {
        private Input input;
        public static Texture2D[] buttons = new Texture2D[3];
        private Texture2D[] backgroundFrames;

        private int currentFrame;
        private double frameTimer;
        private const double frameInterval = 100;

        public Rectangle[] buttonRectangles = new Rectangle[buttons.Length];
        private Rectangle windowRectangle;

        private const int xSpacing = 5;
        private const int ySpacing = 50;
        private const int ButtonSpacing = 100;

        private bool playButtonClicked = false;
        private int playAnimationIndex = 0;
        private Texture2D[] playAnimationFrames;

        public override void Init(ContentManager contentManager)
        {
            input = new Input();
            backgroundFrames = new Texture2D[14];
            playAnimationFrames = new Texture2D[23];
            for (int i = 0; i < backgroundFrames.Length; i++)
            {
                backgroundFrames[i] = contentManager.Load<Texture2D>($"MenuBackground/menuBackgroundIdle-{i}");
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                int buttonYspace = ySpacing + ButtonSpacing * i;
                buttons[i] = contentManager.Load<Texture2D>($"buttonTextures/button_{i}");
                buttonRectangles[i] = new Rectangle(xSpacing, buttonYspace, buttons[i].Width, buttons[i].Height);
            }
            for (int i = 0; i < playAnimationFrames.Length; i++)
            {
                playAnimationFrames[i] = contentManager.Load<Texture2D>($"MenuBackground/MenuBackgroundPlay-{i}");
            }
            windowRectangle = new Rectangle(0, 0, Data.ScreenWidth, Data.ScreenHeight);
        }

        public override void Update(GameTime gameTime)
        {
            input.UpdateMouse(gameTime);

            if (!playButtonClicked)
            {
                frameTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (frameTimer > frameInterval)
                {
                    currentFrame = (currentFrame + 1) % backgroundFrames.Length;
                    frameTimer = 0;
                }
            }
            else
            {
                PlayButtonAnimation(gameTime);
            }
            if (input.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (!playButtonClicked && input.mouseRectangle.Intersects(buttonRectangles[0]))
                {
                    playButtonClicked = true;
                }
                else if (input.mouseRectangle.Intersects(buttonRectangles[1]))
                {
                    Data.CurrentState = GameState.Options;
                }
                else if (input.mouseRectangle.Intersects(buttonRectangles[2]))
                {
                    Data.Exit = true;
                }
            }
        }
        private void PlayButtonAnimation(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer > frameInterval)
            {
                if (playAnimationIndex < playAnimationFrames.Length)
                {
                    currentFrame = playAnimationIndex;
                    playAnimationIndex++;
                }
                else
                {
                    playButtonClicked = false;
                    playAnimationIndex = 0;

                    Data.CurrentState = GameState.Game;
                }

                frameTimer = 0;
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            if (!playButtonClicked)
            {
                _spriteBatch.Draw(backgroundFrames[currentFrame], windowRectangle, Color.White);
                for (int i = 0; i < buttons.Length; i++)
                {
                    _spriteBatch.Draw(buttons[i], buttonRectangles[i], input.mouseRectangle.Intersects(buttonRectangles[i]) ? Color.Gray : Color.White);
                }
            }
            else
            {
                _spriteBatch.Draw(playAnimationFrames[currentFrame], windowRectangle, Color.White);
            }
        }
    }
}