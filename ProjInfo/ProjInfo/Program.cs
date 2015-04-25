using System;
using System.Collections.Generic;
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
            
            /*Console.WriteLine("adresse : ");
            string ad = Console.ReadLine();
            Console.WriteLine("nbr table");
            //int nbr = int.Parse(Console.ReadLine());
            Restaurant R1 = new Restaurant(ad, nbr, 0);
            
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



			client arnaud;

			arnaud=new client("Clavero","Arnaud","06.67.02.68.04");
			Console.WriteLine(arnaud);
			arnaud.modificationClient();
			Console.ReadLine();

        }
    }
}
