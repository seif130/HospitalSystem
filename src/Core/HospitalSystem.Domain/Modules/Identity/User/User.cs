using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Identity.User
{
    public sealed class User : AggregateRoot<UserId>
    {
        public EmailAddress Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public UserAccountStatus Status { get; private set; }
        public int FailedLoginAttempts { get; private set; }
        public DateTime? LastLoginUtc { get; private set; }
        public StaffId? LinkedStaffId { get; private set; } // optional link to an employment record

        private readonly List<UserRoleAssignment> _roleAssignments = new();
        public IReadOnlyCollection<UserRoleAssignment> RoleAssignments => _roleAssignments.AsReadOnly();

        private User() { }

        private User(UserId id, EmailAddress email, string passwordHash, StaffId? linkedStaffId) : base(id)
        {
            Email = email;
            PasswordHash = passwordHash;
            LinkedStaffId = linkedStaffId;
            Status = UserAccountStatus.PendingActivation;
        }

        public static User Register(EmailAddress email, string passwordHash, StaffId? linkedStaffId = null)
        {
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new DomainException("Password hash is required.");
            var user = new User(UserId.New(), email, passwordHash, linkedStaffId);
            user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id, email));
            return user;
        }

        public void Activate()
        {
            if (Status != UserAccountStatus.PendingActivation) throw new DomainException("Only a pending account can be activated.");
            Status = UserAccountStatus.Active;
        }

        public void AssignRole(RoleId roleId)
        {
            if (_roleAssignments.Any(r => r.RoleId == roleId)) throw new DomainException("Role is already assigned to this user.");
            _roleAssignments.Add(new UserRoleAssignment(roleId));
        }

        public void RevokeRole(RoleId roleId) => _roleAssignments.RemoveAll(r => r.RoleId == roleId);

        public void RecordSuccessfulLogin()
        {
            if (Status != UserAccountStatus.Active) throw new DomainException($"Cannot log in — account is {Status}.");
            FailedLoginAttempts = 0;
            LastLoginUtc = DateTime.UtcNow;
        }

        public void RecordFailedLogin()
        {
            FailedLoginAttempts++;
            if (FailedLoginAttempts >= 5)
            {
                Status = UserAccountStatus.Locked;
                RaiseDomainEvent(new UserAccountLockedDomainEvent(Id));
            }
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash)) throw new DomainException("Password hash is required.");
            PasswordHash = newPasswordHash;
        }

        public void Unlock()
        {
            if (Status != UserAccountStatus.Locked) throw new DomainException("Account is not locked.");
            Status = UserAccountStatus.Active;
            FailedLoginAttempts = 0;
        }

        public void Disable() => Status = UserAccountStatus.Disabled;
    }
}
