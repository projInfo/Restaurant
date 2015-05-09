using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Cuisinier : Employe
    {
        private XElement _cuis;

        public Cuisinier(string nom, string prenom, XElement emp ) : base(nom, prenom, emp)
        {
            _cuis = emp.Element("Cuisinier");
            _cuis.Add(new XElement("Employe", new XElement("ID", Id), new XElement("Nom", _nom), new XElement("Prenom", _prenom)));
            _type = "Cuisinier";
        }

        public Cuisinier(string nom, string prenom, XElement emp, int id)
            : base(nom, prenom, emp, id)
        {
            _cuis = emp.Element("Serveur");
            _type = "Cuisinier";
        }
    }
}
