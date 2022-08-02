﻿using MediatR;
using System.ComponentModel;

namespace HangfirePOC.Infrastructure
{
    public class MediatRBridge
    {
        private readonly IMediator _mediator;

        public MediatRBridge(IMediator mediator)
        {
            _mediator = mediator;
        }

        [DisplayName("{0}")]
        public async Task Send(string jobIdentifier, IRequest request)
        {
            await _mediator.Send(request);
        }

        [DisplayName("{0}")]
        public async Task Send<T>(string jobIdentifier, IRequest<T> request)
        {
            await _mediator.Send(request);
        }
    }
}
