using FootballBotAPI.Clients;
using Microsoft.AspNetCore.Mvc;

namespace FootballBotAPI.Controllers
{
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;
        public DatabaseController(ILogger<DatabaseController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/DatabaseController/GetFavouriteTeam")]
        public string GetFavouriteTeam(long userId)
        {
            DatabaseClient db = new DatabaseClient();
            var answer = db.GetFavouriteTeamAsync(userId).Result;
            if (string.IsNullOrEmpty(answer))
            {
                return "На жаль ви не вказували улюблену команду";
            }
            return answer;
        }

        [HttpPost("/DatabaseController/SaveFavouriteTeam")]
        public void SaveTeam(long userId, string teamName)
        {
            DatabaseClient db = new DatabaseClient();

            db.InsertFavouriteTeamAsync(userId, teamName);
            return;
        }

        [HttpPut("/DatabaseController/ChangeFavouriteTeam")]
        public void UpdateTeam(long userId, string teamName)
        {
            DatabaseClient db = new DatabaseClient();

            db.ChangeFavouriteTeamAsync(userId, teamName);
        }

        [HttpDelete("/DatabaseController/DeleteFavouriteTeam")]
        public void DeleteTeam(long userId)
        {
            DatabaseClient db = new DatabaseClient();
            db.DeleteFavouriteTeamAsync(userId);
        }
    }
}
