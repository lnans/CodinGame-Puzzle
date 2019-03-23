using System;
using System.Collections.Generic;
using System.Linq;

namespace CodinGame.Optimization.CodeVsZombies
{
    internal class Program
    {
        private static bool DEBUG;

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            DEBUG = true;
            Game game = new Game(16000, 9000);
            Ai aiBot = new Ai(game);
            // game loop
            while (true)
            {
                aiBot.ReadTurn();
                aiBot.MakeDecision();
            }
        }

        private static string ReadLine(string debugMessage = "Debug")
        {
            string val = Console.ReadLine();
            if (DEBUG) Console.Error.WriteLine($"{debugMessage} - {val}");
            return val;
        }

        private sealed class Ai : Player
        {
            private readonly Game _game;

            public Ai(Game game) : base(new Position(0, 0))
            {
                this._game = game;
            }

            public void ReadTurn()
            {
                string[] inputs = ReadLine()?.Split(' ');
                int x = int.Parse(inputs?[0]);
                int y = int.Parse(inputs?[1]);
                this.Position = new Position(x, y);

                this._game.ReadTurn();
            }

            public void MakeDecision()
            {
                Human nearestHuman = this._game.NearestHuman(this.Position);
                //Zombie nearestZombie = this._game.NearestZombie(this.Position);
                if (this._game.HumanCount == 1)
                {
                    this.MoveTo(nearestHuman.Position);
                    return;
                }

                Human inDangerHuman = this._game.Humans.OrderBy(h => h.RemainingLifeTime).First();
                int costMoveToInDangerHuman = this.Position.Manhattan(inDangerHuman.Position);

                Human nearestHumanExceptInDanger = this._game.NearestHuman(this.Position, inDangerHuman);

                if (costMoveToInDangerHuman > inDangerHuman.RemainingLifeTime)
                    this.MoveTo(nearestHumanExceptInDanger.Position);
                else
                    this.MoveTo(inDangerHuman.Position);
            }

            private void MoveTo(Position position)
            {
                Console.WriteLine(position);
            }
        }

        private class Game
        {
            public Game(int width, int height)
            {
                this.MapWidth = width;
                this.MapHeight = height;
            }

            public int MapWidth { get; }

            public int MapHeight { get; }

            public List<Human> Humans { get; set; }

            public int HumanCount { get; set; }

            public List<Zombie> Zombies { get; set; }

            public int ZombieCount { get; set; }

            public void ReadTurn()
            {
                string[] inputs;

                // Read humans
                int humanCount = int.Parse(ReadLine());
                this.Humans = new List<Human>(humanCount);
                for (int i = 0; i < humanCount; i++)
                {
                    inputs = ReadLine()?.Split(' ');
                    int humanId = int.Parse(inputs?[0]);
                    int humanX = int.Parse(inputs?[1]);
                    int humanY = int.Parse(inputs?[2]);
                    this.Humans.Add(new Human(humanId, new Position(humanX, humanY)));
                }

                this.HumanCount = humanCount;

                // Read Zombies
                int zombieCount = int.Parse(ReadLine());
                this.Zombies = new List<Zombie>(zombieCount);
                for (int i = 0; i < zombieCount; i++)
                {
                    inputs = ReadLine()?.Split(' ');
                    int zombieId = int.Parse(inputs?[0]);
                    int zombieX = int.Parse(inputs?[1]);
                    int zombieY = int.Parse(inputs?[2]);
                    int zombieXNext = int.Parse(inputs?[3]);
                    int zombieYNext = int.Parse(inputs?[4]);

                    this.Zombies.Add(new Zombie(zombieId, new Position(zombieX, zombieY), new Position(zombieXNext, zombieYNext)));
                }

                this.ZombieCount = zombieCount;

                foreach (Human human in this.Humans)
                {
                    Zombie nearest = this.NearestZombie(human.Position);
                    int cost = human.Position.Manhattan(nearest.Position);
                    human.RemainingLifeTime = cost;
                    //List<int> costList = new List<int>(this.ZombieCount);
                    //foreach (Zombie zombie in this.Zombies)
                    //{
                    //    int moveCost = human.Position.Manhattan(zombie.Position);
                    //    costList.Add(moveCost);
                    //}

                    //human.RemainingLifeTime = costList.OrderBy(cost => cost).First();
                }
            }

            public Zombie NearestZombie(Position position)
            {
                Dictionary<Zombie, int> costList = new Dictionary<Zombie, int>(this.ZombieCount);
                foreach (Zombie zombie in this.Zombies)
                {
                    int moveCost = position.Manhattan(zombie.Position);
                    costList.Add(zombie, moveCost);
                }

                return costList.OrderBy(cost => cost.Value).First().Key;
            }

            public Human NearestHuman(Position position, Human exceptHuman = null)
            {
                Dictionary<Human, int> costList = new Dictionary<Human, int>(this.HumanCount);
                foreach (Human human in this.Humans)
                {
                    int moveCost = position.Manhattan(human.Position);
                    costList.Add(human, moveCost);
                }

                IEnumerable<KeyValuePair<Human, int>> result = costList;

                if (exceptHuman != null) result = costList.Where(h => h.Key.Id != exceptHuman.Id);

                return result.OrderBy(cost => cost.Value).First().Key;
            }
        }

        private class Position
        {
            public Position(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; }

            public int Y { get; }

            public int Manhattan(Position p2)
            {
                return Math.Abs(this.X - p2.X) + Math.Abs(this.Y - p2.Y);
            }

            public override string ToString()
            {
                return this.X + " " + this.Y;
            }
        }

        private sealed class Zombie : Player
        {
            public Zombie(int Id, Position position, Position nextPosition) : base(position)
            {
                this.Id = Id;
                this.NextPosition = nextPosition;
            }

            public int Id { get; }

            public Position NextPosition { get; }
        }

        private sealed class Human : Player
        {
            public Human(int Id, Position position) : base(position)
            {
                this.Id = Id;
            }

            public int Id { get; }

            public int RemainingLifeTime { get; set; }
        }

        private abstract class Player
        {
            protected Player(Position position)
            {
                this.Position = position;
            }

            public Position Position { get; set; }
        }
    }
}