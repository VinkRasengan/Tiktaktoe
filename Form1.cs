using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private bool isPlayerXTurn = true; // X đi trước
        private int movesCount = 0;
        private bool gameEnded = false;
        private Label? turnLabel;

        public Form1()
        {
            InitializeComponent();
            InitializeGameBoard();
            // Update turn label
            if (turnLabel != null)
            {
                turnLabel.Text = $"Turn: Player {(isPlayerXTurn ? "X" : "O")}";
            }
        }

        private void InitializeGameBoard()
        {
            this.Text = "Tic Tac Toe";
            this.Size = new System.Drawing.Size(300, 350); // Increase height

            // Create 3x3 grid
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Size = new System.Drawing.Size(80, 80),
                        Location = new System.Drawing.Point(20 + (i * 85), 40 + (j * 85)),
                        Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold),
                        BackColor = System.Drawing.Color.LightGray
                    };
                    buttons[i, j].Click += Button_Click;
                    this.Controls.Add(buttons[i, j]);
                }
            }

            // Add turn label
            turnLabel = new Label
            {
                Text = "Turn: Player X",
                Size = new System.Drawing.Size(100, 20),
                Location = new System.Drawing.Point(100, 10)
            };
            this.Controls.Add(turnLabel);

            // Add reset button
            Button resetButton = new Button
            {
                Text = "Reset",
                Size = new System.Drawing.Size(80, 40),
                Location = new System.Drawing.Point(110, 290)
            };
            resetButton.Click += (s, e) => ResetGame();
            this.Controls.Add(resetButton);
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (gameEnded) return;

            if (sender is not Button clickedButton) return;

            if (clickedButton.Text != "") return;

            clickedButton.Text = isPlayerXTurn ? "X" : "O";
            movesCount++;

            if (CheckWin())
            {
                MessageBox.Show($"Player {(isPlayerXTurn ? "X" : "O")} wins!");
                gameEnded = true;
                return;
            }

            if (movesCount == 9)
            {
                MessageBox.Show("It's a draw!");
                gameEnded = true;
                return;
            }

            isPlayerXTurn = !isPlayerXTurn;
            if (turnLabel != null)
            {
                turnLabel.Text = $"Turn: Player {(isPlayerXTurn ? "X" : "O")}";
            }
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
            gameEnded = false;
            if (turnLabel != null)
            {
                turnLabel.Text = "Turn: Player X";
            }
        }
    }
}