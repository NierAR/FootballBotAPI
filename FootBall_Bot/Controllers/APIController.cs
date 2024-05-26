using Microsoft.AspNetCore.Mvc;
using FootBall_Bot.Clients;

namespace FootBall_Bot.Controllers
{
    
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        private readonly ILogger<APIController> _logger;

        public APIController(ILogger<APIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string[]> GetFixturesAll()
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures());
        }

        [HttpGet("~/GetFixturesByDate")]
        public List<string[]> GetFixtures(string date)
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(date));
        }

        [HttpGet("~/GetFixturesByTeamInDate")]
        public List<string[]> GetFixtures(string teamName, string date)
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(client.GetTeam(teamName).Response[0].Team.ID, date));
        }

        /*[HttpGet("~/GetFixturesByLeagueInSeason")]
        public List<string[]> GetFixtures(int league, int season)
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(league, season));
        }*/
        private static List<string[]> ParseResponse(Models.Fixtures.Fixtures content)
        {
            List<string[]> fixtures = new List<string[]>();

            foreach (Models.Fixtures.Response response in content.Response)
            {

                string[] fixture = {
                    response.Teams.Home.Name,
                    response.Teams.Away.Name,
                    response.Goals.Home + " : " + response.Goals.Away,
                    response.Fixture.Status.Long
                };

                fixtures.Add(fixture);
            }
            return fixtures;
        }

        [HttpPost("~/AddFavouriteTeam")]
        public static void SaveFavouriteTeam()
        {
            string DB;
        }
    }
}
