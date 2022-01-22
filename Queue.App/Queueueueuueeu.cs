using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Queue.App
{
    internal class Queueueueuueeu
    {
        private static double result = 0;
        private int n = Environment.ProcessorCount;
        private const int m = 4;
        private static int nm = 0;

        private static double x = 0;
        private static bool stop = false;
        private double a = 0;
        private double b = 0;
        private static double h = 0;
        private Queue<double> queue = new Queue<double>();

        public Queueueueuueeu(double ax, double bx)
        {
            a = ax;
            b = bx;
            h = (b - a) / ((n - 1) * m);
            StartingThreads();
        }
        private void StartingThreads()
        {

            var threadd = new Thread(zxc =>
            {
                while (!stop)
                {
                    try
                    {
                        Monitor.Enter(queue);
                        while(queue.Count < 3)
                        {
                            Monitor.Wait(queue);
                        }
                        while (queue.Count > 0)
                        {
                            result += queue.Dequeue();
                            Console.WriteLine(result);
                        }
                        if(nm >= n - 1)
                        {
                            stop = true;
                        }
                    }
                    finally
                    {
                        nm++;
                        Monitor.Exit(queue);
                    }
                }
            });
            threadd.Start();
            for (int i = 0; i < n - 1; i++)
            {
                var thread = new Thread(new ParameterizedThreadStart(SolveFx));
                thread.Start(i);
            }
        }

        private void SolveFx(object count)
        {
            while(!stop)
            {
                x = a;
                double fx;
                x += ((b - a) / (n - 1)) * (int)count;
                fx = 0;
                x += h;
                fx = h * (Math.Sqrt(1 - (1 / Math.Exp(x))) / (Math.Pow(x, 2) + 1));
                try
                {
                    Monitor.Enter(queue);
                    queue.Enqueue(fx);
                }
                finally
                {
                    Monitor.Pulse(queue);
                    Monitor.Exit(queue);
                    Thread.Sleep(1500);
                }
            }
        }
    }
}
