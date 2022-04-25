namespace APIFootball.Models
{
    public class HomeModel
    {
        public List<Competition> Competition { get; set; }
        public NewsModel News { get; set; }

    }
    public class CompetitionViewModel
    {
        public FixtureModel Fixtures { get; set; }
        public FixtureModel Results { get; set; }
        public StandingModel Table { get; set; }
        public TeamModel Teams { get; set; }

    }
    public class TeamViewModel
    {
        public List<TeamModel> Teams { get; set; }
    }

    public class TeamDetailViewModel
    {
        public FixtureModel Fixtures { get; set; }
        public FixtureModel Results { get; set; }
        public TeamDetailModel TeamDetail { get; set; }
    }
}
