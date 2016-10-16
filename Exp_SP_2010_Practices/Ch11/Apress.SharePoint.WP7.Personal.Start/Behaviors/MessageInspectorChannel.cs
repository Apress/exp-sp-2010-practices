using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Silverlight.Samples;


namespace Microsoft.Silverlight.Samples
{
    public class MessageInspectorChannel : ChannelBase, IRequestChannel
    {
        private IRequestChannel innerChannel;
        private IClientMessageInspector messageInspector;

        #region Do-nothing properties just so that we don't upset the channel stack
        public Uri Via
        {
            get { return innerChannel.Via; }
        }

        public EndpointAddress RemoteAddress
        {
            get { return innerChannel.RemoteAddress; }
        }

        #endregion

        public MessageInspectorChannel(ChannelManagerBase channelManager, IRequestChannel innerChannel, IClientMessageInspector messageInspector)
            : base(channelManager)
        {
            this.innerChannel = innerChannel;
            this.messageInspector = messageInspector;
        }

        // Plug in IClientMessageInspector in the following three methods
        public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            messageInspector.BeforeSendRequest(ref message, null);
            return innerChannel.BeginRequest(message, timeout, callback, state);
        }

        public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
        {
            return BeginRequest(message, DefaultSendTimeout, callback, state);
        }

        public Message EndRequest(IAsyncResult result)
        {
            Message message = innerChannel.EndRequest(result);
            messageInspector.AfterReceiveReply(ref message, null);
            return message;
        }

        #region Do-nothing methods just so that we don't upset the channel stack


        // No sync methods in Silverlight
        Message IRequestChannel.Request(Message message, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        // No sync methods in Silverlight
        Message IRequestChannel.Request(Message message)
        {
            throw new NotImplementedException();
        }

        protected override void OnAbort()
        {
            innerChannel.Abort();
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return innerChannel.BeginClose(timeout, callback, state);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return innerChannel.BeginOpen(timeout, callback, state);
        }

        // No sync methods in Silverlight
        protected override void OnClose(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndClose(IAsyncResult result)
        {
            innerChannel.EndClose(result);
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            innerChannel.EndOpen(result);
        }

        // No sync methods in Silverlight
        protected override void OnOpen(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
