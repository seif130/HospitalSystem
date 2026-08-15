using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Shared.Abstractions
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}
