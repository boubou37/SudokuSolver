﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku s = new Sudoku();
            s.IsValid(0);
            s.printGrid();
        }
    }
}
