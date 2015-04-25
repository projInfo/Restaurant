using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Table_Carre : Table
    {

        private int _cote;
        private XElement _TabCarre;

        public Table_Carre(int nbrPlace, int Cote, XElement tableGen)
            : base(nbrPlace, tableGen)
        {
            _cote=Cote;
            _type = "carré";
            _dim = "Coté : " + _cote;
            _TabCarre = tableGen.Element("Non_Jumelable").Element("Ronde").Element("Disponible");
            _TabCarre.Add(new XElement("table", new XAttribute("ID", _id),
                new XElement("nbrPlace", nbrPlace), new XElement("Dim", Cote)));
        }

    }
}
