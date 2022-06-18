using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SerializarXml.ModelSerialization
{
    public class InfProcNFe
    {
        
        [XmlAttribute("versao")]
        public string infVersao { get; set; }

        [XmlElement(ElementName = "infProt")]
        public InfProt InformacoesNFe { get; set; }

        public class InfProt
        {
            [XmlElement("chNFe")]
            public string chNFe { get; set; }
        }
    }
}
