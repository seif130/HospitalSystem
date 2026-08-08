using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Common
{
    public interface IDomainEvent
    {
        // تاريخ ووقت وقوع الحدث
        DateTime OccurredOn { get; }
    }
}
