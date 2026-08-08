using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HospitalSystem.Domain.Modules.Administration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoomType
    {
        GeneralWard = 1,
        PrivateRoom = 2,
        ICU = 3,
        OperationTheater = 4,
        Emergency = 5
    }
}
