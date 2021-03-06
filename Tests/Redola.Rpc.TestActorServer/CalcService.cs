﻿using System.Collections.Generic;
using Logrila.Logging;
using Redola.Rpc.TestContracts;

namespace Redola.Rpc.TestActorServer
{
    internal class CalcService : RpcHandler, ICalcService
    {
        private ILog _log = Logger.Get<CalcService>();

        public CalcService(RpcActor localActor)
            : base(localActor)
        {
        }

        public CalcService(RpcActor localActor, IRateLimiter rateLimiter)
            : base(localActor, rateLimiter)
        {
        }

        protected override IEnumerable<RpcMessageContract> RegisterRpcMessageContracts()
        {
            var messages = new List<RpcMessageContract>();

            messages.Add(new ReceiveMessageContract(typeof(AddRequest)));

            return messages;
        }

        private void OnAddRequest(ActorSender sender, ActorMessageEnvelope<AddRequest> request)
        {
            var response = new ActorMessageEnvelope<AddResponse>()
            {
                CorrelationID = request.MessageID,
                CorrelationTime = request.MessageTime,
                Message = Add(request.Message),
            };

            this.BeginReply(sender.ChannelIdentifier, response);
        }

        public AddResponse Add(AddRequest request)
        {
            var response = new AddResponse() { Result = request.X + request.Y };
            _log.DebugFormat("Add, X={0}, Y={1}, Result={2}", request.X, request.Y, response.Result);
            return response;
        }
    }
}
