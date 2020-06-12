using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SnakeOnCSharp
{
    class Game
    {

        public int tickPerSecond;

        private Snake snake;

        private World world;

        private Render render;
        public Game(int tickPerSec = 20)
        {
            snake = new Snake();
            snake.OnCheck += Snake_OnCheck;
            snake.OnEat += Snake_OnEat;
            snake.OnCut += Snake_OnCut;

            world = new World();
            world.Add(snake.snakePeaces.ToArray());

            render = new Render(world);
            
            tickPerSecond = 1000/tickPerSec;
        }

        private void Snake_OnCut(object sender, List<SnakePeace> peaces)
        {
            foreach (var peace in peaces)
            {
                world.Add(new Food(peace.Position));
            }
        }

        private void Snake_OnEat(object sender, Food obj, SnakePeace peace)
        {
            world.Add(peace);
        }

        private IObject Snake_OnCheck(object sender, Vector2 pos)
        {
           return world.Get(pos);
        }

        public void Start()
        {
            Vector2 moveVector = new Vector2(1, 0);

            Random random = new Random();

            Mutex mut = new Mutex();

            void Input_OnKeyPressed(object sender, ConsoleKey key)
            {
                mut.WaitOne();

                Vector2 prevVector = new Vector2(moveVector);

                switch (key)
                {
                    case ConsoleKey.W: moveVector = new Vector2(0, -1); break;
                    case ConsoleKey.A: moveVector = new Vector2(-1, 0); break;
                    case ConsoleKey.S: moveVector = new Vector2(0, 1); break;
                    case ConsoleKey.D: moveVector = new Vector2(1, 0); break;
                }

                if (world.Get(snake.snakePeaces[0].Position + moveVector).GetType() == typeof(SnakePeace))
                    moveVector = prevVector;

                mut.ReleaseMutex();
            }

            Input input = new Input();
            input.OnKeyPressed += Input_OnKeyPressed;
            input.Start();

            while (true)
            {
                Thread.Sleep(tickPerSecond);

                mut.WaitOne();

                snake.Move(moveVector);

                mut.ReleaseMutex();

                if (random.Next(0, 70) == 35)
                {
                    var coords = world.GetEmptyCoords();
                    if (coords.Count > 0)
                    {
                        var rand = random.Next(0, coords.Count - 1);
                        world.Add(new Food(coords[rand]));
                    }
                }

                render.WriteToConsole(render.renderImage());
            }
        
        }

        
    }
}
