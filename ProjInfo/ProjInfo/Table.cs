using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjetInfo_S2
{
    class Table
    {
        protected int _nbrPlace, _id=0;
        protected bool _estJumele, _estDispo;
        protected static int _CompteTable=0;
        protected string _type, _dim;
        protected XDocument _doc;
        protected XElement _tabGen;
        public Table(int nbrPlace, XElement tableGen)    
        {
            _nbrPlace = nbrPlace;
            _CompteTable++;
            _id = _CompteTable;
            _tabGen = tableGen;
            
        }
        
        public override string ToString()
        {
            string ch = "table de type : " + _type + "\n" + _dim + "\nNbr de places : " + _nbrPlace;
            return ch;
        }

        #region Accesseur
        protected int NbrPlace
        {
            get { return _nbrPlace; }
            set { _nbrPlace = value; }
        }

        public bool EstDispo
        {
            get { return _estDispo; }
            set { _estDispo = value; }
        }

        public bool EstJumele
        {
            get { return _estJumele; }
            set { _estJumele = value; }
        }
        #endregion

    }
}
