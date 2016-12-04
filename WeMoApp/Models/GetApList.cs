using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeMoApp.Models
{
    [XmlRoot(ElementName = "GetApListResponse", Namespace = "urn:Belkin:service:WiFiSetup:1")]
    public class GetApListResponse
    {
        [XmlElement(ElementName = "ApList")]
        public string ApList { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Body
    {
        [XmlElement(ElementName = "GetApListResponse", Namespace = "urn:Belkin:service:WiFiSetup:1")]
        public GetApListResponse GetApListResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class GetApList
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }
    }
}
