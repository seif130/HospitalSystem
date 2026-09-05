using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestsByDepartmentQuery
{
    public sealed class GetPurchaseRequestsByDepartmentHandler(IPurchaseRequestRepository requests)
        : IQueryHandler<GetPurchaseRequestsByDepartmentQuery, PaginatedList<PurchaseRequestDto>>
    {
        public async Task<Result<PaginatedList<PurchaseRequestDto>>> Handle(
            GetPurchaseRequestsByDepartmentQuery request,CancellationToken cancellationToken)
        {
            var (requestsList, total) = await requests.GetByDepartmentAsync(
                request.DepartmentId,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var items = requestsList
                .Select(purchaseRequest => purchaseRequest.ToDto())
                .ToList();

            var result = new PaginatedList<PurchaseRequestDto>(
                items,
                total,
                request.PageNumber,
                request.PageSize);

            return Result.Success(result);
        }
    }
}
