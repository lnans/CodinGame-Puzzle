using System;
using System.Collections.Generic;
using System.Linq;

namespace CodinGame.Puzzle.Easy.Temperatures
{
    internal class Program
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
            string[] inputs = Console.ReadLine()?.Split(' '); // all temperatures

            List<int> tempsPositive = new List<int>();
            List<int> tempsNegative = new List<int>(); // store absolute value
            for (int i = 0; i < n; i++)
            {
                int temp = int.Parse(inputs?[i]); // a temperature expressed as an integer ranging from -273 to 5526

                if (Math.Sign(temp) == -1)
                    tempsNegative.Add(Math.Abs(temp));
                else
                    tempsPositive.Add(temp);
            }

            if (tempsPositive.Count == 0 && tempsNegative.Count == 0)
            {
                Console.WriteLine(0);
            }
            else if (tempsPositive.Count == 0)
            {
                Console.WriteLine(-tempsNegative.OrderBy(v => v).First());
            }
            else if (tempsNegative.Count == 0)
            {
                Console.WriteLine(tempsPositive.OrderBy(v => v).First());
            }
            else
            {
                int closestNegative = tempsNegative.OrderBy(v => v).FirstOrDefault();
                int closestPositive = tempsPositive.OrderBy(v => v).FirstOrDefault();

                if (closestNegative == closestPositive) Console.WriteLine(closestPositive);

                if (closestNegative < closestPositive) Console.WriteLine(-closestNegative);

                if (closestPositive < closestNegative) Console.WriteLine(closestPositive);
            }
        }
    }
}