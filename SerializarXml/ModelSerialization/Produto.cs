using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializarXml.ModelSerialization
{
    public class Produto
    {
        public string cProd { get; set; }
        public string cEAN { get; set; }
        public string xProd { get; set; }        
        public string NCM { get; set; }
        public string CEST { get; set; }
        public string CFOP { get; set; }
        public string uCom { get; set; }
        public double qCom { get; set; }
        public double vUnCom { get; set; }
        public double vProd { get; set; }
        public string cEANTrib { get; set; }
        public string uTrib { get; set; }
        public double qTrib { get; set; }
        public double vUnTrib { get; set; }
        public string xPed { get; set; }
        public string nFCI { get; set; }
    }
}
