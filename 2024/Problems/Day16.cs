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
            // Build the map of navigable spaces
            HashSet<Vector2> maze = [];
            Vector2 reindeer = new();
            Vector2 exit = new();
            Vector2 facing = new(0, 1); // Facing east
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    char position = file[i][j];
                    if (position != '#')
                    {
                        maze.Add(new Vector2(i, j));

                        if (position == 'S')
                        {
                            reindeer = new Vector2(i, j);
                        }
                        else if (position == 'E')
                        {
                            exit = new Vector2(i, j);
                        }
                    }
                }
            }

            // Recursively track each possible path to the exit
            // while keeping score (move forward +1, turn 90 degrees +1000)
            return FindPathsWithWeights(maze, reindeer, facing, exit, 0, []);
        }

        private static int FindPathsWithWeights(HashSet<Vector2> maze, Vector2 currPos, Vector2 direction, Vector2 exit, int cumulativeScore, HashSet<Vector2> visited)
        {
            visited.Add(currPos);
            Vector2 targetPos = currPos + direction;

            // Case 0: one direct step away from the exit
            if (targetPos == exit)
            {
                return cumulativeScore + 1;
            }

            // Case 1a: facing an unvisited maze position
            List<int> pathScores = [];
            if (maze.Contains(targetPos) && !visited.Contains(targetPos))
            {
                pathScores.Add(FindPathsWithWeights(maze, targetPos, direction, exit, cumulativeScore + 1, visited));
            }

            // Case 1b: need to rotate to find open path(s)
            var rotateRight = Vector2.TransformNormal(direction, new Matrix3x2()
            {
                M11 = 0,
                M12 = -1,
                M21 = 1,
                M22 = 0,
                M31 = 0,
                M32 = 0,
            });
            var rotateLeft = Vector2.TransformNormal(direction, new Matrix3x2()
            {
                M11 = 0,
                M12 = 1,
                M21 = -1,
                M22 = 0,
                M31 = 0,
                M32 = 0,
            });
            var rightTarget = currPos + rotateRight;
            var leftTarget = currPos + rotateLeft;
            if (maze.Contains(rightTarget) && !visited.Contains(rightTarget))
            {
                pathScores.Add(FindPathsWithWeights(maze, currPos, rotateRight, exit, cumulativeScore + 1000, visited));
            }
            if (maze.Contains(leftTarget) && !visited.Contains(leftTarget))
            {
                pathScores.Add(FindPathsWithWeights(maze, currPos, rotateLeft, exit, cumulativeScore + 1000, visited));
            }

            // Close out Case 1:
            var foundPaths = pathScores.Where(s => s != -1);
            if (foundPaths.Any())
            {
                return foundPaths.Min();
            }
            else
            {
                return -1;
            }
        }
    }
}
