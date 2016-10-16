using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Silverlight.Samples;

namespace Microsoft.Silverlight.Samples
{
    public class MessageInspectorChannelFactory : ChannelFactoryBase<IRequestChannel>
    {
        private IChannelFactory<IRequestChannel> innerChannelFactory;
        private IClientMessageInspector messageInspector;

        public MessageInspectorChannelFactory(IChannelFactory<IRequestChannel> innerChannelFactory, IClientMessageInspector messageInspector)
        {
            this.messageInspector = messageInspector;
            this.innerChannelFactory = innerChannelFactory;
        }

        protected override IRequestChannel OnCreateChannel(EndpointAddress to, Uri via)
        {
            IRequestChannel innerchannel = innerChannelFactory.CreateChannel(to, via);
            MessageInspectorChannel clientChannel = new MessageInspectorChannel(this, innerchannel, messageInspector);

            return (IRequestChannel)clientChannel;
        }

        #region Do-nothing methods just so that we don't upset the channel stack
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return innerChannelFactory.BeginOpen(timeout, callback, state);
        }
        protected override void OnEndOpen(IAsyncResult result)
        {
            innerChannelFactory.EndOpen(result);
        }
        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return innerChannelFactory.BeginClose(timeout, callback, state);
        }
        protected override void OnEndClose(IAsyncResult result)
        {
            innerChannelFactory.EndClose(result);
        }
        protected override void OnOpen(TimeSpan timeout)
        {
            innerChannelFactory.Open(timeout);
        }
        protected override void OnAbort()
        {
            innerChannelFactory.Abort();
        }
        protected override void OnClose(TimeSpan timeout)
        {
            innerChannelFactory.Close(timeout);
        }

        public override T GetProperty<T>()
        {
            return innerChannelFactory.GetProperty<T>();
        }

        #endregion
    }

}
