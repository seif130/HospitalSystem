using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
    public class DepartmentService : BaseEntity
    {
        public Guid DepartmentId { get; private set; }
        public string ServiceName { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public Money Price { get; private set; } = default!;

        public Department Department { get; private set; } = default!;

        private DepartmentService() { }

        private DepartmentService(Guid departmentId, string serviceName, string description, Money price)
        {
            DepartmentId = departmentId;
            ServiceName = serviceName;
            Description = description;
            Price = price;
        }

        internal static Result<DepartmentService> Create(Guid departmentId, string serviceName, string description, Money price)
        {
            var errors = new List<Error>();

            if (departmentId == Guid.Empty)
                errors.Add(Error.Validation("Service.EmptyDepartmentId", "Department ID is required."));

            if (string.IsNullOrWhiteSpace(serviceName))
                errors.Add(Error.Validation("Service.EmptyName", "Service name is required."));

            if (price is null || price.Amount <= 0)
                errors.Add(Error.Validation("Service.InvalidPrice", "Service price must be greater than zero."));

            if (errors.Any())
                return Result<DepartmentService>.Fail(errors);

            return Result<DepartmentService>.Ok(new DepartmentService(departmentId, serviceName, description ?? string.Empty, price));
        }

        public Result UpdatePriceAndDetails(string serviceName, string description, Money price)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                return Result.Fail(Error.Validation("Service.EmptyName", "Service name is required."));
            if (price.Amount <= 0)
                return Result.Fail(Error.Validation("Service.NullPrice", "Service price is required."));

            ServiceName = serviceName;
            Description = description ?? string.Empty;
            Price = price;
            LastModifiedAt = DateTime.UtcNow;

            return Result.ok();
        }
    }
}
