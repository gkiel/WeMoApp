using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace WeMoApp.Utilities
{
    class SoapRequestHelper
    {
        public static string BuildSoapRequest(string actionName, string serviceType, string controlURL, params Tuple<string, object> [] arguments)
        {
            string argList = "";
            foreach(Tuple<string, object> arg in arguments)
            {
                argList += string.Format("<{0}>{1}</{0}>", arg.Item1, arg.Item2);
            }
            string soapBody = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
"<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
  "<s:Body>" +
    "<u:{0} xmlns:u=\"{1}\">" +
       "{2}" +
    "</u:{0}>" +
  "</s:Body>" +
"</s:Envelope>";

            string soapRequest = string.Format(soapBody, actionName, serviceType, argList);

            return soapRequest;
        }

        async public static Task<HttpResponseMessage> MakeRequest(string hostname, int port, string actionName, string serviceType, string controlURL, params Tuple<string, object> [] arguments)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new HttpProductInfoHeaderValue("CyberGarage-HTTP", "1.0"));
            httpClient.DefaultRequestHeaders.Add("SOAPACTION", string.Format("\"{0}#{1}\"", serviceType, actionName));
            httpClient.DefaultRequestHeaders.Add("HOST", hostname);
            string soapBody = BuildSoapRequest(actionName, serviceType, controlURL, arguments);
            HttpStringContent content = new HttpStringContent(soapBody);
            content.Headers.ContentType.CharSet = "utf-8";
            content.Headers.ContentType.MediaType = "text/xml";
            content.Headers.ContentLength = (ulong)soapBody.Length;

            Uri uri = new UriBuilder("http://", hostname, port, controlURL).Uri;
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            return response;
        }
    }
}
