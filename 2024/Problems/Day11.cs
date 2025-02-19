﻿namespace _2024.Problems
{
    public class Day11
    {
        /*
        The ancient civilization on Pluto was known for its ability to manipulate spacetime, and while The Historians explore their infinite corridors, you've noticed a strange set of physics-defying stones.

        At first glance, they seem like normal stones: they're arranged in a perfectly straight line, and each stone has a number engraved on it.

        The strange part is that every time you blink, the stones change.

        Sometimes, the number engraved on a stone changes. Other times, a stone might split in two, causing all the other stones to shift over a bit to make room in their perfectly straight line.

        As you observe them for a while, you find that the stones have a consistent behavior. Every time you blink, the stones each simultaneously change according to the first applicable rule in this list:

        If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
        If the stone is engraved with a number that has an even number of digits, it is replaced by two stones. The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
        If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.
        No matter how the stones change, their order is preserved, and they stay on their perfectly straight line.

        How will the stones evolve if you keep blinking at them? You take a note of the number engraved on each stone in the line (your puzzle input).

        If you have an arrangement of five stones engraved with the numbers 0 1 10 99 999 and you blink once, the stones transform as follows:

        The first stone, 0, becomes a stone marked 1.
        The second stone, 1, is multiplied by 2024 to become 2024.
        The third stone, 10, is split into a stone marked 1 followed by a stone marked 0.
        The fourth stone, 99, is split into two stones marked 9.
        The fifth stone, 999, is replaced by a stone marked 2021976.
        So, after blinking once, your five stones would become an arrangement of seven stones engraved with the numbers 1 2024 1 0 9 9 2021976.

        Here is a longer example:

        Initial arrangement:
        125 17

        After 1 blink:
        253000 1 7

        After 2 blinks:
        253 0 2024 14168

        After 3 blinks:
        512072 1 20 24 28676032

        After 4 blinks:
        512 72 2024 2 0 2 4 2867 6032

        After 5 blinks:
        1036288 7 2 20 24 4048 1 4048 8096 28 67 60 32

        After 6 blinks:
        2097446912 14168 4048 2 0 2 4 40 48 2024 40 48 80 96 2 8 6 7 6 0 3 2
        In this example, after blinking six times, you would have 22 stones. After blinking 25 times, you would have 55312 stones!

        Consider the arrangement of stones in front of you. How many stones will you have after blinking 25 times?

        ===== PART 2 =====
        The Historians sure are taking a long time. To be fair, the infinite corridors are very large.

        How many stones would you have after blinking a total of 75 times?
         */
        public static long CountStonesAfterBlinks(string file, int numBlinks)
        {
            // Parse the file to grab the starting stones
            Dictionary<long, long> stones = [];
            foreach (string stone in file.Trim().Split(' '))
            {
                if (!stones.TryAdd(long.Parse(stone), 1))
                {
                    stones[long.Parse(stone)]++;
                }
            }

            // Now process each blink
            for (int i = 0; i < numBlinks; i++)
            {
                Blink(stones);
            }

            long totalStones = 0;
            foreach (var (stoneValue, count) in stones)
            {
                totalStones += count;
            }

            return totalStones;
        }

        // If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
        // If the stone is engraved with a number that has an even number of digits, it is replaced by two stones.The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
        // If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.
        // No matter how the stones change, their order is preserved, and they stay on their perfectly straight line.
        private static void Blink(Dictionary<long, long> stones)
        {
            var copiedStones = new Dictionary<long, long>(stones);

            foreach (var (stoneValue, count) in copiedStones.Where(s => s.Value != 0))
            {
                stones[stoneValue] -= count;

                if (stoneValue == 0)
                {
                    if (!stones.TryAdd(1, count))
                    {
                        stones[1] += count;
                    }
                }
                else
                {
                    var digits = Math.Floor(Math.Log10(stoneValue) + 1);
                    if (digits % 2 == 0)
                    {
                        long divisor = (long)Math.Pow(10, digits / 2);
                        long firstHalf = stoneValue / divisor;
                        long secondHalf = stoneValue % divisor;

                        if (!stones.TryAdd(firstHalf, count))
                        {
                            stones[firstHalf] += count;
                        }

                        if (!stones.TryAdd(secondHalf, count))
                        {
                            stones[secondHalf] += count;
                        }
                    }
                    else
                    {
                        long newValue = stoneValue * 2024;
                        if (!stones.TryAdd(newValue, count))
                        {
                            stones[newValue] += count;
                        }
                    }
                }
            }
        }
    }
}
