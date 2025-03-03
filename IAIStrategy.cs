namespace TicTacToe
{
    public interface IAIStrategy
    {
        (int row, int col) GetMove(Button[,] board, string playerSymbol);
    }
}