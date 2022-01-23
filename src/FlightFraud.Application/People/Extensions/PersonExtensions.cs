using FlightFraud.Domain.Entities;

namespace FlightFraud.Application.People.Extensions
{
    public static class PersonExtensions
    {
        public static string ToFullName(this Person me)
        {
            return $"{me.FirstName} {me.LastName}";
        }

        public static bool IsIdentificationNumberKnown(this Person me)
        {
            return !string.IsNullOrEmpty(me.IdentificationNumber);
        }

        public static bool IsDateOfBirthKnown(this Person me)
        {
            return me.DateOfBirth.HasValue;
        }
    }
}
