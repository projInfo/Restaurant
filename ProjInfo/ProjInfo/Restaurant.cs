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
        private string _adresse, _nom;
        private List<Table> _ListTable;
        private List<Employe> _ListEmp = new List<Employe>();        
        private int _nbrTable, _nbrEmploye, i;
        private XDocument _doc;
        private XElement _table = new XElement("Tables");
        private XElement _Jum = new XElement("Jumelable");
        private XElement _NonJum = new XElement("Non_Jumelable");
        private XElement _tableRect = new XElement("Rectangulaire");
        private XElement _tableCarre = new XElement("Carré");
        private XElement _tableRonde = new XElement("Ronde");
        private XElement _Dispo = new XElement("Disponible");
        private XElement _Util = new XElement("Utilisée");
        private XElement _Carac = new XElement("Caractéristiques");
        private XElement _Pers = new XElement("Personnel");
        private string chemin ;

        #region Constructeurs
        public Restaurant()
        {
            
            _doc = Initialize();
            
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

        private XDocument Initialize()
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
            XDocument doc = new XDocument(
        new XElement("Restaurant"));
           
            doc.Element("Restaurant").Add(_table);
            doc.Element("Restaurant").Add(_Pers);
            doc.Element("Restaurant").Add(_Carac);
            _Pers.Add(new XElement("Cuisinier"));
            _Pers.Add(new XElement("Serveur"));
            _table.Add(_Jum, _NonJum);
            _Jum.Add(_tableRect, _tableCarre);
            _NonJum.Add(_tableRonde);
            _tableCarre.Add(_Dispo, _Util);
            _tableRect.Add(_Dispo, _Util);
            _tableRonde.Add(_Dispo, _Util);
            _Carac.Add(new XElement("Adresse", _adresse), new XElement("Nbre_Tables", _nbrTable),new XElement("Nbre_Employés", _nbrEmploye));
            InitRessources(nbrtable, nbrEmp);
            doc.Save(chemin);
            return doc;
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
            var tableRe = from a in _doc.Descendants("Rectangulaire")
                          select a;

            foreach (XElement e in tableRe.Descendants("table"))
            {
                int nbrPlace = int.Parse(e.Element("nbrPlace").Value);
                int longu = int.Parse(e.Element("long").Value);
                int large = int.Parse(e.Element("large").Value);
                int id = int.Parse(e.Element("ID").Value);
                _ListTable.Add(new Table_Rect(id, nbrPlace, longu, large, _table));

            }

            var tableC = from a in _doc.Descendants("Carré")
                         select a;

            foreach (XElement e in tableC.Descendants("table"))
            {
                int nbrPlace = int.Parse(e.Element("nbrPlace").Value);
                int cote = int.Parse(e.Element("Dim").Value);
                int id = int.Parse(e.Element("ID").Value);
                _ListTable.Add(new Table_Carre(id, nbrPlace, cote, _table));
            }

            var tableRo = from a in _doc.Descendants("Ronde")
                          select a;

            foreach (XElement e in tableRo.Descendants("table"))
            {

                int nbrPlace = int.Parse(e.Element("nbrPlace").Value); 
                int diam = int.Parse(e.Element("Diam").Value);
                int id = int.Parse(e.Element("ID").Value);
                _ListTable.Add(new Table_Ronde(id, nbrPlace, diam, _table));

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
