using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Employe
    {
        protected string _nom, _prenom, _type;
        protected XElement _emp;
        protected static int _nbrEmploye=0;
        private int _id;
 
        public Employe(string nom, string prenom, XElement emp)
        {
            _nom = nom;
            _prenom = prenom;
            _emp = emp;
            _nbrEmploye++;
            _id = _nbrEmploye;
        }

        public Employe(string nom, string prenom, XElement emp, int id) : this(nom, prenom, emp)
        {
            _id = id;
            if (id > _nbrEmploye)
                _nbrEmploye = id;
        }

        protected virtual void GenereXml()
        {
        }

        public override string ToString()
        {
            string ch = "Nom : " + _nom + "\nPrenom : " + _prenom + "\nFonction : " + _type;
            return ch;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
