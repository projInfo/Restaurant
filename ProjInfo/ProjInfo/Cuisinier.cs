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
            _emp = emp.Element("Cuisinier");
            _empCourant = new XElement("Employe", new XElement("ID", Id), new XElement("Nom", _nom), new XElement("Prenom", _prenom), new XElement("Charge", Charge));
            _emp.Add(_empCourant);
            _type = "Cuisinier";
            Charge = 90;
        }

        public Cuisinier(string nom, string prenom, XElement emp, int id, int charge)
            : base(nom, prenom, emp, id, charge)
        {
            _emp = emp.Element("Cuisinier");
            var employe = from a in _emp.Descendants("Employe")
                          select a;

            foreach (XElement e in employe)
            {
                if (int.Parse(e.Element("ID").Value) == _id)
                    _empCourant = e;
            } 
            _type = "Cuisinier";
        }
    }
}
