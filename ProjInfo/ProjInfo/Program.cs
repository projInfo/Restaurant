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

            /*client arnaud;

            arnaud = new client("Clavero", "Arnaud", "06.67.02.68.04");
            Console.WriteLine(arnaud);
            arnaud.modificationClient();
            Console.ReadLine();*/

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
            
            //Console.WriteLine(tabFiles[i]);
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
            string quest = "Que voulez vous faire ?";
            string affich = " Changer  le nombre de places disponibles\n Utiliser la table\n Rendre la table disponible\n\n"+tableSelec.ToString();
            int select = MenuFleches(quest, affich, 3, 1);
            //Console.WriteLine(tableSelec);
            Console.Clear();
            if (select == 0)
                tableSelec.ModifPlace();
            else if (select == 1)
                tableSelec.Utilise(true);
            else if (select == 2)
                tableSelec.Utilise(false);
            
            MenuNavigation(R);

            
        }

        public static void MenuNavigation(Restaurant R)
        {
            string affich = "";
            Console.Clear();
            string ch = "Que voulez-vous faire ?";
            affich += " Gérer les tables\n Gérer les repas\n Gérer les employer";
            int i=MenuFleches(ch, affich, 3, 1);
            if (i == 0)
                MenuTable(R);
            else if (i == 1)
                ;
            else if (i == 2)
                R.addEmploye();

           
            
        }

        public static void MenuTable(Restaurant R)
        {
            Console.Clear();
            string ch = "Menu gestion des tables";
            string affich = " Ajouter une tables\n Supprimer une table\n Modifier une table\n Jumeler 2 tables\n";
            int select = MenuFleches(ch, affich, 4, 1);
            if (select == 0)
                R.addTable();
            else if (select == 1)
                ;
            else if (select == 2)
                GestTable(R);
            else if (select == 3)
                R.JumeleTables();
                
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
