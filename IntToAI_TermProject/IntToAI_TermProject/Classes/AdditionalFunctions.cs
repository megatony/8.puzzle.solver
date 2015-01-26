using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntToAI_TermProject.Classes
{
    public static class AdditionalFunctions
    {
        public static bool IsSolvable(int[,] array)
        {
            //filll values in one dimensional array
            List<int> values = new List<int>();
            for (int i = 0; i <= array.Rank; i++)
            {
                for (int j = 0; j <= array.Rank; j++)
                {
                    if (array[i,j] != 0)
                    {
                        values.Add(array[i,j]);
                    }
                }
            }

            //count "inversions"
            int inversionsCount = 0;
            for (int valIndex = 0; valIndex < values.Count; valIndex++)
            {
                for (int valSuccIndex = valIndex + 1; valSuccIndex < values.Count; valSuccIndex++)
                {
                    if (values[valIndex] < values[valSuccIndex])
                    {
                        inversionsCount++;
                    }
                }
            }

            
                return inversionsCount % 2 == 0;
        
        }

        public static bool ContentEquals<T>(this T[,] arr, T[,] other) where T : IComparable
        {
            if (arr.GetLength(0) != other.GetLength(0) ||
                arr.GetLength(1) != other.GetLength(1))
                return false;
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j].CompareTo(other[i, j]) != 0)
                        return false;
            return true;
        }

        public static int CalculateManhattan(int[,] given, int[,] goal)
        {
            int toplam = 0;

            for (int i = 0; i <= given.Rank; i++)
            {
                for (int j = 0; j <= goal.Rank; j++)
                {
                    for (int k = 0; k <= given.Rank; k++)
                    {
                        for (int l = 0; l <= goal.Rank; l++)
                        {
                            if (goal[i, j] == given[k, l])
                            {
                                if (given[k, l] != 0)
                                {
                                    int say = Math.Abs(i - k + j - l);
                                    toplam = toplam + say;
                                }
                                
                            }


                        }
                    }
                }
            }
            return toplam;
        }
    }
}
