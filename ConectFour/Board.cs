namespace ConectFour
{
    public class Board
    {
        public List<List<int>> board { get; private set; }

        private int Rows { get; set; }
        private int Cols { get; set; }
        public Board(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
            this.board = new List<List<int>>();
            for (int i = 0; i < rows; i++)
            {
                this.board.Add(new List<int>(new int[cols]));
                for (int j = 0; j < cols; j++)
                {
                    board[i][j] = 0;
                }
            }
        }
        public (int row, int col) PlacePiece(int col, int player)
        {
            for (int i = board.Count - 1; i >= 0; i--)
            {
                if (board[i][col] == 0)
                {
                    board[i][col] = player;
                    return (i, col);
                }
            }
            return (-1, -1);
        }
        public void ClearBoard()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    board[i][j] = 0;
                }
            }
        }
    }
}
