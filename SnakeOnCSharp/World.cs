using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeOnCSharp
{
    class World
    {
        private List<List<IObject>> area;

        public int GWidth { get; private set; }

        public int GHeight { get; private set; }

        public World(int Width = 50, int Height = 50)
        {
            GWidth = Width;
            GHeight = Height;

            area = new List<List<IObject>>(GHeight);

            for (int i = GHeight-1; i >= 0; i--)
            {
                var innerlist = new List<IObject>(GWidth);
                for (int j = 0; j < GWidth; j++)
                {
                    IObject obj = new SceneObject(new Vector2(j, i));
                    innerlist.Add(obj);
                }

                area.Add(innerlist);

            }
        }
        public void Add(IObject obj, Vector2 pos = null)
        {
            if (pos == null)
                pos = obj.Position;

            obj.ChangePosition += OnPosition;
            var coord = ConverseCoord(pos);
            Get(obj.Position).ChangePosition -= OnPosition;
            area[coord.Y][coord.X] = obj;
        }

        public List<Vector2> GetEmptyCoords()
        {
            List<Vector2> coords = new List<Vector2>();

            for (int i = 0; i < GHeight; i++)
            {
                for (int j = 0; j < GWidth; j++)
                {
                    IObject obj;
                    if((obj = Get(new Vector2(j,i))).GetType() == typeof(SceneObject))
                    {
                        coords.Add(obj.Position);
                    }
                }

            }
            return coords;
        }

        public List<Vector2> GetNotEmptyCoords()
        {
            List<Vector2> coords = new List<Vector2>();

            for (int i = 0; i < GHeight; i++)
            {

                for (int j = 0; j < GWidth; j++)
                {
                    IObject obj;
                    if ((obj = Get(new Vector2(j, i))).GetType() != typeof(SceneObject))
                    {
                        coords.Add(obj.Position);
                    }
                }

            }
            return coords;
        }

        public void Add(IObject[] obj)
        {
            foreach (var sobj in obj)
                Add(sobj);
        }

        public IObject Get(Vector2 coord)
        {
            var conCoord = ConverseCoord(NormalizeCoord(coord));
            return area[conCoord.Y][conCoord.X];
        }
            

        private Vector2 ConverseCoord(Vector2 vector2)
        {
            Vector2 vector = new Vector2(vector2.X, vector2.Y);
            vector.Y = GHeight-1 - vector.Y;
            return vector;
        }

        private Vector2 NormalizeCoord(Vector2 vector2)
        {
                Vector2 output = new Vector2(vector2.X, vector2.Y);
                if (vector2.X >= GWidth)
                    output.X = vector2.X - (int)(vector2.X / (GWidth - 1)) * GWidth;
                else
                if (vector2.X < 0)
                    output.X = GWidth - 1 + (vector2.X - (int)(vector2.X / (GWidth - 1)) * GWidth);

                if (vector2.Y >= GHeight)
                    output.Y = vector2.Y - (int)(vector2.Y / (GHeight - 1)) * GHeight;
                else
                if (vector2.Y < 0)
                    output.Y = GHeight - 1 + (vector2.Y - (int)(vector2.Y / (GHeight - 1)) * GHeight);
                return output;
        }

        private Vector2 OnPosition(object sender, Vector2 position)
        {
            var pos = NormalizeCoord(position);
            var obj = (IObject)sender;
            Add(new SceneObject(obj.Position));
            Add(obj, pos);
            return pos;
        }

        public void Remove(IObject obj)
        {
            if (obj == Get(obj.Position))
            {
                obj.ChangePosition -= OnPosition;
                Add(new SceneObject(obj.Position));
            }
        }
        public void Remove(IObject[] objs)
        {
            foreach(var obj in objs)
            Remove(obj);
        }
    }
}
