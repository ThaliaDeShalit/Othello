using System;
using System.Collections.Generic;
using System.Text;

namespace Othello
{
    class Board
    {
        private readonly string r_EmptyCell = "|    ";
        private readonly string r_BlackCell = "| X ";
        private readonly string r_WhiteCell = "| O ";
        private readonly string r_LineEnd = "|    ";
        private readonly string r_LineSeperator;
        private readonly string r_FirstLine;

        public Board(int size)
        {
            if (size == 6)
            {
                r_LineSeperator = "=========================";
                r_FirstLine = " A   B   C   D   E   F";
            }
            else
            {
                r_LineSeperator = "=================================";
                r_FirstLine = " A   B   C   D   E   F   G   H";
            }
        }

        public void DrawBoard(eBoardCell[,] currBoardStatus)
        {
            string board;
            int size = currBoardStatus.GetLength(0);
            string[] linesInBoard = new string[size];
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                sb.Append(i + 1);
                sb.Append(" ");
                for (int j = 0; j < size; j++)
                {
                    if (currBoardStatus[i, j] == eBoardCell.Black)
                    {
                        sb.Append(r_BlackCell);
                    }
                    else if (currBoardStatus[i, j] == eBoardCell.White)
                    {
                        sb.Append(r_WhiteCell);
                    }
                    else
                    {
                        sb.Append(r_EmptyCell);
                    }
                }
                sb.Append(r_LineEnd);
                linesInBoard[i] = sb.ToString();
                sb.Length = 0;
            }

            board = string.Format(@"{0}
{1}
{2}
{1}
{3}
{1}
{4}
{1}
{5}
{1}
{6}
{1}
{7}
{1}", r_FirstLine, r_LineSeperator, linesInBoard);

            if (size == 8)
            {
                board = string.Format(@"{0}
{1}
{2}
{3}
{2}", board, linesInBoard[6], r_LineSeperator, linesInBoard[7]);
            }

            Console.WriteLine(board);
        }
    }
}
