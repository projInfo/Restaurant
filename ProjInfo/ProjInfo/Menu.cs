using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjInfo
{
    class Menu
    {
        private int _charge, _id;
        private string _nom;
        private static int _CompteMenu = 0;
        private XElement _xmlMenu, _xmlMenuCourant;

        public Menu(string nom, int charge, XElement xmlMenu)
        {
            //_duree = duree;
            _nom = nom;
            _charge = charge;
            _CompteMenu++;
            _id = _CompteMenu;
            _xmlMenu = xmlMenu;
            GenereXml();
        }

        public Menu(int id, string nom, int charge, XElement xmlMenu)
        {
            //_duree = duree;
            _nom = nom;
            _charge = charge;
            _xmlMenu = xmlMenu;
            _id = id;
            var menu = from a in _xmlMenu.Descendants("menu")
                          select a;

            foreach (XElement e in menu)
            {
                if (int.Parse(e.Element("ID").Value) == _id)
                    _xmlMenuCourant = e;
            } 
            if (id > _CompteMenu)
                _CompteMenu = id;
        }

        private void GenereXml()
        {
            _xmlMenuCourant = new XElement("menu", new XElement("ID", Id),
                new XElement("Nom", _nom), new XElement("Charge", _charge));
            _xmlMenu.Add(_xmlMenuCourant);                 
        }

        public void SuppXml()
        {
            _xmlMenuCourant.Remove();
        }

        public override string ToString()
        {
            string ch = "";
            ch += "\nId : " + _id + "\nNom du Menu : " + _nom  + "\nCharge : " + _charge + "\n";
            return ch;
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        /*public int Duree
        {
            get { return _duree; }
            set { _duree = value; }
        }*/

        public int Charge
        {
            get { return _charge; }
            set { _charge = value;
            _xmlMenuCourant.Element("Charge").Value = _charge.ToString();
            }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

    }
}
