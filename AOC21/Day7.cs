using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC21
{
    class Day7
    {
        public static string GetAnswer(string[] input)
        {
            int totaldiskspace = 70000000;
            int requiredspace = 30000000;

            Dir active = new Dir("/", null);
            List<Dir> alldirs = new List<Dir>();

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i][0] == '$')
                    if (input[i][2] == 'c')
                        if (input[i][5] == '.')
                            active = active.ParentDir;
                        else
                            active = active.Dirs.First(d => d.Name == input[i].Split(' ')[2]);
                    else if (input[i][2] == 'l')
                    {
                        while (i + 1 < input.Length)
                        {
                            if (input[i + 1][0] == '$')
                                break;

                            i++;
                            if (input[i][0] == 'd')
                                active.AddDir(new Dir(input[i].Split(' ')[1], active));
                            else
                                active.AddSize(Int32.Parse(input[i].Split(' ')[0]));
                        }

                        alldirs.Add(active);
                    }

            }

            int total = 0;
            alldirs.ForEach(d => total += d.Size <= 100000 ? d.Size : 0);

            int spacetoclear = requiredspace - (totaldiskspace - alldirs.FirstOrDefault(d => d.Name == "/").Size);
            int smallestsize = alldirs.Where(d => d.Size >= spacetoclear).Min(d => d.Size);

            return $"The directories < 100000 combine to a size of {total}. The size of the dir to remove is {smallestsize} ";
        }
    }

    class Dir
    {
        public Dir(string name, Dir parent)
        {
            this.Name = name;
            this.ParentDir = parent;
        }

        public string Name;
        public Dir ParentDir;
        public List<Dir> Dirs = new List<Dir>();
        private int size = 0;

        public int Size
        {
            get
            {
                int i = 0;

                foreach (Dir dir in Dirs)
                    i += dir.Size;

                return i + size;
            }
        }
        public void AddDir(Dir dir)
        {
            Dirs.Add(dir);
        }

        public void AddSize(int amount)
        {
            size += amount;
        }
    }
}
