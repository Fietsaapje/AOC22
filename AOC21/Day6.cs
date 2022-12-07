using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC21
{
    class Day6
    {
        public static string GetAnswer(string[] inputunprocessed)
        {
            int markersize = 14;
            char[] input = inputunprocessed[0].ToCharArray();

            for (int i = 0; i < input.Length - markersize; i++)
            {
                //create and fill subarray of markersize
                char[] sub = new char[markersize];
                for (int j = 0; j < markersize; j++)
                    sub[j] = input[i + j];

                //check for [markersize] distinct values within subarray
                if (sub.Distinct().Count() == markersize)
                    return $"Marker found at index {i + markersize}";

            }

            return "Not found";
        }
    }
}
