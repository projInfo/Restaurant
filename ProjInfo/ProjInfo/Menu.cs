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
        private int _duree, _charge, _id;
        private string _nom;
        private static int _CompteMenu = 0;
        private XElement _xmlMenu;

        public Menu(string nom, int duree, int charge, XElement xmlMenu)
        {
            _duree = duree;
            _nom = nom;
            _charge = charge;
            _CompteMenu++;
            _id = _CompteMenu;
            _xmlMenu = xmlMenu;
        }

        public Menu(int id, string nom, int duree, int charge, XElement xmlMenu)
            : this(nom, duree, charge, xmlMenu)
        {
            _id = id;
            if (id > _CompteMenu)
                _CompteMenu = id;
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public int Duree
        {
            get { return _duree; }
            set { _duree = value; }
        }

        public int Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

    }
}
