// See https://aka.ms/new-console-template for more information
using _2024;
using _2024.Problems;

Console.WriteLine("Advent of Code 2024");

Console.WriteLine("_____Day 1_____");
Console.WriteLine(Day1.FindTotalDistance(FileHelper.ReadFile("day1-1.txt"))); // Answer is 11
Console.WriteLine(Day1.FindTotalDistance(FileHelper.ReadFile("day1-input.txt"))); // Answer for me is 2367773
Console.WriteLine(Day1.GetSimilarityScore(FileHelper.ReadFile("day1-1.txt"))); // Answer is 31
Console.WriteLine(Day1.GetSimilarityScore(FileHelper.ReadFile("day1-input.txt"))); // Answer for me is 21271939
Console.WriteLine("\n\n");
