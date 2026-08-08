using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
    public class Doctor : BaseEntity, IAggregateRoot
    {
        public Guid DepartmentId { get; private set; }
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string PhoneNumber { get; private set; } = default!;
        public string Specialization { get; private set; } = default!;
        public DoctorStatus Status { get; private set; }

        public Department Department { get; private set; } = default!;

        private Doctor() { }

        private Doctor(Guid departmentId, string firstName, string lastName, string email, string phoneNumber, string specialization)
        {
            DepartmentId = departmentId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            Status = DoctorStatus.Active;
        }

        public static Result<Doctor> Create(Guid departmentId, string firstName, string lastName, string email, string phoneNumber, string specialization)
        {
            var errors = new List<Error>();

            if (departmentId == Guid.Empty)
                errors.Add(Error.Validation("Doctor.EmptyDepartmentId", "Department ID is required."));
            if (string.IsNullOrWhiteSpace(firstName))
                errors.Add(Error.Validation("Doctor.EmptyFirstName", "First name is required."));
            if (string.IsNullOrWhiteSpace(lastName))
                errors.Add(Error.Validation("Doctor.EmptyLastName", "Last name is required."));
            if (string.IsNullOrWhiteSpace(email))
                errors.Add(Error.Validation("Doctor.EmptyEmail", "Email is required."));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                errors.Add(Error.Validation("Doctor.EmptyPhoneNumber", "Phone number is required."));
            if (string.IsNullOrWhiteSpace(specialization))
                errors.Add(Error.Validation("Doctor.EmptySpecialization", "Specialization is required."));

            if (errors.Any())
                return Result<Doctor>.Fail(errors);

            return Result<Doctor>.Ok(new Doctor(departmentId, firstName, lastName, email, phoneNumber, specialization));
        }

        public Result UpdateInfo(string firstName, string lastName, string email, string phoneNumber, string specialization)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(firstName))
                errors.Add(Error.Validation("Doctor.EmptyFirstName", "First name is required."));
            if (string.IsNullOrWhiteSpace(lastName))
                errors.Add(Error.Validation("Doctor.EmptyLastName", "Last name is required."));
            if (string.IsNullOrWhiteSpace(email))
                errors.Add(Error.Validation("Doctor.EmptyEmail", "Email is required."));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                errors.Add(Error.Validation("Doctor.EmptyPhoneNumber", "Phone number is required."));
            if (string.IsNullOrWhiteSpace(specialization))
                errors.Add(Error.Validation("Doctor.EmptySpecialization", "Specialization is required."));

            if (errors.Any())
                return Result.Fail(errors);

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Specialization = specialization;
            LastModifiedAt = DateTime.UtcNow;

            return Result.ok();
        }

        public Result ChangeStatus(DoctorStatus newStatus)
        {
            Status = newStatus;
            LastModifiedAt = DateTime.UtcNow;
            return Result.ok();
        }
    }

}
