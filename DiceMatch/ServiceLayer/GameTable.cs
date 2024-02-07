using BusinessLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class GameTable
    {
        private const int BoardSize = 3;
        public Player player1;
        public Player player2;
        public bool turn;
        public int Die;
        public bool HasRolled;
        public bool Update;
        public GameTable(User user1, User user2)
        {
            player1 = new Player(user1);
            player2 = new Player(user2);        
            InitiateBoards();
            player1.Score = 0;
            player2.Score = 0;
            turn = true;
            HasRolled = false;
            Update = false;
        }
        public GameTable()
        {

        }
        private void InitiateBoards()
        {
            player1.Board = new int[BoardSize, BoardSize];
            player2.Board = new int[BoardSize, BoardSize];
            player1.InitiateBoard();
            player2.InitiateBoard();
            Update = true;
        }

        public Player CurrentPlayer
        {
            get
            {
                if (turn) return player1;
                else return player2;
            }      
        }
        public Player OpponentPlayer
        {
            get
            {
                if (!turn) return player1;
                else return player2;
            }
        }
        public void Roll()
        {
            if (!HasRolled)
            {
                Die = CurrentPlayer.RollDie();
                HasRolled = true;
            }
            Update = true;
        }

        public void Place(int row, int column)
        {
            CurrentPlayer.PlaceDie(row, column, Die);          
            ProcessOpponentBoard(column);
            EndTurn();
        }
        public void EndTurn()
        {
            player1.UpdateScore();
            player2.UpdateScore();
            if(CurrentPlayer.Character.Cooldown>0) CurrentPlayer.Character.Cooldown--;
            turn = !turn;
            HasRolled = false;
            Update = true;
        }
        public void UseAbility(int[] input = null)
        {
            CurrentPlayer.Character.Ability(this, input);
            player1.UpdateScore();
            player2.UpdateScore();
            Update = true;
        }

        private void ProcessOpponentBoard(int column)
        {           
            int[,] opponentBoard = OpponentPlayer.Board;            
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
