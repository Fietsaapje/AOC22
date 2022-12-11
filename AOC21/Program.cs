using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC21
{

    class Program
    {
        static void Main(string[] args)
        {
            string[] input;
            DateTime before;

            //Day 6
            input = ReadFileStrings(6);
            before = DateTime.Now;

            Console.WriteLine(Day6.GetAnswer(input));

            Console.WriteLine($"Running time:{ DateTime.Now - before}");

            //Day 7
            input = ReadFileStrings(7);
            before = DateTime.Now;

            Console.WriteLine(Day7.GetAnswer(input));

            Console.WriteLine($"Running time:{ DateTime.Now - before}");

            //Day 8
            input = ReadFileStrings(8);
            before = DateTime.Now;
            Day8.GetAnswers(input);
            Console.WriteLine($"Running time:{ DateTime.Now - before}");

            //Day 9
            input = ReadFileStrings(9);
            before = DateTime.Now;
            Day9.GetAnswers2(input);
            Console.WriteLine($"Running time:{ DateTime.Now - before}");

            //Day 10
            input = ReadFileStrings(10);
            before = DateTime.Now;
            Day10.GetAnswer2(input);
            Console.WriteLine($"Running time:{ DateTime.Now - before}");


        }



        public static int AOCD1v1(int day)
        {
            int biggest = 0;
            var input = ReadFileStrings(day);
            int calories = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != "" || i == input.Length)
                    calories += Int32.Parse(input[i]);
                else
                {
                    if (calories > biggest)
                        biggest = calories;
                    calories = 0;
                }
            }

            return biggest;
        }

        public static int AOCD1v2(int day)
        {
            int[] top3 = new int[3];
            var input = ReadFileStrings(day);
            int calories = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != "" || i == input.Length)
                    calories += Int32.Parse(input[i]);
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (top3[j] < calories)
                        {
                            top3[j] = calories;
                            break;
                        }
                    }
                    calories = 0;
                }
            }

            return top3[0] + top3[1] + top3[2];
        }

        public static int AOCD2v1(int day)
        {
            string[] input = ReadFileStrings(day);

            char[] myrps = { 'X', 'Y', 'Z' };
            int myscore = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i][0] == 'A' && input[i][2] == 'X' || input[i][0] == 'B' && input[i][2] == 'Y' || input[i][0] == 'C' && input[i][2] == 'Z')
                    myscore += 3;
                else if (input[i][0] == 'A' && input[i][2] == 'Y' || input[i][0] == 'B' && input[i][2] == 'Z' || input[i][0] == 'C' && input[i][2] == 'X')
                    myscore += 6;


                myscore += Array.IndexOf(myrps, input[i][2]) + 1;
            }

            return myscore;
        }

        public static int AOCD2v2(int day)
        {
            string[] input = ReadFileStrings(day);
            char[] elfrps = { 'A', 'B', 'C' };
            char[] myrps = { 'X', 'Y', 'Z' };
            int myscore = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i][2] == 'X')
                {
                    myscore += input[i][0] == 'A' ? 3 : 0;
                    myscore += input[i][0] == 'B' ? 1 : 0;
                    myscore += input[i][0] == 'C' ? 2 : 0;
                }
                else if (input[i][2] == 'Z')
                {
                    myscore += input[i][0] == 'A' ? 2 : 0;
                    myscore += input[i][0] == 'B' ? 3 : 0;
                    myscore += input[i][0] == 'C' ? 1 : 0;
                }
                else
                    myscore += Array.IndexOf(elfrps, input[i][0]) + 1;

                myscore += Array.IndexOf(myrps, input[i][2]) * 3;
            }

            return myscore;
        }

        public static string AOCD5_1(int day)
        {
            string[] input = ReadFileStrings(day);

            var stacks = new Dictionary<int, Stack<char>>()
            {
                { 1, new Stack<char> ( new char[] { 'F', 'T', 'C', 'L', 'R', 'P', 'G', 'Q' }) },
                { 2, new Stack<char> ( new char[] { 'N', 'Q', 'H', 'W', 'R', 'F', 'S', 'J' }) },
                { 3, new Stack<char> ( new char[] { 'F', 'B', 'H', 'W', 'P', 'M', 'Q' }) },
                { 4, new Stack<char> ( new char[] { 'V', 'S', 'T', 'D', 'F' }) },
                { 5, new Stack<char> ( new char[] { 'Q', 'L', 'D', 'W', 'V', 'F', 'Z' }) },
                { 6, new Stack<char> ( new char[] { 'Z', 'C', 'L', 'S' }) },
                { 7, new Stack<char> ( new char[] { 'Z', 'B', 'M', 'V', 'D', 'F' }) },
                { 8, new Stack<char> ( new char[] { 'T', 'J', 'B' }) },
                { 9, new Stack<char> ( new char[] { 'Q', 'N', 'B', 'G', 'L', 'S', 'P', 'H' }) }
            };

            for (int i = 10; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                    break;

                //process instruction to int array
                string[] strings = Regex.Split(input[i], @"\D+");
                int[] numbers = new int[3];
                for (int j = 0; j < numbers.Length; j++)
                    numbers[j] = int.Parse(strings[j + 1]);

                //execute instruction
                for (int j = 0; j < numbers[0]; j++)
                    stacks[numbers[2]].Push(stacks[numbers[1]].Pop());

            }

            //generate result
            string result = "";

            for (int i = 1; i < 10; i++)
                result += stacks[i].Peek();

            return result;
        }

        public static string AOCD5_2(int day)
        {
            string[] input = ReadFileStrings(day);

            var stacks = new Dictionary<int, Stack<char>>()
            {
                { 1, new Stack<char> ( new char[] { 'F', 'T', 'C', 'L', 'R', 'P', 'G', 'Q' }) },
                { 2, new Stack<char> ( new char[] { 'N', 'Q', 'H', 'W', 'R', 'F', 'S', 'J' }) },
                { 3, new Stack<char> ( new char[] { 'F', 'B', 'H', 'W', 'P', 'M', 'Q' }) },
                { 4, new Stack<char> ( new char[] { 'V', 'S', 'T', 'D', 'F' }) },
                { 5, new Stack<char> ( new char[] { 'Q', 'L', 'D', 'W', 'V', 'F', 'Z' }) },
                { 6, new Stack<char> ( new char[] { 'Z', 'C', 'L', 'S' }) },
                { 7, new Stack<char> ( new char[] { 'Z', 'B', 'M', 'V', 'D', 'F' }) },
                { 8, new Stack<char> ( new char[] { 'T', 'J', 'B' }) },
                { 9, new Stack<char> ( new char[] { 'Q', 'N', 'B', 'G', 'L', 'S', 'P', 'H' }) }
            };

            for (int i = 10; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                    break;

                //process instruction to int array
                string[] strings = Regex.Split(input[i], @"\D+");
                int[] numbers = new int[3];
                for (int j = 0; j < numbers.Length; j++)
                    numbers[j] = int.Parse(strings[j + 1]);

                //execute instruction

                var crane = new Stack<char>();

                for (int j = 0; j < numbers[0]; j++)
                    crane.Push(stacks[numbers[1]].Pop());

                for (int j = 0; j < numbers[0]; j++)
                    stacks[numbers[2]].Push(crane.Pop());

            }

            //generate result
            string result = "";

            for (int i = 1; i < 10; i++)
                result += stacks[i].Peek();

            return result;
        }

        public static string[] ReadFileStrings(int day)
        {
            string text = System.IO.File.ReadAllText(@"C:\\aoc\\" + day + ".txt");
            string[] textsplit = text.Split("\n");
            Array.Resize(ref textsplit, textsplit.Length - 1);
            return textsplit;
        }

        public static int[] ReadFileNumbers(int day)
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\jpulles\Documents\AOC\" + day + ".txt");
            string[] textsplit = text.Split("\n");
            Array.Resize(ref textsplit, textsplit.Length - 1);
            return Array.ConvertAll(textsplit, s => int.Parse(s));
        }
    }

}


