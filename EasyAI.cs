using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public class EasyAI : IAIStrategy
    {
        private Random random = new Random();

        public (int row, int col) GetMove(Button[,] board, string playerSymbol)
        {
            while (true)
            {
                int row = random.Next(0, 3);
                int col = random.Next(0, 3);
                if (board[row, col].Text == "")
                    return (row, col);
            }
        }
    }
}