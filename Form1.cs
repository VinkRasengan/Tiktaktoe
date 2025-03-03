using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private bool isPlayerXTurn = true;
        private int movesCount = 0;
        private bool gameEnded = false;
        private Label? turnLabel;
        private bool isVsComputer = false;
        private IAIStrategy? aiStrategy;

        public Form1()
        {
            InitializeComponent();
            InitializeGameBoard();
            if (turnLabel != null)
            {
                turnLabel.Text = "Turn: Player X";
            }
        }

        private void InitializeGameBoard()
        {
            this.Text = "Tic Tac Toe";
            this.Size = new System.Drawing.Size(300, 400);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Size = new System.Drawing.Size(80, 80),
                        Location = new System.Drawing.Point(20 + (i * 85), 60 + (j * 85)),
                        Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold),
                        BackColor = System.Drawing.Color.LightGray
                    };
                    buttons[i, j].Click += Button_Click;
                    this.Controls.Add(buttons[i, j]);
                }
            }

            turnLabel = new Label
            {
                Text = "Turn: Player X",
                Size = new System.Drawing.Size(100, 20),
                Location = new System.Drawing.Point(100, 10)
            };
            this.Controls.Add(turnLabel);

            Button resetButton = new Button
            {
                Text = "Reset",
                Size = new System.Drawing.Size(80, 40),
                Location = new System.Drawing.Point(110, 330)
            };
            resetButton.Click += (s, e) => ResetGame();
            this.Controls.Add(resetButton);

            ComboBox modeCombo = new ComboBox
            {
                Location = new System.Drawing.Point(20, 30),
                Size = new System.Drawing.Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            modeCombo.Items.AddRange(new[] { "Player vs Player", "Player vs Computer" });
            modeCombo.SelectedIndex = 0;
            modeCombo.SelectedIndexChanged += (s, e) => 
            { 
                isVsComputer = modeCombo.SelectedIndex == 1; 
                ResetGame();
            };
            this.Controls.Add(modeCombo);

            ComboBox difficultyCombo = new ComboBox
            {
                Location = new System.Drawing.Point(150, 30),
                Size = new System.Drawing.Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false
            };
            difficultyCombo.Items.AddRange(new[] { "Easy", "Medium", "Hard" });
            difficultyCombo.SelectedIndex = 0;
            difficultyCombo.SelectedIndexChanged += (s, e) =>
            {
                aiStrategy = difficultyCombo.SelectedIndex switch
                {
                    0 => new EasyAI(),
                    1 => new MediumAI(),
                    2 => new HardAI(),
                    _ => new EasyAI()
                };
                ResetGame();
            };
            modeCombo.SelectedIndexChanged += (s, e) => difficultyCombo.Enabled = isVsComputer;
            this.Controls.Add(difficultyCombo);
            aiStrategy = new EasyAI();
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            if (gameEnded || sender is not Button clickedButton || clickedButton.Text != "") return;

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

            if (isVsComputer && !isPlayerXTurn && !gameEnded)
            {
                ComputerMove();
            }
        }

        private void ComputerMove()
        {
            if (aiStrategy != null)
            {
                var (row, col) = aiStrategy.GetMove(buttons, "O");
                buttons[row, col].PerformClick();
            }
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text != "" && 
                    buttons[i, 0].Text == buttons[i, 1].Text && 
                    buttons[i, 1].Text == buttons[i, 2].Text)
                    return true;
            }
            for (int j = 0; j < 3; j++)
            {
                if (buttons[0, j].Text != "" && 
                    buttons[0, j].Text == buttons[1, j].Text && 
                    buttons[1, j].Text == buttons[2, j].Text)
                    return true;
            }
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
                for (int j = 0; j < 3; j++)
                    buttons[i, j].Text = "";
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