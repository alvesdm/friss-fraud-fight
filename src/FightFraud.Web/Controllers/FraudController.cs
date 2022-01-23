using FlightFraud.Application.Fraud.Commands;
using FlightFraud.Application.People.Extensions;
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
        //private readonly ILogger<FraudController> _logger;

        public FraudController()//ILogger<FraudController> logger)
        {
            //_logger = logger;
        }

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

    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdentificationNumber { get; set; }
    }

    public class MatchingResult
    {
        public double Probability { get; set; }
        public string Name { get; set; }
    }
}
