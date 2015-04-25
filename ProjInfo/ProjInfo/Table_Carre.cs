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

        public Table_Carre(int nbrPlace, int Cote, XElement tableGen)
            : base(nbrPlace, tableGen)
        {
            _cote=Cote;
            _type = "carré";
            _dim = "Coté : " + _cote;
        }

    }
}
