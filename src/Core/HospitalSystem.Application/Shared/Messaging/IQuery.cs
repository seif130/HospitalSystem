using HospitalSystem.Application.Shared.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Shared.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
}
