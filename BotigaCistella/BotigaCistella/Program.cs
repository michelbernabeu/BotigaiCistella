using System;
using System.IO;

namespace BotigaiCistella
{
    class Program
    {
        static Botiga botiga;
        static Cistella cistella;

        static void Main(string[] args)
        {
            botiga = new Botiga("Mi Tienda", 20);

            CargarDatos();

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("Benvingut a la botiga!");
                Console.WriteLine("1. Comprador");
                Console.WriteLine("2. Venedor");
                Console.WriteLine("3. Sortir");
                Console.Write("Escull una opció: ");
                string opcio = Console.ReadLine();

                switch (opcio)
                {
                    case "1":
                        MenuComprador();
                        Return();
                        break;
                    case "2":
                        MenuVenedor();
                        Return();
                        break;
                    case "3":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opció no vàlida. Torna a intentar-ho.");
                        break;
                }
            }

            GuardarDatos();
        }

        static void MenuComprador()
        {
            bool sortir = false;
            while (!sortir)
            {
                Console.WriteLine("\nMenú Comprador");
                Console.WriteLine("1. Mostrar productes disponibles");
                Console.WriteLine("2. Afegir producte a la cistella");
                Console.WriteLine("3. Mostrar cistella");
                Console.WriteLine("4. Realitzar compra");
                Console.WriteLine("5. Tornar al menú principal");
                Console.Write("Escull una opció: ");
                string opcio = Console.ReadLine();

                switch (opcio)
                {
                    case "1":
                        MostrarProductes();
                        Return();
                        break;
                    case "2":
                        AfegirProducteCistella();
                        Return();
                        break;
                    case "3":
                        MostrarCistella();
                        Return();
                        break;
                    case "4":
                        RealitzarCompra();
                        Return();
                        break;
                    case "5":
                        sortir = true;
                        break;
                    default:
                        Console.WriteLine("Opció no vàlida. Torna a intentar-ho.");
                        break;
                }
            }
        }

        static void MenuVenedor()
        {
            bool sortir = false;
            while (!sortir)
            {
                Console.WriteLine("\nMenú Venedor");
                Console.WriteLine("1. Mostrar productes de la botiga");
                Console.WriteLine("2. Afegir producte a la botiga");
                Console.WriteLine("3. Afegir productes a la botiga");
                Console.WriteLine("4. Modificar preu");
                Console.WriteLine("5. Buscar un producte");
                Console.WriteLine("6. Modificar producte");
                Console.WriteLine("7. Ordenar productes");
                Console.WriteLine("8. Ordenar per preus");
                Console.WriteLine("9. Esborrar producte");
                Console.WriteLine("10. Indexador");
                Console.WriteLine("11. To string");
                Console.WriteLine("q. Tornar al menú principal");
                Console.Write("Escull una opció: ");
                string opcio = Console.ReadLine();

                switch (opcio)
                {
                    case "1":
                        botiga.Mostrar();
                        break;
                    case "2":
                        AfegirProducteBotiga();
                        break;
                    case "3":
                        //Botiga.AfegirProductes();
                        break;
                    case "4":
                        Botiga.ModificarPreu();
                        break;
                    case "5":
                        Botiga.BuscarProducte();
                        break;
                    case "6":
                        Botiga.ModificarProducte();
                        break;
                    case "7":
                        //Botiga.OrdenarProducte();
                        break;
                    case "8":
                        //Botiga.OrdenarPreu();
                        break;
                    case "9":
                        Botiga.EsborrarProducte();
                        break;
                    case "10":
                        Botiga.Indexador();
                        break;
                    case "11":
                        Botiga.ToString();
                        break;
                    case "q":
                        sortir = true;
                        break;
                    default:
                        Console.WriteLine("Opció no vàlida. Torna a intentar-ho.");
                        break;
                }
            }
        }

        static void MostrarProductes()
        {
            Console.WriteLine("Productes disponibles:");
            for (int i = 0; i < botiga.Productes.Length; i++)
            {
                Producte producte = botiga.Productes[i];
                if (producte != null)
                {
                    Console.WriteLine($"{i + 1}. {producte}");
                }
            }
        }

        static void AfegirProducteCistella()
        {
            MostrarProductes();
            Console.Write("Introdueix el número del producte que vols afegir a la cistella: ");
            int indexProducte;
            if (int.TryParse(Console.ReadLine(), out indexProducte))
            {
                Console.Write("Introdueix la quantitat: ");
                int quantitat;
                if (int.TryParse(Console.ReadLine(), out quantitat))
                {
                    Producte producteSeleccionat = botiga.ObtenirProducte(indexProducte);
                    if (producteSeleccionat != null)
                    {
                        if (cistella == null)
                        {
                            cistella = new Cistella();
                        }
                        cistella.ComprarProducte(producteSeleccionat, quantitat);
                    }
                    else
                    {
                        Console.WriteLine("El producte seleccionat no és vàlid.");
                    }
                }
                else
                {
                    Console.WriteLine("Quantitat no vàlida.");
                }
            }
            else
            {
                Console.WriteLine("Número de producte no vàlid.");
            }
        }

        static void MostrarCistella()
        {
            if (cistella == null)
            {
                Console.WriteLine("La cistella està buida.");
            }
            else
            {
                Console.WriteLine("Cistella:");
                for (int i = 0; i < cistella.Productes.Length; i++)
                {
                    Producte producte = cistella.Productes[i];
                    if (producte != null)
                    {
                        Console.WriteLine($"{i + 1}. {producte}");
                    }
                }
            }
        }

        static void RealitzarCompra()
        {
            if (cistella == null || cistella.NElements == 0)
            {
                Console.WriteLine("La cistella està buida. No es pot realitzar la compra.");
            }
            else
            {
                Console.WriteLine("Compra realitzada amb èxit!");
                cistella = null;
            }
        }

        static void AfegirProducteBotiga()
        {
            Console.Write("Introdueix el nom del producte: ");
            string nomProducte = Console.ReadLine();
            Console.Write("Introdueix el preu del producte: ");
            double preuProducte;
            if (double.TryParse(Console.ReadLine(), out preuProducte))
            {
                Console.Write("Introdueix la quantitat del producte: ");
                int quantitatProducte;
                if (int.TryParse(Console.ReadLine(), out quantitatProducte))
                {
                    Producte nouProducte = new Producte(nomProducte, preuProducte);
                    botiga.AfegirProducte(nouProducte, quantitatProducte);
                    Console.WriteLine("Producte afegit amb èxit a la botiga.");
                }
                else
                {
                    Console.WriteLine("Quantitat no vàlida.");
                }
            }
            else
            {
                Console.WriteLine("Preu no vàlid.");
            }
        }
        static void GuardarDatos()
        {
            StreamWriter BotigaFitxer = null;
            StreamWriter CistellaFitxer = null;

            BotigaFitxer = new StreamWriter("botiga.csv");
            for (int i = 0; i < botiga.Productes.Length; i++)
            {
                Producte producte = botiga.Productes[i];
                if (producte != null)
                {
                    BotigaFitxer.WriteLine($"{producte.Nom},{producte.Preu},{producte.Quantitat}");
                }
            }
            BotigaFitxer.Close();

            if (cistella != null)
            {
                CistellaFitxer = new StreamWriter("cistella.csv");
                for (int i = 0; i < cistella.Productes.Length; i++)
                {
                    Producte producte = cistella.Productes[i];
                    if (producte != null)
                    {
                        CistellaFitxer.WriteLine($"{producte.Nom},{producte.Preu},{producte.Quantitat}");
                    }
                }
                CistellaFitxer.Close();
            }
        }
        static void CargarDatos()
        {
            if (File.Exists("botiga.csv"))
            {
                botiga = new Botiga();
                StreamReader readerBotiga = new StreamReader("botiga.csv");
                string line;
                while ((line = readerBotiga.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string nom = parts[0];
                    double preu = Convert.ToDouble(parts[1]);
                    int quantitat = Convert.ToInt32(parts[2]);
                    Producte producte = new Producte(nom, preu, quantitat);
                    botiga.AfegirProducte(producte, quantitat);
                }
                readerBotiga.Close();
            }

            if (File.Exists("cistella.csv"))
            {
                cistella = new Cistella();
                StreamReader readerCistella = new StreamReader("cistella.csv");
                string line;
                while ((line = readerCistella.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string nom = parts[0];
                    double preu = Convert.ToDouble(parts[1]);
                    int quantitat = Convert.ToInt32(parts[2]);
                    Producte producte = new Producte(nom, preu, quantitat);
                    cistella.ComprarProducte(producte, quantitat);
                }
                readerCistella.Close();
            }
        }
        static void Return()
        {
            int i = 5;
            while (i != 0)
            {
                Console.Write("\r");
                Console.Write($"Tornant al menu : {i}s");
                Thread.Sleep(1000);
                i--;
            }
            Console.Clear();
        }
    }
    class Producte
    {
        private string nom;
        private double preu;
        private int quantitat;

        public Producte()
        {
            quantitat = 0;
        }
        public Producte(string nom, double preu) : this()
        {
            this.nom = nom;
            this.preu = preu;
        }
        public Producte(string nom, double preu, int quantitat) : this(nom, preu)
        {
            this.quantitat = quantitat;
        }
        public string Nom
        {
            get { return nom; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    nom = value;
                else
                    Console.WriteLine("El nom no pot estar vuit");
            }
        }

        public double Preu
        {
            get { return preu; }
            set
            {
                if (value >= 0)
                    preu = value;
                else
                    Console.WriteLine("El preu no pot ser negatiu");
            }
        }

        public int Quantitat
        {
            get { return quantitat; }
            set
            {
                if (value >= 0)
                    quantitat = value;
                else
                    Console.WriteLine("La quantitat no pot ser negativa");
            }
        }

        public override string ToString()
        {
            return $"Producte: {nom}, Preu: {preu}, Quantitat: {quantitat}";
        }
    }
    class Botiga
    {
        private string nomBotiga;
        private Producte[] productes;
        private int nElements;

        public Botiga()
        {
            nomBotiga = "";
            productes = new Producte[10];
            nElements = 0;
        }

        public Botiga(string nom, int nombreProductes)
        {
            nomBotiga = nom;
            productes = new Producte[nombreProductes];
            nElements = 0;
        }

        public string NomBotiga
        {
            get { return nomBotiga; }
            set { nomBotiga = value; }
        }

        public Producte[] Productes
        {
            get { return productes; }
            set { productes = value; }
        }

        public int NElements
        {
            get { return nElements; }
            set { nElements = value; }
        }

        public int EspaiLliure()
        {
            for (int i = 0; i < productes.Length; i++)
            {
                if (productes[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public Producte ObtenirProducte(int index)
        {
            if (index >= 0 && index < productes.Length)
            {
                return productes[index - 1];
            }
            else
            {
                Console.WriteLine("Índex de producte fora de límits.");
                return null;
            }
        }

        public void AfegirProducte(Producte producte, int quantitat)
        {
            if (producte != null && quantitat > 0)
            {
                int indexLliure = EspaiLliure();
                if (indexLliure != -1)
                {
                    productes[indexLliure] = producte;
                    producte.Quantitat += quantitat;
                    nElements++;
                }
                else
                {
                    Console.WriteLine("No hi ha prou espai a la botiga.");
                }
            }
            else
            {
                Console.WriteLine("Producte o quantitat no vàlids.");
            }
        }
        public void Indexador(string nomProducte)
        {
            for (int i = 0; i < nElements; i++)
            {
                if (productes[i].Nom == nomProducte)
                {
                    Console.WriteLine($"El {nomProducte} es troba a la posició: {i}");
                    Console.WriteLine(productes[i]);
                    return;
                }
            }
            Console.WriteLine("No s'ha trobat el producte");
            return;
        }

        public bool AfegirProducte(Producte producte)
        {
            if (nElements < productes.Length)
            {
                productes[nElements] = producte;
                nElements++;
                Console.WriteLine("Producte afegit amb exit");
                return true;
            }
            else
            {
                Console.WriteLine("La botiga està plena. Vols ampliar-la? (S/N)");
                string resposta = Console.ReadLine();
                if (resposta.ToLower() == "s")
                {
                    Console.WriteLine($"La botiga ara té {productes.Length} de capacitat, fins a quina capacitat la vols ampliar?");
                    int num = Convert.ToInt32(Console.ReadLine());
                    AmpliarBotiga(num);
                    productes[nElements] = producte;
                    Console.WriteLine("Producte afegit amb exit");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AfegirProductes(Producte[] producte)
        {
            for (int i = 0; i < producte.Length; i++)
            {
                if (nElements < productes.Length)
                {
                    productes[nElements] = producte[i];
                    nElements++;
                }
                else
                {
                    Console.WriteLine("La botiga està plena. Vols ampliar-la? (S/N)");
                    string resposta = Console.ReadLine();
                    if (resposta.ToLower() == "s")
                    {
                        Console.WriteLine($"La botiga ara té {productes.Length} de capacitat i queden {producte.Length - i} per afegir, fins a quina capacitat la vols ampliar?");
                        int num = Convert.ToInt32(Console.ReadLine());
                        AmpliarBotiga(num);
                    }
                    else
                    {
                        Console.WriteLine($"Només s'han afegit {i} productes");
                        return false;
                    }
                }
            }
            Console.WriteLine("S'han afegit tots els productes");
            return true;
        }
        public void AmpliarBotiga(int num)
        {
            Array.Resize(ref productes, productes.Length + num);
        }

        public bool ModificarPreu(string nomProducte, double preu)
        {
            for (int i = 0; i < nElements; i++)
            {
                if (productes[i].Nom == nomProducte)
                {
                    productes[i].Preu = preu;
                    return true;
                }
            }
            Console.WriteLine("Producte no trobat.");
            return false;
        }

        public bool BuscarProducte(Producte producte)
        {
            foreach (Producte p in productes)
            {
                if (p == producte)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ModificarProducte(Producte producte, string nouNom, double nouPreu, int novaQuantitat)
        {
            for (int i = 0; i < nElements; i++)
            {
                if (productes[i] == producte)
                {
                    productes[i].Nom = nouNom;
                    productes[i].Preu = nouPreu;
                    productes[i].Quantitat = novaQuantitat;
                    return true;
                }
            }
            return false;
        }
        public bool EsborrarProducte(Producte producte)
        {
            for (int i = 0; i < nElements; i++)
            {
                if (productes[i] == producte)
                {
                    for (int j = i; j < nElements - 1; j++)
                    {
                        productes[j] = productes[j + 1];
                    }
                    productes[nElements - 1] = null;
                    nElements--;
                    return true;
                }
            }
            return false;
        }
        public void Mostrar()
        {
            if (nElements == 0)
            {
                Console.WriteLine("No hi ha productes a la botiga.");
            }
            else
            {
                Console.WriteLine($"Productes a la botiga '{nomBotiga}':");
                for (int i = 0; i < productes.Length; i++)
                {
                    Producte producte = productes[i];
                    if (producte != null)
                    {
                        Console.WriteLine($"{i + 1}. {producte}");
                    }
                }
            }
        }
        public override string ToString()
        {
            string result = $"Botiga: {nomBotiga}\nProductes:\n";
            for (int i = 0; i < nElements; i++)
            {
                result += $"{productes[i]}\n";
            }
            return result;
        }
    }

    class Cistella
    {
        private Producte[] productes;

        public Cistella()
        {
            productes = new Producte[10];
        }

        public Producte[] Productes
        {
            get { return productes; }
            set { productes = value; }
        }

        public int NElements
        {
            get
            {
                int count = 0;
                foreach (Producte producte in productes)
                {
                    if (producte != null)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public void ComprarProducte(Producte producte, int quantitat)
        {
            if (producte != null && quantitat > 0)
            {
                for (int i = 0; i < productes.Length; i++)
                {
                    if (productes[i] == null)
                    {
                        productes[i] = producte;
                        producte.Quantitat -= quantitat;
                        break;
                    }
                }
            }
        }
    }
}
