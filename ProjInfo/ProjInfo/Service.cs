using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Service
    {
        private DateTime _debut, _fin;
        private List<Reservation> _listRes;
        private List<Employe> _listEmp = new List<Employe>();
        private XElement _xmlServ;
        private int _chargeTravail = 0, _chargePossible = 0, _nbPers = 0, _nbPersMax = 0;

        public Service(DateTime debutService, DateTime finService, List<Reservation> listRes, XElement xmlServ)
        {
            _debut = debutService;
            _fin = finService;
            _listRes = listRes;
            _xmlServ = xmlServ;
        }

        public void ajoutEmploye(Employe E)
        {
            if (!_listEmp.Contains(E))
            {
                _listEmp.Add(E);
                if (E is Cuisinier)
                    _chargePossible += 20;
                else
                    _nbPersMax += 10;
                Console.WriteLine("L'employé a été ajouté.");
            }
            else
            {
                Console.WriteLine("Cette employé travail déjà à cette heure ci.");
            }
        }

        public bool AjoutReservation(Reservation R)
        {
            bool verif;
            if (_chargePossible >= _chargeTravail + R.Menu.Charge * R.NbrPers && _nbPersMax >= _nbPers + R.NbrPers)
            {
                _chargeTravail += R.Menu.Charge * R.NbrPers;
                _nbPers += R.NbrPers;
                _listRes.Add(R);
                verif = true;
            }
            else
                verif = false;
            return verif;
        }

        public override string ToString()
        {
            string ch = "Date de début : " + _debut + "\nDate de fin : " + _fin + "\nNbr d'employés : " + _listEmp.Count + "\nNbr de reservations : " + _listRes.Count + "\n";
            return ch;
        }

        public DateTime Fin
        {
            get { return _fin; }
            set { _fin = value; }
        }

        public DateTime Debut
        {
            get { return _debut; }
            set { _debut = value; }
        }

    }
}