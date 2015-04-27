using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Table_Rect : TableJumelable
    {
        private int _long, _large;
        private XElement _TabRect;

        public Table_Rect(int nbrPlace, int longu, int large, XElement tableGen)
            : base(nbrPlace, tableGen)
        {
            _long = longu;
            _large = large;
            _type = "Rectangle";
            _dim = "Longueur : " + _long + "\nLargeur : " + _large;
            _coteJumelable=_large;
            _TabRect = tableGen.Element("Jumelable").Element("Rectangulaire").Element("Disponible");
            _TabRect.Add(new XElement("table", new XAttribute("ID", Id),
                new XElement("nbrPlace", nbrPlace), new XElement("Dim", _long+"x"+_large)));
        }

        public int CoteJumelable
        {
            get { return _large; }
        }
                
    }
}
