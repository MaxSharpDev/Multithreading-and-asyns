﻿using Microsoft.AspNetCore.Mvc;
using Multithreading_and_asyns.Models;
using System.Text.Json;

namespace Multithreading_and_asyns.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoSynsController : ControllerBase
    {
        private const string USER_FILE_PATH = "data/user/json";
        private const string LOCATION_FILE_PATH = "data/locations/json";
        private const string GAMES_FILE_PATH = "data/games/json";

        [HttpGet("user - info")]
        public ActionResult GetUserInfo()
        {
            var userId = GetRandomUserId();

            var location = GetUserLocation(userId);

            var game = GetUserFavoriteGame(userId);

            return Ok(new { userId, location, game });
        }

        private int GetRandomUserId()
        {
            Console.WriteLine("Полученное сообщение");
            var userJson = System.IO.File.ReadAllText(USER_FILE_PATH);
            Task.Delay(1000).Wait();
            Console.WriteLine("Сообщение получено");
            var userData = JsonSerializer.Deserialize<UserData>(userJson);

            return userData.Users.First().Id;
        }
        private string GetUserLocation(int userId)
        {
            Console.WriteLine("Получена локация");
            var locationJson = System.IO.File.ReadAllText(LOCATION_FILE_PATH);
            Task.Delay(1000).Wait();
            Console.WriteLine("Локация получена");
            var locationDate = JsonSerializer.Deserialize<LocationData>(locationJson);

            return locationDate.Locations.First(l => l.UserId == userId).LocationName;

        }
        private string GetUserFavoriteGame(int userId)
        {
            Console.WriteLine("Получена игра");
            var gameJson = System.IO.File.ReadAllText(GAMES_FILE_PATH);
            Thread.Sleep(3000);
            Console.WriteLine("Игра получена");
            var gameDate = JsonSerializer.Deserialize<GameData>(gameJson);

            return gameDate.Games.First(l => l.UserId == userId).FavoriteGame;
        }
    }
}
