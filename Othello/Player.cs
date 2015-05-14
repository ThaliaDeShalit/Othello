using System;
using System.Collections.Generic;
using System.Text;

namespace Othello
{
    class Player
    {
        private string m_Name;
        private int m_CurrScore;
        private eColor m_Color;
        private List<sMatrixCoordinate> m_ValidMoves;
        private List<sMatrixCoordinate> m_CellsOccupied;

        public Player(string i_Name, eColor i_Color, sMatrixCoordinate i_FirstCoinPosition, sMatrixCoordinate i_SecondCoindPosition) 
        {
            m_Name = i_Name;
            m_Color = i_Color;
            m_CellsOccupied.Add(i_FirstCoinPosition);
            m_CellsOccupied.Add(i_SecondCoindPosition);
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public int Score
        {
            set
            {
                m_CurrScore = Score;
            }
            get
            {
                return m_CurrScore;
            }
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }
        }

        public List<sMatrixCoordinate> CellsOccupied
        {
            get
            {
                return m_CellsOccupied;
            }
        }

        public List<sMatrixCoordinate> ValidMoves
        {
            get
            {
                return m_ValidMoves;
            }
        }

        public bool HasValidMoves()
        {
            bool hasMoves = (m_ValidMoves.Count != 0);

            return hasMoves;
        }

        public void GetValidMoves()
        {
            //todo
        }

        public void Restart(sMatrixCoordinate i_FirstCoinPosition, sMatrixCoordinate i_SecondCoindPosition)
        {
            m_ValidMoves.Clear();
            m_CellsOccupied.Clear();

            m_CellsOccupied.Add(i_FirstCoinPosition);
            m_CellsOccupied.Add(i_SecondCoindPosition);
        }

        public sMatrixCoordinate MakeMove()
        {
            Random rnd = new Random(); 
            int randomNumber = rnd.Next() % m_ValidMoves.Count;

            return m_ValidMoves[randomNumber];
        }
    }
}
