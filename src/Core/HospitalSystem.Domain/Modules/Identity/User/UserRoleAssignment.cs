using HospitalSystem.Domain.Identififers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Identity.User
{

    public sealed class UserRoleAssignment
    {
        public RoleId RoleId { get; }
        public DateTime AssignedOnUtc { get; }

        internal UserRoleAssignment(RoleId roleId)
        {
            RoleId = roleId;
            AssignedOnUtc = DateTime.UtcNow;
        }
    }
}
