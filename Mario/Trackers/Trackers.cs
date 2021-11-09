using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Items;

namespace Mario.Trackers
{
    public class StatTracker
    {
        protected const int FRAMESPERSECOND = 60;

        public int lives;
        public int coins;
        public int points;
        public int timeRemaining;

        Vector2 TimeHUDPos;
        Vector2 LivesHUDPos;
        Vector2 ScoreHUDPos;
        Vector2 CoinHUDPos;
        Vector2 CoinSpritePos;
        MapCoin coinSprite;
        SpriteFont HeadsUpDisplay;
        Game1 Theatre;
        //public bool lifeRemovedAfterTimeRemainingIsZero;

        public StatTracker(Game1 theatre)
        {
            Theatre = theatre;           
            TimeHUDPos.X = Theatre.map.camera.Position.X + 200;
            TimeHUDPos.Y = Theatre.map.camera.Position.Y;
            LivesHUDPos.X = Theatre.map.camera.Position.X + 80;
            LivesHUDPos.Y = Theatre.map.camera.Position.Y + 15;
            ScoreHUDPos.X = Theatre.map.camera.Position.X;
            ScoreHUDPos.Y = Theatre.map.camera.Position.Y + 15;
            CoinHUDPos.X = Theatre.map.camera.Position.X + 45;
            CoinHUDPos.Y = Theatre.map.camera.Position.Y + 15;
            CoinSpritePos.X = Theatre.map.camera.Position.X + 30;
            CoinSpritePos.Y = Theatre.map.camera.Position.Y + 13;
            coinSprite = new MapCoin(Theatre, CoinSpritePos);
            HeadsUpDisplay = Theatre.Content.Load<SpriteFont>("HUD");
            lives = 3;
            coins = 0;
            points = 0;
            timeRemaining = FRAMESPERSECOND * 400;
            //lifeRemovedAfterTimeRemainingIsZero = false;
        }

        public void AddCoinCommand()
        {
            coins++;
            if (coins % 100 == 0) AddLifeCommand();
        }

        public void AddLifeCommand()
        {
            lives++;
        }

        public void RemoveLifeCommand()
        {
            lives--;
        }

        public void AddPointsCommand(int pointsToAdd)
        {
            points += pointsToAdd;
        }

        public void DecrementTimeCommand()
        {
            if (timeRemaining > 0) timeRemaining--;
/*            else if (timeRemaining == 0 && !lifeRemovedAfterTimeRemainingIsZero)
            {
                lifeRemovedAfterTimeRemainingIsZero = true;
                
            }*/
        }

        public void ResetTimeRemainingCommand()
        {
            timeRemaining = FRAMESPERSECOND * 400;
            //lifeRemovedAfterTimeRemainingIsZero = false;
            RemoveLifeCommand();
        }

        public void Update()
        {
            TimeHUDPos.X = Theatre.map.camera.Position.X + 200;
            TimeHUDPos.Y = Theatre.map.camera.Position.Y;
            LivesHUDPos.X = Theatre.map.camera.Position.X + 80;
            LivesHUDPos.Y = Theatre.map.camera.Position.Y + 15;
            ScoreHUDPos.X = Theatre.map.camera.Position.X;
            ScoreHUDPos.Y = Theatre.map.camera.Position.Y + 15;
            CoinHUDPos.X = Theatre.map.camera.Position.X + 45;
            CoinHUDPos.Y = Theatre.map.camera.Position.Y + 15;
            CoinSpritePos.X = Theatre.map.camera.Position.X + 30;
            CoinSpritePos.Y = Theatre.map.camera.Position.Y + 13;
            coinSprite.Position = CoinSpritePos;
            coinSprite.Update();
            //System.Diagnostics.Debug.WriteLine("Coins: " + tracker.coins);
            //System.Diagnostics.Debug.WriteLine("Lives: " + tracker.lives);
            if (timeRemaining % 60 == 0)
                System.Diagnostics.Debug.WriteLine("Time Remaining: " + timeRemaining / 60);
            //System.Diagnostics.Debug.WriteLine(tracker.lifeRemovedAfterTimeRemainingIsZero);
            DecrementTimeCommand();
            //if (timeRemaining == 0) map.Reset();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Theatre.map.camera.GetViewMatrix(new Vector2(1f)));
            spriteBatch.DrawString(HeadsUpDisplay, "TIME\n" + (timeRemaining / 60).ToString(), TimeHUDPos, Color.Blue);
            spriteBatch.DrawString(HeadsUpDisplay, "Lives: " + lives.ToString(), LivesHUDPos, Color.Blue);
            spriteBatch.DrawString(HeadsUpDisplay, "X" + coins.ToString(), CoinHUDPos, Color.Blue);
            spriteBatch.DrawString(HeadsUpDisplay, points.ToString(), ScoreHUDPos, Color.Blue);
            coinSprite.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
