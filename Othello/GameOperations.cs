using System;
using System.Collections.Generic;
using System.Text;

namespace Othello
{
    class GameOperations
    {
        private GameState m_CurrentGameState;

        public GameOperations(GameState i_GameState)
        {
            m_CurrentGameState = i_GameState;
        }

        public void UpdateGame(sMatrixCoordinate move)
        {
            m_CurrentGameState.Board[move.x, move.y] = (eBoardCell) m_CurrentGameState.CurrentPlayer.Color;

            //check the cell in every direction (8 directions). If adjacent coin is of the other color, send to checkIfCouldFlipInDirection with that direction
        }

        public void CalcScore()
        {
            m_CurrentGameState.FirstPlayer.Score = 0;
            m_CurrentGameState.SecondPlayer.Score = 0;

            foreach (eBoardCell cell in m_CurrentGameState.Board)
            {
                switch (cell)
                {
                    case eBoardCell.Black:
                        m_CurrentGameState.FirstPlayer.Score++;
                        break;
                    case eBoardCell.White:
                        m_CurrentGameState.SecondPlayer.Score++;
                        break;
                }
            }
        }

        public void Flip(sMatrixCoordinate i_CurrentCoordinate, sMatrixCoordinate i_Direction)
        {
            //flip this cell - update both players cell lists
            //check if next cell is of same color - if yes, send recursively. if not, we flipped all in this direction and can stop
        }

        public class CoinFlipper
        {
            public sMatrixCoordinate CheckIfCouldFlipInDirection(sMatrixCoordinate currentCoordinate, sMatrixCoordinate direction, eBoardCell cellType)
            {
                //if adjacent cell in given direction is a coin of cellType - return MatrixCoordinate of that cell
                //if adjacent cell is the third kind of cell (not same as me, not same as cellType) or out of board bounds - return null
                //if adjacent cell is same color - send recursively with updated currentCoordinate (of the adjacent cell) and same direction

                return new sMatrixCoordinate(0, 0);
            } 

        }
    }
}
