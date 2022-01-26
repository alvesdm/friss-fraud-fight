using FightFraud.Application.Fraud.Commands;
using FightFraud.Application.People.Extensions;
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
    public class FraudController : ApiControllerBase
    {
        [HttpPost]
        [Route("calculate")]
        public async Task<IActionResult> Post(CalculateFraudCommand command)
        {
            var matchingProbability = await Mediator.Send(command);
            return Ok(new MatchingResult
            {
                Name = matchingProbability.Person?.ToFullName(),
                Probability = matchingProbability.MatchingProbaility
            });
        }
    }
}
