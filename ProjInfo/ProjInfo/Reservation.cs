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
        private int _nbrPers, _id;
        protected static int _CompteRes = 0;
        private XElement _Reserv;
        private DateTime _date;
        private List<Table> _tableUtilise;
        private Menu _menu;
        private bool _emport;
        
        public Reservation(client client, int nbrPersonnes, DateTime date, XElement reserv, List<Table> Tables, Menu menu)
        {

            _client = client;
            _nbrPers = nbrPersonnes;
            _Reserv = reserv;
            _date = date;
            _tableUtilise = Tables;
            _menu = menu;
            //_duree = _menu.Duree;
            _CompteRes++;
            _id = _CompteRes;
            if(Tables.Count==0)
                _emport=true;
            else
                _emport=false;
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
            //_duree = _menu.Duree;
            _id = id;

            if (Tables.Count == 0)
                _emport = true;
            else
                _emport = false;
            if (id > _CompteRes)
                _CompteRes = id;
        }

        /*===========================================================
          * private void GenereXml()
          * Role : Ajoute les informations de la réservation dans le fichier XML
          * ==========================================================*/
        private void GenereXml()
        {
            _Reserv.Add(new XElement("Reservation", new XElement("Id", _id), new XElement("Id_Client", _client.Id), new XElement("Id_Menu", Menu.Id), new XElement("nb_Pers", _nbrPers),
                new XElement("Debut", Date), new XElement("Id_Tables", Idtables())));
        }

        public override string ToString()
        {
            string ch = "";
            ch += "Nom du client : " + _client.Nom + "\nNombre de personnes : " + _nbrPers + "\nDate : " + _date 
                + "\nNom du Menu : " + _menu.Nom + "\nListe des tables : " + Idtables();           
            return ch;
        }

        /*===========================================================
         * private string Idtables()
         * Role : Génère la chaine de caractère utilisée dans le fichier Xml
         * pour définir les tables utilisées dans cette réservation.
         * ==========================================================*/
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

        public bool Emport
        {
            get { return _emport; }
            set { _emport = value; }
        }
        #endregion


    }
}
