using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    abstract class Employe
    {
        protected string _nom, _prenom, _type;
        protected XElement _emp, _empCourant;
        protected static int _nbrEmploye=0;
        protected int _id;
        private int _charge;
 
        public Employe(string nom, string prenom, XElement emp)
        {
            _nom = nom;
            _prenom = prenom;
            _emp = emp;
            _nbrEmploye++;
            _id = _nbrEmploye;
        }

        public Employe(string nom, string prenom, XElement emp, int id, int charge) : this(nom, prenom, emp)
        {
            _id = id;
            _charge = charge;
            if (id > _nbrEmploye)
                _nbrEmploye = id;
        }
        
        public override string ToString()
        {
            string ch = "ID : "+_id+"\nNom : " + _nom + "\nPrenom : " + _prenom + "\nFonction : " + _type+"\nCharge de travail : "+Charge;
            return ch;
        }

        public void SuppXml()
        {
            _empCourant.Remove();
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Charge
        {
            get { return _charge; }
            set { _charge = value;
            _empCourant.Element("Charge").Value = _charge.ToString();
            }
        }

    }
}
