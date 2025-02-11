using _2024;
using _2024.Problems;

Console.WriteLine("Advent of Code 2024\n");

Console.WriteLine("_____Day 01_____");
Console.WriteLine(Day1.FindTotalDistance(FileHelper.ReadFile("day1-1.txt"))); // Answer is 11
Console.WriteLine(Day1.FindTotalDistance(FileHelper.ReadFile("day1-input.txt"))); // Answer for me is 2367773
Console.WriteLine(Day1.GetSimilarityScore(FileHelper.ReadFile("day1-1.txt"))); // Answer is 31
Console.WriteLine(Day1.GetSimilarityScore(FileHelper.ReadFile("day1-input.txt"))); // Answer for me is 21271939
Console.WriteLine("\n\n");

Console.WriteLine("_____Day 02_____");
Console.WriteLine(Day2.FindSafeReports(FileHelper.ReadFile("day2-1.txt"))); // Answer is 2
Console.WriteLine(Day2.FindSafeReports(FileHelper.ReadFile("day2-input.txt"))); // Answer for me is 549
Console.WriteLine(Day2.FindSafeReportsWithDampener(FileHelper.ReadFile("day2-1.txt"))); // Answer is 4
Console.WriteLine(Day2.FindSafeReportsWithDampener(FileHelper.ReadFile("day2-input.txt"))); // Answer for me is 589
Console.WriteLine("\n\n");
