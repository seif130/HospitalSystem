using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Modules.Administration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AmbulanceStatus
    {
        Available = 1,
        OnMission = 2,
        UnderMaintenance = 3,
        OutOfService = 4
    }
}
