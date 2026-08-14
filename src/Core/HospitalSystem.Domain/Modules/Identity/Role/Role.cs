using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Identity.Role
{
    public sealed class Role : AggregateRoot<RoleId>
    {
        public string Name { get; private set; } = null!;
        public bool IsSystemRole { get; private set; } // system roles ( "Administrator") cannot be deleted

        private readonly List<PermissionId> _permissionIds = new();
        public IReadOnlyCollection<PermissionId> PermissionIds => _permissionIds.AsReadOnly();

        private Role() { }

        private Role(RoleId id, string name, bool isSystemRole) : base(id)
        {
            Name = name;
            IsSystemRole = isSystemRole;
        }

        public static Role Create(string name, bool isSystemRole = false)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Role name is required.");
            return new Role(RoleId.New(), name.Trim(), isSystemRole);
        }

        public void GrantPermission(PermissionId permissionId)
        {
            if (!_permissionIds.Contains(permissionId)) _permissionIds.Add(permissionId);
        }

        public void RevokePermission(PermissionId permissionId)
        {
            if (IsSystemRole) throw new DomainException("Cannot modify permissions on a system role.");
            _permissionIds.Remove(permissionId);
        }
    }
}
