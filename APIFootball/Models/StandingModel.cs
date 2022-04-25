namespace APIFootball.Models
{
    public class StandingModel
    {
        public Filters filters { get; set; }
        public Competition competition { get; set; }
        public Season season { get; set; }
        public List<Standing> standings { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    

    public class Table
    {
        public int position { get; set; }
        public Team team { get; set; }
        public int playedGames { get; set; }
        public object form { get; set; }
        public int won { get; set; }
        public int draw { get; set; }
        public int lost { get; set; }
        public int points { get; set; }
        public int goalsFor { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference { get; set; }
    }

    public class Standing
    {
        public string stage { get; set; }
        public string type { get; set; }
        public object group { get; set; }
        public List<Table> table { get; set; }
    }

   
}
