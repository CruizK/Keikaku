using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Keikaku.Utils
{
    public struct RectangleF
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(Vector2 pos, Vector2 size)
        {
            X = pos.X;
            Y = pos.Y;
            Width = size.X;
            Height = size.Y;
        }

        public RectangleF(Vector4 vec4)
        {
            X = vec4.X;
            Y = vec4.Y;
            Width = vec4.Z;
            Height = vec4.W;
        }

        public RectangleF(Rectangle rect)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
        }

        public static Rectangle ToRectangle(RectangleF rect)
        {
            return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        public bool Contains(Vector2 vec2)
        {
            bool contains = vec2.X > X && vec2.X < X + Width && vec2.Y > Y && vec2.Y < Y + Height;
            return contains;
        }

        public bool Contains(Point point)
        {
            return Contains(new Vector2(point.X, point.Y));
        }

        public bool Intersects(Rectangle rect)
        {
            //Left Collision
            bool collision = X + Width >= rect.Left && X <= rect.Right && Y+Height >= rect.Top && Y <= rect.Bottom;

            return collision;
        }

        
        public override string ToString()
        {
            return "{X:" + X + ", Y:" + Y + ", Width:" + Width + ", Height" + Height + "}";
        }
    }
}
