using System;
using System.ServiceModel.Channels;
using Microsoft.Silverlight.Samples;

namespace Microsoft.Silverlight.Samples
{
    public class MessageInspectorBindingElement : BindingElement
    {
        public IClientMessageInspector MessageInspector { get; set; }

        public MessageInspectorBindingElement()
        {
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (!this.CanBuildChannelFactory<TChannel>(context))
            {
                throw new InvalidOperationException("Unsupported channel type");
            }
            

            MessageInspectorChannelFactory factory = new MessageInspectorChannelFactory(
                context.BuildInnerChannelFactory<IRequestChannel>(), MessageInspector);

            return (IChannelFactory<TChannel>)factory;

        }

        #region Do-nothing methods just so that we don't upset the channel stack

        public override BindingElement Clone()
        {
            return new MessageInspectorBindingElement() { MessageInspector = this.MessageInspector };
        }

        public override T GetProperty<T>(BindingContext context)
        {
            return context.GetInnerProperty<T>();
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            return context.CanBuildInnerChannelFactory<TChannel>();
        }

        #endregion

    }
}
