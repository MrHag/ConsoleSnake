using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeOnCSharp
{
    class SceneObject : IObject
    {
        public event IObject.PositionChangeEvent ChangePosition;

        private Vector2 position;
        public Vector2 Position { 
            get { return position; } 
            set { position = ChangePosition?.Invoke(this, value); } }

        public SceneObject(Vector2 pos = default)
        {
            position = pos;
        }

        public override string ToString()
        {
            return " ";
        }
    }
}
