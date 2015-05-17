using System;
using System.Collections.Generic;
using System.Text;

namespace Othello
{
    public struct sMatrixCoordinate
    {
        public int m_X;
        public int m_Y;

        public sMatrixCoordinate(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }

        public int x
        {
            get
            {
                return m_X;
            }
        }

        public int y
        {
            get
            {
                return m_Y;
            }
        }

        

        public static sMatrixCoordinate operator +(sMatrixCoordinate i_FirstMatrixCoordiante, sMatrixCoordinate i_SecondMatrixCoordinate)
        {
            return new sMatrixCoordinate(i_FirstMatrixCoordiante.x + i_SecondMatrixCoordinate.x, i_FirstMatrixCoordiante.y + i_SecondMatrixCoordinate.y);
        }
    }

    public enum eBoardCell
    {
        Black,
        White,
        Empty
    }

    public enum eColor
    {
        Black,
        White
    }
}
