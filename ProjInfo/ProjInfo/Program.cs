
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ProjInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            OuvertureFichier();
          
            Console.ReadLine();
            #region Menu
            /*int i = 0;
            ConsoleKeyInfo a;
            bool aff=true;
            string l1 = "1";
            string l2 = "2";
            string l3 = "3";
           
            string affich = " " + l1 + "\n " + l2 + "\n " + l3;
            Console.WriteLine(affich);
            Console.SetCursorPosition(0, i);
            Console.Write("→");
            Console.SetCursorPosition(0, i);
            Console.CursorVisible = false;
            do
            {
                
                a = Console.ReadKey();
                if (a.Key == ConsoleKey.UpArrow)
                {
                    if (i != 0)
                    {
                        i--;
                       
                    }
                    

                }
                else if (a.Key == ConsoleKey.DownArrow)
                {
                    if (i < 2)
                    {
                        i++;
                        
                    }
                    
                }
                Console.Clear();
                Console.WriteLine(affich);
                Console.SetCursorPosition(0, i);
                Console.Write("→");
                Console.SetCursorPosition(0, i);
                
            }
            while (a.Key != ConsoleKey.Enter);
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(i+1);
            Console.ReadLine();*/
            #endregion


        }

        public static void OuvertureFichier()
        {
            Restaurant R1;
            string[] tabFiles;
            string chemin;
            chemin = Directory.GetCurrentDirectory();
            tabFiles = Directory.GetFiles(chemin, "*.xml");
            List<string> files = new List<string>(tabFiles);
            int i = 0;
            string affich = " ";
            foreach (string X in files)
            {
                affich += Path.GetFileName(X) + "\n ";
            }
            affich += "Créer un nouveau restaurant\n";
            i = MenuFleches(affich, tabFiles.Length );
            
            if (i == tabFiles.Length)
                R1 = new Restaurant();
            else
                R1 = new Restaurant(tabFiles[i]);
            Console.Clear();
            Console.WriteLine(R1);
            Console.WriteLine("Appuyer sur 'Entrée' pour passer au menu");
            Console.ReadLine(); 
            MenuNavigation(R1);
            
        }

        public static void GestTable(Restaurant R)
        {
            Console.Clear();
            Console.WriteLine("Voici les tables du restaurant :");
            Console.WriteLine("\n======================");
            string ch = "";
            int i = 0;
            foreach (Table T in R.ListTable)
            {
                ch +="Table numéro "+i+"\n"+ T.ToString() + "\n======================\n";
                i++;
            }
            Console.WriteLine(ch);
            Console.Write("Entrez le numéro de la table que vous voulez modifier : ");
            int num = int.Parse(Console.ReadLine());
            Table tableSelec=R.ListTable.ElementAt(num);
            Console.Clear();
            tableSelec.ModifPlace();          
            
            MenuNavigation(R);

            
        }

        public static void GestEmp(Restaurant R)
        {
            Console.Clear();
            string chPrec = "Que voulez vous faire?";
            string ch = " Ajouter un employé\n Modifier la charge de travail d'un employé\n Supprimer un employé";
            int choix = MenuFleches(chPrec, ch, 3, 1);
            if (choix == 0)
                R.addEmploye();
            else if (choix == 1)
                R.ModifChargeEmp();
            else
                R.SuppEmp();                
        }
        
        public static void GestMenu(Restaurant R)
        {
            Console.Clear();
            string chPrec = "Que voulez vous faire?";
            string ch = " Ajouter un menu\n Modifier la charge de travail d'un menu\n Supprimer un menu";
            int choix = MenuFleches(chPrec, ch, 3, 1);
            if (choix == 0)
                R.ajoutMenu();
            else if (choix == 1)
                R.ModifChargeMenu();
            else
                R.SuppMenu(); 
        }

        public static void MenuNavigation(Restaurant R)
        {
            string affich = "";
            R.SaveDoc();
            Console.Clear();
            string ch = "Que voulez-vous faire ?";
            affich += " Gérer les tables\n Gérer les repas\n Gérer les employer\n Gérer les réservations\n Gérer les Services";
            int i=MenuFleches(ch, affich, 5, 1);
            if (i == 0)
                MenuTable(R);
            else if (i == 1)
                GestMenu(R);
            else if (i == 2)
                GestEmp(R);
            else if (i == 3)
                R.AjoutReserv();
            else if (i == 4)
                R.ajoutService();
            MenuNavigation(R);
           
            
        }

        public static void MenuTable(Restaurant R)
        {
            Console.Clear();
            string ch = "Menu gestion des tables";
            string affich = " Ajouter une tables\n Supprimer une table\n Modifier une table\n";
            int select = MenuFleches(ch, affich, 3, 1);
            if (select == 0)
                R.addTable();
            else if (select == 1)
                R.SuppTable();
            else if (select == 2)
                GestTable(R);
            MenuNavigation(R);
                
        }

        public static int MenuFleches(string affich, int nbrLignes)
        {
            return MenuFleches("",affich, nbrLignes, 0);
        }

        public static int MenuFleches(string texteAvant, string affich, int nbrLignes, int nbrLignesAvant)
        {
            int i = nbrLignesAvant;
            if (texteAvant != "")
                Console.WriteLine(texteAvant);
            Console.WriteLine(affich);
            ConsoleKeyInfo a;
            Console.SetCursorPosition(0, i);
            Console.Write("→");
            Console.SetCursorPosition(0, i);
            Console.CursorVisible = false;

            do
            {

                a = Console.ReadKey();
                if (a.Key == ConsoleKey.UpArrow)
                {
                    if (i != nbrLignesAvant)
                    {
                        i--;

                    }


                }
                else if (a.Key == ConsoleKey.DownArrow)
                {
                    if (i < nbrLignes)
                    {
                        i++;

                    }

                }
                Console.Clear();
                if (texteAvant != "")
                    Console.WriteLine(texteAvant);
                Console.WriteLine(affich);
                Console.SetCursorPosition(0, i);
                Console.Write("→");
                Console.SetCursorPosition(0, i);

            }
            while (a.Key != ConsoleKey.Enter);
            Console.SetCursorPosition(0, nbrLignes + 1);
            Console.CursorVisible = true;

            return i-nbrLignesAvant;
        }

        
    }
}
