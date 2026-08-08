using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
    public class Nurse : BaseEntity, IAggregateRoot
    {
        public Guid DepartmentId { get; private set; }
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string PhoneNumber { get; private set; } = default!;
        public ShiftType Shift { get; private set; }
        public NurseStatus Status { get; private set; }

        public Department Department { get; private set; } = default!;

        private Nurse() { }

        private Nurse(Guid departmentId, string firstName, string lastName, string email, string phoneNumber, ShiftType shift)
        {
            DepartmentId = departmentId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Shift = shift;
            Status = NurseStatus.Active;
        }

        public static Result<Nurse> Create(Guid departmentId, string firstName, string lastName, string email, string phoneNumber, ShiftType shift)
        {
            var errors = new List<Error>();

            if (departmentId == Guid.Empty)
                errors.Add(Error.Validation("Nurse.EmptyDepartmentId", "Department ID is required."));
            if (string.IsNullOrWhiteSpace(firstName))
                errors.Add(Error.Validation("Nurse.EmptyFirstName", "First name is required."));
            if (string.IsNullOrWhiteSpace(lastName))
                errors.Add(Error.Validation("Nurse.EmptyLastName", "Last name is required."));
            if (string.IsNullOrWhiteSpace(email))
                errors.Add(Error.Validation("Nurse.EmptyEmail", "Email is required."));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                errors.Add(Error.Validation("Nurse.EmptyPhoneNumber", "Phone number is required."));

            if (errors.Any())
                return Result<Nurse>.Fail(errors);

            return Result<Nurse>.Ok(new Nurse(departmentId, firstName, lastName, email, phoneNumber, shift));
        }

        public Result UpdateInfo(string firstName, string lastName, string email, string phoneNumber, ShiftType shift)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(firstName))
                errors.Add(Error.Validation("Nurse.EmptyFirstName", "First name is required."));
            if (string.IsNullOrWhiteSpace(lastName))
                errors.Add(Error.Validation("Nurse.EmptyLastName", "Last name is required."));
            if (string.IsNullOrWhiteSpace(email))
                errors.Add(Error.Validation("Nurse.EmptyEmail", "Email is required."));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                errors.Add(Error.Validation("Nurse.EmptyPhoneNumber", "Phone number is required."));

            if (errors.Any())
                return Result.Fail(errors);

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Shift = shift;
            LastModifiedAt = DateTime.UtcNow;

            return Result.ok();
        }

        public Result ChangeStatus(NurseStatus newStatus)
        {
            Status = newStatus;
            LastModifiedAt = DateTime.UtcNow;
            return Result.ok();
        }
    }
}
