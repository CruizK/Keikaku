using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Keikaku.Character
{
    public class IPlayerState
    {
        public static OnGroundState OnGroundState = new OnGroundState();
        public static InAirState InAirState = new InAirState();
        public static JumpingState JumpingState = new JumpingState();
        public static FallingState FallingState = new FallingState();
        public static WallSlideState WallSlideState = new WallSlideState();
        public IPlayerState()
        {
            
        }

        public virtual void Update(ref Player player) { }
    }
}
