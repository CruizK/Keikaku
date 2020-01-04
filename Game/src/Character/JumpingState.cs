using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Keikaku.Character
{
    public class JumpingState : InAirState
    {

        public override void Update(ref Player player)
        {
            player.ChangeAnimation("jumping");

            if(InputManager.IsKeyDown(Keys.Space))
            {
                player.speed.Y += 15f;
            }
            else
            {
                player.speed.Y += 25f;
            }

            base.Update(ref player);
        }
    }
}
