using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private bool isPlayerXTurn = true; // X đi trước
        private int movesCount = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            this.Text = "Tic Tac Toe";
            this.Size = new System.Drawing.Size(300, 300);
            
            // Tạo bảng 3x3
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Size = new System.Drawing.Size(80, 80),
                        Location = new System.Drawing.Point(20 + (i * 85), 20 + (j * 85)),
                        Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold)
                    };
                    buttons[i, j].Click += Button_Click;
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            
            // Kiểm tra nếu ô đã được đánh
            if (clickedButton.Text != "") return;

            // Đánh dấu X hoặc O
            clickedButton.Text = isPlayerXTurn ? "X" : "O";
            movesCount++;

            // Kiểm tra thắng
            if (CheckWin())
            {
                MessageBox.Show($"Player {(isPlayerXTurn ? "X" : "O")} wins!");
                ResetGame();
                return;
            }

            // Kiểm tra hòa
            if (movesCount == 9)
            {
                MessageBox.Show("It's a draw!");
                ResetGame();
                return;
            }

            // Chuyển lượt
            isPlayerXTurn = !isPlayerXTurn;
        }

        private bool CheckWin()
        {
            // Kiểm tra hàng
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text != "" && 
                    buttons[i, 0].Text == buttons[i, 1].Text && 
                    buttons[i, 1].Text == buttons[i, 2].Text)
                    return true;
            }

            // Kiểm tra cột
            for (int j = 0; j < 3; j++)
            {
                if (buttons[0, j].Text != "" && 
                    buttons[0, j].Text == buttons[1, j].Text && 
                    buttons[1, j].Text == buttons[2, j].Text)
                    return true;
            }

            // Kiểm tra đường chéo
            if (buttons[0, 0].Text != "" && 
                buttons[0, 0].Text == buttons[1, 1].Text && 
                buttons[1, 1].Text == buttons[2, 2].Text)
                return true;

            if (buttons[0, 2].Text != "" && 
                buttons[0, 2].Text == buttons[1, 1].Text && 
                buttons[1, 1].Text == buttons[2, 0].Text)
                return true;

            return false;
        }

        private void ResetGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j].Text = "";
                }
            }
            isPlayerXTurn = true;
            movesCount = 0;
        }
    }
}