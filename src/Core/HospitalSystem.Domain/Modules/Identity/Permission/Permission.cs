using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Identity.Permission
{
    public sealed class Permission : AggregateRoot<PermissionId>
    {
        public string Code { get; private set; } = null!;  // "patients.read", "payroll.approve"
        public string Description { get; private set; } = null!;

        private Permission() { }

        private Permission(PermissionId id, string code, string description) : base(id)
        {
            Code = code;
            Description = description;
        }

        public static Permission Create(string code, string description)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new DomainException("Permission code is required.");
            return new Permission(PermissionId.New(), code.Trim().ToLowerInvariant(), description.Trim());
        }
    }
}
