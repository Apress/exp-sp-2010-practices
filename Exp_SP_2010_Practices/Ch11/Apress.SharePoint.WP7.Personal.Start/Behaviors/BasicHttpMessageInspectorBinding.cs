using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Silverlight.Samples;

namespace Microsoft.Silverlight.Samples
{
    public class BasicHttpMessageInspectorBinding : BasicHttpBinding
    {
        private MessageInspectorBindingElement channelBindingElement;

        public BasicHttpMessageInspectorBinding(IClientMessageInspector messageInspector) 
        {
            channelBindingElement = new MessageInspectorBindingElement();
            channelBindingElement.MessageInspector = messageInspector;
        }

        public override BindingElementCollection CreateBindingElements()
        {
            BindingElementCollection bindingElements = base.CreateBindingElements();
            bindingElements.Insert(bindingElements.Count - 1, channelBindingElement);

            return bindingElements;
        }
    }
}
