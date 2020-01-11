using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace Keikaku.Character
{
    public class InAirState : IPlayerState
    {

        public override void Update(ref Player player)
        {
            player.acceleration = 29;

            if(player.collidingX && InputManager.IsKeyDown(Keys.A) != InputManager.IsKeyDown(Keys.D))
            {
                if (player.speed.Y < 0)
                    player.speed.Y = 0;
                else
                    player.speed.Y = Math.Min(player.speed.Y, 100);
                player.ChangeState(WallSlideState);
            }
            else if (player.speed.Y > 0)
            {
                player.ChangeState(FallingState);
            }
            if (player.onGround)
            {
                player.ChangeState(OnGroundState);
            }
        }
    }
}
