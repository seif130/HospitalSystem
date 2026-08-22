using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Shared.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
