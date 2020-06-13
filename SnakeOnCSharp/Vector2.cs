using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SnakeOnCSharp
{
    class Vector2
    {

        public int X;
        public int Y;

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Vector2(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            if (b is null && a is null)
                return true;

            if (a is null)
                return false;

            if (b is null)
                return false;

            return ReferenceEquals(a,b) || (a.X == b.X && a.Y == b.Y);
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }

    }
}
