namespace FootBall_Bot.Models.Teams
{
    public class Teams
    {
        public string Get;
        public Parameters Parameters;
        public string[] Error;
        public int Results;
        public Paging Paging;
        public Response[] Response;
    }
    public class Response
    {
        public Team Team;
        public Venue Venue;
    }

    public class Team
    {
        public int ID;
        public string Name;
        public string Code;
        public string Country;
        public int Founded;
        public bool National;
        public string Logo;
    }

    public class Venue
    {
        public int ID;
        public string Name;
        public string Address;
        public string City;
        public int Capacity;
        public string Surface;
        public string Image;
    }
    public class Paging
    {
        public int Current;
        public int Total;
    }

    public class Parameters
    {
        public int ID;
        public string Name;
        public string Code;
        public int League;
        public int Season;
    }
}
