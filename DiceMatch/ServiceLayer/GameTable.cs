using ServiceLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class GameTable
    {
        private const int BoardSize = 3;
        public Player player1;
        public Player player2;
        public bool turn;
        public int Die;

        public GameTable()
        {
            player1 = new Player();
            player2 = new Player();        
            InitiateBoards();
            player1.Score = 0;
            player2.Score = 0;
            turn = true;
        }
        private void InitiateBoards()
        {
            player1.Board = new int[BoardSize, BoardSize];
            player2.Board = new int[BoardSize, BoardSize];
            player1.InitiateBoard();
            player2.InitiateBoard();
        }

        public Player CurrentPlayer
        {
            get
            {
                if (turn) return player1;
                else return player2;
            }      
        }
        public void Roll()
        {
            Die = CurrentPlayer.RollDie();
        }

        public void Place(int row, int column)
        {
            CurrentPlayer.PlaceDie(row, column, Die);          
            ProcessOpponentBoard(column);
            player1.UpdateScore();
            player2.UpdateScore();
            turn = !turn;
        }

        private void ProcessOpponentBoard(int column)
        {
            turn = !turn;
            int[,] opponentBoard = CurrentPlayer.Board;
            turn = !turn;
            for (int i = 0; i < BoardSize; i++)
            {
                if (opponentBoard[i, column] == Die)
                {
                    opponentBoard[i, column] = 0; // Destroy opponent's dice of same value
                }
            }
        }

        public bool IsBoardFull()
        {
            if(player1.IsBoardFull() || player2.IsBoardFull())
            {
                return true;
            }
            else return false;
        }

        public Player GetWinner()
        {    
            return player1.Score > player2.Score ? player1 : player2;
        }

        public int GetPlayer1Score()
        {
            return player1.Score;
        }

        public int GetPlayer2Score()
        {
            return player2.Score;
        }

    }
}
