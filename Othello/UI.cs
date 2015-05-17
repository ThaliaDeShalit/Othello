using System;
using System.Collections.Generic;
using System.Text;
using Ex02.ConsoleUtils;

namespace Othello
{
    class UI
    {
        private readonly string r_EmptyCell = "|   ";
        private readonly string r_BlackCell = "| X ";
        private readonly string r_WhiteCell = "| O ";
        private readonly string r_LineEnd = "|";
        private readonly string r_LineSeperator;
        private readonly string r_FirstLine;

        public UI(int size)
        {
            if (size == 6)
            {
                r_LineSeperator = "  =========================";
                r_FirstLine = "    A   B   C   D   E   F";
            }
            else
            {
                r_LineSeperator = "  =================================";
                r_FirstLine = "    A   B   C   D   E   F   G   H";
            }
        }

        public void DrawBoard(GameState i_CurrGameState)
        {
            eBoardCell[,] currBoardStatus = i_CurrGameState.Board;
            string board;
            int size = currBoardStatus.GetLength(0);
            string[] linesInBoard = new string[size];
            string nameOfLastPlayer;
            string lastMove;
            StringBuilder sb = new StringBuilder();

            Screen.Clear();

            for (int i = 0; i < size; i++)
            {
                sb.Append(i + 1);
                sb.Append(" ");
                for (int j = 0; j < size; j++)
                {
                    if (currBoardStatus[j, i] == eBoardCell.Black)
                    {
                        sb.Append(r_BlackCell);
                    }
                    else if (currBoardStatus[j, i] == eBoardCell.White)
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
{1}", r_FirstLine, r_LineSeperator, linesInBoard[0], linesInBoard[1], linesInBoard[2], linesInBoard[3], linesInBoard[4], linesInBoard[5]);

            if (size == 8)
            {
                board = string.Format(@"{0}
{1}
{2}
{3}
{2}", board, linesInBoard[6], r_LineSeperator, linesInBoard[7]);
            }



            if (i_CurrGameState.CurrentPlayer == i_CurrGameState.FirstPlayer)
            {
                nameOfLastPlayer = i_CurrGameState.SecondPlayer.Name;
            }
            else
            {
                nameOfLastPlayer = i_CurrGameState.FirstPlayer.Name;
            }

            Console.WriteLine(board);

            if (i_CurrGameState.LastMovePlayed != null)
            {
                lastMove = matrixCoordinateToString((sMatrixCoordinate)i_CurrGameState.LastMovePlayed);

                Console.WriteLine("{0} played {1}{2}", nameOfLastPlayer, lastMove, Environment.NewLine);
            }
        }

        private string matrixCoordinateToString(sMatrixCoordinate i_Move)
        {
            return ((char)(i_Move.x + 65)).ToString() + (i_Move.y + 1).ToString();
        }

        public static string GetName()
        {
            string nameOfPlayer = String.Empty;
            bool isValidName = false;

            Console.WriteLine("Please enter a name:");
            while (!isValidName)
            {
                nameOfPlayer = Console.ReadLine();
                if (nameOfPlayer.Length > 0)
                {
                    isValidName = true;
                }
                else
                {
                    Console.WriteLine("A valid name consists of at least one character. Please enter a valid name:");
                }
            }

            return nameOfPlayer;
        }

        public static int GetBoardSize()
        {
            string inputFromUserForBoardSize;
            int sizeOfBoard = 0;
            bool sizeIsValid = false;

            Console.WriteLine("Please enter a size for the board of the game (either 6 or 8):");
            while (!sizeIsValid)
            {
                inputFromUserForBoardSize = Console.ReadLine();
                sizeIsValid = int.TryParse(inputFromUserForBoardSize, out sizeOfBoard);
                if (sizeIsValid)
                {
                    if (sizeOfBoard != 6 && sizeOfBoard != 8)
                    {
                        Console.WriteLine("A valid size is either 6 or 8. Please enter a valid size:");
                        sizeIsValid = false;
                    }
                }
            }

            return sizeOfBoard;
        }

        public static bool PlayAgainstComputer(out string o_NameOfSecondPlayer)
        {
            string inputFromUser;
            bool playAgainstComputer = false;
            bool inputIsValid = false;

            o_NameOfSecondPlayer = string.Empty;

            Console.WriteLine("Would you like to play against the computer or against a second player? [c/p]");
            while (!inputIsValid)
            {
                inputFromUser = Console.ReadLine();
                if (inputFromUser.Equals("c") || inputFromUser.Equals("C"))
                {
                    o_NameOfSecondPlayer = "Computer";
                    playAgainstComputer = true;
                    inputIsValid = true;
                }
                else if (inputFromUser.Equals("p") || inputFromUser.Equals("P"))
                {
                    o_NameOfSecondPlayer = GetName();
                    inputIsValid = true;
                }
                else
                {
                    Console.WriteLine("A valid input is either c for computer or p for player, please enter a valid input:");
                }
            }

            return playAgainstComputer;
        }

        public void EndGame(GameState i_CurrGameState, out bool o_WantsToQuitGame)
        {
            bool inputIsValid = false;
            string inputFromUser;
            string gameInformation = string.Format(@"{0} is the winner!
{1} has {2} coins on the board
{3} has {4} coins on the board", i_CurrGameState.GetLeader().Name, i_CurrGameState.FirstPlayer.Name, i_CurrGameState.FirstPlayer.CellsOccupied.Count,
                               i_CurrGameState.SecondPlayer.Name, i_CurrGameState.SecondPlayer.CellsOccupied.Count);

            o_WantsToQuitGame = false;

            Console.WriteLine(gameInformation);
            Console.WriteLine("Would you like to play another game? [y/n]");
            while (!inputIsValid)
            {
                inputFromUser = Console.ReadLine();
                if (inputFromUser.Equals("y") || inputFromUser.Equals("Y"))
                {
                    i_CurrGameState.Restart();
                    inputIsValid = true;

                }
                else if (inputFromUser.Equals("n") || inputFromUser.Equals("N"))
                {
                    inputIsValid = true;
                    o_WantsToQuitGame = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter either y for yes or n for no:");
                }
            }
        }

        public sMatrixCoordinate GetNextMove(GameState i_CurrGameState, out bool o_WantsToQuitGame)
        {
            sMatrixCoordinate? move = null;
            bool inputIsValid = false;
            string inputFromUser;

            o_WantsToQuitGame = false;

            if (i_CurrGameState.CurrentPlayer == i_CurrGameState.SecondPlayer && i_CurrGameState.IsAgainstComputer)
            {
                move = i_CurrGameState.SecondPlayer.MakeMove();
            }
            else
            {
                Console.WriteLine(i_CurrGameState.CurrentPlayer.Name + " please enter a move:");
                while (!inputIsValid)
                {
                    inputFromUser = Console.ReadLine();
                    if (inputFromUser.Equals("q") || inputFromUser.Equals("Q"))
                    {
                        o_WantsToQuitGame = true;
                        break;
                    }
                    else
                    {
                        inputIsValid = checkValidityOfMoveInput(inputFromUser, i_CurrGameState.BoardSize, out move);

                        if (!inputIsValid)
                        {
                            Console.WriteLine("Invalid input. Please enter the coordinate of a cell on the board (A letter and then a digit) within range:");
                            continue;
                        }

                        if (!i_CurrGameState.CurrentPlayer.ValidMoves.Contains((sMatrixCoordinate)move))
                        {
                            Console.WriteLine("You can not preform that move, pick a valid move:");
                            inputIsValid = false;
                        }
                    }
                }
            }

            return move ?? new sMatrixCoordinate(0, 0);
        }

        private bool checkValidityOfMoveInput(string i_StringToParse, int i_BoardSize, out sMatrixCoordinate? o_ResultOfParsing)
        {
            bool isValid = true;
            int? newX = null;
            int? newY = null;
            string inputStringAsLower;
            int tempValueToEnsureValidity;


            if (i_StringToParse.Length != 2)
            {
                isValid = false;
            }
            else if (!char.IsLetter(i_StringToParse[0]) || !char.IsDigit(i_StringToParse[1]))
            {
                isValid = false;
            }
            else
            {
                inputStringAsLower = i_StringToParse.ToLower();
                tempValueToEnsureValidity = inputStringAsLower[0] - 97;

                // check validity of x
                if (tempValueToEnsureValidity < 0 || tempValueToEnsureValidity >= i_BoardSize)
                {
                    isValid = false;
                }
                else
                {
                    newX = tempValueToEnsureValidity;
                }


                // check validity of y
                tempValueToEnsureValidity = int.Parse(i_StringToParse[1].ToString()) - 1;

                if (tempValueToEnsureValidity < 0 || tempValueToEnsureValidity >= i_BoardSize)
                {
                    isValid = false;
                }
                else
                {
                    newY = tempValueToEnsureValidity;
                }
            }

            //if the parsing was succesfull, it'll create a new coordinate with the relevant x and y.
            //if not, it'll create a default coordiante of (0,0)
            //the default setting is inspired by the default 0 in the int.TryParse method
            o_ResultOfParsing = new sMatrixCoordinate(newX ?? 0, newY ?? 0);

            return isValid;
        }

        public void QuitGame()
        {
            Console.WriteLine("Thanks for playing! press any key to exit");
            Console.ReadLine();
        }
    }
}
