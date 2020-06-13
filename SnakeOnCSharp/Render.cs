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
        }

        public string renderImage()
        {
            string output = "";

            for (int i = 0; i < world.GHeight; i++)
            {
                for (int j = 0; j < world.GWidth; j++)
                {
                    output += world.Get(new Vector2(j, i)) + " ";
                }
            }

            return output;
        }

        public void WriteToConsole(string input)
        {
            if (Console.WindowHeight != world.GHeight || Console.WindowWidth != world.GWidth * 2 || Console.BufferHeight != world.GHeight|| Console.BufferWidth != world.GWidth * 2)
            {
                try
                {
                    Console.SetWindowSize(world.GWidth * 2, world.GHeight);
                    Console.SetBufferSize(world.GWidth * 2, world.GHeight);
                }
                catch (Exception)
                { }
            }

            if (Console.CursorVisible == true)
                Console.CursorVisible = false;

            var buffer = Encoding.UTF8.GetBytes(input);
            using (var stdout = Console.OpenStandardOutput(world.GHeight * world.GWidth*2))
            {
                stdout.Write(buffer, 0, buffer.Length);
                //stdout.Flush();
            }

        }

    }
}
