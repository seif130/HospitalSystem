using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Modules.Administration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EquipmentStatus
    {
        Operational = 1,
        UnderMaintenance = 2,
        OutOfService = 3
    }
}
