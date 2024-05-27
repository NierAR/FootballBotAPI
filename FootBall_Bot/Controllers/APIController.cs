using Microsoft.AspNetCore.Mvc;
using FootBall_Bot.Clients;
using FootBall_Bot.Models.Fixtures;

namespace FootBall_Bot.Controllers
{
    
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        [HttpGet("~/GetFixturesAll")]
        public string GetFixtures()
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(), false, true);
        }

        [HttpGet("~/GetFixturesByDate")]
        public string GetFixtures(string date, bool IsToday)
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(date), IsToday, false);
        }

        [HttpGet("~/GetFixturesByTeamInSeason")]
        public string GetFixtures(string teamName, ushort season)
        {
            APIClient client = new APIClient();
            return ParseResponse(client.GetFixtures(client.GetTeam(teamName).Response[0].Team.ID, season: season), false, false);
        }
        



        private string ParseResponse(Fixtures content, bool IsToday, bool IsLive)
        {
            int counter = content.Response.Length > 15 ? 15 : content.Response.Length;
            return FormatAnswer(content, IsToday, IsLive, counter);
        }

        private string FormatAnswer(Fixtures content, bool IsToday, bool IsLive, int counter)
        {
            string answer = "";
            for (int i = 0; i < counter; i++)
            {
                if (!IsLive)
                {
                    if (IsToday && content.Response[i].Fixture.Status.Long != "Not Started")
                    {
                        continue;
                    }

                    if (!IsToday && content.Response[i].Fixture.Status.Long != "Match Finished")
                    {
                        continue;
                    }
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
