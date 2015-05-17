using System;
using System.Collections.Generic;
using System.Text;

namespace Othello
{
    class GameOperations
    {
        private GameState m_CurrentGameState;

        private sMatrixCoordinate m_UpLeft = new sMatrixCoordinate(-1, -1);
        private sMatrixCoordinate m_Up = new sMatrixCoordinate(0, -1);
        private sMatrixCoordinate m_UpRight = new sMatrixCoordinate(1, -1);
        private sMatrixCoordinate m_Left = new sMatrixCoordinate(-1, 0);
        private sMatrixCoordinate m_Right = new sMatrixCoordinate(1, 0);
        private sMatrixCoordinate m_DownLeft = new sMatrixCoordinate(-1, 1);
        private sMatrixCoordinate m_Down = new sMatrixCoordinate(0, 1);
        private sMatrixCoordinate m_DownRight = new sMatrixCoordinate(1, 1);

        private sMatrixCoordinate[] m_Directions;

        public GameOperations(GameState i_GameState)
        {
            m_CurrentGameState = i_GameState;

            m_Directions = new sMatrixCoordinate[8] { m_UpLeft, m_Up, m_UpRight, m_Left, m_Right, m_DownLeft, m_Down, m_DownRight };
        }

        public void UpdateGame(sMatrixCoordinate i_Move)
        {
            m_CurrentGameState.Board[i_Move.x, i_Move.y] = (eBoardCell) m_CurrentGameState.CurrentPlayer.Color;
            m_CurrentGameState.CurrentPlayer.CellsOccupied.Add(i_Move);

            //check the cell in every direction (8 directions). If adjacent coin is of the other color, send to checkIfCouldFlipInDirection with that direction
            foreach (sMatrixCoordinate direction in m_Directions)
            {
                sMatrixCoordinate newCoordinate = i_Move + direction;

                int boardSize = m_CurrentGameState.BoardSize - 1;
                if(newCoordinate.x < 0 || newCoordinate.x > boardSize || newCoordinate.y < 0 || newCoordinate.y > boardSize ) {
                    continue;
                }

                eBoardCell adjacentCell = m_CurrentGameState.Board[newCoordinate.x, newCoordinate.y];

                if (!adjacentCell.Equals(eBoardCell.Empty) && !adjacentCell.Equals((eBoardCell) m_CurrentGameState.CurrentPlayer.Color))
                {
                    sMatrixCoordinate? tempCoord = CheckIfCouldFlipInDirection(newCoordinate, direction, (eBoardCell) m_CurrentGameState.CurrentPlayer.Color);

                    if (tempCoord != null)
                    {
                        Flip(newCoordinate, direction, adjacentCell);
                    }
                }
            }

            m_CurrentGameState.LastMovePlayed = i_Move;
            m_CurrentGameState.NextTurn();
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

        public void Flip(sMatrixCoordinate i_CurrentCoordinate, sMatrixCoordinate i_Direction, eBoardCell i_CellType)
        {
            eBoardCell currentBoardCell = m_CurrentGameState.Board[i_CurrentCoordinate.x, i_CurrentCoordinate.y];
            sMatrixCoordinate newCoordinate = i_CurrentCoordinate + i_Direction;
            eBoardCell adjacentCell = m_CurrentGameState.Board[newCoordinate.x, newCoordinate.y];
            
            //check if next cell is of same color - if yes, send recursively. if not, we flipped all in this direction and can stop
            if (adjacentCell.Equals(currentBoardCell))
            {
                Flip(newCoordinate, i_Direction, adjacentCell);
            }

            //flip this cell - update both players cell lists
            if (currentBoardCell.Equals(eBoardCell.Black))
            {
                m_CurrentGameState.Board[i_CurrentCoordinate.x, i_CurrentCoordinate.y] = eBoardCell.White;
            }
            else
            {
                m_CurrentGameState.Board[i_CurrentCoordinate.x, i_CurrentCoordinate.y] = eBoardCell.Black;
            }

            if (m_CurrentGameState.CurrentPlayer.Equals(m_CurrentGameState.FirstPlayer))
            {
                m_CurrentGameState.SecondPlayer.CellsOccupied.Remove(i_CurrentCoordinate);
            }
            else
            {
                m_CurrentGameState.FirstPlayer.CellsOccupied.Remove(i_CurrentCoordinate);
            }
            m_CurrentGameState.CurrentPlayer.CellsOccupied.Add(i_CurrentCoordinate);
        }

        public void CalcValidMoves(Player i_Player)
        {
            i_Player.ValidMoves.Clear();
            
            sMatrixCoordinate? matrixCoord = null;
            
            foreach (sMatrixCoordinate cellOccupied in i_Player.CellsOccupied)
            {
                eBoardCell currentBoardCell = m_CurrentGameState.Board[cellOccupied.x, cellOccupied.y];
                
                foreach (sMatrixCoordinate direction in m_Directions)
                {
                    sMatrixCoordinate newCoordinate = cellOccupied + direction;

                    int boardSize = m_CurrentGameState.BoardSize - 1;
                    if (newCoordinate.x < 0 || newCoordinate.x > boardSize || newCoordinate.y < 0 || newCoordinate.y > boardSize)
                    {
                        continue;
                    }

                    eBoardCell adjacentCell = m_CurrentGameState.Board[newCoordinate.x, newCoordinate.y];


                    if (!currentBoardCell.Equals(adjacentCell) && !adjacentCell.Equals(eBoardCell.Empty))
                    {
                        matrixCoord = CheckIfCouldFlipInDirection(newCoordinate, direction, eBoardCell.Empty);
                    }

                    if (matrixCoord != null)
                    {
                        if (!i_Player.ValidMoves.Contains((sMatrixCoordinate) matrixCoord))
                        {
                            i_Player.ValidMoves.Add((sMatrixCoordinate) matrixCoord);
                        }
                    }
                }
            }
            
        }

        public sMatrixCoordinate? CheckIfCouldFlipInDirection(sMatrixCoordinate i_currentCoordinate, sMatrixCoordinate i_direction, eBoardCell i_CellType)
        {
            sMatrixCoordinate? returnedCoordinate = null;
            sMatrixCoordinate newCoordinate = i_currentCoordinate + i_direction;

            int boardSize = m_CurrentGameState.BoardSize - 1;
            if (!(newCoordinate.x < 0 || newCoordinate.x > boardSize || newCoordinate.y < 0 || newCoordinate.y > boardSize))
            {
                eBoardCell adjacentCell = m_CurrentGameState.Board[newCoordinate.x, newCoordinate.y];

                //if adjacent cell in given direction is a coin of cellType - return MatrixCoordinate of that cell
                if (adjacentCell.Equals(i_CellType))
                {
                    returnedCoordinate = newCoordinate;
                }
                //if adjacent cell is the third kind of cell (not same as me, not same as cellType) or out of board bounds - return null
                else if (!adjacentCell.Equals(m_CurrentGameState.Board[i_currentCoordinate.x, i_currentCoordinate.y]))
                {
                    returnedCoordinate = null;
                }
                //if adjacent cell is same color - send recursively with updated currentCoordinate (of the adjacent cell) and same direction
                else
                {
                    returnedCoordinate = CheckIfCouldFlipInDirection(newCoordinate, i_direction, i_CellType);
                }   
            }

            return returnedCoordinate;
        } 
    }
}
