using System.Numerics;

namespace _2024.Problems
{
    public class Day16
    {
        /*
        It's time again for the Reindeer Olympics! This year, the big event is the Reindeer Maze, where the Reindeer compete for the lowest score.

        You and The Historians arrive to search for the Chief right as the event is about to start. It wouldn't hurt to watch a little, right?

        The Reindeer start on the Start Tile (marked S) facing East and need to reach the End Tile (marked E). They can move forward one tile at a time (increasing their score by 1 point), but never into a wall (#). They can also rotate clockwise or counterclockwise 90 degrees at a time (increasing their score by 1000 points).

        To figure out the best place to sit, you start by grabbing a map (your puzzle input) from a nearby kiosk. For example:

        ###############
        #.......#....E#
        #.#.###.#.###.#
        #.....#.#...#.#
        #.###.#####.#.#
        #.#.#.......#.#
        #.#.#####.###.#
        #...........#.#
        ###.#.#####.#.#
        #...#.....#.#.#
        #.#.#.###.#.#.#
        #.....#...#.#.#
        #.###.#.#.#.#.#
        #S..#.....#...#
        ###############
        There are many paths through this maze, but taking any of the best paths would incur a score of only 7036. This can be achieved by taking a total of 36 steps forward and turning 90 degrees a total of 7 times:

        ###############
        #.......#....E#
        #.#.###.#.###^#
        #.....#.#...#^#
        #.###.#####.#^#
        #.#.#.......#^#
        #.#.#####.###^#
        #..>>>>>>>>v#^#
        ###^#.#####v#^#
        #>>^#.....#v#^#
        #^#.#.###.#v#^#
        #^....#...#v#^#
        #^###.#.#.#v#^#
        #S..#.....#>>^#
        ###############
        Here's a second example:

        #################
        #...#...#...#..E#
        #.#.#.#.#.#.#.#.#
        #.#.#.#...#...#.#
        #.#.#.#.###.#.#.#
        #...#.#.#.....#.#
        #.#.#.#.#.#####.#
        #.#...#.#.#.....#
        #.#.#####.#.###.#
        #.#.#.......#...#
        #.#.###.#####.###
        #.#.#...#.....#.#
        #.#.#.#####.###.#
        #.#.#.........#.#
        #.#.#.#########.#
        #S#.............#
        #################
        In this maze, the best paths cost 11048 points; following one such path would look like this:

        #################
        #...#...#...#..E#
        #.#.#.#.#.#.#.#^#
        #.#.#.#...#...#^#
        #.#.#.#.###.#.#^#
        #>>v#.#.#.....#^#
        #^#v#.#.#.#####^#
        #^#v..#.#.#>>>>^#
        #^#v#####.#^###.#
        #^#v#..>>>>^#...#
        #^#v###^#####.###
        #^#v#>>^#.....#.#
        #^#v#^#####.###.#
        #^#v#^........#.#
        #^#v#^#########.#
        #S#>>^..........#
        #################
        Note that the path shown above includes one 90 degree turn as the very first move, rotating the Reindeer from facing East to facing North.

        Analyze your map carefully. What is the lowest score a Reindeer could possibly get?
         */
        public static int ReindeerMazeScore(string[] file)
        {
            // Parse the input file
            HashSet<Vector2> maze = new();
            Reindeer reindeer = new();
            Vector2 exit = new();
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    char c = file[i][j];
                    if (c != '#')
                    {
                        maze.Add(new Vector2(i, j));
                        if (c == 'S')
                        {
                            reindeer = new()
                            {
                                Position = new Vector2(i, j),
                                Direction = Directions.East,
                                Points = 0,
                            };
                        }
                        else if (c == 'E')
                        {
                            exit = new Vector2(i, j);
                        }
                    }
                }
            }

            // Navigate the maze, tracking the points spent getting to each position
            Dictionary<Vector2, int> visited = new()
            {
                { reindeer.Position, reindeer.Points }
            };
            Queue<Reindeer> navigationQueue = new();
            navigationQueue.Enqueue(reindeer);
            while (navigationQueue.Count > 0)
            {
                var currentReindeer = navigationQueue.Dequeue();

                var moveForward = currentReindeer.Position + currentReindeer.Direction;
                if (maze.Contains(moveForward))
                {
                    int newPoints = currentReindeer.Points + 1;
                    if (!visited.TryGetValue(moveForward, out int origPoints))
                    {
                        visited.Add(moveForward, newPoints);
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveForward,
                            Direction = new Vector2(currentReindeer.Direction.X, currentReindeer.Direction.Y),
                            Points = newPoints,
                        });
                    }
                    else if (origPoints > newPoints)
                    {
                        visited[moveForward] = newPoints;
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveForward,
                            Direction = new Vector2(currentReindeer.Direction.X, currentReindeer.Direction.Y),
                            Points = newPoints,
                        });
                    }
                }

                var moveRightDir = Directions.RotateRight(currentReindeer.Direction);
                var moveRight = currentReindeer.Position + moveRightDir;
                if (maze.Contains(moveRight))
                {
                    int newPoints = currentReindeer.Points + 1001;
                    if (!visited.TryGetValue(moveRight, out int origPoints))
                    {
                        visited.Add(moveRight, newPoints);
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveRight,
                            Direction = moveRightDir,
                            Points = newPoints,
                        });
                    }
                    else if (origPoints > newPoints)
                    {
                        visited[moveRight] = newPoints;
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveRight,
                            Direction = moveRightDir,
                            Points = newPoints,
                        });
                    }
                }

                var moveLeftDir = Directions.RotateLeft(currentReindeer.Direction);
                var moveLeft = currentReindeer.Position + moveLeftDir;
                if (maze.Contains(moveLeft))
                {
                    int newPoints = currentReindeer.Points + 1001;
                    if (!visited.TryGetValue(moveLeft, out int origPoints))
                    {
                        visited.Add(moveLeft, newPoints);
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveLeft,
                            Direction = moveLeftDir,
                            Points = newPoints,
                        });
                    }
                    else if (origPoints > newPoints)
                    {
                        visited[moveLeft] = newPoints;
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveLeft,
                            Direction = moveLeftDir,
                            Points = newPoints,
                        });
                    }
                }
            }

            return visited.TryGetValue(exit, out int points) ? points : -1;
        }

        /*
         *
        Now that you know what the best paths look like, you can figure out the best spot to sit.

        Every non-wall tile (S, ., or E) is equipped with places to sit along the edges of the tile. While determining which of these tiles would be the best spot to sit depends on a whole bunch of factors (how comfortable the seats are, how far away the bathrooms are, whether there's a pillar blocking your view, etc.), the most important factor is whether the tile is on one of the best paths through the maze. If you sit somewhere else, you'd miss all the action!

        So, you'll need to determine which tiles are part of any best path through the maze, including the S and E tiles.

        In the first example, there are 45 tiles (marked O) that are part of at least one of the various best paths through the maze:

        ###############
        #.......#....O#
        #.#.###.#.###O#
        #.....#.#...#O#
        #.###.#####.#O#
        #.#.#.......#O#
        #.#.#####.###O#
        #..OOOOOOOOO#O#
        ###O#O#####O#O#
        #OOO#O....#O#O#
        #O#O#O###.#O#O#
        #OOOOO#...#O#O#
        #O###.#.#.#O#O#
        #O..#.....#OOO#
        ###############
        In the second example, there are 64 tiles that are part of at least one of the best paths:

        #################
        #...#...#...#..O#
        #.#.#.#.#.#.#.#O#
        #.#.#.#...#...#O#
        #.#.#.#.###.#.#O#
        #OOO#.#.#.....#O#
        #O#O#.#.#.#####O#
        #O#O..#.#.#OOOOO#
        #O#O#####.#O###O#
        #O#O#..OOOOO#OOO#
        #O#O###O#####O###
        #O#O#OOO#..OOO#.#
        #O#O#O#####O###.#
        #O#O#OOOOOOO..#.#
        #O#O#O#########.#
        #O#OOO..........#
        #################
        Analyze your map further. How many tiles are part of at least one of the best paths through the maze?
         */
        public static int ReindeerMazePathLength(string[] file)
        {
            // Parse the input file
            HashSet<Vector2> maze = new();
            Reindeer reindeer = new();
            Vector2 exit = new();
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    char c = file[i][j];
                    if (c != '#')
                    {
                        maze.Add(new Vector2(i, j));
                        if (c == 'S')
                        {
                            reindeer = new()
                            {
                                Position = new Vector2(i, j),
                                Direction = Directions.East,
                                Points = 0,
                                PrevPosition = null,
                            };
                        }
                        else if (c == 'E')
                        {
                            exit = new Vector2(i, j);
                        }
                    }
                }
            }

            // Navigate the maze, tracking the points spent getting to each position
            Dictionary<Vector2, Reindeer> visited = new()
            {
                { reindeer.Position, reindeer }
            };
            Queue<Reindeer> navigationQueue = new();
            navigationQueue.Enqueue(reindeer);
            while (navigationQueue.Count > 0)
            {
                var currentReindeer = navigationQueue.Dequeue();

                var moveForward = currentReindeer.Position + currentReindeer.Direction;
                if (maze.Contains(moveForward))
                {
                    int newPoints = currentReindeer.Points + 1;
                    if (!visited.TryGetValue(moveForward, out Reindeer? origPath))
                    {
                        visited.Add(moveForward, new Reindeer() { Points = newPoints, PrevPosition = currentReindeer.Position });
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveForward,
                            Direction = new Vector2(currentReindeer.Direction.X, currentReindeer.Direction.Y),
                            Points = newPoints,
                            PrevPosition = currentReindeer.Position,
                        });
                    }
                    else if (origPath.Points > newPoints)
                    {
                        visited[moveForward] = new Reindeer() { Points = newPoints, PrevPosition = currentReindeer.Position };
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveForward,
                            Direction = new Vector2(currentReindeer.Direction.X, currentReindeer.Direction.Y),
                            Points = newPoints,
                            PrevPosition = currentReindeer.Position,
                        });
                    }
                }

                var moveRightDir = Directions.RotateRight(currentReindeer.Direction);
                var moveRight = currentReindeer.Position + moveRightDir;
                if (maze.Contains(moveRight))
                {
                    int newPoints = currentReindeer.Points + 1001;
                    if (!visited.TryGetValue(moveRight, out Reindeer origPath))
                    {
                        visited.Add(moveRight, new Reindeer() { Points = newPoints, PrevPosition = currentReindeer.Position });
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveRight,
                            Direction = moveRightDir,
                            Points = newPoints,
                            PrevPosition = currentReindeer.Position,
                        });
                    }
                    else if (origPath.Points > newPoints)
                    {
                        visited[moveRight] = new Reindeer() { Points = newPoints, PrevPosition = currentReindeer.Position };
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveRight,
                            Direction = moveRightDir,
                            Points = newPoints,
                            PrevPosition = currentReindeer.Position,
                        });
                    }
                }

                var moveLeftDir = Directions.RotateLeft(currentReindeer.Direction);
                var moveLeft = currentReindeer.Position + moveLeftDir;
                if (maze.Contains(moveLeft))
                {
                    int newPoints = currentReindeer.Points + 1001;
                    if (!visited.TryGetValue(moveLeft, out Reindeer origPath))
                    {
                        visited.Add(moveLeft, new Reindeer() { Points = newPoints, PrevPosition = currentReindeer.Position });
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveLeft,
                            Direction = moveLeftDir,
                            Points = newPoints,
                            PrevPosition = currentReindeer.Position,
                        });
                    }
                    else if (origPath.Points > newPoints)
                    {
                        visited[moveLeft] = new Reindeer() { Points = newPoints, PrevPosition = currentReindeer.Position };
                        navigationQueue.Enqueue(new Reindeer()
                        {
                            Position = moveLeft,
                            Direction = moveLeftDir,
                            Points = newPoints,
                            PrevPosition = currentReindeer.Position,
                        });
                    }
                }
            }

            if (visited.TryGetValue(exit, out Reindeer? fullPath) && fullPath?.PrevPosition != null)
            {
                int pathLength = 1;
                Vector2 position = fullPath.PrevPosition.Value;
                while (visited.TryGetValue(position, out fullPath) && fullPath?.PrevPosition != null)
                {
                    pathLength++;
                    position = fullPath.PrevPosition.Value;
                }

                return pathLength;
            }
            else
            {
                return -1;
            }
        }
    }

    internal class Reindeer
    {
        public Vector2 Position { get; set; }

        public Vector2 Direction { get; set; }

        public int Points { get; set; }

        public Vector2? PrevPosition { get; set; }
    }

    internal static class Directions
    {
        public static readonly Vector2 North = new(-1, 0);

        public static readonly Vector2 East = new(0, 1);

        public static readonly Vector2 South = new(1, 0);

        public static readonly Vector2 West = new(0, -1);

        private static readonly Matrix3x2 leftMatrix = new()
        {
            M11 = 0,
            M12 = 1,
            M21 = -1,
            M22 = 0,
            M31 = 0,
            M32 = 0,
        };

        private static readonly Matrix3x2 rightMatrix = new()
        {
            M11 = 0,
            M12 = -1,
            M21 = 1,
            M22 = 0,
            M31 = 0,
            M32 = 0,
        };

        public static Vector2 RotateLeft(Vector2 vector)
        {
            return Vector2.TransformNormal(vector, leftMatrix);
        }

        public static Vector2 RotateRight(Vector2 vector)
        {
            return Vector2.TransformNormal(vector, rightMatrix);
        }
    }
}
