using Newtonsoft.Json;
using System.Numerics;

namespace ConectFour
{
    public class Game
    {
        private readonly Board board;
        private readonly IConfiguration configuration;
        private readonly List<List<Tuple<int, int>>> WinningCombinations;

        public int moveCount { get; private set; }
        public int turn { get { return (moveCount % 2) + 1; } }
        public Game(Board board, IConfiguration configuration)
        {
            this.board = board;
            this.configuration = configuration;
            this.WinningCombinations = LoadWinningCombinations();
        }
        public bool MakeMove(int column)
        {
            bool winnerFound = false;
            (int row, int col) moveMade = board.PlacePiece(column, turn);
            if (moveMade == (-1, -1))
            {
                throw new Exception("Move could not be placed");
            }
            //moveCount incremented later due to turn calculations and usage.
            //after line 23, movecount ought increment by one, but it would throw off turn's returned value.
            if (moveCount >= 6)
            {
                if (CheckWinner(turn, moveMade))
                {
                    winnerFound = true;
                }
            }
            moveCount++;
            return winnerFound;
        }
        public bool CanMakeMove(int col)
        {
            bool _canMakeMove = false;
            for (int i = board.board.Count - 1; i >= 0; i--)
            {
                if (board.board[i][col] != 0)
                {
                    continue;
                }
                else
                {
                    _canMakeMove = true;
                    break;
                }
            }
            return _canMakeMove;
        }
        public void ResetGame()
        {
            this.moveCount = 0;
            board.ClearBoard();
        }

        public bool CheckWinner(int player, (int row, int col) move)
        {
            List<List<Tuple<int, int>>> CurrentWinCons = WinningCombinations
                .Where(winCon => winCon.Contains(move.ToTuple()))
                .ToList();

            foreach (List<Tuple<int, int>> wincon in CurrentWinCons)
            {
                bool isWinCon = wincon.All(pos => board.board[pos.Item1][pos.Item2] == player);
                if (isWinCon)
                {
                    return true;
                }
            }

            return false;
        }
        private List<List<Tuple<int, int>>> LoadWinningCombinations()
        {

            string filePath = configuration["WinningCombinationsFilePath"];
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"JSON file not found at path: {filePath}");
            }
            string jsonContent = File.ReadAllText(filePath);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new TupleConverter());

            return JsonConvert.DeserializeObject<List<List<Tuple<int, int>>>>(jsonContent, settings);
        }
    }
}
