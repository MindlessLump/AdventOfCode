using System.Numerics;

namespace _2024.Problems
{
    public class Day6
    {
        /*
        Maybe you can work out where the guard will go ahead of time so that The Historians can search safely?

        You start by making a map (your puzzle input) of the situation. For example:

        ....#.....
        .........#
        ..........
        ..#.......
        .......#..
        ..........
        .#..^.....
        ........#.
        #.........
        ......#...
        The map shows the current position of the guard with ^ (to indicate the guard is currently facing up from the perspective of the map). Any obstructions - crates, desks, alchemical reactors, etc. - are shown as #.

        Lab guards in 1518 follow a very strict patrol protocol which involves repeatedly following these steps:

        If there is something directly in front of you, turn right 90 degrees.
        Otherwise, take a step forward.
        Following the above protocol, the guard moves up several times until she reaches an obstacle (in this case, a pile of failed suit prototypes):

        ....#.....
        ....^....#
        ..........
        ..#.......
        .......#..
        ..........
        .#........
        ........#.
        #.........
        ......#...
        Because there is now an obstacle in front of the guard, she turns right before continuing straight in her new facing direction:

        ....#.....
        ........>#
        ..........
        ..#.......
        .......#..
        ..........
        .#........
        ........#.
        #.........
        ......#...
        Reaching another obstacle (a spool of several very long polymers), she turns right again and continues downward:

        ....#.....
        .........#
        ..........
        ..#.......
        .......#..
        ..........
        .#......v.
        ........#.
        #.........
        ......#...
        This process continues for a while, but the guard eventually leaves the mapped area (after walking past a tank of universal solvent):

        ....#.....
        .........#
        ..........
        ..#.......
        .......#..
        ..........
        .#........
        ........#.
        #.........
        ......#v..
        By predicting the guard's route, you can determine which specific positions in the lab will be in the patrol path. Including the guard's starting position, the positions visited by the guard before leaving the area are marked with an X:

        ....#.....
        ....XXXXX#
        ....X...X.
        ..#.X...X.
        ..XXXXX#X.
        ..X.X.X.X.
        .#XXXXXXX.
        .XXXXXXX#.
        #XXXXXXX..
        ......#X..
        In this example, the guard will visit 41 distinct positions on your map.

        Predict the path of the guard. How many distinct positions will the guard visit before leaving the mapped area?
         */
        public static int CountDistinctPositions(string[] file)
        {
            // First, build the map of obstacles
            HashSet<Vector2> obstacles = [];
            Vector2? startingPos = null;
            Vector2? startingDir = null;
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    if (file[i][j] == '#')
                    {
                        obstacles.Add(new Vector2(i, j));
                    }
                    else if (file[i][j] != '.')
                    {
                        startingPos = new Vector2(i, j);
                        switch (file[i][j])
                        {
                            case '^':
                                startingDir = new Vector2(-1, 0);
                                break;
                            case '>':
                                startingDir = new Vector2(0, 1);
                                break;
                            case 'v':
                                startingDir = new Vector2(1, 0);
                                break;
                            case '<':
                                startingDir = new Vector2(0, -1);
                                break;
                        }
                    }
                }
            }

            // Now navigate through the obstacles
            // 1. If there is open space ahead, move forward
            // 2. If there is an obstacle, turn right 90 degrees
            // 3. Proceed until exiting the grid
            var currentPos = startingPos ?? new Vector2(-1, -1);
            var currentDir = startingDir ?? new Vector2(0, 0);
            HashSet<Vector2> visited = [];
            while (currentPos.X >= 0 && currentPos.X < file.Length
                && currentPos.Y >= 0 && currentPos.Y < file[0].Length)
            {
                visited.Add(currentPos);

                var ahead = currentPos + currentDir;
                if (!obstacles.Contains(ahead))
                {
                    currentPos += currentDir;
                }
                else
                {
                    // Rotate the directional vector 90 degrees clockwise
                    currentDir = Vector2.TransformNormal(currentDir, new Matrix3x2()
                    {
                        M11 = 0,
                        M12 = -1,
                        M21 = 1,
                        M22 = 0,
                        M31 = 0,
                        M32 = 0,
                    });
                }
            }

            return visited.Count;
        }
    }
}
