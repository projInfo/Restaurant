using System;

namespace ProjInfo
{
	public class client
	{
		
            //Attributs de la classe client
		private string _nom;
		private string _prenom;
		private string _numeroTelephone;
        private static int _varId=0;
        private int _id;

		//Constructeur de la classe client
		public client (string nom, string prenom, string numeroTelephone)
		{
			_nom = nom;
			_prenom = prenom;
			_numeroTelephone = numeroTelephone;
            _varId++;
            _id = _varId;

		}

		//Accesseurs de la classe client
		public string Nom 
		{
			get { return _nom; }
			set { _nom = value; }
		}

		public string prenom
		{
			get{ return _prenom;}
			set{ _prenom=value; }
		}
        
		public string NumeroTelephone
		{
			get{ return _numeroTelephone;}
			set {_numeroTelephone = value;}
		}
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


		//Méthode Tostring()
		public override string ToString ()
		{
			string txt="";

			txt = "Nom: " + _nom + "\n" + "Prénom: " + _prenom + "\n" + "Numéro de téléphone: " + _numeroTelephone + "\n";

			return txt;

		}

		//Modification client
		public void modificationClient()
		{
			int choix =menuModificationClient ();
			Console.WriteLine ("le choix est le suivant:");
			Console.WriteLine (choix);
            	
		}

		public int menuModificationClient()
		{
			int choix;
			int i = 0;
			ConsoleKeyInfo a;
			string l1 = "1: Modifier le nom";
			string l2 = "2: Modifier le prénom";
			string l3 = "3: Modifier le numéro de téléphone";

			string affich = " " + l1 + "\n " + l2 + "\n " + l3;
			Console.WriteLine (affich);
			Console.Write ("→");
			Console.SetCursorPosition (0, i);
			Console.CursorVisible = false;
			do {

				a = Console.ReadKey ();
				if (a.Key == ConsoleKey.UpArrow) {
					if (i != 0) {
						i--;

					}


				} else if (a.Key == ConsoleKey.DownArrow) {
                    if (i < 2) {
						i++;

					}

				}
				Console.Clear ();
				Console.WriteLine (affich);
				Console.SetCursorPosition (0, i);
				Console.Write ("→");
				Console.SetCursorPosition (0, i);

			} while (a.Key != ConsoleKey.Enter);


				
			choix = i + 1;
			return choix;


			//test
		


		}
	}
}

