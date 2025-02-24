using System.Numerics;

namespace _2024.Problems
{
    public class Day06
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

        /*
        Returning after what seems like only a few seconds to The Historians, they explain that the guard's patrol area is simply too large for them to safely search the lab without getting caught.

        Fortunately, they are pretty sure that adding a single new obstruction won't cause a time paradox. They'd like to place the new obstruction in such a way that the guard will get stuck in a loop, making the rest of the lab safe to search.

        To have the lowest chance of creating a time paradox, The Historians would like to know all of the possible positions for such an obstruction. The new obstruction can't be placed at the guard's starting position - the guard is there right now and would notice.

        In the above example, there are only 6 different positions where a new obstruction would cause the guard to get stuck in a loop. The diagrams of these six situations use O to mark the new obstruction, | to show a position where the guard moves up/down, - to show a position where the guard moves left/right, and + to show a position where the guard moves both up/down and left/right.

        Option one, put a printing press next to the guard's starting position:

        ....#.....
        ....+---+#
        ....|...|.
        ..#.|...|.
        ....|..#|.
        ....|...|.
        .#.O^---+.
        ........#.
        #.........
        ......#...
        Option two, put a stack of failed suit prototypes in the bottom right quadrant of the mapped area:


        ....#.....
        ....+---+#
        ....|...|.
        ..#.|...|.
        ..+-+-+#|.
        ..|.|.|.|.
        .#+-^-+-+.
        ......O.#.
        #.........
        ......#...
        Option three, put a crate of chimney-squeeze prototype fabric next to the standing desk in the bottom right quadrant:

        ....#.....
        ....+---+#
        ....|...|.
        ..#.|...|.
        ..+-+-+#|.
        ..|.|.|.|.
        .#+-^-+-+.
        .+----+O#.
        #+----+...
        ......#...
        Option four, put an alchemical retroencabulator near the bottom left corner:

        ....#.....
        ....+---+#
        ....|...|.
        ..#.|...|.
        ..+-+-+#|.
        ..|.|.|.|.
        .#+-^-+-+.
        ..|...|.#.
        #O+---+...
        ......#...
        Option five, put the alchemical retroencabulator a bit to the right instead:

        ....#.....
        ....+---+#
        ....|...|.
        ..#.|...|.
        ..+-+-+#|.
        ..|.|.|.|.
        .#+-^-+-+.
        ....|.|.#.
        #..O+-+...
        ......#...
        Option six, put a tank of sovereign glue right next to the tank of universal solvent:

        ....#.....
        ....+---+#
        ....|...|.
        ..#.|...|.
        ..+-+-+#|.
        ..|.|.|.|.
        .#+-^-+-+.
        .+----++#.
        #+----++..
        ......#O..
        It doesn't really matter what you choose to use as an obstacle so long as you and The Historians can put it into position without the guard noticing. The important thing is having enough options that you can find one that minimizes time paradoxes, and in this example, there are 6 different positions you could choose.

        You need to get the guard stuck in a loop by adding a single new obstruction. How many different positions could you choose for this obstruction?
         */
        public static int CountPossibleLoops(string[] file)
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

            // Now, let's build a smaller list of possible obstacle positions by finding each grid space visited by the guard
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

            // Let's naively brute force a solution by trying an obstacle in each open position that's visited by the guard
            // and then running our guard logic to see if they enter a loop
            // 1. If there is open space ahead, move forward
            // 2. If there is an obstacle, turn right 90 degrees
            // 3. Proceed until exiting the grid
            // Meanwhile, track each turning position, and if the latest turn matches the position 4 turns ago, it's a loop
            int numPossibleLoops = 0;
            Vector2? previousFakeObstacle = null;
            foreach (var visitedPos in visited)
            {
                var newFakeObstacle = new Vector2(visitedPos.X, visitedPos.Y);

                if (previousFakeObstacle.HasValue && obstacles.Contains(previousFakeObstacle.Value))
                {
                    obstacles.Remove(previousFakeObstacle.Value);
                }

                if (obstacles.Add(newFakeObstacle))
                {
                    currentPos = startingPos ?? new Vector2(-1, -1);
                    currentDir = startingDir ?? new Vector2(0, 0);
                    HashSet<Vector2> turnPositions = [];
                    int loopThreshold = 2;
                    int repeatedConsecutiveTurns = 0;
                    while (currentPos.X >= 0 && currentPos.X < file.Length
                        && currentPos.Y >= 0 && currentPos.Y < file[0].Length)
                    {
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

                            // Track the current turning position.
                            // If we've repeated two or more turns in a row, then it's a loop.
                            if (turnPositions.Add(currentPos))
                            {
                                repeatedConsecutiveTurns = 0;
                            }
                            else
                            {
                                repeatedConsecutiveTurns++;
                                if (repeatedConsecutiveTurns >= loopThreshold)
                                {
                                    numPossibleLoops++;
                                    break;
                                }
                            }
                        }
                    }

                    previousFakeObstacle = newFakeObstacle;
                }
            }

            return numPossibleLoops;
        }
    }
}
