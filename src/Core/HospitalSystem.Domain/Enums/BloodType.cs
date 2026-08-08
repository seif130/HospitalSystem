using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BloodType
    {
        APositive = 1,   // A+
        ANegative = 2,   // A-
        BPositive = 3,   // B+
        BNegative = 4,   // B-
        ABPositive = 5,  // AB+
        ABNegative = 6,  // AB-
        OPositive = 7,   // O+
        ONegative = 8    // O-
    }
}
