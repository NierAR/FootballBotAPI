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

        [HttpGet("~/GetFixturesAll")]
        public static string GetFixtures()
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(), false);
        }

        [HttpGet("~/GetFixturesByDate")]
        public static string GetFixtures(string date)
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(date), true);
        }

        [HttpGet("~/GetFixturesByTeamInSeason")]
        public static string GetFixtures(string teamName, ushort season)
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(client.GetTeam(teamName).Response[0].Team.ID, season: season), false);
        }
        
        private static string ParseResponse(Models.Fixtures.Fixtures content, bool HasToBePlayed)
        {
            int counter = content.Response.Length > 15 ? 15 : content.Response.Length;
            string answer = "";


            for (int i = 0; i < counter; i++)
            {
                if (HasToBePlayed & content.Response[i].Fixture.Status.Long != "Not Started")
                {
                    continue;
                }

                answer += content.Response[i].Teams.Home.Name + "-----" +
                    content.Response[i].Teams.Away.Name + "\n" +
                    content.Response[i].Goals.Home + "\t" + " : " + "\t" +
                    content.Response[i].Goals.Away + "\n" +
                    content.Response[i].Fixture.Status.Long + "\n" + "----------------------------" + "\n";
            }
            return answer;
        }

    }
}
