using System;
using System.Collections.Generic;
using System.Text;

namespace Othello
{
    class GameState
    {
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Player m_CurrentPlayer;
        private bool m_GameAgainstComputer;
        private eBoardCell[,] m_GameBoard;

        public GameState(string i_FirstPlayerName, string i_SecondPlayerName, int i_BoardSize, bool i_AginstComputer)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName, eColor.Black, new sMatrixCoordinate((i_BoardSize / 2) - 1, (i_BoardSize / 2)), new sMatrixCoordinate((i_BoardSize / 2), (i_BoardSize / 2) - 1));
            m_SecondPlayer = new Player(i_SecondPlayerName, eColor.White, new sMatrixCoordinate((i_BoardSize / 2) - 1, (i_BoardSize / 2) - 1), new sMatrixCoordinate((i_BoardSize / 2), (i_BoardSize / 2)));
            m_CurrentPlayer = m_FirstPlayer;

            m_GameAgainstComputer = i_AginstComputer;

            m_GameBoard = instantiateBoard(i_BoardSize);  
        }

        public eBoardCell[,] Board
        {
            get
            {
                return m_GameBoard;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }
        }

        public bool IsAgainstComputer
        {
            get
            {
                return m_GameAgainstComputer;
            }
        }

        private eBoardCell[,] instantiateBoard(int i_BoardSize)
        {
            eBoardCell[,] newBoard = new eBoardCell[i_BoardSize, i_BoardSize];

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if ((i == (i_BoardSize / 2) - 1 || i == i_BoardSize / 2) && i == j) {
                        newBoard[i, j] = eBoardCell.White;
                    }
                    else if ((i == (i_BoardSize / 2) - 1 && j == i_BoardSize / 2) || (i == i_BoardSize / 2 && j == (i_BoardSize / 2) - 1))
                    {
                        newBoard[i, j] = eBoardCell.Black;
                    }
                    else
                    {
                        newBoard[i, j] = eBoardCell.Empty;
                    }
                }
            }

            return newBoard;
        }

        public bool GameOver()
        {
            bool isGameOver = true;

            if (m_FirstPlayer.HasValidMoves() && m_SecondPlayer.HasValidMoves())
            {
                isGameOver = false;
            }

            return isGameOver;
        }

        public void NextTurn()
        {
            if (m_CurrentPlayer.Name == m_FirstPlayer.Name)
            {
                m_CurrentPlayer = m_SecondPlayer;
            }
            else
            {
                m_CurrentPlayer = m_FirstPlayer;
            }
        }

        public Player GetLeader() 
        {
            Player leader;

            if (m_FirstPlayer.Score > m_SecondPlayer.Score)
            {
                leader = m_FirstPlayer;
            }
            else
            {
                leader = m_SecondPlayer;
            }

            return leader;
        }

        public void Restart()
        {
            int sizeOfBoard = m_GameBoard.Length;
            m_GameBoard = instantiateBoard(sizeOfBoard);

            m_FirstPlayer.Restart(new sMatrixCoordinate((sizeOfBoard / 2) - 1, (sizeOfBoard / 2) - 1), new sMatrixCoordinate((sizeOfBoard / 2), (sizeOfBoard / 2)));
            m_SecondPlayer.Restart(new sMatrixCoordinate((sizeOfBoard / 2) - 1, (sizeOfBoard / 2)), new sMatrixCoordinate((sizeOfBoard / 2), (sizeOfBoard / 2) - 1));

            m_CurrentPlayer = m_FirstPlayer;
        }
    }
}
