using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ProjInfo
{
    abstract class Table
    {
        protected int _nbrPlace, _id = 0;
        protected bool _estJumele, _estDispo;
        protected static int _CompteTable=0;
        protected string _type, _dim;
        protected XDocument _doc;
        protected XElement _tabGen, _table;
        public Table(int nbrPlace, XElement tableGen)    
        {
            _nbrPlace = nbrPlace;
            _CompteTable++;
            _id = _CompteTable;
            _tabGen = tableGen;
            majXElem();
        }        

        public Table(int id,int nbrPlace, XElement tableGen)
        {
            _nbrPlace = nbrPlace;
            _id = id;
            _tabGen = tableGen;
            majXElem();
            if (id > _CompteTable)
                _CompteTable = id;

            var table = from a in _tabGen.Descendants("table")
                          select a;

            foreach (XElement e in table)
            {
                if (int.Parse(e.Element("ID").Value) == _id)
                    _table = e;
            }

        }
        
        private void majXElem()
        {
            if(this is TableJumelable)
            {
                _tabGen = _tabGen.Element("Jumelable");
                if (this is Table_Rect)
                    _tabGen = _tabGen.Element("Rectangulaire");
                else if (this is Table_Carre)
                    _tabGen = _tabGen.Element("Carré");
            }
            else
            {
                _tabGen = _tabGen.Element("Non_Jumelable").Element("Ronde");
            }
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

        public void suppXml()
        {
            _table.Remove();
        }

        protected virtual void GenereXml()
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
                        this.GenereXml();
                        _estDispo=false;
                    }
                    else if (e.Parent.Name == "Disponible" && b == false)
                    {
                        Console.WriteLine("La table est déjà disponible");
                    }
                    else if (e.Parent.Name == "Utilisée" && b == false)
                    {
                        e.Remove();
                        this.GenereXml();
                        _estDispo = true;
                        
                    }
                    else
                    {
                        Console.WriteLine("La table est déjà utilisée");
                    }

                    break;
                    //e.Remove();
                }

            }
        }
        
       /* public virtual void suppXml()
        {

        }*/

        public override string ToString()
        {
            string ch = "table de type : " + _type + "\nNuméro "+Id+"\n" + _dim + "\nNbr de places : " + _nbrPlace;
            return ch;
        }

        #region Accesseur
        public int NbrPlace
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

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion

    }
}
