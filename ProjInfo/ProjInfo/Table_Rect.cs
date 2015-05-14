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
            Type = "Rectangulaire";
            _dim = "Longueur : " + _long + "\nLargeur : " + _large;
            _coteJumelable=_large;
            //_TabRect = tableGen.Element("Jumelable").Element("Rectangulaire");
            GenereXml();
            
        }

        public Table_Rect(int id, int nbrPlace, int longu, int large, XElement tableGen)
            : base(id, nbrPlace, tableGen)
        {
            _long = longu;
            _large = large;
            Type = "Rectangulaire";
            _dim = "Longueur : " + _long + "\nLargeur : " + _large;
            _coteJumelable = _large;
            _TabRect = tableGen.Element("Jumelable").Element("Rectangulaire");
            
        }

        protected override void GenereXml()
        {
            _table = new XElement("table", new XElement("ID", Id),
                new XElement("nbrPlace", _nbrPlace), new XElement("long", _long), new XElement("large", _large));
                _tabGen.Add(_table);
            
        }

        public int CoteJumelable
        {
            get { return _large; }
        }
                
    }
}
