using HospitalSystem.WebApi.Endpoints.Scheduling;

namespace HospitalSystem.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSchedulingPresentation(this IServiceCollection services)
        {
            return services;
        }

        public static IEndpointRouteBuilder MapSchedulingEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapAppointmentsEndpoints();
            app.MapDepartmentsEndpoints();
            app.MapClinicRoomsEndpoints();
            app.MapDoctorsEndpoints();
            app.MapWaitlistsEndpoints();

            return app;
        }
    }
}
