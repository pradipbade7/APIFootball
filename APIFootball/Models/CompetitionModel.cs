namespace APIFootball.Models
{
    public class CompetitionModel
    {
        public int count { get; set; }
        public Filters filters { get; set; }
        public List<Competition> competitions { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Filters
    {
        public string plan { get; set; }
        public List<string> status { get; set; }
    }

    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public string ensignUrl { get; set; }
    }

    public class CurrentSeason
    {
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int currentMatchday { get; set; }
        public object winner { get; set; }
    }

    public class Competition
    {
        public int id { get; set; }
        public Area area { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string emblemUrl { get; set; }
        public string plan { get; set; }
        public CurrentSeason currentSeason { get; set; }
        public int numberOfAvailableSeasons { get; set; }
        public DateTime lastUpdated { get; set; }
    }

    


}
