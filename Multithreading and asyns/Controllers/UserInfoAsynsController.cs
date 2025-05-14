using Microsoft.AspNetCore.Mvc;
using Multithreading_and_asyns.Models;
using System.Text.Json;

namespace Multithreading_and_asyns.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoAsynsController : ControllerBase
    {

        private const string USER_FILE_PATH = "data/user/json";
        private const string LOCATION_FILE_PATH = "data/locations/json";
        private const string GAMES_FILE_PATH = "data/games/json";

        [HttpGet("user - info")]
        public async Task<ActionResult> GetUserInfo()
        {
            var task = GetRandomUserId();

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i);
            }

            var userid = await task;
            //var location = GetUserLocation(userId);

            //var game = GetUserFavoriteGame(userId);

            return Ok(new { task /*, location, game*/ });
        }

        private async Task<int> GetRandomUserId()
        {
            Console.WriteLine("Полученное сообщение");

            var userJson = await Task.Run(async () =>
              {
                  var userJson = await System.IO.File.ReadAllTextAsync(USER_FILE_PATH);
                  await Task.Delay(3000);
                  return userJson;
              });

            Console.WriteLine("Сообщение получено");

            var userData = JsonSerializer.Deserialize<UserData>(userJson)
            ?? throw new NullReferenceException();

            return userData.Users.First().Id;


        }
        //    private Task GetUserLocation(int userId)
        //    {
        //        Console.WriteLine("Получена локация");
        //        var locationJson = System.IO.File.ReadAllText(LOCATION_FILE_PATH);
        //        Task.Delay(1000).Wait();
        //        Console.WriteLine("Локация получена");
        //        var locationDate = JsonSerializer.Deserialize<LocationData>(locationJson);

        //        return locationDate.Locations.First(l => l.UserId == userId).LocationName;

        //    }
        //    private Task GetUserFavoriteGame(int userId)
        //    {
        //        Console.WriteLine("Получена игра");
        //        var gameJson = System.IO.File.ReadAllText(GAMES_FILE_PATH);
        //        Thread.Sleep(3000);
        //        Console.WriteLine("Игра получена");
        //        var gameDate = JsonSerializer.Deserialize<GameData>(gameJson);

        //        return gameDate.Games.First(l => l.UserId == userId).FavoriteGame;
        //    }
        //}
    }
}

