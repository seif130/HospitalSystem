using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Assets.EquipmentMaintenanceLog
{
    public sealed class EquipmentMaintenanceLog
    {
        public MaintenanceType Type { get; }
        public string PerformedByStaffId { get; }
        public string Notes { get; }
        public DateTime PerformedOnUtc { get; }
        public DateTime? NextDueUtc { get; }

        internal EquipmentMaintenanceLog(MaintenanceType type, string performedByStaffId, string notes, DateTime? nextDueUtc)
        {
            if (string.IsNullOrWhiteSpace(notes)) throw new DomainException("Maintenance notes are required.");
            Type = type;
            PerformedByStaffId = performedByStaffId;
            Notes = notes.Trim();
            PerformedOnUtc = DateTime.UtcNow;
            NextDueUtc = nextDueUtc;
        }
    }

}
