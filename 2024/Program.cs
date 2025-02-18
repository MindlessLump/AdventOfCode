using _2024;
using _2024.Problems;

Console.WriteLine("Advent of Code 2024");

Console.WriteLine("\n_____Day 01_____");
Console.WriteLine(Day1.FindTotalDistance(FileHelper.ReadLines("day1-1.txt"))); // Answer is 11
Console.WriteLine(Day1.FindTotalDistance(FileHelper.ReadLines("day1-input.txt"))); // Answer for me is 2367773
Console.WriteLine(Day1.GetSimilarityScore(FileHelper.ReadLines("day1-1.txt"))); // Answer is 31
Console.WriteLine(Day1.GetSimilarityScore(FileHelper.ReadLines("day1-input.txt"))); // Answer for me is 21271939

Console.WriteLine("\n_____Day 02_____");
Console.WriteLine(Day2.FindSafeReports(FileHelper.ReadLines("day2-1.txt"))); // Answer is 2
Console.WriteLine(Day2.FindSafeReports(FileHelper.ReadLines("day2-input.txt"))); // Answer for me is 549
Console.WriteLine(Day2.FindSafeReportsWithDampener(FileHelper.ReadLines("day2-1.txt"))); // Answer is 4
Console.WriteLine(Day2.FindSafeReportsWithDampener(FileHelper.ReadLines("day2-input.txt"))); // Answer for me is 589

Console.WriteLine("\n_____Day 03_____");
Console.WriteLine(Day3.TotalMul(FileHelper.ReadWholeFile("day3-1.txt"))); // Answer is 161
Console.WriteLine(Day3.TotalMul(FileHelper.ReadWholeFile("day3-input.txt"))); // Answer for me is 175615763
Console.WriteLine(Day3.ToggledMul(FileHelper.ReadWholeFile("day3-2.txt"))); // Answer is 48
Console.WriteLine(Day3.ToggledMul(FileHelper.ReadWholeFile("day3-input.txt"))); // Answer for me is 74361272

Console.WriteLine("\n_____Day 04_____");
Console.WriteLine(Day4.WordSearch(FileHelper.ReadLines("day4-1.txt"))); // Answer is 18
Console.WriteLine(Day4.WordSearch(FileHelper.ReadLines("day4-input.txt"))); // Answer for me is 2434
Console.WriteLine(Day4.CrossSearch(FileHelper.ReadLines("day4-1.txt"))); // Answer is 9
Console.WriteLine(Day4.CrossSearch(FileHelper.ReadLines("day4-input.txt"))); // Answer for me is 1835

Console.WriteLine("\n_____Day 05_____");
Console.WriteLine(Day5.CheckPageOrder(FileHelper.ReadLines("day5-1.txt"))); // Answer is 143
Console.WriteLine(Day5.CheckPageOrder(FileHelper.ReadLines("day5-input.txt"))); // Answer for me is 7198
Console.WriteLine(Day5.ReorderPageOrder(FileHelper.ReadLines("day5-1.txt"))); // Answer is 123
Console.WriteLine(Day5.ReorderPageOrder(FileHelper.ReadLines("day5-input.txt"))); // Answer for me is 4230

Console.WriteLine("\n_____Day 06_____");
Console.WriteLine(Day6.CountDistinctPositions(FileHelper.ReadLines("day6-1.txt"))); // Answer is 41
Console.WriteLine(Day6.CountDistinctPositions(FileHelper.ReadLines("day6-input.txt"))); // Answer for me is 4647
Console.WriteLine(Day6.CountPossibleLoops(FileHelper.ReadLines("day6-1.txt"))); // Answer is 6
Console.WriteLine(Day6.CountPossibleLoops(FileHelper.ReadLines("day6-input.txt"))); // Answer for me is 1723

Console.WriteLine("\n_____Day 07_____");
Console.WriteLine(Day7.FindTrueEquations(FileHelper.ReadLines("day7-1.txt"))); // Answer is 3749
Console.WriteLine(Day7.FindTrueEquations(FileHelper.ReadLines("day7-input.txt"))); // Answer for me is 2314935962622
Console.WriteLine(Day7.FindMoreTrueEquations(FileHelper.ReadLines("day7-1.txt"))); // Answer is 11387
Console.WriteLine(Day7.FindMoreTrueEquations(FileHelper.ReadLines("day7-input.txt"))); // Answer for me is 401477450831495

Console.WriteLine("\n_____Day 08_____");
Console.WriteLine(Day8.FindAntinodes(FileHelper.ReadLines("day8-1.txt"))); // Answer is 14
Console.WriteLine(Day8.FindAntinodes(FileHelper.ReadLines("day8-input.txt"))); // Answer for me is 228
Console.WriteLine(Day8.FindHarmonicAntinodes(FileHelper.ReadLines("day8-1.txt"))); // Answer is 34
Console.WriteLine(Day8.FindHarmonicAntinodes(FileHelper.ReadLines("day8-input.txt"))); // Answer for me is 766
