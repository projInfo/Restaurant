using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Table_Carre : TableJumelable
    {

        private int _cote;
        private XElement _TabCarre;

        public Table_Carre(int nbrPlace, int Cote, XElement tableGen)
            : base(nbrPlace, tableGen)
        {
            _cote=Cote;
            Type = "carré";
            _dim = "Coté : " + _cote;
            _coteJumelable = _cote;
            _TabCarre = tableGen.Element("Jumelable").Element("Carré");
            GenereXml();
            
        }

        public Table_Carre(int id, int nbrPlace, int Cote, XElement tableGen)
            : base(id, nbrPlace, tableGen)
        {
            _cote = Cote;
            Type = "carré";
            _dim = "Coté : " + _cote;
            _coteJumelable = _cote;
            _TabCarre = tableGen.Element("Jumelable").Element("Carré");
            
        }

        protected override void GenereXml()
        {
           
                _TabCarre.Add(new XElement("table", new XElement("ID", Id),
                new XElement("nbrPlace", _nbrPlace), new XElement("Dim", _cote), new XElement("Jum1", _idJumele1), new XElement("Jum2", _idJumele2)));
                          
        }

        public int CoteJumelable
        {
            get { return _cote; }
        }
        
        public int Cote
        {
            get { return _cote; }
            set { _cote = value; }
        }

    }
}
