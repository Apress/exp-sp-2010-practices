using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Silverlight.Samples
{
 
    public interface IClientMessageInspector
    {
        object BeforeSendRequest(ref Message request, IClientChannel channel);
        void AfterReceiveReply(ref Message reply, object correlationState);
    }
}