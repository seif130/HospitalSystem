using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Assets.EquipmentMaintenanceLog.Enum;
using HospitalSystem.Domain.Modules.Assets.MedicalEquipment.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Assets.MedicalEquipment
{

    public sealed class MedicalEquipment : AggregateRoot<MedicalEquipmentId>
    {
        public string Name { get; private set; } = null!;
        public string SerialNumber { get; private set; } = null!;
        public string Manufacturer { get; private set; } = null!;
        public DateTime PurchasedOnUtc { get; private set; }
        public EquipmentStatus Status { get; private set; }

        private readonly List<EquipmentMaintenanceLog> _maintenanceLogs = new();
        public IReadOnlyCollection<EquipmentMaintenanceLog> MaintenanceLogs => _maintenanceLogs.AsReadOnly();

        private readonly List<AssetAllocation> _allocations = new();
        public IReadOnlyCollection<AssetAllocation> Allocations => _allocations.AsReadOnly();

        public AssetAllocation? CurrentAllocation => _allocations.LastOrDefault(a => a.IsActive);

        private MedicalEquipment() { }

        private MedicalEquipment(MedicalEquipmentId id, string name, string serialNumber, string manufacturer, DateTime purchasedOnUtc) : base(id)
        {
            Name = name;
            SerialNumber = serialNumber;
            Manufacturer = manufacturer;
            PurchasedOnUtc = purchasedOnUtc;
            Status = EquipmentStatus.InService;
        }

        public static MedicalEquipment Register(string name, string serialNumber, string manufacturer, DateTime purchasedOnUtc)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Equipment name is required.");
            if (string.IsNullOrWhiteSpace(serialNumber)) throw new DomainException("Serial number is required.");
            return new MedicalEquipment(MedicalEquipmentId.New(), name.Trim(), serialNumber.Trim(), manufacturer.Trim(), purchasedOnUtc);
        }

        public void AllocateTo(string locationType, Guid locationId)
        {
            if (Status is EquipmentStatus.OutOfService or EquipmentStatus.Retired)
                throw new DomainException($"Cannot allocate equipment that is {Status}.");
            CurrentAllocation?.Release();
            _allocations.Add(new AssetAllocation(locationType, locationId));
        }

        public void ReleaseAllocation() => CurrentAllocation?.Release();

        public void LogMaintenance(MaintenanceType type, string performedByStaffId, string notes, DateTime? nextDueUtc = null)
        {
            _maintenanceLogs.Add(new EquipmentMaintenanceLog(type, performedByStaffId, notes, nextDueUtc));
            if (type is MaintenanceType.Repair) Status = EquipmentStatus.UnderMaintenance;
        }

        public void ReturnToService()
        {
            if (Status != EquipmentStatus.UnderMaintenance) throw new DomainException("Equipment is not currently under maintenance.");
            Status = EquipmentStatus.InService;
        }

        public void TakeOutOfService(string reason)
        {
            Status = EquipmentStatus.OutOfService;
            AddDomainEvent(new EquipmentTakenOutOfServiceDomainEvent(Id, reason));
        }

        public void Retire() => Status = EquipmentStatus.Retired;
    }
}
