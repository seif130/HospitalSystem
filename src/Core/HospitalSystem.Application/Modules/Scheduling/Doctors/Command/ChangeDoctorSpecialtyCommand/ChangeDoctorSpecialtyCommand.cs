using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorSpecialtyCommand
{
    public sealed record ChangeDoctorSpecialtyCommand(
       Guid DoctorId,
       MedicalSpecialty Specialty)
       : ICommand;

}
