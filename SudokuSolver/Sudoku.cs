using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Sudoku
    {
        public int[,] Grid { get; set; }
        public int GridSize { get; set; }

        public LinkedList<int> positions { get; set; }
        public Sudoku()
        {
            this.GridSize = 9;
            int[,] cpy = {
          {7,0,8,0,0,1,0,0,4},
        {0,6,0,7,0,0,0,0,0},
        {0,0,1,0,0,0,0,3,0},
        {0,2,5,1,3,0,0,0,0},
        {0,0,6,8,0,2,5,0,0},
        {0,0,0,0,5,4,1,2,0},
        {0,7,0,0,0,0,4,0,0},
        {0,0,0,0,0,6,0,5,0},
        {1,0,0,9,0,0,3,0,7}
            };
            this.Grid = cpy;
        }

        public LinkedList<int> initPositionsFromGrid()
        {
            Dictionary<int, int> mapPos = new Dictionary<int, int>();
            List<int> ret = new List<int>();
            int _i, _j, count = 0;
            for (int i = 0; i < GridSize * GridSize; i++)
            {
                _i = i / GridSize;
                _j = i % GridSize;
                for (int val = 1; val <= GridSize; val++)
                {
                    if (validForCol(_j, val) && validForRow(_i, val) && validForSquare(_i, _j, val))
                    {
                        count++;
                    }
                }
                mapPos.Add(i, count);
                count = 0;
            }
            ret = mapPos.OrderBy(x => x.Value).Select(x => x.Key).ToList();
            LinkedList<int> oups = new LinkedList<int>(ret);
            return oups;
        }
        public bool validForRow(int i, int val)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (Grid[i, j] == val)
                {
                    return false;
                }
            }
            return true;
        }

        public bool validForCol(int j, int val)
        {
            for (int i = 0; i < GridSize; i++)
            {
                if (Grid[i, j] == val)
                {
                    return false;
                }
            }
            return true;
        }

        // custom rule includes diagonal
        public bool validForDiagLeftToRight(int val)
        {
            for (int i = 0; i < GridSize; i++)
            {
                if (Grid[i, i] == val)
                {
                    return false;
                }
            }
            return true;
        }

        // custom rule includes diagonal
        public bool validForDiagRightToLeft(int val)
        {
            for (int i = 0; i < GridSize; i++)
            {
                if (Grid[i, GridSize - i - 1] == val)
                {
                    return false;
                }
            }
            return true;
        }

        public bool validForSquare(int i, int j, int val)
        {
            int sqLen = (int)Math.Sqrt(GridSize); // always an integer by definition of a square...
            int starti = i - (i % sqLen);
            int startj = j - (j % sqLen);
            for (int _i = starti; _i < starti + sqLen; _i++)
            {
                for (int _j = startj; _j < startj + sqLen; _j++)
                {
                    if (Grid[_i, _j] == val)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsValid(LinkedListNode<int> pos)
        {
            if (pos == null)
            {
                return true;
            }

            int i = pos.Value / GridSize;
            int j = pos.Value % GridSize;

            if (Grid[i, j] != 0)
            {
                return IsValid(pos.Next);
            }

            for (int val = 1; val <= GridSize; val++)
            {
                if (validForCol(j, val) && validForRow(i, val) && validForSquare(i, j, val))
                {
                    Grid[i, j] = val;
                    //backtrack : this below is the "spying ahead" section
                    if (IsValid(pos.Next))
                    {
                        return true;
                    }
                }
            }
            //reset to 0 in case of reject
            Grid[i, j] = 0;
            return false;
        }

        public void printGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    Console.Write(Grid[i, j]);
                }
                Console.WriteLine();
            }
        }

    }
}
