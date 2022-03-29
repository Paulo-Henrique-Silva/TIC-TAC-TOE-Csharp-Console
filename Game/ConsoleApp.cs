using System;
using TicTacToeLogic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class ConsoleApp
    {
        //players
        const char PLAYER1 = 'X', PLAYER2 = 'O';

        //game options - do not use 0 as a valid option
        enum MenuOptions { PvP = 1, PvC, Exit }

        static void Main()
        {
            while (true) //repeats until user press exit key
            {
                switch (Menu())
                {
                    case MenuOptions.PvP:
                        PlayerVsPlayer();
                        break;

                    case MenuOptions.PvC:
                        PlayerVsIA();
                        break;

                    case MenuOptions.Exit:
                        ExitProgram();
                        break;

                    default:
                        InvalidInput();
                        break;
                }

                Console.ReadKey(); //stops before refreshs it
            }
        }

        static MenuOptions Menu()
        {
            Console.Clear();

            Console.WriteLine($"\t\t\t\tTIC-TAC-TOE-C#");
            Console.WriteLine("\t\t\t------------------------------\n");

            Console.WriteLine("\t\t\t[1] - Player vs Player");
            Console.WriteLine("\t\t\t[2] - Player vs Computer");
            Console.WriteLine("\t\t\t[3] - Exit");

            Console.Write("\nEnter a option: ");
            if (!int.TryParse(Console.ReadLine(), out int op)) return 0;

            return (MenuOptions)op;
        }

        static void PlayerVsPlayer()
        {
            int playerInput;
            char playerTurn = PLAYER2;

            //creates a new grid to the game
            Grid grid = new Grid();

            do
            {
                //changes player turn
                //player 1 always goes first
                playerTurn = (playerTurn == PLAYER1) ? PLAYER2 : PLAYER1;

                do
                {
                    PrintBoard(grid); //1 - 9 because is more "User friendly"
                    Console.Write($"\n\nEnter a square (1 - 9) - Player {playerTurn}: "); 
                }
                while 
                (
                    !int.TryParse(Console.ReadLine(), out playerInput) || 
                    !grid.IsAValidBoardSquare(playerInput - 1)
                );

                grid.Board[playerInput - 1] = playerTurn; //-1 because is an array
            }
            while (!grid.IsVictory(playerTurn) && !grid.IsTie()); //reads players input until game ends

            PrintBoard(grid);
            Console.WriteLine("\n\nEND GAME!\a");
            Console.WriteLine(grid.IsVictory(playerTurn) ? $"PLAYER '{playerTurn}' WINS! \\o/" : "TIE! :0");
            Console.WriteLine("Thank you for playing - Paulo");
        }

        static void PlayerVsIA()
        {
            int playerInput;
            char playerChar = PLAYER1, compChar = PLAYER2;

            //creates a new grid to the game
            Grid grid = new Grid();

            while (true)
            {
                //player turn - players always goes first
                do
                {
                    PrintBoard(grid); //1 - 9 because is more "User friendly"
                    Console.Write($"\n\nEnter a square (1 - 9): "); 
                }
                while
                (
                    !int.TryParse(Console.ReadLine(), out playerInput) ||
                    !grid.IsAValidBoardSquare(playerInput - 1)
                );

                grid.Board[playerInput - 1] = playerChar; //-1 because is an array

                if (grid.IsVictory(playerChar) || grid.IsTie()) break;

                //computer turn
                grid.Board[Computer.Play(grid, compChar, playerChar)] = compChar;

                if (grid.IsVictory(compChar) || grid.IsTie()) break;
            }
            
            PrintBoard(grid);
            Console.WriteLine("\n\nEND GAME!\a");

            if (grid.IsTie()) Console.WriteLine("TIE! :0");
            else Console.WriteLine(grid.IsVictory(playerChar) ? "PLAYER WINS \\o/" : "COMPUTER WINS \\o/");

            Console.WriteLine("Thank you for playing - Paulo");
        }

        static void ExitProgram()
        {
            Console.WriteLine("Exiting...");
            Console.ReadKey();
            Environment.Exit(1); //exits program
        }

        static void InvalidInput()
        {
            Console.WriteLine("Invalid Input!");
        }

        //Prints game board in console
        static void PrintBoard(Grid grid)
        {
            Console.Clear();

            Console.WriteLine("\t\t\t\t     |     |    ");
            Console.WriteLine($"\t\t\t\t  {grid.Board[0]}  |  {grid.Board[1]}  |  {grid.Board[2]} ");
            Console.WriteLine("\t\t\t\t     |     |    ");
            Console.WriteLine("\t\t\t\t-----|-----|-----");
            Console.WriteLine("\t\t\t\t     |     |    ");
            Console.WriteLine($"\t\t\t\t  {grid.Board[3]}  |  {grid.Board[4]}  |  {grid.Board[5]} ");
            Console.WriteLine("\t\t\t\t     |     |    ");
            Console.WriteLine("\t\t\t\t-----|-----|-----");
            Console.WriteLine("\t\t\t\t     |     |    ");
            Console.WriteLine($"\t\t\t\t  {grid.Board[6]}  |  {grid.Board[7]}  |  {grid.Board[8]} ");
            Console.WriteLine("\t\t\t\t     |     |    ");
        }
    }
}