using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Keikaku.Character
{
    public class OnGroundState : IPlayerState
    {
        public OnGroundState()
        {

        }

        public override void Update(ref Player player)
        {
            player.acceleration = 50; 
            if (InputManager.IsKeyDown(Keys.Space))
            {
                player.speed.Y = -500;
                player.ChangeState(JumpingState); // Jump State
            }
            else if (!player.onGround)
            {
                player.ChangeState(FallingState); // Falling State
            }
            else if (player.speed.X == 0)
            {
                player.ChangeAnimation("standing");
            }
            else if (player.speed.X != 0)
            {
                player.ChangeAnimation("walking");
            }

            // --- walking state ---
            // if (Keys.a == Keys.d)
            //   go back to previous state
            // else 
            //    OnGround.Update(player)

            // -- JumpingState --
            // if(onGround)
            //  go back to previous state
            // else
            //  InAir.Update(player)

            // I guess InAir, could have movement code, but then you duplicate it in OnGround and InAir
            // You could abstract this then to the base player class since they can move no matter the conditions
            // but what if you had an 
            
            // In this system we cannot both move and jump, we can only do one and when we do the other
            // The other will be erased until we hit its input again
        }
    }
}
