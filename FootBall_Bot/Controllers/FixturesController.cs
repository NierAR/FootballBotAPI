using FootBall_Bot.Model.Fixtures;
using FootBall_Bot.Model.Leagues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;

namespace FootBall_Bot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FixturesController : ControllerBase
    {
        private readonly ILogger<FixturesController> _logger;

        public FixturesController(ILogger<FixturesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string[]> GetFixturesAll()
        {
            BotClient client = new BotClient();
            return ParseResponse(client.GetFixtures());
        }

        [HttpGet("~/GetFixturesByDate")]
        public List<string[]> GetFixtures(string date)
        {
            BotClient client = new BotClient();
            return ParseResponse(client.GetFixtures(date));
        }

        [HttpGet("~/GetFixturesByTeamInDate")]
        public List<string[]> GetFixtures(int teamID, string date)
        {
            BotClient client = new BotClient();
            return ParseResponse(client.GetFixtures(teamID, date));
        }

        [HttpGet("~/GetFixturesByLeagueInSeason")]
        public List<string[]> GetFixtures(int league, int season)
        {
            BotClient client = new BotClient();
            return ParseResponse(client.GetFixtures(league, season));
        }
        private static List<string[]> ParseResponse(Model.Fixtures.Fixtures content)
        {
            List<string[]> fixtures = new List<string[]>();

            foreach (Model.Fixtures.Response response in content.Response)
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
    }
}
