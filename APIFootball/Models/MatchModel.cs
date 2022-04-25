namespace APIFootball.Models
{
    public class MatchModel
    {
        public Head2head head2head { get; set; }
        public Match match { get; set; }
    }
 

    public class Head2head
    {
        public int numberOfMatches { get; set; }
        public int totalGoals { get; set; }
        public HomeTeam homeTeam { get; set; }
        public AwayTeam awayTeam { get; set; }
    }


  
}
