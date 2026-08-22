using HospitalSystem.Domain.Modules.Scheduling.Appointments;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Policy;
using HospitalSystem.Domain.Modules.Scheduling.ClinicRooms.Policy;
using HospitalSystem.Domain.Modules.Scheduling.Doctors;
using HospitalSystem.Domain.Modules.Scheduling.Doctors.Policy;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Policy
{
    public sealed class SchedulingPolicy
    {
        private readonly DoctorAvailabilityPolicy _doctorAvailabilityPolicy;
        private readonly AppointmentConflictPolicy _appointmentConflictPolicy;
        private readonly ClinicRoomAvailabilityPolicy _roomAvailabilityPolicy;

        public SchedulingPolicy(
            DoctorAvailabilityPolicy doctorAvailabilityPolicy,
            AppointmentConflictPolicy appointmentConflictPolicy,
            ClinicRoomAvailabilityPolicy roomAvailabilityPolicy)
        {
            _doctorAvailabilityPolicy = doctorAvailabilityPolicy;
            _appointmentConflictPolicy = appointmentConflictPolicy;
            _roomAvailabilityPolicy = roomAvailabilityPolicy;
        }

        public void EnsureCanSchedule(
            DateRange requestedPeriod,
            IEnumerable<DoctorSchedule> schedules,
            IEnumerable<DoctorTimeOff> timeOffs,
            IEnumerable<Appointment> doctorAppointments,
            IEnumerable<Appointment> roomAppointments)
        {
            if (!_doctorAvailabilityPolicy.IsAvailable(
                    requestedPeriod,
                    schedules,
                    timeOffs))
            {
                throw new DomainException("Doctor is not available during the requested time.");
            }

            if (_appointmentConflictPolicy.HasConflict(
                    requestedPeriod,
                    doctorAppointments))
            {
                throw new DomainException( "Doctor already has an appointment during the requested time.");
            }

            if (_roomAvailabilityPolicy.HasConflict(
                    requestedPeriod,
                    roomAppointments))
            {
                throw new DomainException("Clinic room is already booked during the requested time.");
            }
        }
    }

}
