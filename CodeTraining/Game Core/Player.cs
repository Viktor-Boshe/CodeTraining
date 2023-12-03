using CodeTraining.GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace CodeTraining.Game_Core
{
    internal class Player
    {
        private Dictionary<string, Texture2D[]> animations;
        private string currentAnimation;
        public Rectangle playerRectangle;
        private int currentFrame;
        private double frameTimer;
        private double frameInterval = 75;
        private int playerSpeed = 5;

        private bool AttackMove = false;

        public void LoadContent(ContentManager contentManager)
        {
            animations = new Dictionary<string, Texture2D[]>();

            LoadAnimation(contentManager, "Idle", "ryu-ts-stance", 10);
            LoadAnimation(contentManager, "Forward", "ryu-walkf",11);
            LoadAnimation(contentManager, "Backward", "ryu-walkb", 11);
            LoadAnimation(contentManager, "LightPunch", "ryu_punch_light", 5);
            LoadAnimation(contentManager, "HeavyPunch", "ryu_punch_heavy", 8);
            LoadAnimation(contentManager, "Kick", "ryu_kick", 15);

            currentAnimation = "Idle";
            playerRectangle = new Rectangle(20, 320, animations[currentAnimation][0].Width * 2, animations[currentAnimation][0].Height * 2);
        }

        private void LoadAnimation(ContentManager contentManager, string animationName, string textureName, int frameCount)
        {
            Texture2D[] animationFrames = new Texture2D[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                animationFrames[i] = contentManager.Load<Texture2D>($"playerTextures/{textureName}-{i}");
            }

            animations.Add(animationName, animationFrames);
        }

        public void Update(GameTime gameTime, Input input)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer > frameInterval)
            {
                currentFrame = (currentFrame + 1) % animations[currentAnimation].Length;
                frameTimer = 0;
            }
            if (AttackMove)
            {
                if (currentFrame == animations[currentAnimation].Length - 1)
                {
                    AttackMove = false;
                    SwitchAnimation("Idle");
                }
            }
            else
            {
                if (input.IsKeyDown(Keys.J))
                {
                    AttackMove = true;
                    SwitchAnimation("LightPunch");
                }
                else if (input.IsKeyDown(Keys.K))
                {
                    AttackMove = true;
                    SwitchAnimation("HeavyPunch");
                }
                else if (input.IsKeyDown(Keys.L))
                {
                    AttackMove = true;
                    SwitchAnimation("Kick");
                }
                else if (input.IsKeyDown(Keys.D) && AttackMove == false)
                {
                    Move();
                    SwitchAnimation("Forward");
                }
                else if (input.IsKeyDown(Keys.A) && AttackMove == false)
                {
                    Move();
                    SwitchAnimation("Backward");
                }
                else if(AttackMove == false)
                {
                    SwitchAnimation("Idle");
                }
            }
        }

        private void SwitchAnimation(string animationName)
        {
            if (currentAnimation != animationName)
            {
                currentAnimation = animationName;
                currentFrame = 0;

                int originalWidth = animations[currentAnimation][currentFrame].Width * 2;
                int originalHeight = animations[currentAnimation][currentFrame].Height * 2;

                int newX = playerRectangle.X;
                int newY = playerRectangle.Y + (playerRectangle.Height - originalHeight);

                playerRectangle = new Rectangle(newX, newY, originalWidth, originalHeight);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animations[currentAnimation][currentFrame], playerRectangle, Color.White);
            //DrawRectangle(spriteBatch);
        }
        //public void DrawRectangle(SpriteBatch spriteBatch)
        //{
        //    Texture2D rectTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        //    rectTexture.SetData(new Color[] { Color.White });

        //    spriteBatch.Draw(rectTexture, new Rectangle(playerRectangle.Left, playerRectangle.Top, 2, playerRectangle.Height), Color.Red); // Left
        //    spriteBatch.Draw(rectTexture, new Rectangle(playerRectangle.Right - 2, playerRectangle.Top, 2, playerRectangle.Height), Color.Red); // Right
        //    spriteBatch.Draw(rectTexture, new Rectangle(playerRectangle.Left, playerRectangle.Top, playerRectangle.Width, 2), Color.Red); // Top
        //    spriteBatch.Draw(rectTexture, new Rectangle(playerRectangle.Left, playerRectangle.Bottom - 2, playerRectangle.Width, 2), Color.Red); // Bottom
        //}

        public void Move()
        {
            if(currentAnimation == "Forward")
            {
                if (playerRectangle.X + playerRectangle.Width + playerSpeed < Data.ScreenWidth+20)
                {
                    playerRectangle.X += playerSpeed;
                }
            }
            else if(currentAnimation == "Backward")
            {
                if (playerRectangle.X - playerSpeed >= 0-20)
                {
                    playerRectangle.X -= playerSpeed;
                }
            }
        }
    }

}
