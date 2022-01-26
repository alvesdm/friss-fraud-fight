using FightFraud.Application.Business.People.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FightFraud.Web.Controllers
{
    [Authorize]
    public class PeopleController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(AddPersonCommand command)
        {
            var id = await Mediator.Send(command);
            return Created($"api/people/{id}", new { });
        }
    }
}
