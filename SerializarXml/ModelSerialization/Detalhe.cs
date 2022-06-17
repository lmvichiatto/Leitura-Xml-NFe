using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SerializarXml.ModelSerialization
{
    public class Detalhe
    {
        [XmlAttribute("nItem")]
        public int nItem { get; set; }
        [XmlElement("infAdProd")]
        public string infAdProd2 { get; set; }

        [XmlElement("prod")]
        public Produto Produto { get; set; }

        //[XmlElement("imposto")]
        //public Imposto Imposto { get; set; }

        
    }
}
