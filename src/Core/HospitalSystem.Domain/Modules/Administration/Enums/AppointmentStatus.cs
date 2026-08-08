using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Modules.Administration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AppointmentStatus
    {
        Scheduled = 1,
        Completed = 2,
        Canceled = 3,
        NoShow = 4
    }
}
