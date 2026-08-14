using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Primitives
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
