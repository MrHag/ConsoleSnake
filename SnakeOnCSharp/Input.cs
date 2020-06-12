using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeOnCSharp
{
    class Input
    {

        public delegate void InputEvent(object sender, ConsoleKey key);

        public event InputEvent OnKeyPressed;

        private Thread thread;

        public Input()
        { }

        public void Start()
        {
            thread = new Thread(Listening);
            thread.Start();
        }

        private void Listening()
        {
            while (true)
            {
                using (var g = Console.In)
                {
                    OnKeyPressed?.Invoke(this, Console.ReadKey(true).Key);
                }
            }
        }

        public void Stop()
        {
            thread.Abort();
        }

    }
}
