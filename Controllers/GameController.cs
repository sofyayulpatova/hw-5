using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TicTacToe.Models;

public class GameController : Controller
{
    private const string SessionGameKey = "GameSession";

    private Game GetGameSession()
    {
        // retrieve the game session data as a JSON string from the session state
        var gameSessionJson = HttpContext.Session.GetString(SessionGameKey);

        // Check - retrieved string is null or empty, which means no game session 
        if (string.IsNullOrEmpty(gameSessionJson))
        {
            // If no session data exists, return a new instance of the Game, representing a new game
            return new Game();
        }
        else
        {
            // If session data exists -> deserialize the JSON string back into a Game object
            return JsonSerializer.Deserialize<Game>(gameSessionJson);
        }
    }

    private void SaveGameSession(Game game)
    {
        // Save seesion (serailize to json)
        HttpContext.Session.SetString(SessionGameKey, JsonSerializer.Serialize(game));
    }

    public IActionResult Index()
    {
        var game = GetGameSession();
        return View(game);
    }

    [HttpPost]
    public IActionResult MakeMove(int r, int c)
    {
        var game = GetGameSession();
        var moveResult = game.MakeMove(r, c);
        SaveGameSession(game); // Ensure the game is saved after making a move

        if (!moveResult)
        {
            // Log for debugging
            Console.WriteLine($"Invalid move at {r},{c}");
            ViewBag.Error = "Invalid move!";
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ResetGame()
    {
        var game = new Game();
        SaveGameSession(game);
        return RedirectToAction("Index");
    }
}
