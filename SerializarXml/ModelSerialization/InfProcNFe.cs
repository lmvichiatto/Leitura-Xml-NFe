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
        public InfProtNFe InformacoesNFe { get; set; }

        public class InfProtNFe
        {
            [XmlElement("chNFe")]
            public string chNFe { get; set; }
        }

        [XmlElement(ElementName = "Signature")]
        public Signature Assinatura {get; set;}

    }
}
