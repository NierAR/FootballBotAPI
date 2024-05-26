namespace FootBall_Bot.Models.Fixtures
{
    public class Fixtures
    {
        public string Get;
        public Parameters Parameters;
        public string[] Errors;
        public int Results;
        public Paging Paging;
        public Response[] Response;
    }
    public class Response
    {
        public Fixture Fixture;
        public League League;
        public Teams Teams;
        public Goals Goals;
        public Score Score;
        public Events[] Events;
    }

    public class Events
    {
        public Time Time;
        public Team Team;
        public Player Player;
        public Assist Assist;
        public string Type;
        public string Detail;
        public string Comments;
    }

    public class Parameters
    {
        public string League;
        public int Season;
        public int Team;
        public string Live;
        public string Date;
    }
    public class Paging
    {
        public int Current;
        public int Total;
    }

    public class Time
    {
        public int Elapsed;
        public string Extra;
    }

    public class Team
    {
        public string ID;
        public string Name;
        public string Logo;
    }
    public class Player
    {
        public string ID;
        public string Name;

    }
    public class Assist
    {
        public string ID;
        public string Name;
    }

    public class Fixture
    {
        public int ID;
        public string Referee;
        public string TimeZone;
        public string Date;
        public int TimeStamp;
        public Periods Periods;
        public Venue Venue;
        public Status Status;
    }

    public class Periods
    {
        public string First;
        public string Second;

    }
    public class Venue
    {
        public string ID;
        public string Name;
        public string City;
    }

    public class Status
    {
        public string Long;
        public string Short;
        public string Elapsed;
    }

    public class League
    {
        public int ID;
        public string Name;
        public string Country;
        public string Logo;
        public string Flag;
        public int Season;
        public string Round;
    }

    public class Teams
    {
        public Home Home;
        public Away Away;
    }

    public class Home
    {
        public int ID;
        public string Name;
        public string Logo;
        public string Winner;
    }

    public class Away
    {
        public int ID;
        public string Name;
        public string Logo;
        public string Winner;
    }

    public class Goals
    {
        public string Home;
        public string Away;
    }

    public class Score
    {
        public Halftime Halftime;
        public Fulltime Fulltime;
        public Extratime Extratime;
        public Penalty Penalty;
    }

    public class Halftime
    {
        public string Home;
        public string Away;
    }
    public class Fulltime
    {
        public string Home;
        public string Away;
    }
    public class Extratime
    {
        public string Home;
        public string Away;
    }
    public class Penalty
    {
        public string Home;
        public string Away;
    }
}
