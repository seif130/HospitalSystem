using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddServiceCommand
{
    public class AddServiceCommandHandler : IRequestHandler<AddServiceCommand, Result<Guid>>
    {
        private readonly IHospitalDbContext _context;

        public AddServiceCommandHandler(IHospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(AddServiceCommand request, CancellationToken cancellationToken)
        {
            var department = await _context.Departments
                .Include(d => d.Services)
                .FirstOrDefaultAsync(d => d.Id == request.DepartmentId, cancellationToken);

            if (department is null)
                return Result<Guid>.Fail(Error.NotFound("Department.NotFound", "Department was not found."));

            var moneyResult = Money.Create(request.PriceAmount, request.Currency);
            if (!moneyResult.IsSuccess)
                return Result<Guid>.Fail(moneyResult.Errors);

            var serviceResult = department.AddService(request.ServiceName, request.Description, moneyResult.Data);
            if (!serviceResult.IsSuccess)
                return Result<Guid>.Fail(serviceResult.Errors);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Ok(serviceResult.Data.Id);
        }
    }
}
