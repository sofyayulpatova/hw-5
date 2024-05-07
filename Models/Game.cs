using Xunit;

namespace TicTacToe.Models
{


    public enum States
    {
        Process,
        Draw,
        Win
    }
    public class Game
    {
        public List<List<string>> Board { get; set; } = new List<List<string>>(); // 2 dim list for storing the data of game; 
        public string CurrentPlayer { get; set; } = "X"; // the current player :)
        public States GameState { get; set; } = States.Process; // game state 

        public Game()
        {
            // board Initialization
            for (int i = 0; i < 3; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < 3; j++)
                {
                    row.Add(""); // Initialize each cell with an empty string
                }
                Board.Add(row);
            }
        }


        // method for current player definition
        public void NowPlays()
        {
            if (CurrentPlayer == "X")
            {
                CurrentPlayer = "O";
            }
            else
            {
                CurrentPlayer = "X";
            }
        }

        // method to check that the game is fnished due to the full board
        public bool BoardFull()
        {
            if (Board.All(row => row.All(cell => cell != ""))){
                return true;
            }
            return false;
        }

        // method to check the win exs.
        public bool WinnerExists()
        {
            //  horizontal lines
            for (int i = 0; i < 3; i++)
            {
                if (Board[i][0] == CurrentPlayer && Board[i][1] == CurrentPlayer && Board[i][2] == CurrentPlayer)
                    return true;
            }

            //  vertical lines
            for (int i = 0; i < 3; i++)
            {
                if (Board[0][i] == CurrentPlayer && Board[1][i] == CurrentPlayer && Board[2][i] == CurrentPlayer)
                    return true;
            }

            //  diagonal lines
            if (Board[0][0] == CurrentPlayer && Board[1][1] == CurrentPlayer && Board[2][2] == CurrentPlayer)
                return true;
            if (Board[0][2] == CurrentPlayer && Board[1][1] == CurrentPlayer && Board[2][0] == CurrentPlayer)
                return true;
            return false;
        }

        public bool MakeMove(int r, int c)
        {

            // if the cell is blank and game is not finished
            if (Board[r][c] == "" && GameState == States.Process)
            {
                Board[r][c] = CurrentPlayer; // sign
                if (WinnerExists()) // Check if the game has been won
                {
                    GameState = States.Win;
                }
                else if (BoardFull()) // Check if the board is a draw
                {
                    GameState = States.Draw;
                }
                else
                {
                    NowPlays(); // Switch players if the game hasn't ended
                }
                return true;
            }
            return false;
        }




    }
}
