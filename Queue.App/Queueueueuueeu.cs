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
        private double result = 0;
        private int n = Environment.ProcessorCount;
        private const int m = 4;

        private double a = 0;
        private double b = 0;
        private static double h = 0;
        private Queue<double> queue = new Queue<double>();

        public Queueueueuueeu(double ax, double bx)
        {
            a = ax;
            b = bx;
            h = (b - a) / (n * m);
            StartingThreads();
        }
        private void StartingThreads()
        {
            var t = new List<Thread>();
            for (int i = 0; i < n; i++)
            {
                var thread = new Thread(new ParameterizedThreadStart(SolveFx));
                thread.Start(i);
                t.Add(thread);
            }
            for (int i = 0; i < n; i++)
            {
                t[i].Join();
            }
            var threadd = new Thread(zxc =>
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    result += queue.Dequeue();
                }
            });
            threadd.Start();
            threadd.Join();
            Console.WriteLine(Math.Round(result, 3));
        }

        private void SolveFx(object count)
        {
            double x = a;
            double fx;
            bool block;
            x += ((b - a) / n) * (int)count;
            for (int i = 0; i < m; i++)
            {
                block = false;
                fx = 0;
                x += h;
                fx = h * (Math.Sqrt(1 - (1 / Math.Exp(x))) / (Math.Pow(x, 2) + 1));
                try
                {
                    Monitor.Enter(queue,ref block);
                    queue.Enqueue(fx);
                }
                finally
                {
                    if (block) Monitor.Exit(queue);
                }
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, fx);
                Thread.Sleep(100);
            }
        }
    }
}
