using ServiceLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class GameEngine
    {
        private GameTable gameTable;
        private Player currentPlayer;

        public GameEngine()
        {
            gameTable = new GameTable();
            currentPlayer = Player.Player1;
        }

        public string GetCurrentPlayerName()
        {
            return currentPlayer == Player.Player1 ? "Player 1" : "Player 2";
        }

        public bool PlaceDie(int column)
        {
            bool isPlaced = gameTable.PlaceDie(currentPlayer, column);
            if (isPlaced)
            {
                currentPlayer = currentPlayer == Player.Player1 ? Player.Player2 : Player.Player1;
            }
            return isPlaced;
        }

        public bool IsGameOver()
        {
            return gameTable.GetWinner() != Player.None;
        }

        public string GetGameResult()
        {
            Player winner = gameTable.GetWinner();
            if (winner == Player.None)
            {
                return "The game is still ongoing.";
            }
            else
            {
                string winnerName = winner == Player.Player1 ? "Player 1" : "Player 2";
                return $"Player {winner} wins!";
            }
        }

        public int GetCurrentPlayerScore()
        {
            return currentPlayer == Player.Player1 ? gameTable.GetPlayer1Score() : gameTable.GetPlayer2Score();
        }
    }
}
