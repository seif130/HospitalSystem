using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.LabOrder.Enums
{
    public enum LabOrderStatus { Requested = 1, SpecimenCollected =2, InProgress = 3, Completed = 4, Cancelled = 5 }
}
