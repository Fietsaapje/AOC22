using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC21
{
    class Day10
    {
        public static void GetAnswer1(string[] input)
        {
            int registerval = 1;
            int[] measurecycles = new int[] { 20, 60, 100, 140, 180, 220 };
            int actualcycles = 0;
            int cycle = 0;
            int signalstrengthsum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                actualcycles++;
                cycle = measurecycles.FirstOrDefault(x => x == actualcycles);
                if (cycle != 0)
                    signalstrengthsum += actualcycles * registerval;

                if (input[i][0] == 'a') 
                {
                    int instruction = Int32.Parse(input[i].Split()[1]);
                    actualcycles++;
                    cycle = measurecycles.FirstOrDefault(x => x == actualcycles);
                    if (cycle != 0)
                        signalstrengthsum += actualcycles * registerval;
                    registerval += instruction;
                }



            }
            Console.WriteLine(signalstrengthsum);
        }

        public static void GetAnswer2(string[] input)
        {
            int registerval = 1;

            int crtwidth = 40;
            int crtheight = 6;

            bool[] screen = new bool[crtwidth*crtheight];

            int actualcycle = 0;

            for (int i = 0; i < input.Length; i++)
            {
                actualcycle++;
                if (actualcycle % crtwidth == 0)
                    registerval += crtwidth;
                if (registerval - 1 == actualcycle - 1 || registerval == actualcycle - 1 || registerval + 1 == actualcycle - 1)
                    screen[actualcycle - 1] = true;

                if (input[i][0] == 'a')
                {
                    actualcycle++;
                    if (actualcycle % crtwidth == 0)
                        registerval += crtwidth;

                    if (registerval - 1 == actualcycle - 1 || registerval == actualcycle - 1 || registerval + 1 == actualcycle - 1)
                        screen[actualcycle - 1] = true;

                    int instruction = Int32.Parse(input[i].Split()[1]);
                    registerval += instruction;
                }
            }

            //draw screen
            for (int i = 1; i < crtwidth*crtheight; i++)
            {
                if (i % crtwidth == 0)
                {
                    Console.WriteLine();
                    Console.Write(screen[i-1] == true ? "#" : ".");
                }
                else
                    Console.Write(screen[i-1] == true ? "#" : ".");
            }
        }
    }
}