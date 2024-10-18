using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConectFour.Pages
{
    public class IndexModel : PageModel
    {
        public Board Board { get; set; }
        public Game Game { get; set; }
        public string Message { get; private set; }

        public IndexModel(Board board, Game game)
        {
            this.Board = board;
            this.Game = game;
            this.Message = string.Empty;
        }

        public void OnGet()
        {

        }
        public void OnPostMove(int column)
        {
            bool success = Game.CanMakeMove(column);
            if (success)
            {
                bool winnerFound = Game.MakeMove(column);
                if (winnerFound)
                {
                    Message = $"Player {Game.turn} Wins!";
                }
            }
            else
            {
                Message = "Column is full";
            }
            if (Game.moveCount >= 42)
            {
                Message = "It's a Tie!";
            }
        }
        public void OnPostReset()
        {
            Game.ResetGame();
        }
    }
}
