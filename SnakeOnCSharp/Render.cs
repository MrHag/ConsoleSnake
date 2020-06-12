using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeOnCSharp
{
    class Render
    {
        private World world;
        public Render(World wrld)
        {
            world = wrld;
            Console.SetWindowSize(world.GWidth, world.GHeight);
            Console.SetBufferSize(world.GWidth, world.GHeight);
        }

        public string renderImage()
        {
            string output = "";

            for (int i = 0; i < world.GHeight; i++)
            {
                for (int j = 0; j < world.GWidth; j++)
                {
                    output += world.Get(new Vector2(j, i));
                }
            }

            return output;
        }

        public void WriteToConsole(string input)
        {
            if (Console.WindowHeight != world.GHeight || Console.WindowWidth != world.GWidth)
            {
                try
                {
                    Console.SetWindowSize(world.GWidth, world.GHeight);
                    Console.SetBufferSize(world.GWidth, world.GHeight);
                }
                catch (Exception)
                { }
            }

            if (Console.CursorVisible == true)
                Console.CursorVisible = false;

            Console.SetCursorPosition(0, 0);
            var buffer = Encoding.UTF8.GetBytes(input);
            using (var stdout = Console.OpenStandardOutput(world.GHeight * world.GWidth))
            {
                // fill
                stdout.Write(buffer, 0, buffer.Length);
                // rinse and repeat
            }

        }

    }
}
