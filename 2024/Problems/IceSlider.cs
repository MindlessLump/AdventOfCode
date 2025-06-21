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

        public static string SolvePuzzle(string[] file)
        {
            // First, build the map based on the input file. For the sake of simplicity, we'll assume the map is always square.
            char[,] iceRink = new char[file.Length, file.Length];
            char[,] goal = new char[file.Length, file.Length];
            var allPucks = new List<Puck>();
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    char position = file[i][j];
                    iceRink[i, j] = position;
                    if (char.IsUpper(position))
                    {
                        goal[i, j] = char.ToLower(position);
                        iceRink[i, j] = '.';
                    }
                    else if (!char.IsLower(position))
                    {
                        goal[i, j] = position;
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

            var startString = GetMapString(iceRink);
            var visitedPositions = new Dictionary<string, List<Move>>();

            //while (GetMapString(iceRink) != goalString)
            //{
            //    var input = Console.ReadLine();

            //    switch (input)
            //    {
            //        case "w":
            //            TryMovePucks(iceRink, allPucks, visitedPositions, [], new(Move.Up, new Vector2(-1, 0)), file.Length);
            //            break;
            //        case "d":
            //            TryMovePucks(iceRink, allPucks, visitedPositions, [], new(Move.Right, new Vector2(0, 1)), file.Length);
            //            break;
            //        case "s":
            //            TryMovePucks(iceRink, allPucks, visitedPositions, [], new(Move.Down, new Vector2(1, 0)), file.Length);
            //            break;
            //        case "a":
            //            TryMovePucks(iceRink, allPucks, visitedPositions, [], new(Move.Left, new Vector2(0, -1)), file.Length);
            //            break;
            //    }

            //    PrintMap(iceRink);
            //}

            return DoStep(iceRink, allPucks, goalString, visitedPositions, [], file.Length);
        }

        private static string DoStep(char[,] iceRink, List<Puck> allPucks, string goal, Dictionary<string, List<Move>> visitedPositions, List<Move> currentMoves, int rinkSize)
        {
            var currentMapString = GetMapString(iceRink);
            PrintMap(iceRink);

            // Base Case 1: We have reached the goal
            if (string.Equals(currentMapString, goal, StringComparison.InvariantCulture))
            {
                return GetMovesString(currentMoves);
            }

            // Base Case 2: We have visited this position before
            if (visitedPositions.TryGetValue(currentMapString, out var originalMoves))
            {
                // Unless we somehow got here faster, exit early with an empty string to indicate a failed path
                if (originalMoves.Count <= currentMoves.Count)
                {
                    return string.Empty;
                }
                // If we're faster, continue with re-mapping the grid from here in the main case below
            }

            // Main Case: Try each direction
            string shortestPath = string.Empty;
            foreach (var move in Directions)
            {
                var newRink = TryMovePucks(iceRink, allPucks, move, rinkSize);
                if (newRink != null)
                {
                    visitedPositions.TryAdd(currentMapString, currentMoves);

                    currentMoves.Add(move.Key);
                    var finalPath = DoStep(newRink, allPucks, goal, visitedPositions, currentMoves, rinkSize);
                    if (!string.IsNullOrEmpty(finalPath) && (string.IsNullOrEmpty(shortestPath) || finalPath.Length < shortestPath.Length))
                    {
                        shortestPath = finalPath;
                    }
                }
            }

            return shortestPath ?? string.Empty;
        }

        // Returns a map if one or more pucks moved, nothing if we're in the same position as before the newMove
        private static char[,]? TryMovePucks(char[,] iceRink, List<Puck> allPucks, KeyValuePair<Move, Vector2> newMove, int rinkSize)
        {
            char[,] copyRink = new char[rinkSize, rinkSize];
            for (int i = 0; i < rinkSize; i++)
            {
                for (int j = 0; j < rinkSize; j++)
                {
                    copyRink[i, j] = iceRink[i, j];
                }
            }

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
            foreach (var puck in sortedPucks)
            {
                int idx = allPucks.IndexOf(puck);
                if (TryMovePuck(copyRink, allPucks, idx, newMove.Value, rinkSize))
                {
                    moved = true;
                }
            }

            return moved ? copyRink : null;
        }

        // This method assumes that any pucks it runs into have already moved,
        // so the caller should ensure that pucks are moved in a reasonable order
        private static bool TryMovePuck(char[,] copyRink, List<Puck> allPucks, int puckIdx, Vector2 direction, int rinkSize)
        {
            int moves = 0;
            var puck = allPucks[puckIdx];
            while (!AtMaxMoves(moves, puck.Color))
            {
                var newPos = puck.Position + direction;
                if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= rinkSize || newPos.Y >= rinkSize)
                {
                    return moves > 0; // We've gone out of bounds
                }

                // Make sure the field is clear before proceeding
                if (copyRink[(int)newPos.X, (int)newPos.Y] == '.')
                {
                    // We have succeeded in moving the puck
                    copyRink[(int)newPos.X, (int)newPos.Y] = puck.Color;
                    copyRink[(int)puck.Position.X, (int)puck.Position.Y] = '.';
                    puck.Position = newPos;
                    allPucks[puckIdx] = puck;
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

        private static bool AtMaxMoves(int moves, char puckType)
        {
            return puckType switch
            {
                'r' => moves >= 1,
                'o' => moves >= 2,
                'y' => moves >= 3,
                'b' => moves >= 5,
                _ => true,
            };
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

        private static void PrintMap(char[,] map)
        {
            var output = new StringBuilder();
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    output.Append($" {map[i, j]}");
                }
                output.AppendLine();
            }

            Console.WriteLine(output.ToString());
        }

        private static string GetMovesString(IEnumerable<Move> moves)
        {
            var output = new StringBuilder();
            foreach (var move in moves)
            {
                output.Append((char)move);
            }

            return output.ToString();
        }
    }

    internal class Puck
    {
        public Vector2 Position { get; set; } = new (-1, -1);

        public char Color { get; set; } = '#';
    }

    internal enum Move
    {
        Up = 'U',
        Right = 'R',
        Down = 'D',
        Left = 'L',
    }
}
