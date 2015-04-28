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
        private string chemin ;

        public Restaurant()
        {
            
            _doc = Initialize();
            while(i<_nbrTable)
            {
                addTable();
                i++;
            }
            _doc.Save(chemin);
            
        }

        public Restaurant(string path)
        {
            _doc = XDocument.Load(path);
            _adresse = _doc.Element("Restaurant").Element("Caractéristiques").Element("Adresse").Value;
            _nbrTable = int.Parse(_doc.Element("Restaurant").Element("Caractéristiques").Element("Nbre_Tables").Value);
            _nbrEmploye = int.Parse(_doc.Element("Restaurant").Element("Caractéristiques").Element("Nbre_Employés").Value);
            Console.WriteLine("Addresse : "+_adresse);
            Console.WriteLine("Nombres de tables : " + _nbrTable);
            Console.WriteLine("Nombres d'emplyoés : " + _nbrEmploye);
            var tableC = from a in _doc.Descendants("Rectangulaire")
                        select a;

            foreach (XElement e in tableC.Descendants("table"))
            {
                int nbrPlace = int.Parse(e.Element("nbrPlace").Value);
                int longu = int.Parse(e.Element("long").Value);
                int large = int.Parse(e.Element("large").Value);
                int id = int.Parse(e.Element("ID").Value);
                _ListTable.Add(new Table_Rect(nbrPlace, longu, large, _table));
                //Console.WriteLine(nbrPlace+"//"+longu+"//"+large);
                /*if (e.ElementsAfterSelf() == _Jum)
                {
                    Console.WriteLine("aaa");
                    if (_Jum.ElementsAfterSelf() == _tableCarre)
                        Console.WriteLine("carre");
                    else
                        Console.WriteLine("rectang");
                }
                else
                    Console.WriteLine("bbb");*/
            }
        }

        private void addTable()
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
                _ListTable.Add(new Table_Ronde(_nbrePlace, _cote, _table));
            }
            else
            {
                Console.WriteLine("Mauvaise Saisie");
            }
        }

        public override string ToString()
        {
            string ch = "";
            foreach(Table T in _ListTable)
            {
                ch+=T.ToString()+"\n======================\n";
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
            int nbr = int.Parse(Console.ReadLine());
            _nbrTable = nbr;
            //_nbrEmploye = nbrEmploye;
            _ListTable = new List<Table>();
            _adresse = ad;
            _nom = nom;
            chemin = _nom + ".xml";
            XDocument doc = new XDocument(
        new XElement("Restaurant"));
           
            doc.Element("Restaurant").Add(_table);
            doc.Element("Restaurant").Add(new XElement("Personnel"));
            doc.Element("Restaurant").Add(_Carac);
            _table.Add(_Jum, _NonJum);
            _Jum.Add(_tableRect, _tableCarre);
            _NonJum.Add(_tableRonde);
            _tableCarre.Add(_Dispo, _Util);
            _tableRect.Add(_Dispo, _Util);
            _tableRonde.Add(_Dispo, _Util);
            _Carac.Add(new XElement("Adresse", _adresse), new XElement("Nbre_Tables", _nbrTable),new XElement("Nbre_Employés", _nbrEmploye));
          
            doc.Save(chemin);
            return doc;
        }

    }
}
