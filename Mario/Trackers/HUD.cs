using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Items;
using Mario.States;

namespace Mario.Trackers
{
    public class HUD
    {
        Vector2 TimeHUDPos;
        Vector2 LivesHUDPos;
        Vector2 ScoreHUDPos;
        Vector2 CoinHUDPos;
        Vector2 CoinSpritePos;
        MapCoin coinSprite;
        SpriteFont HeadsUpDisplay;
        StatTracker Tracker;
        Game1 Theatre;

        public HUD(Game1 theatre, StatTracker statTracker)
        {
            Theatre = theatre;
            Tracker = statTracker;
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
            if (Tracker.lives == 0)
            {
                Theatre.map.menu.SwitchOverlay(new GameOverState(HeadsUpDisplay));
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Theatre.map.camera.GetViewMatrix(new Vector2(1f)));
            spriteBatch.DrawString(HeadsUpDisplay, "TIME\n" + (Tracker.timeRemaining / 60).ToString(), TimeHUDPos, Color.Blue);
            spriteBatch.DrawString(HeadsUpDisplay, "Lives: " + Tracker.lives.ToString(), LivesHUDPos, Color.Blue);
            spriteBatch.DrawString(HeadsUpDisplay, "X" + Tracker.coins.ToString(), CoinHUDPos, Color.Blue);
            spriteBatch.DrawString(HeadsUpDisplay, Tracker.points.ToString(), ScoreHUDPos, Color.Blue);
            coinSprite.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
