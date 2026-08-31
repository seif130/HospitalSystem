using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Doctors
{
    public sealed class Doctor : AggregateRoot<DoctorId>
    {
        public PersonName Name { get; private set; } = null!;

        public SpecialtyId SpecialtyId { get; private set; } = null!;

        public DepartmentId DepartmentId { get; private set; } = null!;

        public string LicenseNumber { get; private set; } = null!;

        private Doctor()
        {
        }

        private Doctor(
            DoctorId id,
            PersonName name,
            SpecialtyId specialtyId,
            DepartmentId departmentId,
            string licenseNumber)
            : base(id)
        {
            Name = name;
            SpecialtyId = specialtyId;
            DepartmentId = departmentId;
            LicenseNumber = NormalizeLicenseNumber(licenseNumber);
        }

        public static Doctor Register(
            PersonName name,
            SpecialtyId specialtyId,
            DepartmentId departmentId,
            string licenseNumber)
        {
            ArgumentNullException.ThrowIfNull(name);

            if (string.IsNullOrWhiteSpace(licenseNumber))
            {
                throw new DomainException(
                    "License number is required.");
            }

            return new Doctor(
                DoctorId.New(),
                name,
                specialtyId,
                departmentId,
                licenseNumber);
        }

        public void ChangeName(PersonName name)
        {
            ArgumentNullException.ThrowIfNull(name);

            Name = name;
        }

        public void ChangeSpecialty(SpecialtyId specialtyId)
        {
            SpecialtyId = specialtyId;
        }

        public void ChangeDepartment(DepartmentId departmentId)
        {
            DepartmentId = departmentId;
        }

        public void ChangeLicenseNumber(string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
            {
                throw new DomainException(
                    "License number is required.");
            }

            LicenseNumber = NormalizeLicenseNumber(licenseNumber);
        }

        private static string NormalizeLicenseNumber(string value)
        {
            return value.Trim();
        }
    }
}
