using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Restaurant
    {
        private string _adresse, _nom, chemin;
        private int _nbrTable, _nbrEmploye;
        private List<Table> _ListTable, _ListTableUtilise, _ListTableDispo;
        private List<Employe> _ListEmp = new List<Employe>();
        private List<Reservation> _ListRes = new List<Reservation>();
        private List<client> _ListClient = new List<client>();
        private List<Menu> _ListMenu = new List<Menu>();
        private List<Service> _ListServ = new List<Service>();
        private List<List<TableJumelable>> _ListCombi = new List<List<TableJumelable>>();        
        private XDocument _doc;
        private XElement _table = new XElement("Tables");
        private XElement _Jum = new XElement("Jumelable");
        private XElement _NonJum = new XElement("Non_Jumelable");
        private XElement _tableRect = new XElement("Rectangulaire");
        private XElement _tableCarre = new XElement("Carré");
        private XElement _tableRonde = new XElement("Ronde");
        private XElement _Carac = new XElement("Caractéristiques");
        private XElement _Pers = new XElement("Personnel");
        private XElement _Reserv = new XElement("Reservations");
        private XElement _Service = new XElement("Services");
        private XElement _Menu = new XElement("Menus");
        private XElement _Client = new XElement("Clients");
        private System.Globalization.CultureInfo _Culture = new System.Globalization.CultureInfo("fr-FR");            
        

        #region Constructeurs

        /*===========================================================
         * public Restaurant()
         * Role : Constructeur appelé lors la première fois lors de la
         * création du restaurant.
         * Permet l'initialisation de celui-ci 
         * et la création de son fichier XMl
         * ==========================================================*/
        public Restaurant()
        {           
            Initialize();            
            _doc.Save(chemin);            
        }

        /*===========================================================
         * public Restaurant(string path)
         * Paramètre : string path -> chemin du fichier XML à charger.
         * Role : Constructeur appelé lors du chargement d'un restaurant.
         * ==========================================================*/
        public Restaurant(string path)
        {
            _doc = XDocument.Load(path);
            _adresse = _doc.Element("Restaurant").Element("Caractéristiques").Element("Adresse").Value;
            _nbrTable = int.Parse(_doc.Element("Restaurant").Element("Caractéristiques").Element("Nbre_Tables").Value);
            _nbrEmploye = int.Parse(_doc.Element("Restaurant").Element("Caractéristiques").Element("Nbre_Employés").Value);
            _ListTable = new List<Table>();
            chemin = path;
            _table = _doc.Element("Restaurant").Element("Tables");
            _Pers = _doc.Element("Restaurant").Element("Personnel");
            _Carac = _doc.Element("Restaurant").Element("Caractéristiques");
            _Reserv = _doc.Element("Restaurant").Element("Reservations");
            _Service = _doc.Element("Restaurant").Element("Services");
            _Menu = _doc.Element("Restaurant").Element("Menus");
            _Client=_doc.Element("Restaurant").Element("Clients");
            ChargeRessources();
            _doc.Save(chemin);

        }
#endregion

        #region Methodes

        /*===========================================================
         * public void SaveDoc()
         * Role : Methode public permettant la sauvegarde du fichier
         * principalment utilisée dans le program.cs 
         * ==========================================================*/
        public void SaveDoc()
        {
            _doc.Save(chemin);
        }
        
        /*===========================================================
         * public override string ToString()
         * Role : Affiches toutes les informations du restaurant
         * ==========================================================*/
        public override string ToString()
        {
            
            string ch = "";

            ch += " ======================\n";
            ch += "|                     |\n";
            ch += "|      Restaurant     |\n";
            ch += "|                     |\n";
            ch += " ======================\n";
            ch += "Addresse : " + _adresse + "\n";
            ch += "Nombres de tables : " + _nbrTable + "\n";
            ch += "Nombres d'emplyoés : " + _nbrEmploye + "\n\n";

            ch += " ======================\n";
                ch += "|                     |\n";
                ch += "|       Tables        |\n";
                ch += "|                     |\n";
                ch += " ======================\n";
            foreach(Table T in _ListTable)
            {
                
                ch+=T.ToString()+"\n======================\n";
            }
                ch += " ======================\n";
                ch += "|                     |\n";
                ch += "|       Employés      |\n";
                ch += "|                     |\n";
                ch += " ======================\n";
            foreach (Employe E in _ListEmp)
            {
               
                ch += E.ToString() + "\n======================\n";
            }

            ch += " ======================\n";
            ch += "|                     |\n";
            ch += "|        Menus        |\n";
            ch += "|                     |\n";
            ch += " ======================\n";
            foreach (Menu M in _ListMenu)
            {

                ch += M.ToString() + "\n======================\n";
            }

            ch += " ======================\n";
            ch += "|                     |\n";
            ch += "|      Services       |\n";
            ch += "|                     |\n";
            ch += " ======================\n";
            foreach (Service S in _ListServ)
            {

                ch += S.ToString() + "\n======================\n";
            }
            return ch;
        }

        /*===========================================================
         * private void Initialize()
         * Role : Fonction appelée lors de la création d'un restaurant
         * - Demande les informations sur les restaurant
         * - Ajoute les premières tables et cients
         * - Crée le fichier XML
         * ==========================================================*/
        private void Initialize()
        {
            Console.CursorVisible = true;
            Console.WriteLine("Nom :");
            string nom = Console.ReadLine();
            Console.WriteLine("adresse : ");
            string ad = Console.ReadLine();
            Console.WriteLine("nbr table");
            int nbrtable = int.Parse(Console.ReadLine());
            Console.WriteLine("nbr d'employés");
            int nbrEmp = int.Parse(Console.ReadLine());
            
            //_nbrEmploye = nbrEmploye;
            _ListTable = new List<Table>();
            _adresse = ad;
            _nom = nom;
            chemin = _nom + ".xml";
            _doc = new XDocument(
        new XElement("Restaurant"));
           
            _doc.Element("Restaurant").Add(_table);
            _doc.Element("Restaurant").Add(_Pers);
            _doc.Element("Restaurant").Add(_Carac);
            _doc.Element("Restaurant").Add(_Menu);
            _doc.Element("Restaurant").Add(_Client);
            _doc.Element("Restaurant").Add(_Service);
            _doc.Element("Restaurant").Add(_Reserv);
            _Pers.Add(new XElement("Cuisinier"));
            _Pers.Add(new XElement("Serveur"));
            _table.Add(_Jum, _NonJum);
            _Jum.Add(_tableRect, _tableCarre);
            _NonJum.Add(_tableRonde);
            _Carac.Add(new XElement("Adresse", _adresse), new XElement("Nbre_Tables", _nbrTable),new XElement("Nbre_Employés", _nbrEmploye));
            InitRessources(nbrtable, nbrEmp);
            _doc.Save(chemin);
            
        }

        /*===========================================================
         * private void InitRessources(int nbTables, int nbEmp)
         * Role : Ajoute les tables et les employés en fonction
         * des paramètres donnés
         * ==========================================================*/
        private void InitRessources(int nbTables, int nbEmp)
        {
            int i = 0;
            while (i < nbTables)
            {
                addTable();
                i++;
            }
            i = 0;
            while (i < nbEmp)
            {
                addEmploye();
                i++;
            }
        }

        #region Chargement Fichier
        /*===========================================================
         * private void ChargeRessources()
         * Role : Permet le chargement du fichier XML
         * ==========================================================*/
        private void ChargeRessources()
        {
            ChargeTable();
            Console.WriteLine("Table");
            ChargeEmploye();
            Console.WriteLine("Empoye");
            ChargeClient();
            Console.WriteLine("Client");
            ChargeMenu();
            Console.WriteLine("Menu");
            ChargeReserv();
            Console.WriteLine("Reserv");
            ChargeService();
            Console.WriteLine("Service");
        }

        /*===========================================================
         * private void ChargeEmploye()
         * Role : Lit le fichier XML, instancie tout les employés 
         * et les ajoutes dans _ListEmp
         * ==========================================================*/
        private void ChargeEmploye()
        {
            var serveur = from a in _doc.Descendants("Serveur")
                          select a;

            foreach (XElement e in serveur.Descendants("Employe"))
            {
                string nom = e.Element("Nom").Value;
                string prenom = e.Element("Prenom").Value;
                int id = int.Parse(e.Element("ID").Value);
                _ListEmp.Add(new Serveur(nom, prenom, _Pers, id));

            }

            var cuisinier = from a in _doc.Descendants("Cuisinier")
                          select a;

            foreach (XElement e in cuisinier.Descendants("Employe"))
            {
                string nom = e.Element("Nom").Value;
                string prenom = e.Element("Prenom").Value;
                int id = int.Parse(e.Element("ID").Value);
                _ListEmp.Add(new Cuisinier(nom, prenom, _Pers, id));

            }
        }

        /*===========================================================
         * private void ChargeTable()
         * Role : Lit le fichier XML, instancie toutes les tables 
         * et les ajoutes dans _ListTable
         * ==========================================================*/
        private void ChargeTable()
        {
            var tableRe = from a in _table.Descendants("Rectangulaire")
                          select a;

            foreach (XElement e in tableRe.Descendants("table"))
            {
                int nbrPlace = int.Parse(e.Element("nbrPlace").Value);
                int longu = int.Parse(e.Element("long").Value);
                int large = int.Parse(e.Element("large").Value);
                int id = int.Parse(e.Element("ID").Value);
                Table T=new Table_Rect(id, nbrPlace, longu, large, _table);
                _ListTable.Add(T);
                Console.WriteLine(T.Id + "/");
                

            }

            var tableC = from a in _doc.Descendants("Carré")
                         select a;

            foreach (XElement e in tableC.Descendants("table"))
            {
                int nbrPlace = int.Parse(e.Element("nbrPlace").Value);
                int cote = int.Parse(e.Element("Dim").Value);
                int id = int.Parse(e.Element("ID").Value);
                Table T = new Table_Carre(id, nbrPlace, cote, _table);
                _ListTable.Add(T);
                Console.WriteLine(T.Id + "/");
            }

            var tableRo = from a in _doc.Descendants("Ronde")
                          select a;

            foreach (XElement e in tableRo.Descendants("table"))
            {

                int nbrPlace = int.Parse(e.Element("nbrPlace").Value); 
                int diam = int.Parse(e.Element("Diam").Value);
                int id = int.Parse(e.Element("ID").Value);
                Table T=new Table_Ronde(id, nbrPlace, diam, _table);
                _ListTable.Add(T);
                
            }
        }

        /*===========================================================
         * private void ChargeClient()
         * Role : Lit le fichier XML, instancie tout les clients 
         * et les ajoutes dans _ListClient
         * ==========================================================*/
        private void ChargeClient()
        {
            var clientCharge = from a in _doc.Descendants("client")
                          select a;

            foreach (XElement e in clientCharge)
            {               
                string nom = e.Element("Nom").Value;
                string prenom = e.Element("Prenom").Value;
                int id = int.Parse(e.Element("ID").Value);
                string num = e.Element("numero").Value;
                _ListClient.Add(new client(id, nom, prenom, num, _Client));
            }
        }

        /*===========================================================
         * private void ChargeMenu()
         * Role : Lit le fichier XML, instancie tout les menus 
         * et les ajoutes dans _ListMenu
         * ==========================================================*/
        private void ChargeMenu()
        {
            var menuCharge = from a in _doc.Descendants("menu")
                               select a;

            foreach (XElement e in menuCharge)
            {
                string nom = e.Element("Nom").Value;
                int id = int.Parse(e.Element("ID").Value);
                int duree = int.Parse(e.Element("Duree").Value);
                int charge = int.Parse(e.Element("Charge").Value);
                _ListMenu.Add(new Menu(id, nom, duree, charge, _Menu));
            }
        }

        /*===========================================================
         * private void ChargeReserv()
         * Role : Lit le fichier XML, instancie toutes le réservations 
         * et les ajoutes dans _ListRes
         * ==========================================================*/
        private void ChargeReserv()
        {
            var reservCharge = from a in _doc.Descendants("Reservation")
                             select a;

            foreach (XElement e in reservCharge)
            {
                int id = int.Parse(e.Element("Id").Value);
                int idClient = int.Parse(e.Element("Id_Client").Value);
                int idMenu = int.Parse(e.Element("Id_Menu").Value);
                int nbP = int.Parse(e.Element("nb_Pers").Value);
                DateTime debut = DateTime.Parse(e.Element("Debut").Value);
                string[] idTable = e.Element("Id_Tables").Value.Split('/');
                int i = 0;
                List<Table> LT = new List<Table>();

                while(i<idTable.Length)
                {
                    
                    if (idTable[i] != "")
                    {
                        foreach (Table T in _ListTable)
                        {
                            if (T.Id == int.Parse(idTable[i]))
                            {                                
                                LT.Add(T);
                            }
                        }
                    }
                    i++;
                }
                client clientCharge = null;
                foreach(client C in _ListClient)
                {
                    if (C.Id == idClient)
                        clientCharge = C;
                }
                Menu menuCharge = null;
                foreach(Menu M in _ListMenu)
                {
                    if (M.Id == idMenu)
                        menuCharge = M;
                }
                _ListRes.Add(new Reservation(id, clientCharge, nbP, debut, _Reserv, LT, menuCharge));
            }
        }

        /*===========================================================
         * private void ChargeService()
         * Role : Lit le fichier XML, instancie tout les services 
         * et les ajoutes dans _ListServ
         * ==========================================================*/
        private void ChargeService()
        {
            var servCharge = from a in _doc.Descendants("Service")
                             select a;

            foreach (XElement e in servCharge)
            {
                int id = int.Parse(e.Element("ID").Value);
                DateTime debut = DateTime.Parse(e.Element("debut").Value);
                DateTime fin = DateTime.Parse(e.Element("fin").Value);
                Service Serv = new Service(id, debut, fin, _Service);
                _ListServ.Add(Serv);
                string [] idRes=e.Element("Id_res").Value.Split('/');
                string[] idEmp = e.Element("Id_Emp").Value.Split('/');
                int i = 0;
                List<Reservation> LR = new List<Reservation>();
                while(i<idRes.Length)
                {
                if (idRes[i] != "")
                {
                    foreach (Reservation R in _ListRes)
                    {

                        if (R.Id == int.Parse(idRes[i]))
                            LR.Add(R);
                    }
                }
                    i++;
                }
                i = 0;
                List<Employe> LE = new List<Employe>();
                while (i < idEmp.Length)
                {
                    foreach (Employe E in _ListEmp)
                    {
                        if (idEmp[i] != "")
                        {
                            if (E.Id == int.Parse(idEmp[i]))
                                LE.Add(E);
                        }
                    }
                    i++;
                }
                Serv.ListEmp = LE;
                Serv.ListRes = LR;
                Console.ReadLine();
            }
            
        }
        #endregion

        #region Ajout de ressources
        /*===========================================================
         * public void addTable()
         * Role : Demande les informations de la table à ajouter, 
         * instancie la table et l'ajoute dans _ListTable
         * ==========================================================*/
        public void addTable()
        {

            Console.WriteLine(@"Type : 
1:Ronde
2:Rectangle
3:Carré");
            string _type = Console.ReadLine();
            if (_type == "1")
            {
                Console.WriteLine("Diam :");
                int _diam = int.Parse(Console.ReadLine());
                Console.WriteLine("nbr places :");
                int _nbrePlace = int.Parse(Console.ReadLine());
                _ListTable.Add(new Table_Ronde(_nbrePlace, _diam, _table));
            }
            else if (_type == "2")
            {
                Console.WriteLine("Longueur :");
                int _long = int.Parse(Console.ReadLine());
                Console.WriteLine("Largeur :");
                int _large = int.Parse(Console.ReadLine());
                Console.WriteLine("nbr places :");
                int _nbrePlace = int.Parse(Console.ReadLine());
                _ListTable.Add(new Table_Rect(_nbrePlace, _long, _large, _table));
            }
            else if (_type == "3")
            {
                Console.WriteLine("Coté:");
                int _cote = int.Parse(Console.ReadLine());
                Console.WriteLine("nbr places :");
                int _nbrePlace = int.Parse(Console.ReadLine());
                _ListTable.Add(new Table_Carre(_nbrePlace, _cote, _table));
            }
            else
            {
                Console.WriteLine("Mauvaise Saisie");
            }

            if (_type == "1" || _type == "2" || _type == "3")
            {
                _nbrTable++;
                _Carac.Element("Nbre_Tables").Value = _nbrTable.ToString();
            }
            _doc.Save(chemin);
        }

        /*===========================================================
         * public void addEmploye()
         * Role : Demande les informations de l'employé à ajouter 
         * instancie l'employé et l'ajoute dans _ListEmp
         * ==========================================================*/
        public void addEmploye()
        {
            Console.Clear();
            Console.Write("Entrez le nom de l'employé : ");
            string nom = Console.ReadLine();
            Console.Write("Entrez le prénom de l'employé : ");
            string prenom = Console.ReadLine();                
            Console.WriteLine(@"Quelle est sa fonction? 
1:Serveur
2:Cuisinier");
            int _type = int.Parse(Console.ReadLine());
            if(_type==1)
            {
                _ListEmp.Add(new Serveur(nom, prenom, _Pers));
            }
            else if (_type==2)
            {
                _ListEmp.Add(new Cuisinier(nom, prenom, _Pers));
            }
            else
            {
                Console.WriteLine("Mauvaise saisie");
            }
            if (_type == 1 || _type == 2)
            {
                _nbrEmploye++;
                _Carac.Element("Nbre_Employés").Value = _nbrTable.ToString();
            }
            _doc.Save(chemin);
        }

        /*===========================================================
         * public void addEmploye()
         * Role : Demande les informations du service à ajouter 
         * instancie le service et l'ajoute dans _ListServ
         * ==========================================================*/
        public void ajoutService()
        {
            DateTime debutServ, finServ;
            Console.Clear();
            Console.WriteLine("Quand commencera ce service?");
            Console.WriteLine("jj/mm/aaaa");
            string date = Console.ReadLine();
            Console.WriteLine("A quelle heure?");
            Console.WriteLine("hh:min");
            date +=" "+ Console.ReadLine()+":00";
            
            debutServ = DateTime.Parse(date, _Culture);
            Console.Clear();
            Console.WriteLine("Durée du service? (en heures)");
            int duree = int.Parse(Console.ReadLine());
            finServ = debutServ.AddHours(duree);
            bool check=true;
            foreach(Service Serv in _ListServ)
            {
                if((debutServ>=Serv.Debut&&debutServ<Serv.Fin)||(finServ>Serv.Debut&&finServ<=Serv.Fin))
                {
                    check = false;
                }
            }
            if (check == false)
            {
                Console.WriteLine("Il y a dejà un service à cette heure ci");
                Console.ReadLine();
            }
            else
            {
                Service S = new Service(debutServ, finServ, _Service);
                _ListServ.Add(S);
                _doc.Save(chemin);
                Console.WriteLine("Combien d'employés travailleront?");
                int nbEmp = int.Parse(Console.ReadLine());
                int i = 0;
                while (i < nbEmp)
                {
                    Console.Clear();
                    Console.WriteLine("Quel Employé ajouter?");
                    int j = 0;
                    foreach (Employe E in _ListEmp)
                    {
                        Console.WriteLine("===================");
                        Console.WriteLine("Employé numero " + j);
                        Console.WriteLine(E);
                        j++;
                    }
                    Console.WriteLine("Quel employé ajouter?");
                    int choix = int.Parse(Console.ReadLine());
                    S.ajoutEmploye(_ListEmp.ElementAt(choix));
                    i++;
                }
                _doc.Save(chemin);
            }
            /*
            try
            {
                debutRes = DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture); 
            }
            catch
            {                
                ajoutService();
            }*/

        }

        /*===========================================================
         * public void addEmploye()
         * Role : Demande les informations du menu à ajouter 
         * instancie le menu et l'ajoute dans _ListMenu
         * ==========================================================*/
        public void ajoutMenu()
        {
            Console.Clear();
            Console.WriteLine("Entrez le nom du menu");
            string nom = Console.ReadLine();
            Console.WriteLine("Combien de temps faut il pour le préparer? (en minutes)");
            int duree = int.Parse(Console.ReadLine());
            Console.WriteLine("Quelle charge de travail impose ce menu? (entre 0 et 5)");
            int charge = int.Parse(Console.ReadLine());
            if (charge > 5)
                charge = 5;
            else if (charge < 0)
                charge = 0;
            _ListMenu.Add(new Menu(nom, duree, charge, _Menu));

        }
        #endregion

        public void SuppTable()
        {
            Console.Clear();
            foreach(Table T in _ListTable)
            {
                Console.WriteLine(T);
                Console.WriteLine("\n=========================\n");
            }
            Console.WriteLine("Quelle table voulez vous supprimer?");
            int choix = int.Parse(Console.ReadLine());
            Table tabSup = null;
            foreach(Table T in _ListTable)
            {
                if (T.Id == choix)
                    tabSup = T;
            }
            tabSup.suppXml();
            _ListTable.Remove(tabSup);
        }

        #region Gestion reservation

        public void AjoutReserv()
        {
            client ClientRes=null;
            Console.WriteLine("etes vous déjà venu? (0:non   1:oui");
            int venu = int.Parse(Console.ReadLine());
            if (venu == 1)
            {
                Console.WriteLine("Quel est votre nom");
                string nom = Console.ReadLine();
                int count = 0;
                foreach (client C in _ListClient)
                {
                    if (C.Nom == nom)
                    {
                        ClientRes = C;
                        count++;
                    }
                }
                if (count == 0)
                {
                    Console.WriteLine("Nous n'avons pas de client avec ce nom.");
                    Console.ReadLine();
                    AjoutReserv();
                }
                else if (count > 1)
                {
                    Console.WriteLine("Quel est votre prénom?");
                    string prenom = Console.ReadLine();
                    count = 0;
                    foreach (client C in _ListClient)
                    {
                        if (C.Nom == nom && C.prenom == prenom)
                        {
                            ClientRes = C;
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        Console.WriteLine("Nous n'avons pas de client avec ce prenom.");
                        Console.ReadLine();
                        AjoutReserv();
                    }
                    else if (count > 1)
                    {
                        Console.WriteLine("Quel est votre numéro?");
                        string num = Console.ReadLine();
                        count = 0;
                        foreach (client C in _ListClient)
                        {
                            if (C.Nom == nom && C.prenom == prenom && C.NumeroTelephone == num)
                            {
                                ClientRes = C;
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            Console.WriteLine("Nous n'avons pas de client avec ces données.");
                            Console.ReadLine();
                            AjoutReserv();
                        }
                    }
                }

                }
                else
                {
                    Console.WriteLine("Quel est votre nom");
                    string nom = Console.ReadLine();
                    Console.WriteLine("Quel est votre prenom");
                    string prenom = Console.ReadLine();
                    Console.WriteLine("Quel est votre numero");
                    string num = Console.ReadLine();
                    ClientRes = new client(nom, prenom, num, _Client);

                }
                Console.WriteLine("Combiens serez vous?");
                int nbrPer = int.Parse(Console.ReadLine());
                Menu menuSelect = null;
                while (menuSelect == null)
                {
                    Console.WriteLine("Quel Menu désirez vous?");
                    int i = 0;
                    foreach (Menu M in _ListMenu)
                    {
                        i++;
                        Console.WriteLine("Menu numero " + i + M);
                    }
                    Console.Write("Vous voulez le menu :");
                    int choix = int.Parse(Console.ReadLine());

                    foreach (Menu M in _ListMenu)
                    {
                        if (M.Id == choix)
                        {
                            menuSelect = M;
                            break;
                        }
                    }
                    if (menuSelect == null)
                    {
                        Console.WriteLine("Mauvaise saisie");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                DateTime datereserv;
                Console.Clear();
                Console.WriteLine("Quand voulez vous venir?");
                Console.WriteLine("jj/mm/aaaa");
                string dateR = Console.ReadLine();
                Console.WriteLine("A quelle heure?");
                Console.WriteLine("hh:min");
                dateR += " " + Console.ReadLine() + ":00";

                datereserv = DateTime.Parse(dateR, _Culture);
                Service servCourant = null;
                foreach (Service S in _ListServ)
                {
                    if (datereserv >= S.Debut && datereserv <= S.Fin)
                    {
                        servCourant = S;
                    }
                }
                if (servCourant == null)
                {
                    Console.WriteLine("Il n'y a pas de service à cette date");
                    Console.ReadLine();
                }
                else
                {

                    foreach (Reservation R in _ListRes)
                    {
                        if (R.Date <= datereserv && R.Date >= datereserv)
                        {
                            _ListTableUtilise.AddRange(R.TableUtilise);
                        }
                    }
                    _ListTableUtilise = new List<Table>();
                    List<Table> tableReserv = ChoixTables(_ListTableUtilise, nbrPer);

                    foreach (Table T in tableReserv)
                    {
                        Console.WriteLine(T
                            );
                    }

                    if (tableReserv.Count != 0 && servCourant.testAjout(menuSelect, nbrPer) == true)
                    {
                        //Console.WriteLine(VerifTables(_ListTableUtilise, nbrPer));
                        Reservation Ra = new Reservation(ClientRes, nbrPer, datereserv, _Reserv, tableReserv, menuSelect);
                        _ListRes.Add(Ra);
                        servCourant.AjoutReservation(Ra);
                        Console.WriteLine("Reservation ajoutée !");
                        _doc.Save(chemin);
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Erreur ajout reservation");
                        Console.ReadLine();
                    }

                }
            
        }

        public List<Table> ChoixTables( List<Table>tableUtilise, int nbPers)
        {
            List<Table> tableReserv=new List<Table>();
            List<Table> tableTri = new List<Table>();
            List<TableJumelable> tableTriJum = new List<TableJumelable>();
                foreach(Table Tr in _ListTable)
                {
                    if(Tr.NbrPlace>=nbPers&&!tableUtilise.Contains(Tr)&&Tr.Type=="ronde")
                    {
                        tableTri.Add(Tr);
                    }
                    tableTri.Sort((x, y) => string.Compare(x.NbrPlace.ToString(), y.NbrPlace.ToString()));
                }
                
                if (tableTri.Count == 0)
                {
                    foreach (Table Tj in _ListTable)
                    {
                        if (Tj.NbrPlace >= nbPers && !tableUtilise.Contains(Tj) && Tj.Type != "ronde")
                        {
                            tableTri.Add(Tj);
                        }
                    }
                    tableTri.Sort((x, y) => string.Compare(x.NbrPlace.ToString(), y.NbrPlace.ToString()));
                    
                }
                    
            if(tableTri.Count!=0)
            {   
                tableReserv.Add(tableTri.ElementAt(0));
                return tableReserv;
            }
            else
            {
                
                foreach (Table Tj in _ListTable)
                {
                    if (!tableUtilise.Contains(Tj) && Tj.Type != "ronde")
                    {                        
                        tableTriJum.Add((TableJumelable)Tj);
                    }
                }
                _ListCombi.Clear();
                CombinaisonTable(tableTriJum, tableTriJum.Count, 0, new List<TableJumelable>(), 0);
                _ListCombi = testJumelable(_ListCombi);
               /* _ListCombi.Sort(delegate(List<TableJumelable> L1, List<TableJumelable> L2)
                {
                    return places(L1).ToString().CompareTo(places(L2).ToString());
                });*/
               // _ListCombi.Sort((x, y) => string.Compare(places(x).ToString(), places(y).ToString()));
                foreach (List<TableJumelable> Lt in _ListCombi)
                {
                    foreach (TableJumelable t in Lt)
                    {
                        Console.Write(t.Id + "//");
                    }
                    Console.WriteLine("nb places: " + places(Lt));
                    Console.WriteLine("\n================");
                }
                int i = 0;
                List<TableJumelable> listTj=new List<TableJumelable>();
                while(i<_ListCombi.Count)
                {
                    if (places(_ListCombi.ElementAt(i)) >= nbPers)
                    {
                        
                        if(places((List<TableJumelable>)listTj)>places(_ListCombi.ElementAt(i)))
                        {
                            listTj = _ListCombi.ElementAt(i);                            
                        }
                        else if (places((List<TableJumelable>)listTj) == places(_ListCombi.ElementAt(i)) && listTj.Count > _ListCombi.ElementAt(i).Count)
                        {
                            listTj = _ListCombi.ElementAt(i);
                        }
                        else if(listTj.Count==0)
                        {
                            listTj = _ListCombi.ElementAt(i);
                        }

                    }

                    i++;
                }
                tableReserv.AddRange(listTj);
                                
                return tableReserv;
            }
            
            
        }

        public  void CombinaisonTable(List<TableJumelable> LT, int profMax,
         int profCourante, List<TableJumelable> prefix, int rang)
        {


            if (profCourante < profMax)
            {
                List<TableJumelable> test = new List<TableJumelable>();
                List<TableJumelable> test2 = new List<TableJumelable>();
                for (int i = rang; i < LT.Count; i++)
                {

                    test.Clear();
                    test.AddRange(prefix);
                    test.Add(LT.ElementAt(i));
                    _ListCombi.Add(new List<TableJumelable>(test));
                    /*foreach (Table t in test)
                    {
                        Console.Write(t.Id + "//");
                    }
                    Console.WriteLine();*/


                }

                for (int i = rang; i < LT.Count; i++)
                {
                    test2.Clear();
                    test2.AddRange(prefix);
                    test2.Add(LT.ElementAt(i));
                    CombinaisonTable(LT, profMax, profCourante + 1, test2, i + 1);
                }

            }

        }

        public List<List<TableJumelable>>  testJumelable(List<List<TableJumelable>> list)
        {
            List<List<TableJumelable>> listreturn = new List<List<TableJumelable>>();
            foreach(List<TableJumelable> Lj in list)
            {
                bool jum = true;
                foreach(TableJumelable Tj in Lj)
                {
                    if(Lj.IndexOf(Tj)!=Lj.Count-1)
                    {
                        if(Tj.JumelableAvec(Lj.ElementAt(Lj.IndexOf(Tj)+1))==false)
                        {
                            jum = false;                            
                        }
                    }
                    

                }
                if (jum == true)
                {
                    listreturn.Add(Lj);
                }

            }
            return listreturn;

        }

        public int places(List<TableJumelable> LT)
        {
            int nbplaces=0;
            foreach (TableJumelable T in LT)
            {
                nbplaces += T.NbrPlace;
            }
            return nbplaces;
        }
        #endregion
        #endregion

        #region Accesseurs
        public List<Table> ListTable
        {
            get { return _ListTable; }
            set { _ListTable = value; }
        }

        public List<Employe> ListEmp
        {
            get { return _ListEmp; }
            set { _ListEmp = value; }
        }
#endregion
    }
}
    

