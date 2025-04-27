using Microsoft.AspNetCore.Mvc;

namespace League.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ILogger<TeamsController> _logger;

        public TeamsController(ILogger<TeamsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTeams")]
        public IEnumerable<Team> Get()
        {
            return Enumerable
                .Select(
                    index =>
                        new Team
                        {
                            Id = index,
                            Name = $"Team {index}",
                            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                        }
                )
                .ToArray();
        }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
    }
}
