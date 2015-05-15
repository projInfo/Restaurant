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

        public Table_Ronde(int nbrPlace, int Diam, XElement tableGen) : base (nbrPlace, tableGen)
        {
            _diam = Diam;
            Type = "Ronde";
            _dim = "Diam : " + _diam;
            GenereXml();
            
        }

        public Table_Ronde(int id, int nbrPlace, int Diam, XElement tableGen)
            : base(id, nbrPlace, tableGen)
        {
            _diam = Diam;
            Type = "Ronde";
            _dim = "Diam : " + _diam;            
        }

        /*===========================================================
         * private void GenereXml()
         * Role : Ajoute les informations de la table dans le fichier XML
         * ==========================================================*/
        protected override void GenereXml()
        {
            base.GenereXml();
            _table = new XElement("table", new XElement("ID", Id),
                    new XElement("nbrPlace", _nbrPlace), new XElement("Diam", _diam));
            _tabGen.Add(_table); 

        }
        
    }
}
