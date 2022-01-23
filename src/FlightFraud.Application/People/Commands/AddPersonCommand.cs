using FlightFraud.Application.Common.Exceptions;
using FlightFraud.Application.Common.Interfaces;
using FlightFraud.Domain.Entities;
using FlightFraud.Domain.Events;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightFraud.Application.People.Commands
{
    public class AddPersonCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IdentificationNumber { get; set; }
    }

    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public AddPersonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.IdentificationNumber) && _context.People.Any(p => p.IdentificationNumber == request.IdentificationNumber))
            {
                throw new AlreadyExistsException<Person, string>(request.IdentificationNumber);
            }

            var entity = new Person
            {
                DateOfBirth = request.DateOfBirth,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentificationNumber = request.IdentificationNumber
            };

            entity.DomainEvents.Add(new PersonCreatedEvent(entity));

            _context.People.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;

        }
    }
}
