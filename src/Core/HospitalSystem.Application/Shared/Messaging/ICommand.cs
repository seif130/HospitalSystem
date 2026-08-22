using HospitalSystem.Application.Shared.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Shared.Messaging
{
        public interface ICommand : IRequest<Result>;
        public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
    
}
