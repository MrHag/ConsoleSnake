using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeOnCSharp
{
    class Food : SceneObject
    {
        public int Mass { get; private set; }

        public Food(Vector2 pos = default, int mas = 1) : base(pos)
        {
            Mass = mas;
        }

        public override string ToString()
        {
            return "@";
        }

    }
}
