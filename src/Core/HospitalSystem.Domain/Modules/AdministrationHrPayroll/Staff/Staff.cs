using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.Staff
{

    public sealed class Staff : AggregateRoot<StaffId>
    {
        public PersonName Name { get; private set; } = null!;
        public StaffRole Role { get; private set; }
        public DepartmentId DepartmentId { get; private set; } = null!;
        public EmailAddress Email { get; private set; } = null!;
        public PhoneNumber Phone { get; private set; } = null!;
        public DateTime HiredOnUtc { get; private set; }
        public EmploymentStatus Status { get; private set; }

        private Staff() { }

        private Staff(StaffId id, PersonName name, StaffRole role, DepartmentId departmentId, EmailAddress email, PhoneNumber phone) : base(id)
        {
            Name = name;
            Role = role;
            DepartmentId = departmentId;
            Email = email;
            Phone = phone;
            HiredOnUtc = DateTime.UtcNow;
            Status = EmploymentStatus.Active;
        }

        public static Staff Onboard(PersonName name, StaffRole role, DepartmentId departmentId, EmailAddress email, PhoneNumber phone) =>
            new(StaffId.New(), name, role, departmentId, email, phone);

        public void PlaceOnLeave()
        {
            EnsureActiveOrOnLeave();
            Status = EmploymentStatus.OnLeave;
        }

        public void ReturnFromLeave()
        {
            if (Status != EmploymentStatus.OnLeave) throw new DomainException("Staff member is not currently on leave.");
            Status = EmploymentStatus.Active;
        }

        public void Suspend()
        {
            EnsureActiveOrOnLeave();
            Status = EmploymentStatus.Suspended;
        }

        public void Terminate()
        {
            if (Status == EmploymentStatus.Terminated) throw new DomainException("Staff member is already terminated.");
            Status = EmploymentStatus.Terminated;
            RaiseDomainEvent(new StaffTerminatedDomainEvent(Id));
        }

        private void EnsureActiveOrOnLeave()
        {
            if (Status is EmploymentStatus.Terminated)
                throw new DomainException("Cannot change status of a terminated staff member.");
        }
    }
}
