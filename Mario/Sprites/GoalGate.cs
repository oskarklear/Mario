using Mario.Sprites.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

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
    }


    class GoalGateMovingPart : SpriteTemplate
    {
        bool up;

        public GoalGateMovingPart(Game1 theatre, Vector2 location)
        {
            texture = theatre.Content.Load<Texture2D>("GoalGateMovingPiece");
            position = location;
            
            up = true;
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
    }
}
