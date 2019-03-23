using System;
using System.Collections.Generic;
using System.Linq;

namespace CodinGame.Puzzle.Easy.TheDescent
{
    internal class Program
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            // game loop
            while (true)
            {
                Dictionary<int, int> mountains = new Dictionary<int, int>(8);
                for (int i = 0; i < 8; i++)
                {
                    int mountainH = int.Parse(Console.ReadLine());
                    mountains.Add(i, mountainH);
                }

                KeyValuePair<int, int> mountainToShoot = mountains.OrderByDescending(m => m.Value).First();
                Console.WriteLine(mountainToShoot.Key);
            }
        }
    }
}