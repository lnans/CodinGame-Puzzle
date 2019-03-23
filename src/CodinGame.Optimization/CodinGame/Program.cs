using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CodinGame.Optimization.CodinGame
{
    [SuppressMessage("ReSharper", "UnusedVariable")]
    internal class Program
    {
        private static readonly bool debug = false;

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            int firstInitInput = int.Parse(ReadLine("firstInit"));
            int secondInitInput = int.Parse(ReadLine("secondInit"));
            int thirdInitInput = int.Parse(ReadLine("thirdInit"));
            Console.Error.WriteLine("Game Information :");
            Console.Error.WriteLine($"[{firstInitInput}-{secondInitInput}-{thirdInitInput}]");

            int turn = 1;


            // game loop
            while (true)
            {
                List<int[]> list = new List<int[]>();
                Console.Error.WriteLine($"Start Turn {turn}...");
                string inputChar = "";
                inputChar += ReadLine("first");
                inputChar += ReadLine("second");
                inputChar += ReadLine("third");
                inputChar += ReadLine("fourth");
                Console.Error.WriteLine("Turn Information => " + inputChar);
                for (int i = 0; i < thirdInitInput; i++)
                {
                    string[] inputs = ReadLine().Split(' ');
                    int fifthInput = int.Parse(inputs[0]);
                    int sixthInput = int.Parse(inputs[1]);
                    Console.Error.WriteLine($"Input [{i}] : [{firstInitInput}-{sixthInput}]");
                    //if(sixthInput > thirdInitInput)
                    list.Add(new[] {i, sixthInput});
                }

                list = list.OrderBy(v => v[1]).ToList();

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");


                int output = list.First()[0];

                Console.WriteLine(((Output) output).ToString());
                //output++;
                //if (output > 4) output = 1;
                turn++;
            }
        }

        private static string ReadLine(string mess = "default")
        {
            string val = Console.ReadLine();
            if (debug) Console.Error.WriteLine($"{mess} - Read : {val}");
            return val;
        }

        private enum Output
        {
            A = 0,
            B = 1,
            C = 2,
            D = 3,
            E = 4
        }
    }
}