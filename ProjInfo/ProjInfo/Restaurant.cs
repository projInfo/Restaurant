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
        private string _adresse;
        private List<Table> _ListTable;
        private int _nbrTable, _nbrEmploye, i;
        private XDocument _doc;
        private XElement _table = new XElement("Tables");
        private XElement _Jum = new XElement("Jumelable");
        private XElement _NonJum = new XElement("Non_Jumelable");
        private XElement _tableRect = new XElement("Rectangulaie");
        private XElement _tableCarre = new XElement("Carré");
        private XElement _tableRonde = new XElement("Ronde");
        private XElement _Dispo = new XElement("Disponible");
        private XElement _Util = new XElement("Utilisée");

        public Restaurant(string adresse, int nbrtable, int nbrEmploye)
        {
            _nbrTable = nbrtable;
            _nbrEmploye = nbrEmploye;
            _ListTable=new List<Table>();
            _doc = Initialize();
            while(i<_nbrTable)
            {
                addTable();
                i++;
            }
            _doc.Save(@"C:\Users\Guillaume\Desktop\ProjetInfo\test.xml");
            
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
            XDocument doc = new XDocument(
        new XElement("Restaurant"));
           
            doc.Element("Restaurant").Add(_table);
            doc.Element("Restaurant").Add(new XElement("Personnel"));
            _table.Add(_Jum, _NonJum);
            _Jum.Add(_tableRect, _tableCarre);
            _NonJum.Add(_tableRonde);
            _tableCarre.Add(_Dispo, _Util);
            _tableRect.Add(_Dispo, _Util);
            _tableRonde.Add(_Dispo, _Util);
          
            doc.Save(@"C:\Users\Guillaume\Desktop\ProjetInfo\test.xml");
            return doc;
        }

    }
}
