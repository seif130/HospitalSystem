using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Modules.Administration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NurseStatus
    {
        Active = 1,
        OnLeave = 2,
        Inactive = 3
    }
}
