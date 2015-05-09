using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Reservation
    {
        private client _client;
        private int _nbrPers, _duree, _id;
        protected static int _CompteRes = 0;
        private XElement _Reserv;
        private DateTime _date;
        private List<Table> _tableUtilise;
        private Menu _menu;
        
        public Reservation(client client, int nbrPersonnes, DateTime date, XElement reserv, List<Table> Tables, Menu menu)
        {

            _client = client;
            _nbrPers = nbrPersonnes;
            _Reserv = reserv;
            _date = date;
            _tableUtilise = Tables;
            _menu = menu;
            _duree = _menu.Duree;
            _CompteRes++;
            _id = _CompteRes;
            GenereXml();
        }

        public Reservation(int id, client client, int nbrPersonnes, DateTime date, XElement reserv, List<Table> Tables, Menu menu)
        {
            _client = client;
            _nbrPers = nbrPersonnes;
            _Reserv = reserv;
            _date = date;
            _tableUtilise = Tables;
            _menu = menu;
            _duree = _menu.Duree;
            _id = id;
            if (id > _CompteRes)
                _CompteRes = id;
        }

        /*public Reservation(int nbrPersonnes,DateTime date, XElement reserv, List<Table> Tables, Menu menu)
        {
            _nbrPers = nbrPersonnes;
            Console.WriteLine("Quel est le nom du client?");
            string nom = Console.ReadLine();
            Console.WriteLine("Quel est le prénom du client?");
            string prenom = Console.ReadLine();
            Console.WriteLine("Quel est son numéro de téléphone?");
            string tel = Console.ReadLine();
            _client = new client(nom, prenom, tel, _Client);
            _Reserv = reserv;
            _date = date;
            _tableUtilise = Tables;
            _menu = menu;
            _duree = _menu.Duree;
            _CompteRes++;
            _id = _CompteRes;
        }*/

        private void GenereXml()
        {
            _Reserv.Add(new XElement("Reservation", new XElement("Id", _id), new XElement("Id_Client", _client.Id), new XElement("Id_Menu", Menu.Id), new XElement("nb_Pers", _nbrPers),
                new XElement("Debut", Date), new XElement("Fin", DateFin), new XElement("Id_Tables", Idtables())));
        }

        public override string ToString()
        {
            string ch = "";
            ch += "=====================";
            ch += "Nom du client : " + _client.Nom + "\nNombre de personnes : " + _nbrPers + "\nDate : " + _date + "\nDurée : " + _duree
                + "\nNom du Menu : " + _menu.Nom + "\nListe des tables : " + Idtables();
            return ch;
        }

        private string Idtables()
        {
            string ch = "";
            foreach(Table T in _tableUtilise)
            {
                ch += T.Id + "/";
            }
            return ch;
        }

        

        #region Accesseurs
        public List<Table> TableUtilise
        {
            get { return _tableUtilise; }
            set { _tableUtilise = value; }
        }
        
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public DateTime DateFin
        {
            get { return _date.AddMinutes(_duree); }
            
        }

        public Menu Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        public int NbrPers
        {
            get { return _nbrPers; }
            set { _nbrPers = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion


    }
}
