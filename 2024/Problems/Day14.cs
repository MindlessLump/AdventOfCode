using System.Numerics;

namespace _2024.Problems
{
    public class Day14
    {
        /*
        To get The Historian safely to the bathroom, you'll need a way to predict where the robots will be in the future. Fortunately, they all seem to be moving on the tile floor in predictable straight lines.

        You make a list (your puzzle input) of all of the robots' current positions (p) and velocities (v), one robot per line. For example:

        p=0,4 v=3,-3
        p=6,3 v=-1,-3
        p=10,3 v=-1,2
        p=2,0 v=2,-1
        p=0,0 v=1,3
        p=3,0 v=-2,-2
        p=7,6 v=-1,-3
        p=3,0 v=-1,-2
        p=9,3 v=2,3
        p=7,3 v=-1,2
        p=2,4 v=2,-3
        p=9,5 v=-3,-3
        Each robot's position is given as p=x,y where x represents the number of tiles the robot is from the left wall and y represents the number of tiles from the top wall (when viewed from above). So, a position of p=0,0 means the robot is all the way in the top-left corner.

        Each robot's velocity is given as v=x,y where x and y are given in tiles per second. Positive x means the robot is moving to the right, and positive y means the robot is moving down. So, a velocity of v=1,-2 means that each second, the robot moves 1 tile to the right and 2 tiles up.

        The robots outside the actual bathroom are in a space which is 101 tiles wide and 103 tiles tall (when viewed from above). However, in this example, the robots are in a space which is only 11 tiles wide and 7 tiles tall.

        The robots are good at navigating over/under each other (due to a combination of springs, extendable legs, and quadcopters), so they can share the same tile and don't interact with each other. Visually, the number of robots on each tile in this example looks like this:

        1.12.......
        ...........
        ...........
        ......11.11
        1.1........
        .........1.
        .......1...
        These robots have a unique feature for maximum bathroom security: they can teleport. When a robot would run into an edge of the space they're in, they instead teleport to the other side, effectively wrapping around the edges. Here is what robot p=2,4 v=2,-3 does for the first few seconds:

        Initial state:
        ...........
        ...........
        ...........
        ...........
        ..1........
        ...........
        ...........

        After 1 second:
        ...........
        ....1......
        ...........
        ...........
        ...........
        ...........
        ...........

        After 2 seconds:
        ...........
        ...........
        ...........
        ...........
        ...........
        ......1....
        ...........

        After 3 seconds:
        ...........
        ...........
        ........1..
        ...........
        ...........
        ...........
        ...........

        After 4 seconds:
        ...........
        ...........
        ...........
        ...........
        ...........
        ...........
        ..........1

        After 5 seconds:
        ...........
        ...........
        ...........
        .1.........
        ...........
        ...........
        ...........
        The Historian can't wait much longer, so you don't have to simulate the robots for very long. Where will the robots be after 100 seconds?

        In the above example, the number of robots on each tile after 100 seconds has elapsed looks like this:

        ......2..1.
        ...........
        1..........
        .11........
        .....1.....
        ...12......
        .1....1....
        To determine the safest area, count the number of robots in each quadrant after 100 seconds. Robots that are exactly in the middle (horizontally or vertically) don't count as being in any quadrant, so the only relevant robots are:

        ..... 2..1.
        ..... .....
        1.... .....
           
        ..... .....
        ...12 .....
        .1... 1....
        In this example, the quadrants contain 1, 3, 4, and 1 robot. Multiplying these together gives a total safety factor of 12.

        Predict the motion of the robots in your list within a space which is 101 tiles wide and 103 tiles tall. What will the safety factor be after exactly 100 seconds have elapsed?
         */
        public static int FindSafetyFactor(string[] file, int seconds, int widthX, int widthY)
        {
            // Parse the file to get starting positions and velocities for each robot
            List<(Vector2, Vector2)> robots = [];
            foreach (string line in file)
            {
                var split = line.Split(' ').Select(s => s.Substring(2)).ToList(); // Remove "p=" and "v="
                var pos = split[0].Split(',');
                var vel = split[1].Split(',');
                robots.Add((new Vector2(int.Parse(pos[0]), int.Parse(pos[1])), new Vector2(int.Parse(vel[0]), int.Parse(vel[1]))));
            }

            // Track each robot's movement throughout the specified number of seconds
            for (int j = 0; j < robots.Count; j++)
            {
                (var robotPos, var robotVel) = robots[j];

                var newPos = robotPos + robotVel * seconds;
                while (newPos.X >= widthX)
                {
                    newPos.X -= widthX;
                }
                while (newPos.X < 0)
                {
                    newPos.X += widthX;
                }
                while (newPos.Y >= widthY)
                {
                    newPos.Y -= widthY;
                }
                while (newPos.Y < 0)
                {
                    newPos.Y += widthY;
                }

                robots[j] = (newPos, robotVel);
            }

            // Count the number of robots in each quadrant
            double middleX = (double)widthX / 2 - 0.5;
            double middleY = (double)widthY / 2 - 0.5;
            int quadrant1 = 0;
            int quadrant2 = 0;
            int quadrant3 = 0;
            int quadrant4 = 0;
            foreach ((var pos, var vel) in robots)
            {
                if (pos.X > middleX && pos.Y < middleY)
                {
                    quadrant1++;
                }
                else if (pos.X > middleX && pos.Y > middleY)
                {
                    quadrant2++;
                }
                else if (pos.X < middleX && pos.Y > middleY)
                {
                    quadrant3++;
                }
                else if (pos.X < middleX && pos.Y < middleY)
                {
                    quadrant4++;
                }
            }

            return quadrant1 * quadrant2 * quadrant3 * quadrant4;
        }

        /*
        During the bathroom break, someone notices that these robots seem awfully similar to ones built and used at the North Pole. If they're the same type of robots, they should have a hard-coded Easter egg: very rarely, most of the robots should arrange themselves into a picture of a Christmas tree.

        What is the fewest number of seconds that must elapse for the robots to display the Easter egg?
         */
        public static int FindChristmasTree(string[] file, int widthX, int widthY)
        {
            // Parse the file to get starting positions and velocities for each robot
            List<(Vector2, Vector2)> robots = [];
            int[,] map = new int[widthY, widthX];
            for (int y = 0; y < widthY; y++)
            {
                for (int x = 0; x < widthX; x++)
                {
                    map[y, x] = 0;
                }
            }

            foreach (string line in file)
            {
                var split = line.Split(' ').Select(s => s.Substring(2)).ToList(); // Remove "p=" and "v="
                var pos = split[0].Split(',').Select(s => int.Parse(s)).ToList();
                var vel = split[1].Split(',').Select(s => int.Parse(s)).ToList();
                robots.Add((new Vector2(pos[0], pos[1]), new Vector2(vel[0], vel[1])));

                map[pos[1], pos[0]]++;
            }

            // Track each robot's movement throughout the specified number of seconds
            int maxSteps = widthX * widthY;
            var middle = new Vector2((float)((double)widthX / 2 - 0.5), (float)((double)widthY / 2 - 0.5));
            Dictionary<int, double> spreadByStep = [];
            for (int step = 1; step <= maxSteps; step++)
            {
                double totalDist = 0;
                for (int j = 0; j < robots.Count; j++)
                {
                    (var robotPos, var robotVel) = robots[j];

                    var newPos = robotPos + robotVel;
                    while (newPos.X >= widthX)
                    {
                        newPos.X -= widthX;
                    }
                    while (newPos.X < 0)
                    {
                        newPos.X += widthX;
                    }
                    while (newPos.Y >= widthY)
                    {
                        newPos.Y -= widthY;
                    }
                    while (newPos.Y < 0)
                    {
                        newPos.Y += widthY;
                    }

                    map[(int)robotPos.Y, (int)robotPos.X]--;
                    map[(int)newPos.Y, (int)newPos.X]++;
                    robots[j] = (newPos, robotVel);
                    totalDist += (middle - newPos).Length();
                }

                spreadByStep.Add(step, totalDist);
            }

            return spreadByStep.OrderBy(kvp => kvp.Value).First().Key;
        }
    }
}
