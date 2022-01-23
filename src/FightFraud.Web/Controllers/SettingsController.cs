using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightFraud.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(ILogger<SettingsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<MatchingRuleModel> Get()
        {
            return new MatchingRuleModel
            {
                DateOfBirthSamePercent = 1,
                FirstNameSamePercent = 2,
                FirstNameSimilarPercent = 3,
                LastNameSamePercent = 4,
            };
        }

        [HttpPut]
        public async Task<IActionResult> Put(MatchingRuleModel model)
        {
            return NoContent();
        }
    }

    public class MatchingRuleModel
    {
        public decimal LastNameSamePercent { get; set; }
        public decimal FirstNameSamePercent { get; set; }
        public decimal FirstNameSimilarPercent { get; set; }
        public decimal DateOfBirthSamePercent { get; set; }
    }
}
