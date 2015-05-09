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
        private int _nbrPers, _duree;
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
        }

        public Reservation(int nbrPersonnes,DateTime date, XElement reserv, List<Table> Tables, Menu menu)
        {
            _nbrPers = nbrPersonnes;
            Console.WriteLine("Quel est le nom du client?");
            string nom = Console.ReadLine();
            Console.WriteLine("Quel est le prénom du client?");
            string prenom = Console.ReadLine();
            Console.WriteLine("Quel est son numéro de téléphone?");
            string tel = Console.ReadLine();
            _client = new client(nom, prenom, tel);
            _Reserv = reserv;
            _date = date;
            _tableUtilise = Tables;
            _menu = menu;
            _duree = _menu.Duree;
        }


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

               
    }
}
