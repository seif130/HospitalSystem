using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Emergency.AmbulanceDispatch.Enum
{
    public enum DispatchStatus { Requested =1, EnRouteToPickup = 2, PickedUp = 3, EnRouteToHospital = 4, Completed = 5, Cancelled = 6 }

}
