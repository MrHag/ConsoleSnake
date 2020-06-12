using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeOnCSharp
{
    class SnakePeace : SceneObject
    {
        public SnakePeace(Vector2 pos = default) : base(pos)
        { }
        public override string ToString()
        {
            return "1";
        }
    }
}
