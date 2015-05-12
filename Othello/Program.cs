﻿using System;
using System.Collections.Generic;
using System.Text;
using Ex02.ConsoleUtils;

namespace Othello
{
    class Program
    {
        public void Main()
        {
            string firstPlayerName;
            string secondPlayerName;
            bool isGameAgainstComputer;
            int boardSize;
            sMatrixCoordinate move;
            GameState currGameState;
            GameOperations gameOperator;
            Board currBoardState;

            firstPlayerName = getName();
            boardSize = getBoardSize();
            isGameAgainstComputer = playAgainstComputer(out secondPlayerName);

            // Initiate GameState according to stats
            //Initate GameOperations
            currBoardState = new Board(boardSize);

            while (true)
            {
                Screen.Clear();
                currBoardState.DrawBoard(currGameState.gameBoard);

                if (currGameState.GameOver)
                {
                    endGame(currGameState);
                }
                else if (!currGameState.HasValidMoves)
                {
                    currGameState.NextTurn();
                    continue;
                }
                else
                {
                    move = getNextMove(currGameState);
                    gameOperator.update(move);
                }
            }


        }

        private string getName()
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

        private int getBoardSize()
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

        private bool playAgainstComputer(out string o_NameOfSecondPlayer) {
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
            }

            return playAgainstComputer;
        }

        private void endGame(GameState currGameState)
        {
            bool inputIsValid = false;
            string inputFromUser;
            string gameInformation = string.Format(@"{0} is the winner!
{1} has {2} coins on the board
{3} has {4} coins on the board", currGameState.GetLeader().Name, currGameState.firstPlayer.Name, currGameState.firstPlayer.cellsThisPlayerOccupies.Count, 
                               currGameState.secondPlayer.Name, currGameState.secondPlayer.cellsThisPlayerOccupies.Count);

            Console.WriteLine(gameInformation);
            Console.WriteLine("Would you like to play another game? [y/n]");
            while (!inputIsValid)
            {
                inputFromUser = Console.ReadLine();
                if (inputFromUser.Equals("y") || inputFromUser.Equals("Y"))
                {
                    currGameState.restart();
                    inputIsValid = true;

                }
                else if (inputFromUser.Equals("n") || inputFromUser.Equals("N"))
                {
                    inputIsValid = true;
                    exitGame();
                }
                else
                {
                    Console.WriteLine("Please enter either y for yes or n for no:");
                }
            }
        }

        private void exitGame()
        {
            Console.WriteLine("Thanks for playing!");
            Environment.Exit(0);
        }

        private sMatrixCoordinate getNextMove(GameState currGameState)
        {
            sMatrixCoordinate? move = null;
            bool inputIsValid = false;
            string inputFromUser;
            
            if (currGameState.currentPlayer == currGameState.secondPlayer && currGameState.isAgainstComputer)
            {
                move = currGameState.secondPlayer.MakeMove();
                Console.WriteLine("Computer made his move");
            }
            else
            {
                Console.WriteLine(currGameState.currentPlayer.Name + " please enter a move:");
                while (!inputIsValid)
                {
                    inputFromUser = Console.ReadLine();
                    if (inputFromUser.Equals("q") || inputFromUser.Equals("Q"))
                    {
                        exitGame();
                    }
                    else
                    {
                        inputIsValid = sMatrixCoordinate.TryParse(inputFromUser, out move);

                        if (!inputIsValid)
                        {
                            continue;
                        } 
                        else if (!currGameState.currentPlayer.ValidMoves.Contains(move))
                        {
                            inputIsValid = false;
                            continue;
                        }
                    }
                }
            }

            return (sMatrixCoordinate) move;
        }

    }
}
