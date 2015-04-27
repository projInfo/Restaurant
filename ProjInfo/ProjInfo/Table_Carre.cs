﻿using System;
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
            _type = "carré";
            _dim = "Coté : " + _cote;
            _coteJumelable = _cote;
            _TabCarre = tableGen.Element("Non_Jumelable").Element("Ronde").Element("Disponible");
            _TabCarre.Add(new XElement("table", new XAttribute("ID", Id),
                new XElement("nbrPlace", nbrPlace), new XElement("Dim", Cote)));
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
