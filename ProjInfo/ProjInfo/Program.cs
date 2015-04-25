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
            
            Console.WriteLine("adresse : ");
            string ad = Console.ReadLine();
            Console.WriteLine("nbr table");
            int nbr = int.Parse(Console.ReadLine());
            Restaurant R1 = new Restaurant(ad, nbr, 0);
            //Console.WriteLine(R1);

        /*XDocument doc = new XDocument(
        new XElement("Restaurant"));
        XElement _table = new XElement("Table");
        XElement _Jum = new XElement("Jumelable");
        XElement _NonJum = new XElement("Non_Jumelable");
        XElement _tableRect = new XElement("Rectangulaie");
        XElement _tableCarre = new XElement("Carré");
        XElement _tableRonde = new XElement("Ronde");
        XElement _Dispo = new XElement("Disponible");
        XElement _Util = new XElement("Utilisée");
             doc.Element("Restaurant").Add(_table);
             doc.Element("Restaurant").Add(new XElement("Personnel"));
             _table.Add(_Jum, _NonJum);
             _Jum.Add(_tableRect, _tableCarre);
             _NonJum.Add(_tableRonde);
             _tableCarre.Add(_Dispo, _Util);
             _tableRect.Add(_Dispo, _Util);
             _tableRonde.Add(_Dispo, _Util);
            
            doc.Save(@"C:\Users\Guillaume\Desktop\ProjetInfo\test.xml");
            string var = (string)doc.Element("root");
            Console.WriteLine(var);*/
            

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
    }
}
