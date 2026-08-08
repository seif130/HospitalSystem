using HospitalSystem.Application.Modules.Administration.Departments.Common;
using HospitalSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Administration.Departments.Commands.AddEquipmentCommand
{
    public sealed record AddEquipmentCommand(Guid DepartmentId, string EquipmentName, string SerialNumber, DateTime PurchaseDate) : IRequest<Result<Guid>>;

}
