using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeMoApp.Models
{
    [XmlRoot(ElementName = "specVersion", Namespace = "urn:Belkin:device-1-0")]
    public class SpecVersion
    {
        [XmlElement(ElementName = "major", Namespace = "urn:Belkin:device-1-0")]
        public string Major { get; set; }
        [XmlElement(ElementName = "minor", Namespace = "urn:Belkin:device-1-0")]
        public string Minor { get; set; }
    }

    [XmlRoot(ElementName = "icon", Namespace = "urn:Belkin:device-1-0")]
    public class Icon
    {
        [XmlElement(ElementName = "mimetype", Namespace = "urn:Belkin:device-1-0")]
        public string Mimetype { get; set; }
        [XmlElement(ElementName = "width", Namespace = "urn:Belkin:device-1-0")]
        public string Width { get; set; }
        [XmlElement(ElementName = "height", Namespace = "urn:Belkin:device-1-0")]
        public string Height { get; set; }
        [XmlElement(ElementName = "depth", Namespace = "urn:Belkin:device-1-0")]
        public string Depth { get; set; }
        [XmlElement(ElementName = "url", Namespace = "urn:Belkin:device-1-0")]
        public string Url { get; set; }
    }

    [XmlRoot(ElementName = "iconList", Namespace = "urn:Belkin:device-1-0")]
    public class IconList
    {
        [XmlElement(ElementName = "icon", Namespace = "urn:Belkin:device-1-0")]
        public Icon Icon { get; set; }
    }

    [XmlRoot(ElementName = "service", Namespace = "urn:Belkin:device-1-0")]
    public class Service
    {
        [XmlElement(ElementName = "serviceType", Namespace = "urn:Belkin:device-1-0")]
        public string ServiceType { get; set; }
        [XmlElement(ElementName = "serviceId", Namespace = "urn:Belkin:device-1-0")]
        public string ServiceId { get; set; }
        [XmlElement(ElementName = "controlURL", Namespace = "urn:Belkin:device-1-0")]
        public string ControlURL { get; set; }
        [XmlElement(ElementName = "eventSubURL", Namespace = "urn:Belkin:device-1-0")]
        public string EventSubURL { get; set; }
        [XmlElement(ElementName = "SCPDURL", Namespace = "urn:Belkin:device-1-0")]
        public string SCPDURL { get; set; }
    }

    [XmlRoot(ElementName = "serviceList", Namespace = "urn:Belkin:device-1-0")]
    public class ServiceList
    {
        [XmlElement(ElementName = "service", Namespace = "urn:Belkin:device-1-0")]
        public List<Service> Service { get; set; }
    }

    [XmlRoot(ElementName = "device", Namespace = "urn:Belkin:device-1-0")]
    public class Device
    {
        [XmlElement(ElementName = "deviceType", Namespace = "urn:Belkin:device-1-0")]
        public string DeviceType { get; set; }
        [XmlElement(ElementName = "friendlyName", Namespace = "urn:Belkin:device-1-0")]
        public string FriendlyName { get; set; }
        [XmlElement(ElementName = "manufacturer", Namespace = "urn:Belkin:device-1-0")]
        public string Manufacturer { get; set; }
        [XmlElement(ElementName = "manufacturerURL", Namespace = "urn:Belkin:device-1-0")]
        public string ManufacturerURL { get; set; }
        [XmlElement(ElementName = "modelDescription", Namespace = "urn:Belkin:device-1-0")]
        public string ModelDescription { get; set; }
        [XmlElement(ElementName = "modelName", Namespace = "urn:Belkin:device-1-0")]
        public string ModelName { get; set; }
        [XmlElement(ElementName = "modelNumber", Namespace = "urn:Belkin:device-1-0")]
        public string ModelNumber { get; set; }
        [XmlElement(ElementName = "modelURL", Namespace = "urn:Belkin:device-1-0")]
        public string ModelURL { get; set; }
        [XmlElement(ElementName = "serialNumber", Namespace = "urn:Belkin:device-1-0")]
        public string SerialNumber { get; set; }
        [XmlElement(ElementName = "UDN", Namespace = "urn:Belkin:device-1-0")]
        public string UDN { get; set; }
        [XmlElement(ElementName = "UPC", Namespace = "urn:Belkin:device-1-0")]
        public string UPC { get; set; }
        [XmlElement(ElementName = "macAddress", Namespace = "urn:Belkin:device-1-0")]
        public string MacAddress { get; set; }
        [XmlElement(ElementName = "firmwareVersion", Namespace = "urn:Belkin:device-1-0")]
        public string FirmwareVersion { get; set; }
        [XmlElement(ElementName = "iconVersion", Namespace = "urn:Belkin:device-1-0")]
        public string IconVersion { get; set; }
        [XmlElement(ElementName = "binaryState", Namespace = "urn:Belkin:device-1-0")]
        public string BinaryState { get; set; }
        [XmlElement(ElementName = "iconList", Namespace = "urn:Belkin:device-1-0")]
        public IconList IconList { get; set; }
        [XmlElement(ElementName = "serviceList", Namespace = "urn:Belkin:device-1-0")]
        public ServiceList ServiceList { get; set; }
        [XmlElement(ElementName = "presentationURL", Namespace = "urn:Belkin:device-1-0")]
        public string PresentationURL { get; set; }
    }

    [XmlRoot(ElementName = "root", Namespace = "urn:Belkin:device-1-0")]
    public class DeviceSetup
    {
        [XmlElement(ElementName = "specVersion", Namespace = "urn:Belkin:device-1-0")]
        public SpecVersion SpecVersion { get; set; }
        [XmlElement(ElementName = "device", Namespace = "urn:Belkin:device-1-0")]
        public Device Device { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
