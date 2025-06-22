using System.Numerics;
using System.Text;

namespace _2024.Problems
{
    public class IceSlider
    {
        /*
         * RULES:
         * - All pucks move in the same direction when the arrowkey is pressed.
         * - Red (r) moves up to 1 tile, Orange (o) up to 2, Yellow (y) up to 3, and Blue (b) up to 5 tiles.
         * - Walls (#) stop movement, as do the edges of the rink.
         * 
         * MAP: (Uppercase is goal, lowercase is starting position)
         * . . . . . .
         * O . . . . .
         * Y B . . . .
         * R Y O . . .
         * # # # # # .
         * b y y o o r
         */

        private static readonly Dictionary<Move, Vector2> Directions = new()
        {
            { Move.Up, new Vector2(-1, 0) }, // Up
            { Move.Right, new Vector2(0, 1) }, // Right
            { Move.Down, new Vector2(1, 0) }, // Down
            { Move.Left, new Vector2(0, -1) }, // Left
        };

        public static string SolvePuzzle(string[] file, bool automatic = true, bool exitEarly = false)
        {
            // First, build the map based on the input file. For the sake of simplicity, we'll assume the map is always square.
            char[,] goal = new char[file.Length, file.Length];
            char[,] iceRink = new char[file.Length, file.Length];
            var allPucks = new List<Puck>();
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    char position = file[i][j];
                    if (char.IsUpper(position))
                    {
                        goal[i, j] = char.ToLower(position);
                    }
                    else if (!char.IsLower(position))
                    {
                        goal[i, j] = position;
                        if (position == '#')
                        {
                            allPucks.Add(new Puck()
                            {
                                Position = new Vector2(i, j),
                                Color = position,
                            });
                        }
                    }
                    else
                    {
                        goal[i, j] = '.';
                        allPucks.Add(new Puck()
                        {
                            Position = new Vector2(i, j),
                            Color = position,
                        });
                    }  
                }
            }

            var goalString = GetMapString(goal);
            PrepMap(iceRink, allPucks, file.Length);
            var mapString = GetMapString(iceRink);
            var visitedPositions = new Dictionary<string, string>();

            // Use this section instead of the one below if you want to "play" manually.
            if (!automatic)
            {
                var moves = string.Empty;
                while (mapString != goalString)
                {
                    visitedPositions.TryAdd(mapString, moves);
                    PrintMap(mapString, file.Length);
                    Console.Write($"Moves so far: {moves}\nInput: ");

                    var input = Console.ReadLine();

                    string newString = string.Empty;
                    switch (input)
                    {
                        case "w":
                            newString = TryMovePucks(mapString, new(Move.Up, new Vector2(-1, 0)), file.Length);
                            if (!string.IsNullOrEmpty(newString))
                            {
                                mapString = newString;
                                moves = moves + (char)Move.Up;
                            }
                            break;
                        case "d":
                            newString = TryMovePucks(mapString, new(Move.Right, new Vector2(0, 1)), file.Length);
                            if (!string.IsNullOrEmpty(newString))
                            {
                                mapString = newString;
                                moves = moves + (char)Move.Right;
                            }
                            break;
                        case "s":
                            newString = TryMovePucks(mapString, new(Move.Down, new Vector2(1, 0)), file.Length);
                            if (!string.IsNullOrEmpty(newString))
                            {
                                mapString = newString;
                                moves = moves + (char)Move.Down;
                            }
                            break;
                        case "a":
                            newString = TryMovePucks(mapString, new(Move.Left, new Vector2(0, -1)), file.Length);
                            if (!string.IsNullOrEmpty(newString))
                            {
                                mapString = newString;
                                moves = moves + (char)Move.Left;
                            }
                            break;
                    }
                }

                return moves;
            }
            else
            {
                var mapQueue = new Queue<(string, string)>();
                mapQueue.Enqueue((mapString, string.Empty));
                visitedPositions.Add(mapString, string.Empty);
                BuildMoveMap(mapQueue, visitedPositions, file.Length, goalString, exitEarly);
                if (visitedPositions.TryGetValue(goalString, out var moves))
                {
                    Console.WriteLine($"\n{string.Join('\n', file)}");
                    Console.WriteLine($"\nCalculated moves to {visitedPositions.Count} possible positions.\nSolution involved {moves.Length} moves.");
                    return moves;
                }
                else
                {
                    Console.WriteLine(string.Join('\n', file));
                    Console.WriteLine($"\nCalculated moves to {visitedPositions.Count} possible positions.\n No solution found.");
                    return "Failed!";
                }
            }
        }

        private static void BuildMoveMap(Queue<(string, string)> positionsToTry, Dictionary<string, string> visitedPositions, int rinkSize, string goalString, bool exitEarly)
        {
            while (positionsToTry.Count > 0)
            {
                var (mapString, currentMoves) = positionsToTry.Dequeue();

                // Uncomment these lines for debugging as needed
                //PrintMap(mapString, rinkSize);
                //Console.WriteLine($"Moves so far: {currentMoves}");

                // Optionally exit early if we've already found the goal
                if (exitEarly && string.Equals(mapString, goalString, StringComparison.InvariantCulture))
                {
                    return;
                }

                // Main Case: Try each direction
                // Do not recurse if...
                // A. We have visited the new position before
                // B. No pucks can move in the specified direction
                foreach (var move in Directions)
                {
                    var newMapString = TryMovePucks(mapString, move, rinkSize);
                    if (!string.IsNullOrEmpty(newMapString)) // This covers case B
                    {
                        if (!visitedPositions.ContainsKey(newMapString)) // This covers case A
                        {
                            string newMoves = currentMoves + (char)move.Key;
                            visitedPositions.Add(newMapString, newMoves);
                            positionsToTry.Enqueue((newMapString, newMoves));
                        }
                    }
                }
            }
        }

        private static string TryMovePucks(string currentMap, KeyValuePair<Move, Vector2> newMove, int rinkSize)
        {
            // First, turn the value type currentMap into a list of pucks
            var allPucks = GetPucks(currentMap, rinkSize);

            IOrderedEnumerable<Puck> sortedPucks;
            if (newMove.Key == Move.Up)
            {
                sortedPucks = allPucks.OrderBy(p => p.Position.X);
            }
            else if (newMove.Key == Move.Down)
            {
                sortedPucks = allPucks.OrderByDescending(p => p.Position.X);
            }
            else if (newMove.Key == Move.Left)
            {
                sortedPucks = allPucks.OrderBy(p => p.Position.Y);
            }
            else
            {
                sortedPucks = allPucks.OrderByDescending(p => p.Position.Y);
            }

            bool moved = false;
            foreach (var puck in sortedPucks.Where(p => p.Color != '#'))
            {
                int idx = allPucks.IndexOf(puck);
                if (TryMovePuck(allPucks, idx, newMove.Value, rinkSize))
                {
                    moved = true;
                }
            }

            return moved ? GetMap(allPucks, rinkSize) : string.Empty;
        }

        // This method assumes that any pucks it runs into have already moved,
        // so the caller should ensure that pucks are moved in a reasonable order
        private static bool TryMovePuck(List<Puck> allPucks, int puckIdx, Vector2 direction, int rinkSize)
        {
            int moves = 0;
            var puck = allPucks[puckIdx];
            while (moves < puck.MaxMoves)
            {
                var newPos = puck.Position + direction;
                if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= rinkSize || newPos.Y >= rinkSize)
                {
                    return moves > 0; // We've gone out of bounds
                }

                // Make sure the field is clear before proceeding
                if (!allPucks.Any(p => p.Position == newPos))
                {
                    // We have succeeded in moving the puck
                    puck.Position = newPos;
                    moves++;
                }
                else
                {
                    return moves > 0; // We've hit another puck or a wall
                }
            }

            // We've moved the puck as far as it can go this step
            return moves > 0;
        }

        private static List<Puck> GetPucks(string mapString, int mapSize)
        {
            List<Puck> pucks = [];
            for (int idx = 0; idx < mapString.Length; idx++)
            {
                if (mapString[idx] != '.')
                {
                    int i = idx / mapSize;
                    int j = idx % mapSize;
                    pucks.Add(new Puck()
                    {
                        Position = new Vector2(i, j),
                        Color = mapString[idx],
                    });
                }
            }

            return pucks;
        }

        private static string GetMap(List<Puck> pucks, int mapSize)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    int idx = i * mapSize + j;
                    var puckAtPos = pucks.SingleOrDefault(p => p.Position.X == i && p.Position.Y == j);
                    if (puckAtPos != null)
                    {
                        sb.Append(puckAtPos.Color);
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }
            }

            return sb.ToString();
        }

        private static void PrintMap(string mapString, int mapSize)
        {
            var output = new StringBuilder();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    int idx = i * mapSize + j;
                    output.Append($" {mapString[idx]}");
                }
                output.AppendLine();
            }

            Console.Write(output.ToString());
        }

        private static void PrepMap(char[,] map, List<Puck> allPucks, int mapSize)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = '.';
                }
            }

            foreach (var puck in allPucks)
            {
                map[(int)puck.Position.X, (int)puck.Position.Y] = puck.Color;
            }
        }

        private static string GetMapString(char[,] map)
        {
            var output = new StringBuilder();
            foreach (var row in map)
            {
                output.Append(string.Join(string.Empty, row));
            }

            return output.ToString();
        }
    }

    internal class Puck
    {
        public Vector2 Position { get; set; } = new (-1, -1);

        public char Color { get; set; } = '#';

        public bool IsWall { get { return this.Color == '#'; } }

        public int MaxMoves
        {
            get
            {
                return this.Color switch
                {
                    'r' => 1,
                    'o' => 2,
                    'y' => 3,
                    'b' => 5,
                    _ => 0,
                };
            }
        }
    }

    internal enum Move
    {
        Up = 'U',
        Right = 'R',
        Down = 'D',
        Left = 'L',
    }
}
