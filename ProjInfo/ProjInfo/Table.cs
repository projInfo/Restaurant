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

        /*===========================================================
         * private void majXelem()
         * Role : Attribue la branche correspondant à l'objet courant 
         * à _tabGen
         * ==========================================================*/
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

        /*===========================================================
         * private void ModifPlace()
         * Role : Modifie le nombre de places de la table et modifie le 
         * fichier Xml en conséquant.
         * ==========================================================*/
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
            _nbrPlace = newPlace;
        }

        /*===========================================================
         * private void SuppXml()
         * Role : Supprime la branche correspondante à cette objet
         * ==========================================================*/
        public void suppXml()
        {
            _table.Remove();
        }

        /*===========================================================
         * private void GenereXml()
         * Role : Ajoute les informations de la table dans le fichier XML
         * Redéfinit dans les classes filles.
         * ==========================================================*/
        protected virtual void GenereXml()
        {
            
        }
       
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
