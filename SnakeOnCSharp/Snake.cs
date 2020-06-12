using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SnakeOnCSharp
{
    class Snake
    {
        public List<SnakePeace> snakePeaces;

        public delegate void SnakeEatEvent(object sender, Food obj, SnakePeace peace);

        public delegate void SnakeCutEvent(object sender, List<SnakePeace> peaces);

        public delegate IObject SnakeCheckEvent(object sender, Vector2 pos);

        public event SnakeEatEvent OnEat;

        public event SnakeCheckEvent OnCheck;

        public event SnakeCutEvent OnCut;

        public Snake()
        {
            snakePeaces = new List<SnakePeace>() { new SnakePeace(new Vector2(5, 5)), new SnakePeace(new Vector2(5, 6)), new SnakePeace(new Vector2(5, 8)), new SnakePeace(new Vector2(1, 6)), new SnakePeace(new Vector2(1, 5)) };
        }

        private void Eat(Food obj)
        {
            for (int i = 0; i < obj.Mass; i++)
            {
                var pos = obj.Position;
                var peace = new SnakePeace(pos);
                snakePeaces.Insert(0, peace);
                OnEat?.Invoke(this, obj, peace);
            }
        }

        private void Cut(SnakePeace peace)
        {
            var index = snakePeaces.IndexOf(peace);
            var specCount = snakePeaces.Count - index;
            var peaces = snakePeaces.GetRange(index, specCount);
            snakePeaces.RemoveRange(index, specCount);
            OnCut?.Invoke(this, peaces);
        }

        public void Move(Vector2 relativePos)
        {
            Move(relativePos.X, relativePos.Y);
        }
        public void Move(int x, int y)
        { 
            if (OnCheck != null)
            {
                var headpos = snakePeaces[0].Position;

                Vector2 pos = headpos + new Vector2(x, y);
                IObject obj;
                if ((obj = OnCheck.Invoke(this, pos)).GetType() != typeof(SceneObject))
                {
                    if (obj is SnakePeace peace)
                        Cut(peace);
                    else
                    if (obj is Food food)
                        Eat(food);      
                }
                else
                for (int i = 0; i < snakePeaces.Count; i++)
                {
                    var peace = snakePeaces[i];
                    Vector2 tmppos = peace.Position;
                    peace.Position = pos;
                    pos = tmppos;
                }
            }
        }
    }
}
