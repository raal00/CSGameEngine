using System;

using System.IO;

namespace GameLib
{
    public static class LevelMatrixIO
    {
        public static void LoadMTRX(this string path, ref byte[,]matr, int w, int h) 
        {
            string line;
            int i = 0;
            using (TextReader tr = new StreamReader(path)) 
            {
                while ((line = tr.ReadLine()) != null) 
                {
                    for (int j = 0; j < line.Length; j++) 
                    {
                        if (line[j] == '1') matr[i, j] = 1; 
                        else matr[i,j] = 0;
                    }
                    i++;
                }
            }
        }


        public static void SaveMTRX(this byte[,] mtrx, int w, int h, string path) 
        {
            using (TextWriter tw = new StreamWriter(path))
            {
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        tw.Write(mtrx[i, j]);
                    }
                    tw.WriteLine();
                }
            }
        }
    }
}
