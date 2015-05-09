using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Serveur : Employe
    {
        private XElement _serv;

        public Serveur(string nom, string prenom, XElement emp ) : base(nom, prenom, emp)
        {
            
            _serv = emp.Element("Serveur");
            _serv.Add(new XElement("Employe", new XElement("ID", Id), new XElement("Nom", _nom),
                new XElement("Prenom", _prenom)));
            _type = "Serveur";
        }

        public Serveur(string nom, string prenom, XElement emp, int id)
            : base(nom, prenom, emp, id)
        {
            _serv = emp.Element("Serveur");
            _type = "Serveur";
        }


    }
}
