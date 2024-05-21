namespace FootBall_Bot.Model.Leagues
{
    public class Leagues
    {
        public string Get;
        public string[] Parameters;
        public string[] Errors;
        public int Results;
        public Paging Paging;
        public Response[] Response;
    }

    public class Response
    {
        public League League;
        public Country Country;
        public Seasons[] Seasons;
    }
    public class Paging
    {
        public int Current;
        public int Total;
    }
    public class League
    {
        public int ID;
        public string Name;
        public string Type;
        public string Logo;
    }

    public class Country
    {
        public string Name;
        public string Code;
        public string Flag;
    }

    public class Seasons
    {
        public int Year;
        public string Start;
        public string End;
        public bool Current;
        public Coverage Coverage;
    }
    public class Coverage
    {
        public Fixtures Fixtures;
        public bool Standings;
        public bool Players;
        public bool TopScores;
        public bool TopAssists;
        public bool TopCards;
        public bool Injuries;
        public bool Predicsions;
        public bool Odds;
    }
    public class Fixtures
    {
        public bool Events;
        public bool Lineups;
        public bool StatisticsFixtures;
        public bool StatisticsPlayer;
    }
}
