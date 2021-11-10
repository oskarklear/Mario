using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mario.Sprites.Items;
using Mario.Sprites.Mario;

namespace Mario.Trackers
{
    public class HUD
    {
        Vector2 PlayerHUDPos;
        Vector2 TimeHUDPos;
        Vector2 LivesHUDPos;
        Vector2 ScoreHUDPos;
        Vector2 CoinHUDPos;
        Vector2 CoinSpritePos;
        Vector2 MarioIconPos;
        MapCoin coinSprite;
        MarioIcon Marioicon;
        SpriteFont HeadsUpDisplay;
        StatTracker Tracker;
        Game1 Theatre;

        public HUD(Game1 theatre, StatTracker statTracker)
        {
            Theatre = theatre;
            Tracker = statTracker;
            PlayerHUDPos.X = Theatre.map.camera.Position.X + 5;
            PlayerHUDPos.Y = Theatre.map.camera.Position.Y + 5;
            ScoreHUDPos.X = Theatre.map.camera.Position.X + 5;
            ScoreHUDPos.Y = Theatre.map.camera.Position.Y + 23;
            LivesHUDPos.X = Theatre.map.camera.Position.X + 100;
            LivesHUDPos.Y = Theatre.map.camera.Position.Y + 23;        
            CoinHUDPos.X = Theatre.map.camera.Position.X + 50;
            CoinHUDPos.Y = Theatre.map.camera.Position.Y + 23;
            CoinSpritePos.X = Theatre.map.camera.Position.X + 35;
            CoinSpritePos.Y = Theatre.map.camera.Position.Y + 21;
            coinSprite = new MapCoin(Theatre, CoinSpritePos);
            MarioIconPos.X = Theatre.map.camera.Position.X + 83;
            MarioIconPos.Y = Theatre.map.camera.Position.Y + 19;
            Marioicon = new MarioIcon(theatre, MarioIconPos);
            TimeHUDPos.X = Theatre.map.camera.Position.X + 163;
            TimeHUDPos.Y = Theatre.map.camera.Position.Y + 5;
            HeadsUpDisplay = Theatre.Content.Load<SpriteFont>("HUD");
        }

        public void Update()
        {
            PlayerHUDPos.X = Theatre.map.camera.Position.X + 5;
            PlayerHUDPos.Y = Theatre.map.camera.Position.Y + 5;
            ScoreHUDPos.X = Theatre.map.camera.Position.X + 5;
            ScoreHUDPos.Y = Theatre.map.camera.Position.Y + 23;
            LivesHUDPos.X = Theatre.map.camera.Position.X + 100;
            LivesHUDPos.Y = Theatre.map.camera.Position.Y + 23;
            CoinHUDPos.X = Theatre.map.camera.Position.X + 50;
            CoinHUDPos.Y = Theatre.map.camera.Position.Y + 23;
            CoinSpritePos.X = Theatre.map.camera.Position.X + 35;
            CoinSpritePos.Y = Theatre.map.camera.Position.Y + 21;
            coinSprite.Position = CoinSpritePos;
            coinSprite.Update();
            MarioIconPos.X = Theatre.map.camera.Position.X + 83;
            MarioIconPos.Y = Theatre.map.camera.Position.Y + 19;
            Marioicon.Position = MarioIconPos;
            TimeHUDPos.X = Theatre.map.camera.Position.X + 163;
            TimeHUDPos.Y = Theatre.map.camera.Position.Y + 5;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Theatre.map.camera.GetViewMatrix(new Vector2(1f)));
            spriteBatch.DrawString(HeadsUpDisplay, "Mario", PlayerHUDPos, Color.White);
            spriteBatch.DrawString(HeadsUpDisplay, "TIME\n" + (Tracker.timeRemaining / 60).ToString(), TimeHUDPos, Color.Yellow);
            spriteBatch.DrawString(HeadsUpDisplay, "X" + Tracker.lives.ToString(), LivesHUDPos, Color.White);
            spriteBatch.DrawString(HeadsUpDisplay, "X" + Tracker.coins.ToString(), CoinHUDPos, Color.White);
            spriteBatch.DrawString(HeadsUpDisplay, Tracker.points.ToString(), ScoreHUDPos, Color.White);
            coinSprite.Draw(spriteBatch);
            Marioicon.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
