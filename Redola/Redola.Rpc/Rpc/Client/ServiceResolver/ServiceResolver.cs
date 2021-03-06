﻿using System;

namespace Redola.Rpc
{
    public class ServiceResolver : IServiceResolver
    {
        private IServiceRetriever _retriever;
        private IServiceLoadBalancingStrategy _defaultStrategy = new RandomServiceLoadBalancingStrategy();

        public ServiceResolver(IServiceRetriever retriever)
        {
            if (retriever == null)
                throw new ArgumentNullException("retriever");
            _retriever = retriever;
        }

        public ServiceActor Resolve(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var services = _retriever.Retrieve(serviceType);
            return _defaultStrategy.Select(services);
        }

        public ServiceActor Resolve(Type serviceType, IServiceLoadBalancingStrategy strategy)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");
            if (strategy == null)
                throw new ArgumentNullException("strategy");

            var services = _retriever.Retrieve(serviceType);
            return strategy.Select(services);
        }
    }
}
