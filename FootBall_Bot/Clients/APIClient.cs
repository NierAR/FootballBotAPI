using Newtonsoft.Json;
using RestSharp;

namespace FootBall_Bot.Clients
{
    public class APIClient
    {
        RestClient client;

        public APIClient()
        {
            client = new RestClient();
        }
        public Models.Leagues.Leagues GetLeagues()
        {
            string content = ExecuteRequest("https://v3.football.api-sports.io/leagues");
            Models.Leagues.Leagues result = JsonConvert.DeserializeObject<Models.Leagues.Leagues>(content);
            return result;
        }

        public Models.Leagues.Leagues GetLeagues(string name)
        {
            name = FormatToURL(name);
            
            var content = ExecuteRequest($"https://v3.football.api-sports.io/leagues?name={name}");
            Models.Leagues.Leagues result = JsonConvert.DeserializeObject<Models.Leagues.Leagues>(content);
            return result;
        }



        public Models.Fixtures.Fixtures GetFixtures()
        {
            
            var content = ExecuteRequest("https://v3.football.api-sports.io/fixtures?live=all");
            Models.Fixtures.Fixtures result = JsonConvert.DeserializeObject<Models.Fixtures.Fixtures>(content);
            return result;
        }   //used

        public Models.Fixtures.Fixtures GetFixtures(string date)
        {
            
            var content = ExecuteRequest($"https://v3.football.api-sports.io/fixtures?date={date}");
            Models.Fixtures.Fixtures result = JsonConvert.DeserializeObject<Models.Fixtures.Fixtures>(content);
            return result;
        }   //used

        public Models.Fixtures.Fixtures GetFixtures(int league, int season)
        {
            
            var content = ExecuteRequest($"https://v3.football.api-sports.io/fixtures?league={league}&season={season}");
            Models.Fixtures.Fixtures result = JsonConvert.DeserializeObject<Models.Fixtures.Fixtures>(content);
            return result;
        }

        public Models.Fixtures.Fixtures GetFixtures(int teamID, ushort season)
        {

            var content = ExecuteRequest($"https://v3.football.api-sports.io/fixtures?season={season}&team={teamID}");
            Models.Fixtures.Fixtures result = JsonConvert.DeserializeObject<Models.Fixtures.Fixtures>(content);
            return result;
        }   //used

        public Models.Fixtures.Fixtures GetFixtures(int teamID, string date)
        {
            var season = int.Parse(date.Split("-")[0]) - 1;
            
            var content = ExecuteRequest($"https://v3.football.api-sports.io/fixtures?season={season}&team={teamID}&date={date}");
            Models.Fixtures.Fixtures result = JsonConvert.DeserializeObject<Models.Fixtures.Fixtures>(content);
            return result;
        }

        public Models.Fixtures.Fixtures GetFixtures(int teamID, int season, int league)
        {
            
            var content = ExecuteRequest($"https://v3.football.api-sports.io/fixtures?league={league}&season={season}&team={teamID}");
            Models.Fixtures.Fixtures result = JsonConvert.DeserializeObject<Models.Fixtures.Fixtures>(content);
            return result;
        }



        

        public Models.Teams.Teams GetTeamByCode(string code)
        {
           
            var content = ExecuteRequest($"https://v3.football.api-sports.io/teams?code={code}");
            Models.Teams.Teams result = JsonConvert.DeserializeObject<Models.Teams.Teams>(content);
            return result;
        }

        public Models.Teams.Teams GetTeam(string teamName)
        {
            teamName = FormatToURL(teamName);
            
            var content = ExecuteRequest($"https://v3.football.api-sports.io/teams?name={teamName}");
            Models.Teams.Teams result = JsonConvert.DeserializeObject<Models.Teams.Teams>(content);
            return result;
        }
        public Models.Teams.Teams GetTeam(string code, int league, int season)
        {
            var content = ExecuteRequest($"https://v3.football.api-sports.io/teams?code={code}&league={league}&season={season}");
            Models.Teams.Teams result = JsonConvert.DeserializeObject<Models.Teams.Teams>(content);
            return result;
        }


        private string ExecuteRequest(string url)
        {
            var request = new RestRequest($"{url}", Method.Get);
            request.AddHeader("x-rapidapi-key", Constants.ApiKey);
            request.AddHeader("x-rapidapi-host", Constants.Host);
            var content = client.Execute(request).Content;
            return content;
        }
        private string FormatToURL(string name)
        {
            string[] nameParts = name.Split(" ");
            if (nameParts.Length > 1)
            {
                name = "";
                for (int i = 0; i < nameParts.Length; i++)
                {
                    if (i == nameParts.Length - 1)
                    {
                        name += nameParts[i];
                        break;
                    }
                    name += nameParts[i] + "%20";
                }
            }

            return name;
        }

    }
}
