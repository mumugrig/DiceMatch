using ServiceLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class GameTable
    {
        private const int BoardSize = 3;
        private int[,] player1Board;
        private int[,] player2Board;
        private int player1Score;
        private int player2Score;

        public GameTable()
        {
            player1Board = new int[BoardSize, BoardSize];
            player2Board = new int[BoardSize, BoardSize];
            player1Score = 0;
            player2Score = 0;
        }

        public int RollDie()
        {
            Random random = new Random();
            return random.Next(1, 7); // Rolling a single 6-sided die
        }

        public bool PlaceDie(Player player, int column)
        {
            int[,] currentBoard = player == Player.Player1 ? player1Board : player2Board;
            int[,] opponentBoard = player == Player.Player1 ? player2Board : player1Board;

            for (int i = 0; i < BoardSize; i++)
            {
                if (currentBoard[i, column] == 0)
                {
                    int dieValue = RollDie();
                    currentBoard[i, column] = dieValue;
                    ProcessOpponentBoard(opponentBoard, column, dieValue);
                    CalculateScore(player);
                    return true;
                }
            }

            return false; // Column is full, cannot place die
        }

        private void ProcessOpponentBoard(int[,] opponentBoard, int column, int dieValue)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                if (opponentBoard[i, column] == dieValue)
                {
                    opponentBoard[i, column] = 0; // Destroy opponent's dice of same value
                }
            }
        }

        private void CalculateScore(Player player)
        {
            int[,] currentBoard = player == Player.Player1 ? player1Board : player2Board;
            int score = 0;

            for (int col = 0; col < BoardSize; col++)
            {
                HashSet<int> valuesInColumn = new HashSet<int>();
                int columnScore = 0;

                for (int row = 0; row < BoardSize; row++)
                {
                    int currentValue = currentBoard[row, col];

                    if (currentValue != 0 && !valuesInColumn.Contains(currentValue))
                    {
                        int occurrences = CountOccurrences(currentBoard, row, col, currentValue);
                        valuesInColumn.Add(currentValue);
                        columnScore += currentValue * occurrences;
                    }
                }

                score += columnScore;
            }

            if (player == Player.Player1)
            {
                player1Score = score;
            }
            else
            {
                player2Score = score;
            }
        }

        private int CountOccurrences(int[,] board, int row, int col, int value)
        {
            int occurrences = 0;

            for (int i = 0; i < BoardSize; i++)
            {
                if (board[i, col] == value)
                {
                    occurrences++;
                }
            }

            return occurrences;
        }

        public Player GetWinner()
        {
            if (IsBoardFull(player1Board) || IsBoardFull(player2Board))
            {
                return player1Score > player2Score ? Player.Player1 : Player.Player2;
            }

            return Player.None;
        }

        private bool IsBoardFull(int[,] board)
        {
            foreach (var cell in board)
            {
                if (cell == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public int GetPlayer1Score()
        {
            return player1Score;
        }

        public int GetPlayer2Score()
        {
            return player2Score;
        }
    }
}
