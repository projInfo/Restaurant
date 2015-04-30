using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Table_Ronde : Table
    {
        private int _diam;
        private XElement _TabRonde;

        public Table_Ronde(int nbrPlace, int Diam, XElement tableGen) : base (nbrPlace, tableGen)
        {
            _diam = Diam;
            _type = "ronde";
            _dim = "Diam : " + _diam;
            _TabRonde = tableGen.Element("Non_Jumelable").Element("Ronde");
            GenereXml(true);
            
        }

        public Table_Ronde(int id, int nbrPlace, int Diam, XElement tableGen)
            : base(id, nbrPlace, tableGen)
        {
            _diam = Diam;
            _type = "ronde";
            _dim = "Diam : " + _diam;
            _TabRonde = tableGen.Element("Non_Jumelable").Element("Ronde");
            
        }

        protected override void GenereXml(bool dispo)
        {
            if (dispo==true)
            {
                ;
                _TabRonde.Element("Disponible").Add(new XElement("table", new XElement("ID", Id),
                    new XElement("nbrPlace", _nbrPlace), new XElement("Diam", _diam)));
            }
            else
            {
                _TabRonde.Element("Utilisée").Add(new XElement("table", new XElement("ID", Id),
                    new XElement("nbrPlace", _nbrPlace), new XElement("Diam", _diam)));
            }
        }

    }
}
