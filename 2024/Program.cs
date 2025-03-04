﻿using _2024;
using _2024.Problems;
using System.Numerics;

Console.WriteLine("Advent of Code 2024");

//Console.WriteLine("\n_____Day 01_____");
//Console.WriteLine(Day01.FindTotalDistance(FileHelper.ReadLines("day01-1.txt"))); // Answer is 11
//Console.WriteLine(Day01.FindTotalDistance(FileHelper.ReadLines("day01-input.txt"))); // Answer for me is 2367773
//Console.WriteLine(Day01.GetSimilarityScore(FileHelper.ReadLines("day01-1.txt"))); // Answer is 31
//Console.WriteLine(Day01.GetSimilarityScore(FileHelper.ReadLines("day01-input.txt"))); // Answer for me is 21271939

//Console.WriteLine("\n_____Day 02_____");
//Console.WriteLine(Day02.FindSafeReports(FileHelper.ReadLines("day02-1.txt"))); // Answer is 2
//Console.WriteLine(Day02.FindSafeReports(FileHelper.ReadLines("day02-input.txt"))); // Answer for me is 549
//Console.WriteLine(Day02.FindSafeReportsWithDampener(FileHelper.ReadLines("day02-1.txt"))); // Answer is 4
//Console.WriteLine(Day02.FindSafeReportsWithDampener(FileHelper.ReadLines("day02-input.txt"))); // Answer for me is 589

//Console.WriteLine("\n_____Day 03_____");
//Console.WriteLine(Day03.TotalMul(FileHelper.ReadWholeFile("day03-1.txt"))); // Answer is 161
//Console.WriteLine(Day03.TotalMul(FileHelper.ReadWholeFile("day03-input.txt"))); // Answer for me is 175615763
//Console.WriteLine(Day03.ToggledMul(FileHelper.ReadWholeFile("day03-2.txt"))); // Answer is 48
//Console.WriteLine(Day03.ToggledMul(FileHelper.ReadWholeFile("day03-input.txt"))); // Answer for me is 74361272

//Console.WriteLine("\n_____Day 04_____");
//Console.WriteLine(Day04.WordSearch(FileHelper.ReadLines("day04-1.txt"))); // Answer is 18
//Console.WriteLine(Day04.WordSearch(FileHelper.ReadLines("day04-input.txt"))); // Answer for me is 2434
//Console.WriteLine(Day04.CrossSearch(FileHelper.ReadLines("day04-1.txt"))); // Answer is 9
//Console.WriteLine(Day04.CrossSearch(FileHelper.ReadLines("day04-input.txt"))); // Answer for me is 1835

//Console.WriteLine("\n_____Day 05_____");
//Console.WriteLine(Day05.CheckPageOrder(FileHelper.ReadLines("day05-1.txt"))); // Answer is 143
//Console.WriteLine(Day05.CheckPageOrder(FileHelper.ReadLines("day05-input.txt"))); // Answer for me is 7198
//Console.WriteLine(Day05.ReorderPageOrder(FileHelper.ReadLines("day05-1.txt"))); // Answer is 123
//Console.WriteLine(Day05.ReorderPageOrder(FileHelper.ReadLines("day05-input.txt"))); // Answer for me is 4230

//Console.WriteLine("\n_____Day 06_____");
//Console.WriteLine(Day06.CountDistinctPositions(FileHelper.ReadLines("day06-1.txt"))); // Answer is 41
//Console.WriteLine(Day06.CountDistinctPositions(FileHelper.ReadLines("day06-input.txt"))); // Answer for me is 4647
//Console.WriteLine(Day06.CountPossibleLoops(FileHelper.ReadLines("day06-1.txt"))); // Answer is 6
//Console.WriteLine(Day06.CountPossibleLoops(FileHelper.ReadLines("day06-input.txt"))); // Answer for me is 1723

//Console.WriteLine("\n_____Day 07_____");
//Console.WriteLine(Day07.FindTrueEquations(FileHelper.ReadLines("day07-1.txt"))); // Answer is 3749
//Console.WriteLine(Day07.FindTrueEquations(FileHelper.ReadLines("day07-input.txt"))); // Answer for me is 2314935962622
//Console.WriteLine(Day07.FindMoreTrueEquations(FileHelper.ReadLines("day07-1.txt"))); // Answer is 11387
//Console.WriteLine(Day07.FindMoreTrueEquations(FileHelper.ReadLines("day07-input.txt"))); // Answer for me is 401477450831495

//Console.WriteLine("\n_____Day 08_____");
//Console.WriteLine(Day08.FindAntinodes(FileHelper.ReadLines("day08-1.txt"))); // Answer is 14
//Console.WriteLine(Day08.FindAntinodes(FileHelper.ReadLines("day08-input.txt"))); // Answer for me is 228
//Console.WriteLine(Day08.FindHarmonicAntinodes(FileHelper.ReadLines("day08-1.txt"))); // Answer is 34
//Console.WriteLine(Day08.FindHarmonicAntinodes(FileHelper.ReadLines("day08-input.txt"))); // Answer for me is 766

//Console.WriteLine("\n_____Day 09_____");
//Console.WriteLine(Day09.FindCompressedChecksum(FileHelper.ReadWholeFile("day09-1.txt"))); // Answer is 1928
//Console.WriteLine(Day09.FindCompressedChecksum(FileHelper.ReadWholeFile("day09-input.txt"))); // Answer for me is 6330095022244
//Console.WriteLine(Day09.FindChecksumWithoutFragmentation(FileHelper.ReadWholeFile("day09-1.txt"))); // Answer is 2858
//Console.WriteLine(Day09.FindChecksumWithoutFragmentation(FileHelper.ReadWholeFile("day09-input.txt"))); // Answer for me is 6359491814941

//Console.WriteLine("\n_____Day 10_____");
//Console.WriteLine(Day10.FindTrailheadScores(FileHelper.ReadLines("day10-1.txt"))); // Answer is 1
//Console.WriteLine(Day10.FindTrailheadScores(FileHelper.ReadLines("day10-2.txt"))); // Answer is 36
//Console.WriteLine(Day10.FindTrailheadScores(FileHelper.ReadLines("day10-input.txt"))); // Answer for me is 566
//Console.WriteLine(Day10.FindTrailheadRatings(FileHelper.ReadLines("day10-2.txt"))); // Answer is 81
//Console.WriteLine(Day10.FindTrailheadRatings(FileHelper.ReadLines("day10-input.txt"))); // Answer for me is 1324

//Console.WriteLine("\n_____Day 11_____");
//Console.WriteLine(Day11.CountStonesAfterBlinks(FileHelper.ReadWholeFile("day11-1.txt"), 25)); // Answer is 55312
//Console.WriteLine(Day11.CountStonesAfterBlinks(FileHelper.ReadWholeFile("day11-input.txt"), 25)); // Answer for me is 200446
//Console.WriteLine(Day11.CountStonesAfterBlinks(FileHelper.ReadWholeFile("day11-input.txt"), 75)); // Answer for me is 238317474993392

//Console.WriteLine("\n_____Day 12_____");
//Console.WriteLine(Day12.CalculateTotalFenceCosts(FileHelper.ReadLines("day12-1.txt"))); // Answer is 1930
//Console.WriteLine(Day12.CalculateTotalFenceCosts(FileHelper.ReadLines("day12-input.txt"))); // Answer for me is 1477924
//Console.WriteLine(Day12.CalculateDiscountedFenceCosts(FileHelper.ReadLines("day12-1.txt"))); // Answer is 1206
//Console.WriteLine(Day12.CalculateDiscountedFenceCosts(FileHelper.ReadLines("day12-input.txt"))); // Answer for me is 841934

//Console.WriteLine("\n_____Day 13_____");
//Console.WriteLine(Day13.CalculateTokensForPrizes(FileHelper.ReadLines("day13-1.txt"))); // Answer is 480
//Console.WriteLine(Day13.CalculateTokensForPrizes(FileHelper.ReadLines("day13-input.txt"))); // Answer for me is 26599
//Console.WriteLine(Day13.CalculateTokensForFartherPrizes(FileHelper.ReadLines("day13-input.txt"))); // Answer for me is 106228669504887

//Console.WriteLine("\n_____Day 14_____");
//Console.WriteLine(Day14.FindSafetyFactor(FileHelper.ReadLines("day14-1.txt"), 100, 11, 7)); // Answer is 12
//Console.WriteLine(Day14.FindSafetyFactor(FileHelper.ReadLines("day14-input.txt"), 100, 101, 103)); // Answer for me is 218295000
//Console.WriteLine(Day14.FindChristmasTree(FileHelper.ReadLines("day14-input.txt"), 101, 103)); // Answer for me is 6870

//Console.WriteLine("\n_____Day 15_____");
//Console.WriteLine(Day15.FindGoodsPositioningAfterRobot(FileHelper.ReadLines("day15-1.txt"))); // Answer is 2028
//Console.WriteLine(Day15.FindGoodsPositioningAfterRobot(FileHelper.ReadLines("day15-2.txt"))); // Answer is 10092
//Console.WriteLine(Day15.FindGoodsPositioningAfterRobot(FileHelper.ReadLines("day15-input.txt"))); // Answer for me is 1509074
//Console.WriteLine(Day15.FindWideGoodsPositioningAfterRobot(FileHelper.ReadLines("day15-2.txt"))); // Answer is 9021
//Console.WriteLine(Day15.FindWideGoodsPositioningAfterRobot(FileHelper.ReadLines("day15-input.txt"))); // Answer for me is 1521453

Console.WriteLine("\n_____Day 16_____");
Console.WriteLine(Day16.ReindeerMazeScore(FileHelper.ReadLines("day16-1.txt"))); // Answer is 7036
//Console.WriteLine(Day16.ReindeerMazeScore(FileHelper.ReadLines("day16-2.txt"))); // Answer is 11048
