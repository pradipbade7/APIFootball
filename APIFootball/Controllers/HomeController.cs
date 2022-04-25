using APIFootball.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace APIFootball.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            HomeModel objHome = new HomeModel();
            CompetitionModel objcompetition = new CompetitionModel();
            NewsModel objNews = new NewsModel();

            try
            {
                using (StreamReader r = new StreamReader(@"wwwroot\data\competitions.json"))
                {
                    string data = r.ReadToEnd();
                    objcompetition = JsonConvert.DeserializeObject<CompetitionModel>(data);
                }

                string newsURL = "https://newsapi.org/v2/top-headlines?country=gb&category=sports&apiKey=0b7344382b2f49c48aa3975648f25088";
                string news = GetNewsFromAPI(newsURL);
                List<int> competitionSelected = new List<int>() { 2021, 2002, 2014, 2019, 2015, 2001 };
                objHome.Competition = objcompetition.competitions.Where(t => competitionSelected.Contains(t.id)).ToList();
                objHome.News = JsonConvert.DeserializeObject<NewsModel>(news);
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View(objHome);
        }

        [Route("/Competition/{string}")]
        public IActionResult Competition()
        {
            FixtureModel? objFixture = new FixtureModel();
            StandingModel? objStanding= new StandingModel();
            CompetitionViewModel objCompetition = new CompetitionViewModel();
            string competitionId = Url.RouteUrl(RouteData.Values).Replace("/Competition/", "");
            if (string.IsNullOrEmpty(competitionId))
            {
                return View("Error");
            }
            try
            {
                int id = int.Parse(competitionId);
                //Fixtures
                var dateFromFixture=DateTime.Now.ToString("yyyy-MM-dd");
                var dateToFixture = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd");

                string URL = "https://api.football-data.org/v2/competitions/" + id + "/matches?status=SCHEDULED&dateFrom="+ dateFromFixture + "&dateTo="+dateToFixture;
                string data = GetDataFromAPI(URL);
                objCompetition.Fixtures = JsonConvert.DeserializeObject<FixtureModel>(data);
                
                //Results
                var dateFromResult = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                var dateToResult = DateTime.Now.ToString("yyyy-MM-dd");

                string URLresult = "https://api.football-data.org/v2/competitions/" + id + "/matches?status=FINISHED&dateFrom=" + dateFromResult + "&dateTo=" + dateToResult;
                string dataresult = GetDataFromAPI(URLresult);
                objCompetition.Results = JsonConvert.DeserializeObject<FixtureModel>(dataresult);
                //Standing
                if (id!=2000)
                {
                    string URLtable = "https://api.football-data.org/v2/competitions/" + id + "/standings";
                    string dataTable = GetDataFromAPI(URLtable);
                    objCompetition.Table = JsonConvert.DeserializeObject<StandingModel>(dataTable);
                }
                

                //teams
                using (StreamReader r = new StreamReader(@"wwwroot\data\teams.json"))
                {
                    string json = r.ReadToEnd();
                    var Teams = JsonConvert.DeserializeObject<List<TeamModel>>(json).Where(x=>x.competition.id== id).FirstOrDefault();
                    objCompetition.Teams = Teams;
                }


            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View(objCompetition);
        }
        [Route("/News")]
        public IActionResult News()
        {
            NewsModel? objNews = new NewsModel();
            try
            {
                string URL = "https://newsapi.org/v2/top-headlines?country=gb&category=sports&apiKey=0b7344382b2f49c48aa3975648f25088";
                string data = GetNewsFromAPI(URL);
                objNews = JsonConvert.DeserializeObject<NewsModel>(data);
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View(objNews);
        }

        [Route("/Fixtures")]
        public IActionResult Fixture()
        {
            CompetitionModel objcompetition = new CompetitionModel();
            try
            {
                using (StreamReader r = new StreamReader(@"wwwroot\data\competitions.json"))
                {
                    string data = r.ReadToEnd();
                    objcompetition = JsonConvert.DeserializeObject<CompetitionModel>(data);
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View(objcompetition);
        }
        [Route("/Teams")]
        public IActionResult Teams()
        {
            TeamViewModel objTeams = new TeamViewModel();
            try
            {
                using (StreamReader r = new StreamReader(@"wwwroot\data\teams.json"))
                {
                    string json = r.ReadToEnd();
                    objTeams.Teams = JsonConvert.DeserializeObject<List<TeamModel>>(json);
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View(objTeams);
        }
        [Route("/Team/{string}")]
        public IActionResult TeamDetail()
        {
            TeamDetailViewModel objTeam = new TeamDetailViewModel();
            string teamId = Url.RouteUrl(RouteData.Values).Replace("/Team/", "");
            if (string.IsNullOrEmpty(teamId))
            {
                return View("Error");
            }
            try
            {
                int id = int.Parse(teamId);
                //Fixtures
                var dateFromFixture = DateTime.Now.ToString("yyyy-MM-dd");
                var dateToFixture = DateTime.Now.AddDays(60).ToString("yyyy-MM-dd");

                string URL = "http://api.football-data.org/v2/teams/" + id + "/matches?status=SCHEDULED&dateFrom=" + dateFromFixture + "&dateTo=" + dateToFixture;
                string data = GetDataFromAPI(URL);
                objTeam.Fixtures = JsonConvert.DeserializeObject<FixtureModel>(data);

                //Results
                var dateFromResult = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
                var dateToResult = DateTime.Now.ToString("yyyy-MM-dd");

                string URLresult = "https://api.football-data.org/v2/teams/" + id + "/matches?status=FINISHED&dateFrom=" + dateFromResult + "&dateTo=" + dateToResult;
                string dataresult = GetDataFromAPI(URLresult);
                objTeam.Results = JsonConvert.DeserializeObject<FixtureModel>(dataresult);
                //Team Detail
                string URLdetail = "https://api.football-data.org/v2/teams/" + id;
                string dataDetail = GetDataFromAPI(URLdetail);
                objTeam.TeamDetail = JsonConvert.DeserializeObject<TeamDetailModel>(dataDetail);

            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View(objTeam);
        }

        [Route("/Match/{string}")]
        public IActionResult Match()
        {
            MatchModel objMatch = new MatchModel();
            string matchId = Url.RouteUrl(RouteData.Values).Replace("/Match/", "");
            if (string.IsNullOrEmpty(matchId))
            {
                return View("Error");
            }
            try
            {
                int id = int.Parse(matchId);

                string URL = "http://api.football-data.org/v2/matches/" + id;
                string data = GetDataFromAPI(URL);
                objMatch = JsonConvert.DeserializeObject<MatchModel>(data);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View(objMatch);
        }
        public string GetDataFromAPI(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", "f9dc01b54a664f6eb57a9ea75518f77f");
            var response = client.GetStringAsync(url).Result;
            client.Dispose();
            return response;
        }
        public string GetNewsFromAPI(string url)
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(url).Result;
            client.Dispose();
            return response;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SetTimezone(string Timezone)
        {

        }
    }
}