using System;
using System.Windows.Data;
using System.Net;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Apress.SharePoint.WP7.Personal

{
    public class FBA
    {
        #region Properties

        public event EventHandler<FBAAuthenticatedEventArgs> OnAuthenticated;
        public event EventHandler OnFailedAuthentication;

        private CookieContainer cookieJar = new CookieContainer();

        #endregion

        public void Authenticate(string userId, string pwd, string authServiceURL)
        {
            System.Uri authServiceUri = new Uri(authServiceURL);
            AuthHTTPWebRequest authRequest = new AuthHTTPWebRequest();

            HttpWebRequest spAuthReq = HttpWebRequest.Create(authServiceURL) as HttpWebRequest;
            authRequest.req = spAuthReq;
            authRequest.req.CookieContainer = cookieJar;
            authRequest.req.Headers["SOAPAction"] = "http://schemas.microsoft.com/sharepoint/soap/Login";
            authRequest.req.ContentType = "text/xml; charset=utf-8";
            authRequest.req.Method = "POST";
            authRequest.userId = userId;
            authRequest.pwd = pwd;

            authRequest.req.BeginGetRequestStream(new AsyncCallback(AuthReqCallBack), authRequest);
        }

        private void AuthReqCallBack(IAsyncResult asyncResult)
        {
            string envelope =
                    @"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                      <soap:Body>
                        <Login xmlns=""http://schemas.microsoft.com/sharepoint/soap/"">
                          <username>{0}</username>
                          <password>{1}</password>
                        </Login>
                      </soap:Body>
                    </soap:Envelope>";

            UTF8Encoding encoding = new UTF8Encoding();
            AuthHTTPWebRequest request = (AuthHTTPWebRequest)asyncResult.AsyncState;
            Stream _body = request.req.EndGetRequestStream(asyncResult);
            envelope = string.Format(envelope, request.userId, request.pwd);
            byte[] formBytes = encoding.GetBytes(envelope);

            _body.Write(formBytes, 0, formBytes.Length);
            _body.Close();

            request.req.BeginGetResponse(new AsyncCallback(AuthCallback), request);
        }

        private void AuthCallback(IAsyncResult asyncResult)
        {
            string authResut = "";

            AuthHTTPWebRequest request = (AuthHTTPWebRequest)asyncResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.req.EndGetResponse(asyncResult);
            Stream responseStream = response.GetResponseStream();

            if (request != null && response != null)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    XElement results = XElement.Load(responseStream);
                    XNamespace n = "http://schemas.microsoft.com/sharepoint/soap/";

                    var r = from result in results.Descendants(n + "ErrorCode")
                            select result.Value;

                    authResut = r.ToList().FirstOrDefault<String>();
                }
            }

            if (authResut == "NoError")
            {
                //Error handing omitted for clarity
                EventHandler<FBAAuthenticatedEventArgs> authenticated = OnAuthenticated;
                if (authenticated != null)
                {
                    authenticated(this, new FBAAuthenticatedEventArgs(cookieJar));
                    
                }
            }
            else
            {
                //Error handing omitted for clarity
                EventHandler failedAuth = OnFailedAuthentication;
                if (failedAuth != null)
                {
                    failedAuth(this, null);
                }
            }

            
        }

        
    }

    public class FBAAuthenticatedEventArgs : EventArgs
    {
        public CookieContainer CookieJar { get; private set; }

        public FBAAuthenticatedEventArgs(CookieContainer c)
        {
            CookieJar = c;
        }
    }

    internal class AuthHTTPWebRequest 
    {
        public AuthHTTPWebRequest() { }
        public HttpWebRequest req;
        public string userId;
        public string pwd;
    }
}