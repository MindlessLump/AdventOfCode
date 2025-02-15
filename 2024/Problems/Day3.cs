using System.Text.RegularExpressions;

namespace _2024.Problems
{
    public class Day3
    {
        /*
        The computer appears to be trying to run a program, but its memory (your puzzle input) is corrupted. All of the instructions have been jumbled up!

        It seems like the goal of the program is just to multiply some numbers. It does that with instructions like mul(X,Y), where X and Y are each 1-3 digit numbers. For instance, mul(44,46) multiplies 44 by 46 to get a result of 2024. Similarly, mul(123,4) would multiply 123 by 4.

        However, because the program's memory has been corrupted, there are also many invalid characters that should be ignored, even if they look like part of a mul instruction. Sequences like mul(4*, mul(6,9!, ?(12,34), or mul ( 2 , 4 ) do nothing.

        For example, consider the following section of corrupted memory:

        xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
        Only the four highlighted sections are real mul instructions. Adding up the result of each instruction produces 161 (2*4 + 5*5 + 11*8 + 8*5).

        Scan the corrupted memory for uncorrupted mul instructions. What do you get if you add up all of the results of the multiplications?
         */
        public static int TotalMul(string file)
        {
            // Use RegEx to find sections within the string that match mul(XXX,YYY) without unexpected characters
            // Using capture groups, we should be able to easily fetch XXX and YYY from each match
            var mulRegex = new Regex(@"mul\(([1-9][0-9]{0,2}),([1-9][0-9]{0,2})\)");

            int totalMul = 0;
            MatchCollection matches = mulRegex.Matches(file);
            foreach (Match match in matches)
            {
                totalMul += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }

            return totalMul;
        }

        /*
        There are two new instructions you'll need to handle:

        The do() instruction enables future mul instructions.
        The don't() instruction disables future mul instructions.
        Only the most recent do() or don't() instruction applies. At the beginning of the program, mul instructions are enabled.

        For example:

        xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
        This corrupted memory is similar to the example from before, but this time the mul(5,5) and mul(11,8) instructions are disabled because there is a don't() instruction before them. The other mul instructions function normally, including the one at the end that gets re-enabled by a do() instruction.

        This time, the sum of the results is 48 (2*4 + 8*5).

        Handle the new instructions; what do you get if you add up all of the results of just the enabled multiplications?
         */
        public static int ToggledMul(string file)
        {
            // Find indexes of all do() instructions (plus index 0)
            var doRegex = new Regex(@"do\(\)");
            var doInstructions = doRegex.Matches(file);
            List<int> doIndexes = [0];
            foreach (Match match in doInstructions)
            {
                doIndexes.Add(match.Index);
            }

            // Find indexes of all don't() instructions
            var dontRegex = new Regex(@"don't\(\)");
            var dontInstructions = dontRegex.Matches(file);
            List<int> dontIndexes = [];
            foreach (Match match in dontInstructions)
            {
                dontIndexes.Add(match.Index);
            }

            // If there is no don't() instruction, then the whole file is enabled
            if (dontIndexes.Count == 0)
            {
                return TotalMul(file);
            }

            int j = 0;
            int currIdx = 0;
            int totalMul = 0;
            for (int i = 0; i < doIndexes.Count;)
            {
                if (doIndexes[i] < currIdx)
                {
                    i++;
                    continue;
                }

                currIdx = doIndexes[i];

                int distToNextDont;
                if (j < dontIndexes.Count) // There's at least one don't() coming up
                {
                    distToNextDont = dontIndexes[j] - doIndexes[i];
                }
                else
                {
                    distToNextDont = file.Length - currIdx;
                }

                if (distToNextDont > 0)
                {
                    totalMul += TotalMul(file.Substring(currIdx, distToNextDont));
                    currIdx += distToNextDont;
                }

                j++;
            }

            return totalMul;
        }
    }
}
