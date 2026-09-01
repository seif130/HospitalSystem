using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.Staff.Enums;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.SalaryStructure
{
    public sealed class SalaryStructure : AggregateRoot<SalaryStructureId>
    {
        public string GradeName { get; private set; } = null!;
        public StaffRole ApplicableRole { get; private set; }
        public Money BaseSalary { get; private set; } = null!;
        public decimal HousingAllowancePercentage { get; private set; }
        public decimal TransportAllowancePercentage { get; private set; }

        private SalaryStructure() { }

        private SalaryStructure(SalaryStructureId id, string gradeName, StaffRole applicableRole, Money baseSalary,
            decimal housingAllowancePercentage, decimal transportAllowancePercentage) : base(id)
        {
            GradeName = gradeName;
            ApplicableRole = applicableRole;
            BaseSalary = baseSalary;
            HousingAllowancePercentage = housingAllowancePercentage;
            TransportAllowancePercentage = transportAllowancePercentage;
        }

        public static SalaryStructure Define(string gradeName, StaffRole applicableRole, Money baseSalary,
            decimal housingAllowancePercentage, decimal transportAllowancePercentage)
        {
            if (string.IsNullOrWhiteSpace(gradeName)) throw new DomainException("Grade name is required.");
            if (housingAllowancePercentage < 0 || transportAllowancePercentage < 0)
                throw new DomainException("Allowance percentages cannot be negative.");
            return new SalaryStructure(SalaryStructureId.New(), gradeName.Trim(), applicableRole, baseSalary,
                housingAllowancePercentage, transportAllowancePercentage);
        }

        public Money CalculateGrossSalary() =>
            BaseSalary
                .Add(BaseSalary.Multiply(HousingAllowancePercentage / 100m))
                .Add(BaseSalary.Multiply(TransportAllowancePercentage / 100m));
    }
}
