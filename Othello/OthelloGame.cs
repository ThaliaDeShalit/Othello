using System;
using System.Collections.Generic;
using System.Text;

namespace Othello
{
    class OthelloGame
    {
        public static void RunGame()
        {
            string firstPlayerName;
            string secondPlayerName;
            bool isGameAgainstComputer;
            int boardSize;
            sMatrixCoordinate move;
            GameState currGameState;
            GameOperations gameOperator;
            UI userInterface;
            bool exitGame = false;

            firstPlayerName = UI.GetName();
            boardSize = UI.GetBoardSize();
            isGameAgainstComputer = UI.PlayAgainstComputer(out secondPlayerName);

            currGameState = new GameState(firstPlayerName, secondPlayerName, boardSize, isGameAgainstComputer);
            gameOperator = new GameOperations(currGameState);
            userInterface = new UI(boardSize);

            while (true)
            {
                gameOperator.CalcValidMoves(currGameState.FirstPlayer);
                gameOperator.CalcValidMoves(currGameState.SecondPlayer);
                
                userInterface.DrawBoard(currGameState);                

                if (currGameState.GameOver())
                {
                    gameOperator.CalcScore();
                    userInterface.EndGame(currGameState, out exitGame);
                }
                else if (!currGameState.CurrentPlayer.HasValidMoves())
                {
                    
                    // TODO: add output to player
                    currGameState.NextTurn();
                    continue;
                }
                else
                {
                    move = userInterface.GetNextMove(currGameState, out exitGame);
                    gameOperator.UpdateGame(move);
                }

                if (exitGame)
                {
                    break;
                }
            }

            userInterface.QuitGame();
        }
    }
}
