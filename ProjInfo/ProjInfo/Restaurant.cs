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
        private List<Table> _ListTable;
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
         * Role : Constructeur appelé lors la première utilisation lors de la
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
         * Paramètre d'entrée : string path -> chemin du fichier XML à charger.
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
         * Role : Méthode public permettant la sauvegarde du fichier
         * principalement utilisée dans le program.cs 
         * ==========================================================*/
        public void SaveDoc()
        {
            _doc.Save(chemin);
        }
        
        /*===========================================================
         * public override string ToString()
         * Role : Méthode qui affiche toutes les informations du restaurant
         * ==========================================================*/
        public override string ToString()
        {
            
            string ch = "";

            ch += " ======================\n";
            ch += "|                     |\n";
            ch += "|      Restaurant     |\n";
            ch += "|                     |\n";
            ch += " ======================\n";
            ch += "Adresse : " + _adresse + "\n";
            ch += "Nombres de tables : " + _nbrTable + "\n";
            ch += "Nombres d'employés : " + _nbrEmploye + "\n\n";

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
         * - Demande les informations sur le restaurant
         * - Ajoute le nombre de tables et employés
         * - Crée le fichier XML
         * ==========================================================*/
        private void Initialize()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Quel est le nom de votre restaurant ?");
            string nom = Console.ReadLine();
            Console.WriteLine("Quel est l'adresse de votre restaurant ?");
            string ad = Console.ReadLine();
            Console.WriteLine("Combien de tables contient votre restaurant ?");
            int nbrtable;
            if (!int.TryParse(Console.ReadLine(), out nbrtable))
                Initialize();            
            //int nbrtable = int.Parse(Console.ReadLine());
            Console.WriteLine("Combien d'employés travaillent dans votre restaurant ?");
            int nbrEmp ;
            if(!int.TryParse(Console.ReadLine(), out nbrEmp))
                Initialize();
            
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
         * Role: Ajoute les tables et les employés en fonction
         * des paramètres donnés et 5 menus par défaut.
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
            _ListMenu.Add(new Menu("Consommation simple", 1, _Menu));
            _ListMenu.Add(new Menu("Menu rapide", 2, _Menu));
            _ListMenu.Add(new Menu("Menu classique", 3, _Menu));
            _ListMenu.Add(new Menu("Menu Gastronomique", 5, _Menu));
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
         * Role : Lit le fichier XML, instancie tous les employés 
         * et les ajoute dans _ListEmp
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
                int charge = int.Parse(e.Element("Charge").Value);
                _ListEmp.Add(new Serveur(nom, prenom, _Pers, id, charge));
            }

            var cuisinier = from a in _doc.Descendants("Cuisinier")
                          select a;

            foreach (XElement e in cuisinier.Descendants("Employe"))
            {
                string nom = e.Element("Nom").Value;
                string prenom = e.Element("Prenom").Value;
                int id = int.Parse(e.Element("ID").Value);
                int charge = int.Parse(e.Element("Charge").Value);
                _ListEmp.Add(new Cuisinier(nom, prenom, _Pers, id, charge));

            }
        }

        /*===========================================================
         * private void ChargeTable()
         * Role : Lit le fichier XML, instancie toutes les tables 
         * et les ajoute dans _ListTable
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
         * Role : Lit le fichier XML, instancie tous les clients 
         * et les ajoute dans _ListClient
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
         * Role : Lit le fichier XML, instancie tous les menus 
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
                //int duree = int.Parse(e.Element("Duree").Value);
                int charge = int.Parse(e.Element("Charge").Value);
                _ListMenu.Add(new Menu(id, nom, charge, _Menu));
            }
        }

        /*===========================================================
         * private void ChargeReserv()
         * Role : Lit le fichier XML, instancie toutes les réservations 
         * et les ajoute dans _ListRes
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
         * et les ajoute dans _ListServ
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
            Console.Clear();
            string préc = "Quelle est la forme de la table ? ";
            string ch = @" 1:Ronde
 2:Rectangle
 3:Carré";
            int _type=Program.MenuFleches(préc, ch, 3, 1);
           
            if (_type == 0)
            {
                Console.WriteLine("Quel est le diamètre de la table ?");
                int _diam = VerifSaisie(Console.ReadLine());
                Console.WriteLine("Combien de places contient cette table ?");
                int _nbrePlace = VerifSaisie(Console.ReadLine());
                _ListTable.Add(new Table_Ronde(_nbrePlace, _diam, _table));
            }
            else if (_type == 1)
            {
                Console.WriteLine("Quelle est la longueur de la table ?");
                int _long = VerifSaisie(Console.ReadLine());
                Console.WriteLine("Quelle est la largeur de la table ?");
                int _large = VerifSaisie(Console.ReadLine());
				Console.WriteLine("Combien de places contient cette table ?");
                int _nbrePlace = VerifSaisie(Console.ReadLine());
                _ListTable.Add(new Table_Rect(_nbrePlace, _long, _large, _table));
            }
            else if (_type == 2)
            {
                Console.WriteLine("Quelle est la longueur d'un coté de la table ?");
                int _cote = VerifSaisie(Console.ReadLine());
				Console.WriteLine("Combien de places contient cette table ?");
                int _nbrePlace = VerifSaisie(Console.ReadLine());
                _ListTable.Add(new Table_Carre(_nbrePlace, _cote, _table));
            }
            else
            {
                Console.WriteLine("Mauvaise Saisie. La saisie ne correspond à aucune option.");
            }

                _nbrTable++;
                _Carac.Element("Nbre_Tables").Value = _nbrTable.ToString();
            
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
            int _type = VerifSaisie(Console.ReadLine());
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
            Console.WriteLine("Quel est la date du début du service ?");
            Console.WriteLine("Format: jj/mm/aaaa");
            string date = Console.ReadLine();
            Console.WriteLine("A quelle heure ce service va-t-il débuter?");
            Console.WriteLine("Format: hh:min");
            date +=" "+ Console.ReadLine()+":00";
            
            debutServ = DateTime.Parse(date, _Culture);
            Console.Clear();
            Console.WriteLine("Combien de temps va durer ce service? (en heures)");
            int duree = VerifSaisie(Console.ReadLine());
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
                Console.WriteLine("Il y a dejà un service à cette heure-ci");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(_ListServ.Count);
                Service S = new Service(debutServ, finServ, _Service);
                _ListServ.Add(S);
                _doc.Save(chemin);
                Console.WriteLine("Combien d'employés travailleront lors de ce service?");
                int nbEmp = VerifSaisie(Console.ReadLine());
                int i = 0;
                while (i < nbEmp)
                {
                    Console.Clear();
                    Console.WriteLine("Quel Employé voulez-vous ajouter?");
                    int j = 0;
                    foreach (Employe E in _ListEmp)
                    {
                        Console.WriteLine("===================");
                        Console.WriteLine("Employé n° " + j);
                        Console.WriteLine(E);
                        j++;
                    }
                    Console.WriteLine("Quel employé voulez-vous ajouter?");
                    int choix = VerifSaisie(Console.ReadLine());
                    if (choix < _ListEmp.Count&&S.ajoutEmploye(_ListEmp.ElementAt(choix)))
                    {
                        
                        i++;
                    }
                    else
                    {
                        Console.WriteLine("La saisie ne correspond à aucune option. Saisie incorrect");
                    }
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
            Console.WriteLine("Quel est le nom du menu ?");
            string nom = Console.ReadLine();
            Console.WriteLine("Quelle charge de travail impose ce menu? (entre 1 et 5)");
			Console.WriteLine ("1 réprésente un menu avec une faible charge de travail.");
			Console.WriteLine ("5 représente un menu avec la plus grande charge de travail.");
            int charge = VerifSaisie(Console.ReadLine());
            if (charge > 5)
                charge = 5;
            else if (charge < 0)
                charge = 0;
            _ListMenu.Add(new Menu(nom, charge, _Menu));

        }
        #endregion

		/*===========================================================
         * public void SuppTable()
         * Role : Permet à l'utilisateur de pourvoir supprimer une table. 
         * 
         * ==========================================================*/

        public void SuppTable()
        {
            Console.Clear();
            foreach(Table T in _ListTable)
            {
                Console.WriteLine(T);
                Console.WriteLine("\n=========================\n");
            }
            Console.WriteLine("Quelle table voulez-vous supprimer?");
            int choix = VerifSaisie(Console.ReadLine());
            Table tabSup = null;
            foreach(Table T in _ListTable)
            {
                if (T.Id == choix)
                    tabSup = T;
            }
            tabSup.suppXml();
            _ListTable.Remove(tabSup);
        }

		/*===========================================================
         * public void SuppEmp()
         * Role : Permet à l'utilisateur de pourvoir supprimer un 
         * employé.
         * ==========================================================*/

        public void SuppEmp()
        {
            Console.Clear();
            foreach (Employe E in _ListEmp)
            {
                Console.WriteLine(E);
                Console.WriteLine("\n=========================\n");
            }
            Console.WriteLine("Quelle employé voulez vous modifier?");
            int choix = VerifSaisie(Console.ReadLine());
            Employe Esupp = null;
            foreach (Employe E in _ListEmp)
            {
                if (E.Id == choix)
                    Esupp = E;
            }
            Console.Clear();
            string chPrec = "Voulez-vous vraiment le supprimer?";
            string ch = " Oui\n Non";
            int selec = Program.MenuFleches(chPrec, ch, 2, 1);           
            
            if(selec==0)
            {
                _ListEmp.Remove(Esupp);
                Esupp.SuppXml();
                Console.WriteLine("L'employé a été supprimé !");
                Console.ReadLine();
            }
        }

		/*===========================================================
         * public void SuppMenu()
         * Role : Permet à l'utilisateur de pourvoir supprimer un 
         * menu.
         * ==========================================================*/
        public void SuppMenu()
        {
            Console.Clear();
            foreach (Menu M in _ListMenu)
            {
                Console.WriteLine(M);
                Console.WriteLine("\n=========================\n");
            }
            Console.WriteLine("Quelle menu voulez vous modifier?");
            int choix = VerifSaisie(Console.ReadLine());
            Menu Msupp = null;
            foreach (Menu M in _ListMenu)
            {
                if (M.Id == choix)
                    Msupp = M;
            }
            Console.Clear();
            string chPrec = "Voulez-vous vraiment le supprimer?";
            string ch = " Oui\n Non";
            int selec = Program.MenuFleches(chPrec, ch, 2, 1);

            if (selec == 0)
            {
                _ListMenu.Remove(Msupp);
                Msupp.SuppXml();
                Console.WriteLine("Le menu a été supprimé !");
                Console.ReadLine();
            }
        }
		/*===========================================================
         * public void ModifChargeEmp()
         * Role : Permet à l'utilisateur de pourvoir modifier la 
         * charge de travail qu'un employé peut accepter.
         * ==========================================================*/

        public void ModifChargeEmp()
        {
            Console.Clear();
            foreach (Employe E in _ListEmp)
            {
                Console.WriteLine(E);
                Console.WriteLine("\n=========================\n");
            }
            Console.WriteLine("Quelle employé voulez vous modifier?");
            int choix = VerifSaisie(Console.ReadLine());
            Employe Emodif = null;
            foreach (Employe E in _ListEmp)
            {
                if (E.Id == choix)
                    Emodif = E;
            }
            Console.Clear();
            Console.WriteLine("Vous avez sélectionné :");
            Console.WriteLine(Emodif);
            Console.WriteLine("\nEntrez la nouvelle valeure de sa charge de travail : ");
            int charge = VerifSaisie(Console.ReadLine());
            Emodif.Charge = charge;
            Console.WriteLine("Modification effectuée !");
            Console.ReadLine();

        }
		/*===========================================================
         * public void ModifChargeMenu()
         * Role : Permet à l'utilisateur de pourvoir modifier la 
         * charge de travail que va couûter un menu à un employé.
         * ==========================================================*/

        public void ModifChargeMenu()
        {
            Console.Clear();
            foreach (Menu M in _ListMenu)
            {
                Console.WriteLine(M);
                Console.WriteLine("\n=========================\n");
            }
            Console.WriteLine("Quelle Menu voulez vous modifier?");
            int choix = VerifSaisie(Console.ReadLine());
            Menu Mmodif = null;
            foreach (Menu M in _ListMenu)
            {
                if (M.Id == choix)
                    Mmodif = M;
            }
            Console.Clear();
            Console.WriteLine("Vous avez sélectionné :");
            Console.WriteLine(Mmodif);
            Console.WriteLine("\nEntrez la nouvelle valeure de la charge : ");
            int charge = VerifSaisie(Console.ReadLine()); 
            Mmodif.Charge = charge;
            Console.WriteLine("Modification effectuée !");
            Console.ReadLine();
        }
		/*===========================================================
         * public int VerifSaisie(string ch)
         * Parametre d'entrée: string ch -> saisie de l'utilisateur
         * Parametre de sortie: int number -> choix de l'utilisateur
         * Role : Permet de vérifier la saisie de l'utilisateur
         * ==========================================================*/

        public int VerifSaisie(string ch)
        {
            int number;
            if(!int.TryParse(ch, out number))
            {
                Console.WriteLine("Saisie non valide !");
                Program.MenuNavigation(this);
            }
            return number;
        }

        #region Gestion reservation


		/*===========================================================
         * public void AjoutReserv()
         * Role : Permet à l'utilisateur d'effectuer une réservation.
         * La méthode vérifie dans un premier temps si le client est 
         * déjà venu au restaurant. Afin d'éviter une saisie 
         * supplémentaire du numéro de téléphone.
         * Puis elle ajoute la réservation à _ListRes.
         * ==========================================================*/
        public void AjoutReserv()
        {
            Console.Clear();
            client ClientRes=null;
            string chPrec="Etes vous déjà venu?";
            string ch=" Oui\n Non";
            int venu = Program.MenuFleches(chPrec, ch, 2, 1);
            //int venu = int.Parse(Console.ReadLine());
            if (venu == 0)
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
                    Console.WriteLine("Quel est le nom du client ?");
                    string nom = Console.ReadLine();
                    Console.WriteLine("Quel est le prenom du client ?");
                    string prenom = Console.ReadLine();
                    Console.WriteLine("Quel est le numéro de téléphone du client ?");
                    string num = Console.ReadLine();
                    ClientRes = new client(nom, prenom, num, _Client);

                }
                Console.WriteLine("Pour combien de personnes est la résevation ?");
                //int nbrPer = int.Parse(Console.ReadLine());
                int nbrPer = VerifSaisie(Console.ReadLine());
                Menu menuSelect = null;
                while (menuSelect == null)
                {
				Console.WriteLine("Quel menu le client ou les clients désire(nt)-t-il(s) ?");
                    int i = 0;
                    foreach (Menu M in _ListMenu)
                    {
                        i++;
                        Console.WriteLine("Menu n° " + i + M);
                    }
                    Console.Write("Le client désire le menu :");
                    int choix = VerifSaisie(Console.ReadLine());

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
                        Console.WriteLine("La saisie ne correpsond à aucune option. Mauvaise saisie");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                DateTime datereserv;
                Console.Clear();
                Console.WriteLine("Quand le client compte-t-il venir ?");
                Console.WriteLine("Format: jj/mm/aaaa");
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
                    Console.WriteLine("Il n'y a pas de service à cette date.");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    chPrec = "Le client désire-t-il un menu :";
                    ch = " A emporter\n Sur place";
                   // Console.WriteLine("(1) A emporter\n(2) Sur place");
                    int choix = Program.MenuFleches(chPrec, ch, 2, 1);
                    //int choix = int.Parse(Console.ReadLine());
                    bool emport;
                    if (choix == 0)
                        emport = true;
                    else
                        emport = false;
                    List<Table> tableReserv = new List<Table>();
                    List<Table> _ListTableUtilise = new List<Table>();
                    if (emport == false)
                    {
                        foreach (Reservation R in _ListRes)
                        {
                            if (R.Date <= datereserv && R.Date >= datereserv)
                            {
                                
                                _ListTableUtilise.AddRange(R.TableUtilise);
                            }
                        }
                        
                        tableReserv = ChoixTables(_ListTableUtilise, nbrPer);

                        foreach (Table T in tableReserv)
                        {
                            Console.WriteLine(T);
                            Console.WriteLine("==================");
                        }
                    }
                    if (tableReserv.Count != 0 && servCourant.testAjout(menuSelect, nbrPer, emport) == true)
                    {
                        //Console.WriteLine(VerifTables(_ListTableUtilise, nbrPer));
                        Reservation Ra = new Reservation(ClientRes, nbrPer, datereserv, _Reserv, tableReserv, menuSelect);
                        _ListRes.Add(Ra);
                        servCourant.AjoutReservation(Ra);
                        Console.WriteLine("Reservation ajoutée !");
                        _doc.Save(chemin);
                        Console.ReadLine();
                    }
                    else if(tableReserv.Count == 0 && servCourant.testAjout(menuSelect, nbrPer, emport) == true)
                    {
                        Console.WriteLine("Nous n'avons pas assez de tables");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Erreur dans l'ajout de la réservation");
                        Console.ReadLine();
                    }
                }
            
        }
		/*===========================================================
         * public List<Table> ChoixTables( List<Table>tableUtilise, int nbPers)
         * Parametre d'entrée:
         * List<Table>tableUtilise -> Liste des tables déjà réservées
         * int nbPers -> Nombre de personnes à loger sur une table
         * Parametre de sortie:
         * tableReserv -> Liste de tables avec la nouvelle réservation
         * Role : 
         * Permet de choisir une ou des table(s) pour la réservation, en fonction
         * du nombre de personnes, de la disponibilité des
         * tables ainsi que de la capacité d'accueil.
         * ==========================================================*/

        public List<Table> ChoixTables( List<Table>tableUtilise, int nbPers)
        {
            List<Table> tableReserv=new List<Table>();
            List<Table> tableTri = new List<Table>();
            List<TableJumelable> tableTriJum = new List<TableJumelable>();
                foreach(Table Tr in _ListTable)
                {
                    if(Tr.NbrPlace>=nbPers&&!tableUtilise.Contains(Tr)&&Tr.Type=="Ronde")
                    {
                        tableTri.Add(Tr);
                    }
                    tableTri.Sort((x, y) => string.Compare(x.NbrPlace.ToString(), y.NbrPlace.ToString()));
                }
                
                if (tableTri.Count == 0)
                {
                    foreach (Table Tj in _ListTable)
                    {
                        if (Tj.NbrPlace >= nbPers && !tableUtilise.Contains(Tj) && Tj.Type != "Ronde")
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
                    if (!tableUtilise.Contains(Tj) && Tj.Type !="Ronde")
                    {                        
                        tableTriJum.Add((TableJumelable)Tj);
                    }
                }
                _ListCombi.Clear();
                CombinaisonTable(tableTriJum, tableTriJum.Count, 0, new List<TableJumelable>(), 0);
                _ListCombi = testJumelable(_ListCombi);
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

        /*===========================================================
         * public  void CombinaisonTable(List<TableJumelable> LT, int profMax,
         int profCourante, List<TableJumelable> prefix, int rang)
         * Parametre d'entrée:
         * List<TableJumelable> LT : Liste des tables jumelables disponibles pour la réservation
         * int profMax : nombres de tables maximum à jumeler entre elles
         * int profCourante : nombres de tables à jumeler entre elles
         * List<TableJumelable> prefix : Liste de tables à essayer d'ajouter avec les autres
         * int rang : definit la table à ajouter au prefix
         * Role:
         * Permet de trouver toutes les combinaisons possibles entre
         * des tables jumelables entre elles.
         * Ces combinaisons sont rentrées dans une liste de liste de tables.
         * ==========================================================*/

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

		/*===========================================================
         * public List<List<TableJumelable>>  testJumelable(List<List<TableJumelable>> list)
         * Parametre d'entrée:
         * List<List<TableJumelable>> list -> liste des listes de tables jumelables
         * Role:
         * Retourne les combinaisons des tables qui sont jumelables entre elles.
         * ==========================================================*/

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

		/*===========================================================
         * public int places(List<TableJumelable> LT)
         * Parametre d'entrée:
         * List<TableJumelable> LT -> liste des tables jumelables
         * Parametre de sortie:
         * nbplaces -> nombre de places totales de la liste de tables passées en pramètre.
         * Role:
         * Permet de calculer le nombre de places totales que peut 
         * supporter une combinaison de tables.
         * ==========================================================*/

        public int places(List<TableJumelable> LT)
        {
            int nbplaces=0;
            foreach (TableJumelable T in LT)
            {
                nbplaces += T.NbrPlace;
            }
            nbplaces -= (LT.Count * 2) - 2;
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
    

