namespace Orc.EntityFrameworkCore
{
    using System;

    public class EntityTypeException : Exception
    {
        public EntityTypeException(string message) 
            : base(message)
        {
        }
    }
}
