using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Mario.States;
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

        public GoalGateMovingPart(Game1 theatre, Vector2 location)
        {
            Theatre = theatre;
            texture = theatre.Content.Load<Texture2D>("GoalGateMovingPiece");
            position = location;
            
            up = true;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 10, 2);
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
            Theatre.map.menu.SwitchOverlay(new WinState(Theatre.map.font));
            Theatre.IsMenuVisible = true;
            //Does nothing
        }
    }
}
