﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Restaurant
    {
        private string _adresse, _nom;
        private List<Table> _ListTable, _ListTableUtilise, _ListTableDispo;
        private List<Employe> _ListEmp = new List<Employe>();
        private List<Reservation> _ListRes = new List<Reservation>();
        private List<client> _ListClient = new List<client>();
        private List<Menu> _ListMenu = new List<Menu>();
        private List<Service> _ListServ = new List<Service>();
        private int _nbrTable, _nbrEmploye, i;
        private XDocument _doc;
        private XElement _table = new XElement("Tables");
        private XElement _Jum = new XElement("Jumelable");
        private XElement _NonJum = new XElement("Non_Jumelable");
        private XElement _tableRect = new XElement("Rectangulaire");
        private XElement _tableCarre = new XElement("Carré");
        private XElement _tableRonde = new XElement("Ronde");
        private XElement _Carac = new XElement("Caractéristiques");
        private XElement _Pers = new XElement("Personnel");
        private XElement _Reserv = new XElement("Reservation");
        private XElement _Service = new XElement("Service");
        private XElement _Menu = new XElement("Menu");
        private client ClientRes;
        private string chemin ;
        private List<List<TableJumelable>>_ListCombi=new List<List<TableJumelable>>();
        

        #region Constructeurs
        public Restaurant()
        {
            
            Initialize();
            
            _doc.Save(chemin);
            
        }

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
            _Reserv = _doc.Element("Restaurant").Element("Reservation");
            ChargeTable();
            ChargeEmploye();
            _doc.Save(chemin);
        }
#endregion

        #region Methodes
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

            if(_type=="1"||_type=="2"||_type=="3")
            {
                _nbrTable++;
                _Carac.Element("Nbre_Tables").Value = _nbrTable.ToString(); 
            }
            _doc.Save(chemin);
        }

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
            return ch;
        }

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
            _doc.Element("Restaurant").Add(_Reserv);
            _doc.Element("Restaurant").Add(_Carac);
            _Pers.Add(new XElement("Cuisinier"));
            _Pers.Add(new XElement("Serveur"));
            _table.Add(_Jum, _NonJum);
            _Jum.Add(_tableRect, _tableCarre);
            _NonJum.Add(_tableRonde);
            _Carac.Add(new XElement("Adresse", _adresse), new XElement("Nbre_Tables", _nbrTable),new XElement("Nbre_Employés", _nbrEmploye));
            InitRessources(nbrtable, nbrEmp);
            _doc.Save(chemin);
            
        }

        private void InitRessources(int nbTables, int nbEmp)
        {
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

            foreach (XElement e in serveur.Descendants("Employe"))
            {
                string nom = e.Element("Nom").Value;
                string prenom = e.Element("Prenom").Value;
                int id = int.Parse(e.Element("ID").Value);
                _ListEmp.Add(new Cuisinier(nom, prenom, _Pers, id));

            }
        }

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

        public void addEmploye()
        {
            Console.Write("Entrez le nom de l'employé : ");
            string nom = Console.ReadLine();
            Console.Write("Entrez le prénom de l'employé : ");
            string prenom = Console.ReadLine();                
            Console.WriteLine(@"Quelle est sa fonction? 
1:Serveur
2:Cuisnier");
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
            
            debutServ = DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture);
            Console.Clear();
            Console.WriteLine("Durée du service? (en heures)");
            int duree = int.Parse(Console.ReadLine());
            finServ = debutServ.AddHours(duree);
            bool check=true;
            foreach(Service Serv in _ListServ)
            {
                if((debutServ>Serv.Debut&&debutServ<Serv.Fin)||(finServ>Serv.Debut&&finServ<Serv.Fin))
                {
                    check = false;
                }
            }
            if (check == true)
            {
                Console.WriteLine("Il y a dejà un service à cette heure ci");
            }
            else
            {
                Service S = new Service(debutRes, finRes, _ListRes, _Service);
                _ListServ.Add(S);
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

        #region Gestion reservation
        public void JumeleTables()
        {
            Console.WriteLine("Sélectionner la table que vous voulez jumeler :");
            string ch = "";
            int i = 0; ;
            foreach (TableJumelable T in ListTable)
            {
                if (T.EstDispo)
                {
                    ch += "Table numéro " + i + "\n" + T.ToString() + "\n======================\n";
                    i++;
                }
            }
            Console.WriteLine(ch);
            int select = int.Parse(Console.ReadLine());
            Console.Clear();
            TableJumelable Tselec = (TableJumelable)ListTable.ElementAt(select);
            Console.WriteLine("Vous avez sélectionné :");
            Console.WriteLine(Tselec);
            Console.WriteLine("Cette table est jumelable avec :");
            i = 0;
            ch = "";
            foreach(TableJumelable T in ListTable)
            {
                if(T.Id!=ListTable.ElementAt(select).Id&&T.JumelableAvec(Tselec))
                {
                    ch += "Table numéro " + i + "\n" + T.ToString() + "\n======================\n";
                    Console.WriteLine(ch);
                    i++;
                }
            }
            if(i==0)
            {
                Console.WriteLine("Cette table n'est pas jumelable");
            }
            else
            {
                Console.WriteLine("Sélectionner la table : ");
                select=int.Parse(Console.ReadLine());
                TableJumelable Tselec2 = (TableJumelable)ListTable.ElementAt(select);
                Tselec.JumeleAvec(Tselec2);
                _doc.Save(chemin);
            }
        }

        public void AjoutReserv()
        {
            
            DateTime date=new DateTime();
            Console.WriteLine("etes vous déjà venu? (0:non   1:oui");
            int venu = int.Parse(Console.ReadLine());
            if(venu==1)
            {
                Console.WriteLine("Quel est votre nom");
                string nom=Console.ReadLine();
                foreach(client C in _ListClient)
                {
                    if(C.Nom==nom)
                    {
                        ClientRes = C;
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
                ClientRes = new client(nom, prenom, num);

            }
            Console.WriteLine("Combiens serez vous?");
            int nbrPer = int.Parse(Console.ReadLine());
            Menu menuSelect=null;
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

            datereserv = DateTime.Parse(dateR, System.Globalization.CultureInfo.InvariantCulture);
            Service servCourant=null;
            foreach(Service S in _ListServ)
            {
                if(datereserv>=S.Debut&&datereserv<=S.Fin)
                {
                    servCourant=S;
                }
            }
            if(servCourant == null)
            {
                Console.WriteLine("Il n'y a pas de service à cette date");
            }
            else
            {

            foreach(Reservation R in _ListRes)
            {
                if(R.Date<=datereserv&&R.Date>=datereserv)
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
            if (tableReserv.Count != 0&&servCourant.AjoutReservation(new Reservation(ClientRes, nbrPer, date, _Reserv, tableReserv, menuSelect))==true)
            {
                //Console.WriteLine(VerifTables(_ListTableUtilise, nbrPer));
                _ListRes.Add(new Reservation(ClientRes, nbrPer, date, _Reserv, tableReserv, menuSelect));
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
                Console.WriteLine("bbbbb");
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
