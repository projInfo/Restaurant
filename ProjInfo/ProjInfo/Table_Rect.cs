using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Table_Rect : Table
    {
        private int _long, _large;

        public Table_Rect(int nbrPlace, int longu, int large, XElement tableGen)
            : base(nbrPlace, tableGen)
        {
            _long = longu;
            _large = large;
            _type = "Rectangle";
            _dim = "Longueur : " + _long + "\nLargeur : " + _large;
        }

    }
}
