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
        private List<sMatrixCoordinate> cellsOccupied;

        public Player(string i_Name, eColor i_Color) 
        {
            m_Name = i_Name;
            m_Color = i_Color;
            //instantiate cellsOccupied
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

        public bool HasValidMoves()
        {
            bool hasMoves = (m_ValidMoves.Count != 0);

            return hasMoves;
        }

        public void GetValidMoves()
        {
            //todo
        }
    }
}
