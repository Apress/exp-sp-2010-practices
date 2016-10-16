using Microsoft.Silverlight.Samples;
using System.Diagnostics;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace SilverlightGuidAsmx.Behaviors
{
    public class SPAsmxMessageInspector : IClientMessageInspector
    {
        XNamespace WcfNamespace = "http://schemas.microsoft.com/2003/10/Serialization/";
        XNamespace XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
        XNamespace XsdNamespace = "http://www.w3.org/2001/XMLSchema";

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {

            Debug.WriteLine("Apply receive reply");
            
            XElement body = (XElement)XElement.ReadFrom(reply.GetReaderAtBodyContents());
            body.Add(new XAttribute(XNamespace.Xmlns + "xsd", XsdNamespace));

            XName XsiTypeAttirbute = XsiNamespace + "type";

            IEnumerable<XElement> asmxTypes = body.DescendantsAndSelf().Where<XElement>(
                x => x.Attributes().Any<XAttribute>(
                    y => y.Name.Equals(XsiTypeAttirbute) &&
                         (y.Value.EndsWith(":char") || y.Value.EndsWith(":guid"))));

            if (asmxTypes.Count<XElement>() != 0)//later change this to Any()
            {
                XName WcfNamespaceAttribute = XNamespace.Xmlns + "wcf";

                foreach (XElement e in asmxTypes)
                {
                    e.SetAttributeValue(WcfNamespaceAttribute, WcfNamespace);
                    XAttribute typeAttribute = e.Attribute(XsiTypeAttirbute);
                    typeAttribute.Value = "wcf:" + typeAttribute.Value.Split(':')[1];
                }

            }

            Message newMessage = Message.CreateMessage(reply.Version, null, body.CreateReader());
            newMessage.Headers.CopyHeadersFrom(reply.Headers);
            newMessage.Properties.CopyProperties(reply.Properties);

            reply = newMessage;
             
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            Debug.WriteLine("Before Send Request");
            return null;
        }

        
    }
}
