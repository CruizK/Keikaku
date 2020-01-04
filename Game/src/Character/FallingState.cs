using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keikaku.Character
{
    public class FallingState : InAirState
    {

        public override void Update(ref Player player)
        {
            player.speed.Y += 25f;
            player.ChangeAnimation("falling");

            base.Update(ref player);
        }
    }
}
