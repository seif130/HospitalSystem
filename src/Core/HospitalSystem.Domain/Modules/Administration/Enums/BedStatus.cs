using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Modules.Administration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BedStatus
    {
        Available = 1,
        Occupied = 2,
        UnderMaintenance = 3
    }
}
