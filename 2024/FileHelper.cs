﻿namespace _2024
{
    public class FileHelper
    {
        /// <summary>
        /// Read the specified text file and returns a string array with one line per entry.
        /// </summary>
        /// <param name="relativePath">The relative path and file name (starting in the TestFiles folder).</param>
        /// <returns>A string array with the text file contents.</returns>
        public static string[] ReadLines(string relativePath)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(relativePath);

            return File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/TestFiles/" + relativePath);
        }

        /// <summary>
        /// Read the specified text file and returns a string with the whole file.
        /// </summary>
        /// <param name="relativePath">The relative path and file name (starting in the TestFiles folder).</param>
        /// <returns>A string with the text file contents.</returns>
        public static string ReadWholeFile(string relativePath)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(relativePath);

            return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/TestFiles/" + relativePath);
        }
    }
}
