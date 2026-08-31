using HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.CancelWaitlistCommand;
using HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.ConfirmWaitlistBookingCommand;
using HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.ExpireWaitlistOfferCommand;
using HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.JoinWaitlistCommand;
using HospitalSystem.Application.Modules.Scheduling.Waitlists.Command.OfferWaitlistSlotCommand;
using HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitingByDoctorQuery;
using HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitlistEntryByIdQuery;
using HospitalSystem.Application.Modules.Scheduling.Waitlists.Quries.GetWaitlists;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Enums;
using HospitalSystem.WebApi.Endpoints.Contracts.Scheduling.Waitlist;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
namespace HospitalSystem.WebApi.Endpoints.Scheduling
{

    public static class WaitlistEndpoints
    {
        public static IEndpointRouteBuilder MapWaitlistEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app
                .MapGroup("/api/hospital/scheduling/waitlists")
                .WithTags("Waitlists");

  
            group.MapPost("/", JoinWaitlist);


            group.MapGet("/{id:guid}", GetById);

            group.MapGet("/doctor/{doctorId:guid}", GetWaitingByDoctor);

            group.MapGet("/", GetWaitlists);

            group.MapDelete("/{id:guid}", Cancel);

            group.MapPost("/{id:guid}/offer", OfferSlot);

            group.MapPost("/{id:guid}/confirm", ConfirmBooking);

            group.MapPost("/{id:guid}/expire", ExpireOffer);

            return app;
        }

        private static async Task<IResult> JoinWaitlist(
            JoinWaitlistRequest request,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var command = new JoinWaitlistCommand(
                request.PatientId,
                request.DoctorId,
                request.PreferredFromUtc,
                request.PreferredToUtc);

            var result = await sender.Send(
                command,
                cancellationToken);

            return result.ToCreatedResult(id => $"/api/hospital/scheduling/waitlists/{id}");
        }

        private static async Task<IResult> GetById(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new GetWaitlistByIdQuery(id),
                cancellationToken);

            return result.ToHttpResult();
        }

        private static async Task<IResult> GetWaitingByDoctor(Guid doctorId,
         DateTime fromUtc,DateTime toUtc,ISender sender,CancellationToken cancellationToken)
        {
            var query = new GetWaitingByDoctorQuery(doctorId,fromUtc,toUtc);

            var result = await sender.Send( query,cancellationToken);

            return result.ToHttpResult();
        }

        private static async Task<IResult> GetWaitlists(
            Guid? doctorId,
            Guid? patientId,
            WaitlistEntryStatus? status,
            int page,
            int pageSize,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var query = new GetWaitlistsQuery(
                doctorId,
                patientId,
                status,
                page,
                pageSize);

            var result = await sender.Send(
                query,
                cancellationToken);

            return result.ToHttpResult();
        }

        private static async Task<IResult> Cancel(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new CancelWaitlistCommand(id),
                cancellationToken);

            return result.ToHttpResult();
        }

        private static async Task<IResult> OfferSlot(
            Guid id,
            OfferWaitlistSlotRequest request,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var command = new OfferWaitlistSlotCommand(
                id,
                request.AppointmentId);

            var result = await sender.Send(
                command,
                cancellationToken);

            return result.ToHttpResult();
        }

        private static async Task<IResult> ConfirmBooking(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new ConfirmWaitlistBookingCommand(id),
                cancellationToken);

            return result.ToHttpResult();
        }

        private static async Task<IResult> ExpireOffer(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(
                new ExpireWaitlistOfferCommand(id),
                cancellationToken);

            return result.ToHttpResult();
        }
    }
}
