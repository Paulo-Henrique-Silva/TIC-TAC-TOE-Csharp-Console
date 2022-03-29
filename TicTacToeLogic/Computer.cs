using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLogic
{
    public static class Computer
    {
        //Returns where computer can place its piece (0 - 8)
        public static int Play(Grid grid, char compChar, char playerChar)
        {
            int square;

            //computer tries to find a good move to end the game and win
            if ((square = WhereIsAGoodMoveFor(grid, compChar)) != -1) return square;
            
            //... good move to defend against the player
            if ((square = WhereIsAGoodMoveFor(grid, playerChar)) != -1) return square;

            //... a trick to win the game
            if ((square = IsPossibleToTrick(grid, compChar, playerChar)) != -1) return square;

            return RandomEmptySquare(grid); //randomly plays if there is no good moves
        }

        //Checks which square(0 - 8) is empty for a good move, based in a player char
        //ex: 0 | 1 | 2 - If 0 and 1 are occupied and 2 is empty, it will return 2
        //it will return -1 if there is no valid good move.
        private static int WhereIsAGoodMoveFor(Grid grid, char player)
        {
            //rows
            for (int i = 0; i < 9; i += 3)
            {
                if (grid.Board[i] == player && grid.Board[i + 1] == player && grid.Board[i + 2] == ' ')
                    return i + 2;

                if (grid.Board[i] == player && grid.Board[i + 1] == ' ' && grid.Board[i + 2] == player)
                    return i + 1;

                if (grid.Board[i] == ' ' && grid.Board[i + 1] == player && grid.Board[i + 2] == player)
                    return i;
            }

            //colums
            for (int i = 0; i < 3; i++)
            {
                if (grid.Board[i] == player && grid.Board[i + 3] == player && grid.Board[i + 6] == ' ')
                    return i + 6;

                if (grid.Board[i] == player && grid.Board[i + 3] == ' ' && grid.Board[i + 6] == player)
                    return i + 3;

                if (grid.Board[i] == ' ' && grid.Board[i + 3] == player && grid.Board[i + 6] == player)
                    return i;
            }

            //main diagonal
            if (grid.Board[0] == player && grid.Board[4] == player && grid.Board[8] == ' ') return 8;
            if (grid.Board[0] == player && grid.Board[4] == ' ' && grid.Board[8] == player) return 4;
            if (grid.Board[0] == ' ' && grid.Board[4] == player && grid.Board[8] == player) return 0;

            //secondary diagonal
            if (grid.Board[2] == player && grid.Board[4] == player && grid.Board[6] == ' ') return 6;
            if (grid.Board[2] == player && grid.Board[4] == ' ' && grid.Board[6] == player) return 4;
            if (grid.Board[2] == ' ' && grid.Board[4] == player && grid.Board[6] == player) return 2;

            return -1;
        }

        //Checks if it is possible to take the corners and center
        //the best strategy to win tic-tac-toe is to take the corners and, in some cases, the center square
        //returns -1 if is not possible
        private static int IsPossibleToTrick(Grid grid, char compChar, char playerChar)
        {
            Random rand = new Random();
            int[] corners = { 0, 2, 6, 8 };
            int square;
            bool isFirst_move = true;

            //checks if it is the first move of computer
            foreach (char c in grid.Board)
            {
                if (c == compChar) //if it has at least one compChar, it means it isn't the 1st move
                {
                    isFirst_move = false;
                    break;
                }
            }

            if //if the player took one corner in first move, the computer takes the center
            (
                isFirst_move && (grid.Board[0] == playerChar || grid.Board[2] == playerChar ||
                grid.Board[6] == playerChar || grid.Board[8] == playerChar)
            )
            {
                return 4;
            }
            else if //computer tries to take the corners if it is not first move(good strategy)
            (
                grid.Board[0] == ' ' || grid.Board[2] == ' ' || grid.Board[6] == ' ' ||
                grid.Board[8] == ' '
            )
            {
                while //selects a random corner
                (
                    Array.BinarySearch(corners, square = rand.Next(0, 9)) < 0 ||
                    !grid.IsAValidBoardSquare(square)
                ) ;

                return square;
            }
            else
            {
                return -1;
            }
        }

        private static int RandomEmptySquare(Grid grid)
        {
            Random rand = new Random();
            int square;

            //generates numbers until it is a valid board square
            while (!grid.IsAValidBoardSquare(square = rand.Next(0, 9)));

            return square;
        }
    }
}
