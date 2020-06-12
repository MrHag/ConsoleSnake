using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SnakeOnCSharp
{
    interface IObject
    {
        public delegate Vector2 PositionChangeEvent(object sender, Vector2 position);

        public event PositionChangeEvent ChangePosition;

        public Vector2 Position { get; set; }

        public abstract string ToString();
    }
}
