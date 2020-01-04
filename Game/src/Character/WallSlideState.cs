using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;


namespace Keikaku.Character
{
    public class WallSlideState : InAirState
    {

        const int WALL_JUMP_Y = 500;
        const int WALL_JUMP_X = 500;

        public override void Update(ref Player player)
        {
            player.ChangeAnimation("standing");
            player.speed.Y += 10f;
            if (InputManager.IsKeyDown(Keys.Space))
            {
                player.speed.Y = -WALL_JUMP_Y;
                if (player.collidingRight) 
                    player.speed.X = -WALL_JUMP_X;
                else if (player.collidingLeft)
                    player.speed.X = WALL_JUMP_X;
                player.ChangeState(JumpingState);
            }
            else if (InputManager.IsKeyDown(Keys.A) == InputManager.IsKeyDown(Keys.D) || !player.collidingX )
                player.ChangeState(FallingState);

        }
    }
}
