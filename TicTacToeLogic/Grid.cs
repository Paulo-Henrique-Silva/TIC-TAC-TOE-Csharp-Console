using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//this a c# library
//I can't run this as program because it just works to references in other programs

namespace TicTacToeLogic
{
    public class Grid
    {
        //property to Board
        public char[] Board { get; set; }

        //constructor to grid
        public Grid()
        {
            Board = new char[9]; //assigns as an array - do not delete it

            for (int i = 0; i < Board.Length; i++)
                Board[i] = ' ';
        }

        //It needs to be between 0 and 8 and empty 
        public bool IsAValidBoardSquare(int pieceNumber)
        {
            return pieceNumber >= 0 && pieceNumber < 9 && Board[pieceNumber] == ' ';
        }

        //checks if it is a Victory
        public bool IsVictory(char playerChar)
        {
            //rows
            for (int i = 0; i < 9; i += 3)
            {
                if (Board[i] == playerChar && Board[i + 1] == playerChar && Board[i + 2] == playerChar)
                    return true;
            }

            //columns
            for (int i = 0; i < 3; i++)
            {
                if (Board[i] == playerChar && Board[i + 3] == playerChar && Board[i + 6] == playerChar)
                    return true;
            }

            //main diagonal
            if (Board[0] == playerChar && Board[4] == playerChar && Board[8] == playerChar) return true;

            //secondary diagonal
            if (Board[2] == playerChar && Board[4] == playerChar && Board[6] == playerChar) return true;

            return false;
        }

        //returns true if it is a valid tie. Else returns false.
        //if it has at least one empty, it means it isn't a tie yet
        public bool IsTie()
        {
            foreach (char c in Board)
            {
                if (c == ' ') return false;
            }

            return true;
        }
    }
}
