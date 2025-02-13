using System.Numerics;

namespace _2024.Problems
{
    public class Day4
    {
        /*
        As the search for the Chief continues, a small Elf who lives on the station tugs on your shirt; she'd like to know if you could help her with her word search (your puzzle input). She only has to find one word: XMAS.

        This word search allows words to be horizontal, vertical, diagonal, written backwards, or even overlapping other words. It's a little unusual, though, as you don't merely need to find one instance of XMAS - you need to find all of them. Here are a few ways XMAS might appear, where irrelevant characters have been replaced with .:


        ..X...
        .SAMX.
        .A..A.
        XMAS.S
        .X....
        The actual word search will be full of letters instead. For example:

        MMMSXXMASM
        MSAMXMSMSA
        AMXSXMAAMM
        MSAMASMSMX
        XMASAMXAMM
        XXAMMXXAMA
        SMSMSASXSS
        SAXAMASAAA
        MAMMMXMMMM
        MXMXAXMASX
        In this word search, XMAS occurs a total of 18 times; here's the same word search again, but where letters not involved in any XMAS have been replaced with .:

        ....XXMAS.
        .SAMXMS...
        ...S..A...
        ..A.A.MS.X
        XMASAMX.MM
        X.....XA.A
        S.S.S.S.SS
        .A.A.A.A.A
        ..M.M.M.MM
        .X.X.XMASX
        Take a look at the little Elf's word search. How many times does XMAS appear?
         */
        public static int WordSearch(string[] file)
        {
            string word = "XMAS";

            // Start by saving the grid positions of the word's starting letter for future calculations
            List<Vector2> firstLetters = [];
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    if (word.Contains(file[i][j]))
                    {
                        if (file[i][j] == word[0])
                        {
                            firstLetters.Add(new Vector2(i, j));
                        }
                    }
                }
            }

            // Now start from the first letters and try to find the rest of the word
            int found = 0;
            List<Vector2> directions = [
                new Vector2(-1, -1),
                new Vector2(-1, 0),
                new Vector2(-1, 1),
                new Vector2(0, -1),
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, -1),
                new Vector2(1, 0),
                new Vector2(1, 1),
            ];
            // Start from each first letter
            foreach (var letter in firstLetters)
            {
                // Try to find the rest of the word in one of the eight possible directions
                foreach (var direction in directions)
                {
                    int i = 1;
                    for (; i < word.Length; i++)
                    {
                        int x = (int)(letter.X + (direction.X * i));
                        int y = (int)(letter.Y + (direction.Y * i));
                        if (
                            x < 0 || x >= file.Length ||
                            y < 0 || y >= file[0].Length ||
                            word[i] != file[x][y])
                        {
                            // The word is not present in this direction
                            break;
                        }
                    }

                    if (i == word.Length)
                    {
                        found++;
                    }
                }
            }

            return found;
        }

        /*
        Looking for the instructions, you flip over the word search to find that this isn't actually an XMAS puzzle; it's an X-MAS puzzle in which you're supposed to find two MAS in the shape of an X. One way to achieve that is like this:

        M.S
        .A.
        M.S
        Irrelevant characters have again been replaced with . in the above diagram. Within the X, each MAS can be written forwards or backwards.

        Here's the same example from before, but this time all of the X-MASes have been kept instead:

        .M.S......
        ..A..MSMS.
        .M.S.MAA..
        ..A.ASMSM.
        .M.S.M....
        ..........
        S.S.S.S.S.
        .A.A.A.A..
        M.M.M.M.M.
        ..........
        In this example, an X-MAS appears 9 times.

        Flip the word search from the instructions back over to the word search side and try again. How many times does an X-MAS appear?
         */
        public static int CrossSearch(string[] file)
        {
            char middleLetter = 'A';
            char oneEdge = 'M';
            char otherEdge = 'S';

            // Start by saving the grid positions of the middle letter for future calculations
            List<Vector2> middleLetters = [];
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[i].Length; j++)
                {
                    if (file[i][j] == middleLetter)
                    {
                        middleLetters.Add(new Vector2(i, j));
                    }
                }
            }

            // Now start from the first letters and try to find the rest of the word
            int found = 0;
            // Start from each first letter
            foreach (var letter in middleLetters)
            {
                if (letter.X < 1 || letter.X >= file.Length - 1 ||
                    letter.Y < 1 || letter.Y >= file[0].Length - 1)
                {
                    continue;
                }

                var topLeft = file[(int)letter.X - 1][(int)letter.Y - 1];
                var topRight = file[(int)letter.X - 1][(int)letter.Y + 1];
                var bottomLeft = file[(int)letter.X + 1][(int)letter.Y - 1];
                var bottomRight = file[(int)letter.X + 1][(int)letter.Y + 1];

                if (topLeft == oneEdge && topRight == oneEdge &&
                    bottomLeft == otherEdge && bottomRight == otherEdge)
                {
                    found++;
                }
                else if (topLeft == oneEdge && bottomLeft == oneEdge &&
                    topRight == otherEdge && bottomRight == otherEdge)
                {
                    found++;
                }
                else if (topLeft == otherEdge && topRight == otherEdge &&
                    bottomLeft == oneEdge && bottomRight == oneEdge)
                {
                    found++;
                }
                else if (topLeft == otherEdge && bottomLeft == otherEdge &&
                    topRight == oneEdge && bottomRight == oneEdge)
                {
                    found++;
                }
            }

            return found;
        }
    }
}
