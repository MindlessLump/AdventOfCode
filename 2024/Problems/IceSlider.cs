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

        public static string SolvePuzzle(string[] file)
        {
            // First, build the map based on the input file. For the sake of simplicity, we'll assume the map is always square.
            var walls = new HashSet<Vector2>();
            var redEnd = new Vector2();
            var orange1End = new Vector2();
            var orange2End = new Vector2();
            var yellow1End = new Vector2();
            var yellow2End = new Vector2();
            var blueEnd = new Vector2();
            var redStart = new Vector2();
            var orange1Start = new Vector2();
            var orange2Start = new Vector2();
            var yellow1Start = new Vector2();
            var yellow2Start = new Vector2();
            var blueStart = new Vector2();
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    char position = file[i][j];
                    switch (position)
                    {
                        case '#':
                            walls.Add(new Vector2(i, j));
                            break;
                        case 'R':
                            redEnd = new Vector2(i, j);
                            break;
                        case 'O':
                            if (orange1End.X == 0 && orange1End.Y == 0)
                            {
                                orange1End = new Vector2(i, j);
                            }
                            else
                            {
                                orange2End = new Vector2(i, j);
                            }
                            break;
                        case 'Y':
                            if (yellow1End.X == 0 && yellow1End.Y == 0)
                            {
                                yellow1End = new Vector2(i, j);
                            }
                            else
                            {
                                yellow2End = new Vector2(i, j);
                            }
                            break;
                        case 'B':
                            blueEnd = new Vector2(i, j);
                            break;
                        case 'r':
                            redStart = new Vector2(i, j);
                            break;
                        case 'o':
                            if (orange1Start.X == 0 && orange1Start.Y == 0)
                            {
                                orange1Start = new Vector2(i, j);
                            }
                            else
                            {
                                orange2Start = new Vector2(i, j);
                            }
                            break;
                        case 'y':
                            if (yellow1Start.X == 0 && yellow1Start.Y == 0)
                            {
                                yellow1Start = new Vector2(i, j);
                            }
                            else
                            {
                                yellow2Start = new Vector2(i, j);
                            }
                            break;
                        case 'b':
                            blueStart = new Vector2(i, j);
                            break;
                    }
                }
            }

            // Our state machine holds the position of all 6 pucks in one giant vector.
            // We'll need to ensure that we treat the Yellows and Oranges as interchangeable,
            // since we don't care which matching puck lands in each final position.
            var goal = SortPucks(redEnd, orange1End, orange2End, yellow1End, yellow2End, blueEnd);

            var start = SortPucks(redStart, orange1Start, orange2Start, yellow1Start, yellow2Start, blueStart);

            Console.WriteLine($"\n[{string.Join("; ", goal)}]");
            Console.WriteLine($"[{string.Join("; ", start)}]");

            return string.Empty;
        }

        private static HashSet<Vector2> SortPucks(Vector2 red, Vector2 orange1, Vector2 orange2, Vector2 yellow1, Vector2 yellow2, Vector2 blue)
        {
            if (orange1.X > orange2.X
                || (orange1.X == orange2.X && orange1.Y > orange2.Y))
            {
                (orange1, orange2) = (orange2, orange1);
            }

            if (yellow1.X > yellow2.X
                || (yellow1.X == yellow2.X && yellow1.Y > yellow2.Y))
            {
                (yellow1, yellow2) = (yellow2, yellow1);
            }

            return new HashSet<Vector2>()
            {
                red,
                orange1,
                orange2,
                yellow1,
                yellow2,
                blue,
            };
        }
    }
}
