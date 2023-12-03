using CodeTraining.GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CodeTraining.Game_Core
{
    internal class OpponentAI
    {
        enum OpponentState
        {
            Idle,
            Forward,
            Backward,
            LightPunch,
            HeavyPunch,
            Kick
        }
        Random random = new Random();
        private OpponentState currentState = OpponentState.Idle;
        private Player player;
        private Dictionary<string, Texture2D[]> animations;
        private string currentAnimation;
        private Rectangle opponentRectangle;
        private int currentFrame;
        private double frameTimer;
        private double frameInterval = 75;
        private int opponentSpeed = 5;
        private bool AttackMove = false;

        public void LoadContent(ContentManager contentManager)
        {
            animations = new Dictionary<string, Texture2D[]>();

            LoadAnimation(contentManager, "Idle", "OpponentTextures/ryu-ts-stance", 10);
            LoadAnimation(contentManager, "Forward", "OpponentTextures/ryu-walkf", 11);
            LoadAnimation(contentManager, "Backward", "OpponentTextures/ryu-walkb", 11);
            LoadAnimation(contentManager, "LightPunch", "OpponentTextures/ryu_punch_light", 5);
            LoadAnimation(contentManager, "HeavyPunch", "OpponentTextures/ryu_punch_heavy", 8);
            LoadAnimation(contentManager, "Kick", "OpponentTextures/ryu_kick", 15);

            currentAnimation = "Idle";
            opponentRectangle = new Rectangle(Data.ScreenWidth - (78 * 2 + 20), 320, animations[currentAnimation][0].Width * 2, animations[currentAnimation][0].Height * 2);
        }
        public OpponentAI(Player player)
        {
            this.player = player;
        }
        private void LoadAnimation(ContentManager contentManager, string animationName, string textureName, int frameCount)
        {
            Texture2D[] animationFrames = new Texture2D[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                animationFrames[i] = contentManager.Load<Texture2D>($"{textureName}-{i}");
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
            UpdateState(input);

            switch (currentState)
            {
                case OpponentState.Idle:
                    break;

                case OpponentState.Forward:
                    Move();
                    break;

                case OpponentState.Backward:
                    Move();
                    break;

                case OpponentState.LightPunch:
                    break;

                case OpponentState.HeavyPunch:
                    break;

                case OpponentState.Kick:
                    break;

                default:
                    break;
            }
        }

        private void UpdateState(Input input)
        {
            if (AttackMove)
            {
                if (currentFrame == animations[currentAnimation].Length - 1)
                {
                    AttackMove = false;
                    SwitchState(OpponentState.Idle);
                }
            }
            else
            {
                if (ShouldMove())
                {
                    SwitchState(OpponentState.Forward);
                }
                else if (ShouldAttack())
                {
                    AttackMove = true;
                    Random random = new Random();
                    int randomNumber = random.Next(3);
                    switch (randomNumber)
                    {
                        case (0):
                            SwitchState(OpponentState.LightPunch);
                            break;
                        case (1):
                            SwitchState(OpponentState.HeavyPunch);
                            break;
                        case (2):
                            SwitchState(OpponentState.Kick); 
                            break;

                    }
                }
                else
                {
                    SwitchState(OpponentState.Idle);
                }
            }
        }

        private bool ShouldAttack()
        {
            if (Math.Abs(opponentRectangle.X - player.playerRectangle.X) < 200)
            {
                return true;
            }
            else { return false; }

        }

        private bool ShouldMove()
        {
            float distanceToPlayer = Math.Abs(opponentRectangle.X - player.playerRectangle.X);
            float moveThreshold = 200f;

            if (distanceToPlayer > moveThreshold)
            {
                SwitchAnimation("Forward");
                return true;
            }
            else if (distanceToPlayer < 150)
            {
                SwitchAnimation("Backward");
                return true;
            }

            return false;
        }

        private void SwitchState(OpponentState newState)
        {
            if (currentState != newState)
            {
                currentState = newState;
                currentAnimation = newState.ToString();
                currentFrame = 0;

                int originalWidth = animations[currentAnimation][currentFrame].Width * 2;
                int originalHeight = animations[currentAnimation][currentFrame].Height * 2;

                int newX = opponentRectangle.X - (originalWidth - opponentRectangle.Width);
                int newY = opponentRectangle.Y + (opponentRectangle.Height - originalHeight);

                opponentRectangle = new Rectangle(newX, newY, originalWidth, originalHeight);
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

                int newX = opponentRectangle.X - (originalWidth - opponentRectangle.Width);
                int newY = opponentRectangle.Y + (opponentRectangle.Height - originalHeight);

                opponentRectangle = new Rectangle(newX, newY, originalWidth, originalHeight);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animations[currentAnimation][currentFrame], opponentRectangle, Color.White);
            //    DrawRectangle(spriteBatch);
        }

        //public void DrawRectangle(SpriteBatch spriteBatch)
        //{
        //    Texture2D rectTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        //    rectTexture.SetData(new Color[] { Color.White });

        //    spriteBatch.Draw(rectTexture, new Rectangle(opponentRectangle.Left, opponentRectangle.Top, 2, opponentRectangle.Height), Color.Red); // Left
        //    spriteBatch.Draw(rectTexture, new Rectangle(opponentRectangle.Right - 2, opponentRectangle.Top, 2, opponentRectangle.Height), Color.Red); // Right
        //    spriteBatch.Draw(rectTexture, new Rectangle(opponentRectangle.Left, opponentRectangle.Top, opponentRectangle.Width, 2), Color.Red); // Top
        //    spriteBatch.Draw(rectTexture, new Rectangle(opponentRectangle.Left, opponentRectangle.Bottom - 2, opponentRectangle.Width, 2), Color.Red); // Bottom
        //}

        public void Move()
        {
            if (currentAnimation == "Forward")
            {
                if (opponentRectangle.X - opponentSpeed >= 0 - 20)
                {
                    opponentRectangle.X -= opponentSpeed;
                }
            }
            else if (currentAnimation == "Backward")
            {
                if (opponentRectangle.X - opponentRectangle.Width + opponentSpeed < Data.ScreenWidth + 20)
                {
                    opponentRectangle.X += opponentSpeed;
                }
            }
        }
    }
}
