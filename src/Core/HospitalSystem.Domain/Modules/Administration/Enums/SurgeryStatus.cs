using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Modules.Administration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SurgeryStatus
    {
        Scheduled = 1,
        InProgress = 2,
        Completed = 3,
        Canceled = 4
    }
}
