using FightFraud.Application.Business.Fraud.Commands;
using FightFraud.Domain.Entities;
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
    public class SettingsController : ApiControllerBase
    {
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(ILogger<SettingsController> logger)
        {
            _logger = logger;
        }

        //20 sec for the sake of ResponseCache example, as it impacts the user experience after the Put method result
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 20)] 
        [HttpGet]
        public async Task<MatchingRuleSettings> Get()
        {
            return await Mediator.Send(new GetMatchingRuleSettingsCommand());
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateMatchingRuleSettingsCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }


}
