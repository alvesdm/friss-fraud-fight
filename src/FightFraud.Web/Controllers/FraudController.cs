using FightFraud.Application.Business.Fraud.Commands;
using FightFraud.Application.Business.People.Extensions;
using FightFraud.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightFraud.Web.Controllers
{
    /// <summary>
    /// Provides with methods to deal with Fraud.
    /// </summary>
    [Authorize]
    public class FraudController : ApiControllerBase
    {
        /// <summary>
        /// Calculates the fraud probability of a provided person, against the existing ones in the database.
        /// </summary>
        /// <param name="command">The info related to person to be assessed.</param>
        /// <returns>The probability of matching with an existing person in the database.</returns>
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
