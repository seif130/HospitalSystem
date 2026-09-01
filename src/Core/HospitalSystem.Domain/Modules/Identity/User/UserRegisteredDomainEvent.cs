using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Identity.User
{
    public sealed record UserRegisteredDomainEvent(UserId UserId, EmailAddress Email) : DomainEvent;
}
