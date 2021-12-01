using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Mario.Sprites
{
    class GoalGate : SpriteTemplate
    {
        public GoalGate(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("GoalGate");
            position = location;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

        public void Update()
        {

        }

        public void Collision(ISprite collider)
        {
            
        }
    }


    class GoalGateMovingPart : SpriteTemplate
    {
        bool up;
        Game1 Theatre;
        public SoundEffect win { get; }
        public GoalGateMovingPart(Game1 theatre, Vector2 location)
        {
            Theatre = theatre;
            texture = theatre.Content.Load<Texture2D>("GoalGateMovingPiece");
            position = location;
            
            up = true;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 20, 2);
            win = theatre.Content.Load<SoundEffect>("SoundEffects/course_clear");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

        public override void Update()
        {
            if (up)
            {
                position.Y = position.Y - 1;
                hitbox.Y = hitbox.Y - 1;
                if (position.Y <= 141)
                    up = false;
            }
            else
            {
                position.Y = position.Y + 1;
                hitbox.Y = hitbox.Y + 1;
                if (position.Y >= 240)
                    up = true;
            }
        }

        public override void Collision(ISprite collider)
        {

            if (position.Y > 228 && position.Y <= 240)
            {
                Theatre.tracker.AddPointsCommand(100);
            }
            else if (position.Y > 204 && position.Y <= 228)
            {
                Theatre.tracker.AddPointsCommand(400);
            }
            else if (position.Y > 189 && position.Y <= 204)
            {
                Theatre.tracker.AddPointsCommand(800);
            }
            else if (position.Y > 159 && position.Y <= 189)
            {
                Theatre.tracker.AddPointsCommand(2000);
            }
            else if (position.Y > 145 && position.Y <= 159)
            {
                Theatre.tracker.AddPointsCommand(4000);
            }
            else if (position.Y > 141 && position.Y <= 145)
            {
                Theatre.tracker.AddLifeCommand();
            }
            MediaPlayer.Stop();
            win.Play();
            Theatre.tracker.ConvertTimeToPoints();
            Theatre.map.menu.SwitchOverlay(new WinState(Theatre.map.font,Theatre.map.menu));
            Theatre.IsMenuVisible = true;
        }
    }
}
