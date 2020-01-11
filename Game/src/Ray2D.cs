using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Keikaku
{
    public class Ray2D
    {

        public Vector2 Position;
        public Vector2 Direction;

        public Ray2D()
        {

        }

        public Ray2D(Vector2 pos, Vector2 dir)
        {
            Position = pos;
            Direction = dir;
        }

    }
}
