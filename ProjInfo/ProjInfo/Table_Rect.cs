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
            _TabRect = tableGen.Element("Jumelable").Element("Rectangulaire");
            GenereXml(true);
            
        }

        public Table_Rect(int id, int nbrPlace, int longu, int large, XElement tableGen)
            : base(id, nbrPlace, tableGen)
        {
            _long = longu;
            _large = large;
            _type = "Rectangle";
            _dim = "Longueur : " + _long + "\nLargeur : " + _large;
            _coteJumelable = _large;
            _TabRect = tableGen.Element("Jumelable").Element("Rectangulaire");
            
        }

        protected override void GenereXml(bool dispo)
        {
            if (dispo == true)
            {
                
                _TabRect.Element("Disponible").Add(new XElement("table", new XElement("ID", Id),
                    new XElement("nbrPlace", _nbrPlace), new XElement("long", _long), new XElement("large", _large)));
            }
            else
            {
               
                _TabRect.Element("Utilisée").Add(new XElement("table", new XElement("ID", Id),
                    new XElement("nbrPlace", _nbrPlace), new XElement("long", _long), new XElement("large", _large)));
            }
        }

        public int CoteJumelable
        {
            get { return _large; }
        }
                
    }
}
