using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ProjInfo
{
    class Table
    {
        protected int _nbrPlace, _id = 0;
        protected bool _estJumele, _estDispo;
        protected static int _CompteTable=0;
        protected string _type, _dim;
        protected XDocument _doc;
        protected XElement _tabGen;
        public Table(int nbrPlace, XElement tableGen)    
        {
            _nbrPlace = nbrPlace;
            _CompteTable++;
            _id = _CompteTable;
            _tabGen = tableGen;
            
        }

        

        public Table(int nbrPlace, int id, XElement tableGen)
        {
            _nbrPlace = nbrPlace;
            _id = id;
            _tabGen = tableGen;
            if (id > _CompteTable)
                _CompteTable = id;

        }
        
        public void ModifPlace()
        {
            Console.WriteLine("Description de la table actuelle : ");
            Console.WriteLine(this);
            Console.Write("Entrez la nouvelle valeur de nombre de places : ");
            int newPlace = int.Parse(Console.ReadLine());
            var table = from a in _tabGen.Descendants("table")
                        select a;
            
            foreach (XElement e in table)
            {
                if (int.Parse(e.Element("ID").Value) == _id)
                {
                    
                    e.Element("nbrPlace").Value = newPlace.ToString();
                }

            }
        }

        protected virtual void GenereXml(bool dispo)
        {

        }

        public void Utilise(bool b)
        {
            var table = from a in _tabGen.Descendants("table")
                        select a;

            foreach (XElement e in table)
            {
                if (int.Parse(e.Element("ID").Value) == _id)
                {
                    int nbrPlace = int.Parse(e.Element("nbrPlace").Value);
                    int longu = int.Parse(e.Element("long").Value);
                    int large = int.Parse(e.Element("large").Value);
                    int id = int.Parse(e.Element("ID").Value);
                    if (e.Parent.Name=="Disponible"&&b==true)
                    {
                        e.Remove();
                        this.GenereXml(false);
                        Console.WriteLine("a");
                    }
                    else if (e.Parent.Name == "Disponible" && b == false)
                    {
                        Console.WriteLine("La table est déjà disponible");
                    }
                    else if (e.Parent.Name == "Utilisée" && b == false)
                    {
                        e.Remove();
                        this.GenereXml(true);
                        Console.WriteLine("b");
                    }
                    else
                    {
                        Console.WriteLine("La table estt déjà utilisée");
                    }

                    break;
                    //e.Remove();
                }

            }
        }

        public override string ToString()
        {
            string ch = "table de type : " + _type + "\n" + _dim + "\nNbr de places : " + _nbrPlace;
            return ch;
        }

        #region Accesseur
        protected int NbrPlace
        {
            get { return _nbrPlace; }
            set { _nbrPlace = value; }
        }
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool EstDispo
        {
            get { return _estDispo; }
            set { _estDispo = value; }
        }

        public bool EstJumele
        {
            get { return _estJumele; }
            set { _estJumele = value; }
        }
        #endregion

    }
}
