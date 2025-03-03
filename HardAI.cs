using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public class HardAI : IAIStrategy
    {
        private const string AI_SYMBOL = "O";
        private const string PLAYER_SYMBOL = "X";

        public (int row, int col) GetMove(Button[,] board, string playerSymbol)
        {
            int bestScore = int.MinValue;
            (int row, int col) bestMove = (-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j].Text == "")
                    {
                        board[i, j].Text = AI_SYMBOL;
                        int score = Minimax(board, 0, false);
                        board[i, j].Text = "";
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }
                    }
                }
            }

            if (bestMove == (-1, -1))
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j].Text == "")
                        {
                            return (i, j);
                        }
                    }
                }
            }
            return bestMove != (-1, -1) ? bestMove : (0, 0);
        }

        private int Minimax(Button[,] board, int depth, bool isMaximizing)
        {
            string? result = CheckWinner(board);
            if (result != null)
            {
                if (result == AI_SYMBOL) return 10 - depth;
                if (result == PLAYER_SYMBOL) return depth - 10;
                return 0;
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j].Text == "")
                        {
                            board[i, j].Text = AI_SYMBOL;
                            int score = Minimax(board, depth + 1, false);
                            board[i, j].Text = "";
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j].Text == "")
                        {
                            board[i, j].Text = PLAYER_SYMBOL;
                            int score = Minimax(board, depth + 1, true);
                            board[i, j].Text = "";
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }

        private string? CheckWinner(Button[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0].Text != "" && board[i, 0].Text == board[i, 1].Text && board[i, 1].Text == board[i, 2].Text)
                    return board[i, 0].Text;
                if (board[0, i].Text != "" && board[0, i].Text == board[1, i].Text && board[1, i].Text == board[2, i].Text)
                    return board[0, i].Text;
            }
            if (board[0, 0].Text != "" && board[0, 0].Text == board[1, 1].Text && board[1, 1].Text == board[2, 2].Text)
                return board[0, 0].Text;
            if (board[0, 2].Text != "" && board[0, 2].Text == board[1, 1].Text && board[1, 1].Text == board[2, 0].Text)
                return board[0, 2].Text;

            bool isFull = true;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j].Text == "") isFull = false;
            return isFull ? "Draw" : null;
        }
    }
}