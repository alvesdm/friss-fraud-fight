using System;

namespace FightFraud.Application.Common.Exceptions
{
    public class AlreadyExistsException<T, t> : Exception
    {
        public AlreadyExistsException()
            : base()
        {
        }

        public AlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public AlreadyExistsException(t key)
            : base($"Entity \"{nameof(T)}\" with Id ({key}) already exists.")
        {
        }
    }
}