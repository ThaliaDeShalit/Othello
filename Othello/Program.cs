using System;
using System.Collections.Generic;
using System.Text;
using Ex02.ConsoleUtils;

namespace Othello
{
    class Program
    {
        public static void Main()
        {
            string firstPlayerName;
            string secondPlayerName;
            bool isGameAgainstComputer;
            int boardSize;
            sMatrixCoordinate move;
            GameState currGameState;
            GameOperations gameOperator;
            Board currBoardState;
            bool exitGame = false;

            firstPlayerName = getName();
            boardSize = getBoardSize();
            isGameAgainstComputer = playAgainstComputer(out secondPlayerName);

            currGameState = new GameState(firstPlayerName, secondPlayerName, boardSize, isGameAgainstComputer);
            gameOperator = new GameOperations(currGameState);
            currBoardState = new Board(boardSize);

            while (true)
            {
                gameOperator.CalcValidMoves(currGameState.FirstPlayer);
                gameOperator.CalcValidMoves(currGameState.SecondPlayer);
                
                Screen.Clear();
                currBoardState.DrawBoard(currGameState.Board);

                if (currGameState.GameOver())
                {
                    endGame(currGameState, out exitGame);
                }
                else if (!currGameState.CurrentPlayer.HasValidMoves())
                {
                    currGameState.NextTurn();
                    continue;
                }
                else
                {
                   
                    
                    move = getNextMove(currGameState, out exitGame);

                    
                    gameOperator.UpdateGame(move);
                }

                if (exitGame)
                {
                    break;
                }
            }

            Console.WriteLine("Thanks for playing!");
        }

        private static string getName()
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

        private static int getBoardSize()
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

        private static bool playAgainstComputer(out string o_NameOfSecondPlayer)
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
                    o_NameOfSecondPlayer = getName();
                    inputIsValid = true;
                }
                else
                {
                    Console.WriteLine("A valid input is either c for computer or p for player, please enter a valid input:");
                }
            }

            return playAgainstComputer;
        }

        private static void endGame(GameState currGameState, out bool o_WantsToQuitGame)
        {
            bool inputIsValid = false;
            string inputFromUser;
            string gameInformation = string.Format(@"{0} is the winner!
{1} has {2} coins on the board
{3} has {4} coins on the board", currGameState.GetLeader().Name, currGameState.FirstPlayer.Name, currGameState.FirstPlayer.CellsOccupied.Count, 
                               currGameState.SecondPlayer.Name, currGameState.SecondPlayer.CellsOccupied.Count);

            o_WantsToQuitGame = false;

            Console.WriteLine(gameInformation);
            Console.WriteLine("Would you like to play another game? [y/n]");
            while (!inputIsValid)
            {
                inputFromUser = Console.ReadLine();
                if (inputFromUser.Equals("y") || inputFromUser.Equals("Y"))
                {
                    currGameState.Restart();
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

        private static sMatrixCoordinate getNextMove(GameState currGameState, out bool o_WantsToQuitGame)
        {
            sMatrixCoordinate? move = null;
            bool inputIsValid = false;
            string inputFromUser;

            o_WantsToQuitGame = false;

            if (currGameState.CurrentPlayer == currGameState.SecondPlayer && currGameState.IsAgainstComputer)
            {
                move = currGameState.SecondPlayer.MakeMove();
                Console.WriteLine("Computer made his move");
            }
            else
            {
                Console.WriteLine(currGameState.CurrentPlayer.Name + " please enter a move:");
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
                        inputIsValid = sMatrixCoordinate.TryParse(inputFromUser, out move);

                        if (!currGameState.CurrentPlayer.ValidMoves.Contains((sMatrixCoordinate) move))
                        {
                            Console.WriteLine("You can not preform that move, pick a valid move:");
                            inputIsValid = false;
                        }
                    }
                }
            }

            return move?? new sMatrixCoordinate(0, 0);
        }
    }
}
