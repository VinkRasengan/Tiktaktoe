using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public class MediumAI : IAIStrategy
    {
        private Random random = new Random();
        private HardAI hardAI = new HardAI();

        public (int row, int col) GetMove(Button[,] board, string playerSymbol)
        {
            // 60% cơ hội chơi ngẫu nhiên, 40% dùng Minimax
            if (random.NextDouble() < 0.6)
            {
                while (true)
                {
                    int row = random.Next(0, 3);
                    int col = random.Next(0, 3);
                    if (board[row, col].Text == "")
                        return (row, col);
                }
            }
            return hardAI.GetMove(board, playerSymbol);
        }
    }
}