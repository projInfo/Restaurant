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
        private List<Reservation> _listRes = new List<Reservation>();
        private List<Employe> _listEmp = new List<Employe>();
        private XElement _xmlServ;
        private int _chargeTravail = 0, _chargePossible = 0, _nbPers = 0, _nbPersMax = 0;
        protected static int _nbrServ = 0;
        private int _id;

        public Service(DateTime debutService, DateTime finService, XElement xmlServ)
        {
            _debut = debutService;
            _fin = finService;
            _xmlServ = xmlServ;
            _nbrServ++;
            _id = _nbrServ;
            GenereXml();
        }

        public Service(int id, DateTime debutService, DateTime finService, XElement xmlServ)
        {
            _debut = debutService;
            _fin = finService;
            _xmlServ = xmlServ;
            _id = id;
            if (id > _nbrServ)
                _nbrServ = id;
        }
        private void GenereXml()
        {
            Console.WriteLine("Gene xml");
            _xmlServ.Add(new XElement("Service",new XElement("ID", _id), new XElement("debut", _debut), new XElement("fin", _fin), new XElement("Id_res", IdRes()), new XElement("Id_Emp", IdEmp())));
        }
        
        private string IdRes()
        {
            string ch = "";
            foreach(Reservation R in _listRes)
            {
                ch += R.Id + "/";
            }
            return ch;
        }

        private string IdEmp()
        {
            string ch = "";
            foreach(Employe E in _listEmp)
            {
                ch += E.Id + "/";
            }
            return ch;
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

                var emp = from a in _xmlServ.Descendants("Service")
                             select a;

                foreach (XElement e in emp)
                {
                    if(int.Parse(e.Element("ID").Value)==_id)
                    {
                        e.Element("Id_Emp").Value = IdEmp();
                    }
                }
            }
            else
            {
                Console.WriteLine("Cette employé travail déjà à cette heure ci.");
            }
            Console.ReadLine();
        }

        public bool testAjout(Menu M, int nbPers)
    {
        bool verif;
        if (_chargePossible >= _chargeTravail + M.Charge * nbPers && _nbPersMax >= _nbPers + nbPers)
        {
            verif = true;
        }
        else
        {
            if (_chargePossible < _chargeTravail + M.Charge * nbPers)
                Console.WriteLine("Manque de cuisiniers");
            else
                Console.WriteLine("Manque de serveurs");
            Console.WriteLine("CP" + _chargePossible + "/Ct" + _chargeTravail + "\nnM" + _nbPersMax + "nP" + _nbPers);
            verif = false;
        }

            return verif;
    }

        public void AjoutReservation(Reservation R)
        {
            
                _chargeTravail += R.Menu.Charge * R.NbrPers;
                _nbPers += R.NbrPers;
                _listRes.Add(R);
                

                var res = from a in _xmlServ.Descendants("Service")
                          select a;

                foreach (XElement e in res)
                {
                    if (int.Parse(e.Element("ID").Value) == _id)
                    {
                        e.Element("Id_res").Value = IdRes();
                    
                    }
                }
            
            
        }

        public override string ToString()
        {
            string ch = "Date de début : " + _debut + "\nDate de fin : " + _fin + "\nNbr d'employés : " + _listEmp.Count
                + "\nNbr de reservations : " + _listRes.Count + "\nListe des employés : ";
                ch+="\n++++++++++++++++++++++++++++\n";
                ch+="           Employés\n";
            ch+="++++++++++++++++++++++++++++\n";
            foreach(Employe E in _listEmp)
            {                
                ch += "\n=========================\n" + E.ToString();
            }
            ch += "\n++++++++++++++++++++++++++++\n";
            ch += "         Réservations\n";
            ch += "++++++++++++++++++++++++++++\n";
            foreach(Reservation R in _listRes)
            {
                ch += "\n=========================\n" + R.ToString();
            }
            ch += "\n=========================";
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

        public List<Employe> ListEmp
        {
            get { return _listEmp; }
            set { _listEmp = value;
            foreach(Employe E in value)
            {
                if (E is Cuisinier)
                    _chargePossible += 20;
                else
                    _nbPersMax += 10;
            }
            }
        }

        public List<Reservation> ListRes
        {
            get { return _listRes; }
            set { _listRes = value;
            foreach (Reservation R in value)
            {
                
                _chargeTravail += R.Menu.Charge * R.NbrPers;
                _nbPers += R.NbrPers;
            }
            }
        }

    }
}