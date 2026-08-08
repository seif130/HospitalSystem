using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Modules.Administration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShiftType
    {
        Morning = 1,
        Evening = 2,
        Night = 3
    }
}
