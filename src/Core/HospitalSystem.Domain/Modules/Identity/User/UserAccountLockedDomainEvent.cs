using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Identity.User
{
    public sealed record UserAccountLockedDomainEvent(UserId UserId) : DomainEvent;

}
