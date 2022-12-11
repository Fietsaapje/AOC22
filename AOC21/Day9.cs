using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AOC21
{
    class Day9
    {
        public static void GetAnswers(string[] input)
        {
            //Parse instructions
            (char, int)[] Instructions = new (char, int)[input.Length];

            for (int i = 0; i < input.Length; i++)
                Instructions[i] = (input[i].Split()[0][0], Int32.Parse(input[i].Split()[1]));

            //Create map, rope and mark off starting position
            int gridsize = 300;
            int startpos = 150;

            bool[,] map = new bool[gridsize, gridsize];
            (int, int) newheadposition = (startpos, startpos);
            (int, int) prevheadposition = (startpos, startpos);
            (int, int) tailposition = (startpos, startpos);
            map[tailposition.Item1, tailposition.Item2] = true;

            //Perform instruction
            for (int i = 0; i < Instructions.Length; i++)
            {
                for (int j = 0; j < Instructions[i].Item2; j++)
                {
                    prevheadposition = newheadposition;

                    switch (Instructions[i].Item1)
                        {
                            case 'U':
                                newheadposition.Item2++;
                                if (newheadposition.Item2 - tailposition.Item2 > 1)
                                    tailposition = prevheadposition;
                                break;
                            case 'R':
                                newheadposition.Item1++;
                                if (newheadposition.Item1 - tailposition.Item1 > 1)
                                    tailposition = prevheadposition;
                                break;
                            case 'D':
                                newheadposition.Item2--;
                                if (tailposition.Item2 - newheadposition.Item2 > 1)
                                    tailposition = prevheadposition;
                                break;
                            case 'L':
                                newheadposition.Item1--;
                                if (tailposition.Item1 - newheadposition.Item1 > 1)
                                    tailposition = prevheadposition;
                                break;
                            default:
                                //unknown instruction
                                break;
                        }

                    map[tailposition.Item1, tailposition.Item2] = true;
                }

            }

            int locations = 0;
            foreach(var item in map)
            {
                if(item == true)
                    locations++;
            }

            Console.WriteLine(locations);
        }

        public static void GetAnswers2(string[] input)
        {
            //Parse instructions
            (char, int)[] Instructions = new (char, int)[input.Length];

            for (int i = 0; i < input.Length; i++)
                Instructions[i] = (input[i].Split()[0][0], Int32.Parse(input[i].Split()[1]));

            //Create map, rope and mark off starting position
            int gridsize = 300;
            int startposition = 150;
            int ropesize = 10;

            bool[,] map = new bool[gridsize, gridsize];

            (int, int)[] rope = new (int, int)[ropesize];
            for (int i = 0; i < rope.Length; i++)
                rope[i].Item1 = rope[i].Item2 = startposition;

            //Perform instruction
            for (int i = 0; i < Instructions.Length; i++)
            {
                for (int j = 0; j < Instructions[i].Item2; j++)
                {
                    for (int k = 0; k < rope.Length; k++)
                    {
                        if (k == 0)
                            switch (Instructions[i].Item1)
                            {
                                case 'U':
                                    rope[k].Item2++;
                                    break;
                                case 'R':
                                    rope[k].Item1++;
                                    break;
                                case 'D':
                                    rope[k].Item2--;
                                    break;
                                case 'L':
                                    rope[k].Item1--;
                                    break;
                                default:
                                    //unknown instruction
                                    break;
                            }
                        else
                        {
                            int difx = rope[k - 1].Item1 - rope[k].Item1;
                            int dify = rope[k - 1].Item2 - rope[k].Item2;

                            if (difx > 1 || difx < -1)
                            {
                                if (difx > 1)
                                    rope[k].Item1++;
                                else if (difx < -1)
                                    rope[k].Item1--;

                                if (dify > 0)
                                    rope[k].Item2++;
                                else if (dify < 0)
                                    rope[k].Item2--;
                            }

                            difx = rope[k - 1].Item1 - rope[k].Item1;
                            dify = rope[k - 1].Item2 - rope[k].Item2;

                            if (dify > 1 || dify < -1)
                            {
                                if (dify > 1)
                                    rope[k].Item2++;
                                else if (dify < -1)
                                    rope[k].Item2--;

                                if (difx > 0)
                                    rope[k].Item1++;
                                else if (difx < 0)
                                    rope[k].Item1--;
                            }
                        }
                    }

                    //mark off tail on map
                    map[rope[rope.Length - 1].Item1, rope[rope.Length - 1].Item2] = true;

                }

            }

            //Count all locations
            int locations = 0;
            foreach (var item in map)
            {
                if (item == true)
                    locations++;
            }

            Console.WriteLine(locations);
        }
    }
}
