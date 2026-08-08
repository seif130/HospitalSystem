using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Administration.Entities
{
    public class DepartmentEquipment : BaseEntity
    {
        public Guid DepartmentId { get; private set; }
        public string EquipmentName { get; private set; } = default!;
        public string SerialNumber { get; private set; } = default!;
        public EquipmentStatus Status { get; private set; }
        public DateTime PurchaseDate { get; private set; }

        public Department Department { get; private set; } = default!;

        private DepartmentEquipment() { }

        private DepartmentEquipment(Guid departmentId, string equipmentName, string serialNumber, DateTime purchaseDate)
        {
            DepartmentId = departmentId;
            EquipmentName = equipmentName;
            SerialNumber = serialNumber;
            Status = EquipmentStatus.Operational;
            PurchaseDate = purchaseDate;
        }

        internal static Result<DepartmentEquipment> Create(Guid departmentId, string equipmentName, string serialNumber, DateTime purchaseDate)
        {
            var errors = new List<Error>();

            if (departmentId == Guid.Empty)
                errors.Add(Error.Validation("Equipment.EmptyDepartmentId", "Department ID is required."));
            if (string.IsNullOrWhiteSpace(equipmentName))
                errors.Add(Error.Validation("Equipment.EmptyName", "Equipment name is required."));
            if (string.IsNullOrWhiteSpace(serialNumber))
                errors.Add(Error.Validation("Equipment.EmptySerialNumber", "Serial number is required."));

            if (errors.Any())
                return Result<DepartmentEquipment>.Fail(errors);

            return Result<DepartmentEquipment>.Ok(new DepartmentEquipment(departmentId, equipmentName, serialNumber, purchaseDate));
        }

        public Result UpdateStatus(EquipmentStatus newStatus)
        {
            Status = newStatus;
            LastModifiedAt = DateTime.UtcNow;
            return Result.ok();
        }
    }
}
