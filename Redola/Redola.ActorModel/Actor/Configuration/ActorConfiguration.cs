﻿namespace Redola.ActorModel
{
    public abstract class ActorConfiguration
    {
        private ActorIdentity _localActor;
        private ActorChannelConfiguration _channelConfiguration;

        public ActorConfiguration()
        {
        }
        
        public ActorIdentity LocalActor { get { return _localActor; } }
        public ActorChannelConfiguration ChannelConfiguration { get { return _channelConfiguration; } }

        public ActorConfiguration Build()
        {
            _localActor = BuildLocalActor();
            _channelConfiguration = BuildChannelConfiguration();

            return this;
        }
        
        protected abstract ActorIdentity BuildLocalActor();

        protected virtual ActorChannelConfiguration BuildChannelConfiguration()
        {
            return new ActorChannelConfiguration();
        }
    }
}
