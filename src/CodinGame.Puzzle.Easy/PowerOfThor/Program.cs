using System;
using System.Collections.Generic;
using System.Linq;

namespace CodinGame.Puzzle.Easy.PowerOfThor
{
    public class Program
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            // Register all direction possibility, based on (x,y) graph of with length of 1
            Dictionary<string, KeyValuePair<int, int>> compass = new Dictionary<string, KeyValuePair<int, int>>(8)
            {
                {"NW", new KeyValuePair<int, int>(-1, 1)},
                {"N", new KeyValuePair<int, int>(0, 1)},
                {"NE", new KeyValuePair<int, int>(1, 1)},
                {"E", new KeyValuePair<int, int>(1, 0)},
                {"SE", new KeyValuePair<int, int>(1, -1)},
                {"S", new KeyValuePair<int, int>(0, -1)},
                {"SW", new KeyValuePair<int, int>(-1, -1)},
                {"W", new KeyValuePair<int, int>(-1, 0)}
            };

            string[] inputs = Console.ReadLine()?.Split(' ');
            int lightX = int.Parse(inputs?[0]); // the X position of the light of power
            int lightY = int.Parse(inputs?[1]); // the Y position of the light of power

            Position lightPos = new Position(lightX, lightY);

            int initialTX = int.Parse(inputs?[2]); // Thor's starting X position
            int initialTY = int.Parse(inputs?[3]); // Thor's starting Y position

            Position thorPos = new Position(initialTX, initialTY);

            // game loop
            while (true)
            {
                int unused = int.Parse(Console.ReadLine()); // The remaining amount of turns Thor can move. Do not remove this line.
                KeyValuePair<int, int> direction = GetGridPositionTarget(thorPos, lightPos);
                thorPos = thorPos.Move(compass.First(c => c.Value.Key == direction.Key && c.Value.Value == direction.Value));
            }
        }

        private static KeyValuePair<int, int> GetGridPositionTarget(Position main, Position target)
        {
            int xGraphPos = 0;
            if (target.X > main.X) xGraphPos = 1;
            if (target.X == main.X) xGraphPos = 0;
            if (target.X < main.X) xGraphPos = -1;

            int yGraphPos = 0;
            if (target.Y < main.Y) yGraphPos = 1;
            if (target.Y == main.Y) yGraphPos = 0;
            if (target.Y > main.Y) yGraphPos = -1;

            KeyValuePair<int, int> result = new KeyValuePair<int, int>(xGraphPos, yGraphPos);

            return result;
        }

        public class Position
        {
            public Position(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; set; }

            public int Y { get; set; }
        }
    }

    public static class Extensions
    {
        public static Program.Position Move(this Program.Position currentPos, KeyValuePair<string, KeyValuePair<int, int>> direction)
        {
            Console.WriteLine(direction.Key);
            return new Program.Position(currentPos.X + direction.Value.Key, currentPos.Y - direction.Value.Value);
        }
    }
}