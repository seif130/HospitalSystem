using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Assets.AssetAllocation
{
    public sealed class AssetAllocation
    {
        public string LocationType { get; } // "Department", "Ward", "ClinicRoom"
        public Guid LocationId { get; }
        public DateTime AllocatedOnUtc { get; }
        public DateTime? ReleasedOnUtc { get; private set; }

        internal AssetAllocation(string locationType, Guid locationId)
        {
            if (string.IsNullOrWhiteSpace(locationType)) throw new DomainException("Location type is required.");
            LocationType = locationType;
            LocationId = locationId;
            AllocatedOnUtc = DateTime.UtcNow;
        }

        internal void Release() => ReleasedOnUtc = DateTime.UtcNow;
        public bool IsActive => !ReleasedOnUtc.HasValue;
    }

}
