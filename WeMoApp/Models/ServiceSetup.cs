using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeMoApp.Models
{
    [XmlRoot(ElementName = "specVersion", Namespace = "urn:Belkin:service-1-0")]
    public class ServiceSpecVersion
    {
        [XmlElement(ElementName = "major", Namespace = "urn:Belkin:service-1-0")]
        public string Major { get; set; }
        [XmlElement(ElementName = "minor", Namespace = "urn:Belkin:service-1-0")]
        public string Minor { get; set; }
    }

    [XmlRoot(ElementName = "argument", Namespace = "urn:Belkin:service-1-0")]
    public class Argument
    {
        [XmlElement(ElementName = "retval", Namespace = "urn:Belkin:service-1-0")]
        public string Retval { get; set; }
        [XmlElement(ElementName = "name", Namespace = "urn:Belkin:service-1-0")]
        public string Name { get; set; }
        [XmlElement(ElementName = "relatedStateVariable", Namespace = "urn:Belkin:service-1-0")]
        public string RelatedStateVariable { get; set; }
        [XmlElement(ElementName = "direction", Namespace = "urn:Belkin:service-1-0")]
        public string Direction { get; set; }
    }

    [XmlRoot(ElementName = "argumentList", Namespace = "urn:Belkin:service-1-0")]
    public class ArgumentList
    {
        [XmlElement(ElementName = "argument", Namespace = "urn:Belkin:service-1-0")]
        public List<Argument> Argument { get; set; }
    }

    [XmlRoot(ElementName = "action", Namespace = "urn:Belkin:service-1-0")]
    public class Action
    {
        [XmlElement(ElementName = "name", Namespace = "urn:Belkin:service-1-0")]
        public string Name { get; set; }
        [XmlElement(ElementName = "argumentList", Namespace = "urn:Belkin:service-1-0")]
        public ArgumentList ArgumentList { get; set; }
    }

    [XmlRoot(ElementName = "actionList", Namespace = "urn:Belkin:service-1-0")]
    public class ActionList
    {
        [XmlElement(ElementName = "action", Namespace = "urn:Belkin:service-1-0")]
        public List<Action> Action { get; set; }
    }

    [XmlRoot(ElementName = "stateVariable", Namespace = "urn:Belkin:service-1-0")]
    public class StateVariable
    {
        [XmlElement(ElementName = "name", Namespace = "urn:Belkin:service-1-0")]
        public string Name { get; set; }
        [XmlElement(ElementName = "dataType", Namespace = "urn:Belkin:service-1-0")]
        public string DataType { get; set; }
        [XmlElement(ElementName = "defaultValue", Namespace = "urn:Belkin:service-1-0")]
        public string DefaultValue { get; set; }
        [XmlAttribute(AttributeName = "sendEvents")]
        public string SendEvents { get; set; }
    }

    [XmlRoot(ElementName = "serviceStateTable", Namespace = "urn:Belkin:service-1-0")]
    public class ServiceStateTable
    {
        [XmlElement(ElementName = "stateVariable", Namespace = "urn:Belkin:service-1-0")]
        public List<StateVariable> StateVariable { get; set; }
    }

    [XmlRoot(ElementName = "scpd", Namespace = "urn:Belkin:service-1-0")]
    public class ServiceSetup
    {
        [XmlElement(ElementName = "specVersion", Namespace = "urn:Belkin:service-1-0")]
        public SpecVersion SpecVersion { get; set; }
        [XmlElement(ElementName = "actionList", Namespace = "urn:Belkin:service-1-0")]
        public ActionList ActionList { get; set; }
        [XmlElement(ElementName = "serviceStateTable", Namespace = "urn:Belkin:service-1-0")]
        public ServiceStateTable ServiceStateTable { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
