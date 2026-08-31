using HospitalSystem.WebApi.Endpoints.Scheduling;

namespace HospitalSystem.WebApi.Modules.Scheduling
{
    public static class SchedulingModule
    {
        public static IEndpointRouteBuilder MapSchedulingEndpoints(
            this IEndpointRouteBuilder app)
        {
            app.MapAppointmentEndpoints();
            app.MapDepartmentEndpoints();
            app.MapClinicRoomEndpoints();
            app.MapWaitlistEndpoints();
            app.MapSpecialtyEndpoints();
            app.MapDoctorEndpoints();

            return app;
        }
    }
}
