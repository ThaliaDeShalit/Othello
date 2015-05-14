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

        public static bool TryParse(string i_StringToParse, out sMatrixCoordinate? o_ResultOfParsing)
        {
            bool isValid = true;
            int? newX = null;
            int? newY = null;


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
                if (i_StringToParse[0].Equals('a') || i_StringToParse[0].Equals('A'))
                {
                    newX = 0;
                }
                else if (i_StringToParse[0].Equals('b') || i_StringToParse[0].Equals('B'))
                {
                    newX = 1;
                }
                else if (i_StringToParse[0].Equals('c') || i_StringToParse[0].Equals('C'))
                {
                    newX = 2;
                }
                else if (i_StringToParse[0].Equals('d') || i_StringToParse[0].Equals('D'))
                {
                    newX = 3;
                }
                else if (i_StringToParse[0].Equals('e') || i_StringToParse[0].Equals('E'))
                {
                    newX = 4;
                }
                else if (i_StringToParse[0].Equals('f') || i_StringToParse[0].Equals('F'))
                {
                    newX = 5;
                }
                else if (i_StringToParse[0].Equals('g') || i_StringToParse[0].Equals('G'))
                {
                    newX = 6;
                }
                else if (i_StringToParse[0].Equals('h') || i_StringToParse[0].Equals('H'))
                {
                    newX = 7;
                }

                newY = int.Parse(i_StringToParse[1].ToString()) - 1;    
            }

            //if the parsing was succesfull, it'll create a new coordinate with the relevant x and y.
            //if not, it'll create a default coordiante of (0,0)
            //the default setting is inspired by the default 0 in the int.TryParse method
            o_ResultOfParsing = new sMatrixCoordinate(newX?? 0, newY?? 0);

            return isValid;
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
