using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC21
{
    public class Day8
    {
        static int gridsize;
        static int[][] rows;
        static int[][] columns;

        public static void GetAnswers(string[] input)
        {
            GetRowsColumns(input);
            GetAnswer1();
            GetAnswer2();
        }

        public static void GetAnswer1()
        {
            int trees = 0;

            for (int i = 0; i < gridsize; i++)
            {

                for (int j = 0; j < gridsize; j++)
                {
     

                    bool treeOK = false;

                    int treeheight = rows[i][j];

                    if (j == 0 || j == gridsize || i == 0 || i == gridsize)
                        treeOK = true;
                    else
                    {
                        var test = rows[i].Skip(0).Take(j).ToArray();

                        if (rows[i].Skip(0).Take(j).ToArray().All(t => t < treeheight))
                            treeOK = true;

                        test = rows[i].Skip(j+1).Take(gridsize - j - 1).ToArray();

                        if (rows[i].Skip(j+1).Take(gridsize - j - 1).ToArray().All(t => t < treeheight))
                            treeOK = true;

                        test = columns[j].Skip(0).Take(i).ToArray();

                        if (columns[j].Skip(0).Take(i).ToArray().All(t => t < treeheight))
                            treeOK = true;

                        test = columns[j].Skip(i+1).Take(gridsize - i - 1).ToArray();

                        if (columns[j].Skip(i+1).Take(gridsize - i - 1).ToArray().All(t => t < treeheight))
                            treeOK = true;
                    }

                    if (treeOK)
                        trees++;
                }
            }

            Console.WriteLine($"Suitable trees: {trees}");

        }

        public static void GetAnswer2()
        {
            int bestscore = 0;

            for (int i = 0; i < gridsize; i++)
            {

                for (int j = 0; j < gridsize; j++)
                {

                    int treeheight = rows[i][j];
                    int score = 0;
                    int totalscore = 1;


                    if (j != 0)
                    {
                        var treeswest = rows[i].Skip(0).Take(j).ToArray().Reverse().ToArray();
                        for (int k = 0; k < treeswest.Length; k++)
                        {
                            if (treeswest[k] < treeheight)
                                score++;
                            else
                            {
                                score++;
                                break;
                            }

                        }
                        totalscore = totalscore * score;
                    }

                    score = 0;
                    if (j != gridsize)
                    {
                        var treeseast = rows[i].Skip(j + 1).Take(gridsize - j - 1).ToArray();
                        for (int k = 0; k < treeseast.Length; k++)
                        {
                            if (treeseast[k] < treeheight)
                                score++;
                            else
                            {
                                score++;
                                break;
                            }
                        }
                        totalscore = totalscore * score;
                    }

                    score = 0;
                    if (i != 0)
                    {
                        var treesnorth = columns[j].Skip(0).Take(i).ToArray().Reverse().ToArray();
                        for (int k = 0; k < treesnorth.Length; k++)
                        {
                            if (treesnorth[k] < treeheight)
                                score++;
                            else
                            {
                                score++;
                                break;
                            }
                        }
                        totalscore = totalscore * score;
                    }

                    score = 0;
                    if (i != gridsize)
                    {
                        var treessouth = columns[j].Skip(i + 1).Take(gridsize - i - 1).ToArray();
                        for (int k = 0; k < treessouth.Length; k++)
                        {
                            if (treessouth[k] < treeheight)
                                score++;
                            else
                            {
                                score++;
                                break;
                            }
                        }
                        totalscore = totalscore * score;
                    }

                    if (totalscore > bestscore)
                        bestscore = totalscore;

                }
            }

            Console.WriteLine($"Best scenic score: {bestscore}");
        }

        public static void GetRowsColumns(string[] input)
        {
            gridsize = input.Length;
            rows = new int[gridsize][];
            columns = new int[gridsize][];

            for (int j = 0; j < gridsize; j++)
            {
                int[] row = new int[gridsize];
                int[] column = new int[gridsize];

                for (int i = 0; i < gridsize; i++)
                {
                    row[i] = (int)(input[j][i] - '0');
                    column[i] = (int)(input[i][j] - '0');
                }

                rows[j] = row;
                columns[j] = column;

            }
        }
    }
}
