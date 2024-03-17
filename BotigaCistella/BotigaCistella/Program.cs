class Producte
{
    private string nom;
    private double preuSenseIVA;
    private double iva;
    private int quantitat;

    // Constructors
    public Producte()
    {
        iva = 0.21; // IVA per defecte del 21%
        quantitat = 0;
    }

    public Producte(string nom, double preu)
    {
        this.nom = nom;
        this.preuSenseIVA = preu;
        iva = 0.21;
        quantitat = 0;
    }

    public Producte(string nom, double preu, double iva, int quantitat)
    {
        this.nom = nom;
        this.preuSenseIVA = preu;
        this.iva = iva;
        this.quantitat = quantitat;
    }

    // Properties
    public string Nom
    {
        get { return nom; }
        set { nom = value; }
    }

    public double PreuSenseIVA
    {
        get { return preuSenseIVA; }
        set
        {
            if (value >= 0)
                preuSenseIVA = value;
            else
                Console.WriteLine("El preu no pot ser negatiu.");
        }
    }

    public double Iva
    {
        get { return iva; }
        set
        {
            if (value >= 0)
                iva = value;
            else
                Console.WriteLine("L'IVA no pot ser negatiu.");
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
                Console.WriteLine("La quantitat no pot ser negativa.");
        }
    }

    // Métodos públicos
    public double Preu()
    {
        return preuSenseIVA * (1 + iva);
    }

    public override string ToString()
    {
        return $"{nom}: {Preu()} € - Quantitat: {quantitat}";
    }
}

class Botiga
{
    private string nomBotiga;
    private Producte[] productes;
    private int nElements;

    // Constructors
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

    public Botiga(string nom, Producte[] productes)
    {
        nomBotiga = nom;
        this.productes = productes;
        nElements = productes.Length;
    }

    // Properties
    public string NomBotiga
    {
        get { return nomBotiga; }
        set { nomBotiga = value; }
    }

    public int NElements
    {
        get { return nElements; }
    }
    static void Return()
    {
        int i = 3;
        while (i != 0)
        {
            Console.Write("\r");
            Console.Write($"Tornant al menu {i}s");
            Thread.Sleep(1000);
            i--;
        }
    }
    // Métodos

    public void AfegirProductealCSV(Producte producte, string nombreArchivo)
    {
        productes[nElements] = producte;
        nElements++;
        using (var writer = new StreamWriter(nombreArchivo, true))
        {
            writer.WriteLine($"{producte.Nom},{producte.Preu()},{producte.Quantitat}");
        }

    }
    public int EsapiLliure()
    {
        for (int i = 0; i < productes.Length; i++)
        {
            if (productes[i] == null)
                return i;
        }
        return -1;
    }

    public Producte this[string nomProducte]
    {
        get
        {
            for (int i = 0; i < nElements; i++)
            {
                if (productes[i].Nom == nomProducte)
                    return productes[i];
            }
            return null;
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
                Return();
            }
        }
        Console.WriteLine("No s'ha trobat el producte");
        Return();
    }
    public void IrModificar(Botiga botiga, Producte producte, string nom)
    {

    }
    public bool AfegirProducte(Producte producte)
    {
        if (nElements < productes.Length)
        {
            productes[nElements] = producte;
            nElements++;
            Console.WriteLine("Producte afegit amb exit");
            Return();
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
                Return();
                return true;
            }
            else
            {
                Return();
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
                    Return();
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
                productes[i].PreuSenseIVA = preu;
                Return();
                return true;
            }
        }
        Console.WriteLine("Producte no trobat.");
        Return();
        return false;
    }
    public void BuscarProductesec(string nom)
    {
        for (int i = 0; i < nElements; i++)
        {
            if (productes[i].Nom == nom)
            {
                BuscarProducte(productes[i]);
                Return();

            }

        }
        Console.WriteLine("No s'ha trobat el producte");
        Return();

    }
    public bool BuscarProducte(Producte producte)
    {
        for (int i = 0; i < nElements; i++)
        {
            if (productes[i] == producte)
            {
                Console.WriteLine($"El producte es troba a la posicio {i}");
                Return();
                return true;
            }

        }
        Return();
        return false;
    }
    public void ModificarProductesec(Botiga botiga, string nom)
    {
        for (int i = 0; i < nElements; i++)
        {
            if (productes[i].Nom == nom)
            {
                Console.WriteLine("Quin nom li vols posar al producte?");
                string nounom = Console.ReadLine();
                Console.WriteLine("Quin preu li vols posar al producte?");
                double noupreu = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Quanta quantitat vols que hi hagi?");
                int novaquantitat = Convert.ToInt32(Console.ReadLine());
                ModificarProducte(productes[i], nom, nounom, noupreu, novaquantitat);
                Return();
            }

        }
        Console.WriteLine("No s'ha trobat el producte");
        Return();
    }

    public bool ModificarProducte(Producte producte, string nom, string nouNom, double nouPreu, int novaQuantitat)
    {
        for (int i = 0; i < nElements; i++)
        {
            if (productes[i].Nom == nom)
            {
                productes[i].Nom = nouNom;
                productes[i].PreuSenseIVA = nouPreu;
                productes[i].Quantitat = novaQuantitat;
                Console.WriteLine("Producte modificat amb exit");
                Return();
                return true;
            }
        }
        Console.WriteLine("Producte no trobat.");
        Return();
        return false;
    }

    public void OrdenarProducte()
    {
        for (int i = 0; i < nElements - 1; i++)
        {
            for (int j = 0; j < nElements - 1 - i; j++)
            {
                if (string.Compare(productes[j].Nom, productes[j + 1].Nom) > 0)
                {
                    Producte temp = productes[j];
                    productes[j] = productes[j + 1];
                    productes[j + 1] = temp;
                }
            }
        }
        Console.WriteLine("Productes ordenats");
        Return();

    }

    public void OrdenarPreus()
    {
        for (int i = 0; i < nElements - 1; i++)
        {
            for (int j = 0; j < nElements - 1 - i; j++)
            {
                if (productes[j].Preu() > productes[j + 1].Preu())
                {
                    Producte temp = productes[j];
                    productes[j] = productes[j + 1];
                    productes[j + 1] = temp;
                }
            }
        }
        Console.WriteLine("Productes ordenats per preu");
        Return();

    }
    public void EsborrarProductesec(string nom)
    {
        for (int i = 0; i < nElements; i++)
        {
            if (productes[i].Nom == nom)
            {
                EsborrarProducte(productes[i]);
                Return();
            }

        }
        Console.WriteLine("No s'ha trobat el producte");
        Return();
    }
    public bool EsborrarProducte(Producte producte)
    {
        for (int i = 0; i < nElements; i++)
        {
            if (productes[i] == producte)
            {
                Array.Copy(productes, i + 1, productes, i, nElements - i - 1);
                productes[nElements - 1] = null;
                nElements--;
                Return();
                return true;
            }
        }
        Console.WriteLine("Producte no trobat.");
        Return();
        return false;
    }

    public void Mostrar()
    {
        Console.WriteLine($"Botiga: {nomBotiga}");
        for (int i = 0; i < nElements; i++)
        {
            Console.WriteLine(productes[i]);
        }
        Return();
    }

    public override string ToString()
    {
        string result = $"Botiga: {nomBotiga}\n";
        int index = 0;
        while (index < productes.Length && productes[index] != null)
        {
            result += $"{productes[index]}\n";
            index++;
        }
        return result;
    }
}

class Cistella
{
    private Botiga botiga;
    private DateTime data;
    private Producte[] productes;
    private int[] quantitat;
    private int nElements;
    private double diners;

    // Constructor
    public Cistella()
    {
        botiga = null;
        data = DateTime.Now;
        productes = new Producte[10];
        quantitat = new int[10];
        nElements = 0;
        diners = 0;
    }

    public Cistella(Botiga botiga, Producte[] productes, int[] quantitats, double diners)
    {
        this.botiga = botiga;
        this.productes = new Producte[productes.Length];
        Array.Copy(productes, this.productes, productes.Length);
        this.quantitat = new int[quantitats.Length];
        Array.Copy(quantitats, this.quantitat, quantitats.Length);
        nElements = productes.Length;
        this.diners = diners;
    }

    // Properties
    public Producte[] Productes
    {
        get { return productes; }
    }

    public int NElements
    {
        get { return nElements; }
    }

    public double Diners
    {
        get { return diners; }
        set { diners = value; }
    }

    public DateTime Data
    {
        get { return data; }
    }

    // Métodos
    static void Return()
    {
        int i = 3;
        while (i != 0)
        {
            Console.Write("\r");
            Console.Write($"Tornant al menu {i}s");
            Thread.Sleep(1000);
            i--;
        }
    }
    public void AmpliarCistella(int num)
    {
        Array.Resize(ref productes, productes.Length + num);
    }
    public void AfegirDiners(double dinersafegits)
    {
        diners += dinersafegits;
        return;
    }
    public void ComprarProducte(Producte producte, int quantitat)
    {
        if (nElements >= productes.Length)
        {
            Console.WriteLine("No hi ha prou espai a la cistella. Vols ampliar-la? (S/N).");
            string resposta = Console.ReadLine();
            if (resposta.ToLower() == "s")
            {
                Console.WriteLine($"La cistella ara té {productes.Length} de capacitat, fins a quina capacitat la vols ampliar?");
                int num = Convert.ToInt32(Console.ReadLine());
                AmpliarCistella(num);
                productes[nElements] = producte;
                Console.WriteLine("Producte afegit amb exit");
                Return();
            }
            else
            {
                Return();
            }
        }

        if (producte == null)
        {
            Console.WriteLine("El producte no existeix.");
            Return();
            return;
        }

        if (producte.Quantitat < quantitat)
        {
            Console.WriteLine("No hi ha prou stock del producte.");
            Return();
            return;
        }

        double costTotal = producte.Preu() * quantitat;
        if (diners < costTotal)
        {
            Console.WriteLine("No tens prou diners per comprar aquest producte. Vols afegir diners? (S/N)");
            string resposta = Console.ReadLine();
            if (resposta.ToLower() == "s")
            {
                Console.WriteLine($"Ara tens {diners} d'euros, quants diners vols afegir?");
                double num = Convert.ToDouble(Console.ReadLine());
                AfegirDiners(num);
                ComprarProducte(producte, quantitat);
            }
            else
            {
                Return();
                return;
            }
        }

        // Añadir producte a la cistella
        productes[nElements] = producte;
        this.quantitat[nElements] = quantitat;
        nElements++;

        // Canviar els diners
        diners -= costTotal;

        // Actualitzar el stock en la botiga
        producte.Quantitat -= quantitat;

        // Actualizar la data
        data = DateTime.Now;

        Console.WriteLine("Producte afegit a la cistella");
        Return();
    }

    public void ComprarProducte(Producte[] productes, int[] quantitats)
    {
        if (botiga == null)
        {
            Console.WriteLine("No s'ha assignat cap botiga a la cistella.");
            Return();
        }

        if (nElements + productes.Length > this.productes.Length)
        {
            Console.WriteLine("No hi ha prou espai a la cistella. Amplia-la abans de continuar.");
            Return();
        }

        for (int i = 0; i < productes.Length; i++)
        {
            ComprarProducte(productes[i], quantitats[i]);
        }
    }

    public void OrdernarCistella()
    {
        for (int i = 0; i < nElements - 1; i++)
        {
            for (int j = 0; j < nElements - 1 - i; j++)
            {
                if (string.Compare(productes[j].Nom, productes[j + 1].Nom) > 0)
                {
                    Producte tempProducte = productes[j];
                    productes[j] = productes[j + 1];
                    productes[j + 1] = tempProducte;

                    int tempQuantitat = quantitat[j];
                    quantitat[j] = quantitat[j + 1];
                    quantitat[j + 1] = tempQuantitat;
                }
            }
            Console.WriteLine("Cistella ordenada amb exit");
            Return();
        }
    }

    public void Mostra()
    {
        Console.WriteLine($"Data de la compra: {data}");
        Console.WriteLine("Productes comprats:");
        for (int i = 0; i < nElements; i++)
        {
            Console.WriteLine($"Nom: {productes[i].Nom}, Quantitat: {quantitat[i]}, Preu unitari: {productes[i].Preu()}, Preu total: {productes[i].Preu() * quantitat[i]}");
        }
        Console.WriteLine($"Cost total amb IVA: {CostTotal()}");
        Return();

    }

    public double CostTotal()
    {
        double costTotal = 0;
        for (int i = 0; i < nElements; i++)
        {
            costTotal += productes[i].Preu() * quantitat[i];
        }
        Console.WriteLine($"El cost total es: {costTotal}");
        Return();
        return costTotal;
    }

    public override string ToString()
    {
        string result = $"Data de la compra: {data}\n";
        result += "Productes comprats:\n";
        for (int i = 0; i < nElements; i++)
        {
            result += $"Nom: {productes[i].Nom}, Quantitat: {quantitat[i]}, Preu unitari: {productes[i].Preu()}, Preu total: {productes[i].Preu() * quantitat[i]}\n";
        }
        result += $"Cost total amb IVA: {CostTotal()}\n";
        Return();
        return result;
    }
    public void MostrarBotiga()
    {
        Console.WriteLine($"Botiga: {botiga.NomBotiga}");
        for (int i = 0; i < nElements; i++)
        {
            Console.WriteLine(productes[i]);
        }
    }
    public void ComprarProducte(Producte producte, int quantitat, string nombreArchivo)
    {
        using (var writer = new StreamWriter(nombreArchivo, true))
        {
            writer.WriteLine($"{producte.Nom},{quantitat},{producte.Preu() * quantitat}");
        }
        Console.WriteLine("Dades del producte comprat afegides al fitxer CSV.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Botiga botiga = new Botiga("Kebab Chema", 10);
        Cistella cistella = new Cistella();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Benvingut a la botiga virtual!");
            Console.WriteLine("Escull una opció:");
            Console.WriteLine("1. Venedor");
            Console.WriteLine("2. Comprador");
            Console.WriteLine("3. Sortir");

            int opcio;
            if (!int.TryParse(Console.ReadLine(), out opcio))
            {
                Console.WriteLine("Opció no vàlida. Si us plau, introdueix un nombre vàlid.");
                continue;
            }

            switch (opcio)
            {
                case 1:
                    GestionarBotiga(botiga);
                    break;
                case 2:
                    GestionarCistella(botiga, cistella);
                    break;
                case 3:
                    Console.WriteLine("Gràcies per utilitzar la botiga virtual. Fins aviat!");
                    return;
                default:
                    Console.WriteLine("Opció no valida. Escriu un numero entre el 1 i el 3.");
                    break;
            }
        }
    }

    static void GestionarBotiga(Botiga botiga)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nGestionar Botiga:");
            Console.WriteLine("Escull una opció:");
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
            Console.WriteLine("-1. Tornar al menú principal");

            int opcio;
            if (!int.TryParse(Console.ReadLine(), out opcio))
            {
                Console.WriteLine("Opció no vàlida. Si us plau, introdueix un nombre vàlid.");
                continue;
            }

            switch (opcio)
            {
                case 1:
                    MostrarProductes(botiga);
                    break;
                case 2:
                    AfegirProducte(botiga);
                    break;
                case 3:
                //botiga.AfegirProductes(producte);
                //break;
                case 4:
                    ModificarPreu(botiga);
                    break;
                case 5:
                    BuscarProducte(botiga);
                    break;
                case 6:
                    ModificarProducte(botiga);
                    break;
                case 7:
                    botiga.OrdenarProducte();
                    break;
                case 8:
                    botiga.OrdenarPreus();
                    break;
                case 9:
                    EsborrarProducte(botiga);
                    break;
                case 10:
                    Indexador(botiga);
                    break;
                case 11:
                    botiga.ToString();
                    break;
                case -1:
                    return;
                default:
                    Console.WriteLine("Opció no vàlida. Si us plau, introdueix un nombre vàlid.");
                    break;
            }
        }
    }

    static void AfegirProducte(Botiga botiga)
    {
        Console.WriteLine("Introdueix el nom del producte:");
        string nom = Console.ReadLine();

        Console.WriteLine("Introdueix el preu del producte:");
        double preu;
        if (!double.TryParse(Console.ReadLine(), out preu))
        {
            Console.WriteLine("Preu no vàlid. Si us plau, introdueix un preu vàlid.");
            return;
        }

        Console.WriteLine("Introdueix la quantitat del producte:");
        int quantitat;
        if (!int.TryParse(Console.ReadLine(), out quantitat))
        {
            Console.WriteLine("Quantitat no vàlida. Si us plau, introdueix una quantitat vàlida.");
            return;
        }

        Producte producte = new Producte(nom, preu, 0.21, quantitat); // Afegeixo 0.21 com a valor per defecte per l'IVA
        if (botiga.AfegirProducte(producte))
        {
            Console.WriteLine("Producte afegit a la botiga amb èxit!");
        }
        else
        {
            Console.WriteLine("No s'ha pogut afegir el producte a la botiga.");
        }
    }

    static void MostrarProductes(Botiga botiga)
    {
        Console.WriteLine("\nProductes a la Botiga:");
        botiga.Mostrar();

    }
    static void ModificarPreu(Botiga botiga)
    {
        Console.WriteLine("De quin producte vols modificar el preu?");
        botiga.Mostrar();
        string nom = Console.ReadLine();
        Console.WriteLine("Quin preu li vols aplicar?");
        double preu = Convert.ToDouble(Console.ReadLine());
        if (botiga.ModificarPreu(nom, preu))
        {
            Console.WriteLine("Producte afegit a la botiga amb èxit!");
        }
        else
        {
            Console.WriteLine("No s'ha pogut afegir el producte a la botiga.");
        }
    }
    static void BuscarProducte(Botiga botiga)
    {
        Console.WriteLine("Quin producte vols comprobar si existeix?");
        string nom = Console.ReadLine();
        botiga.BuscarProductesec(nom);
    }
    static void ModificarProducte(Botiga botiga)
    {
        Console.WriteLine("Quin producte vols modificar?");
        botiga.Mostrar();
        string nom = Console.ReadLine();
        botiga.ModificarProductesec(botiga, nom);
    }
    static void EsborrarProducte(Botiga botiga)
    {
        Console.WriteLine("Quin producte vols eliminar?");
        botiga.Mostrar();
        string nom = Console.ReadLine();
        botiga.EsborrarProductesec(nom);

    }
    static void Indexador(Botiga botiga)
    {
        Console.WriteLine("Quin producte vols trobar?");
        string nom = Console.ReadLine();
        botiga.Indexador(nom);
    }
    static void GestionarCistella(Botiga botiga, Cistella cistella)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nGestionar Cistella:");
            Console.WriteLine("Escull una opció:");
            Console.WriteLine("1. Comprar Producte");
            Console.WriteLine("2. Comprar Productes");
            Console.WriteLine("3. Ordenar la cistella");
            Console.WriteLine("4. Mostrar Cistella");
            Console.WriteLine("5. Mostrar el cost total");
            Console.WriteLine("6. Cistella toString");
            Console.WriteLine("7. Afegir diners");
            Console.WriteLine("-1. Tornar al menú principal");

            int opcio;
            opcio = Convert.ToInt32(Console.ReadLine());

            switch (opcio)
            {
                case 1:
                    ComprarProducte(botiga, cistella);
                    break;
                case 2:
                    ComprarProducte(botiga, cistella);
                    break;
                case 3:
                    cistella.OrdernarCistella();
                    break;
                case 4:
                    MostrarCistella(cistella);
                    break;
                case 5:
                    cistella.CostTotal();
                    break;
                case 6:
                    cistella.ToString();
                    break;
                case 7:
                    Diners(cistella);
                    break;
                case -1:
                    return;
                default:
                    Console.WriteLine("Opció no vàlida. Si us plau, introdueix un nombre vàlid.");
                    break;
            }
        }
    }

    static void ComprarProducte(Botiga botiga, Cistella cistella)
    {
        cistella.MostrarBotiga();
        Console.WriteLine();
        Console.WriteLine("Introdueix el nom del producte que vols comprar:");
        string nom = Console.ReadLine();

        Console.WriteLine("Introdueix la quantitat del producte que vols comprar:");
        int quantitat;
        if (!int.TryParse(Console.ReadLine(), out quantitat))
        {
            Console.WriteLine("Quantitat no vàlida. Si us plau, introdueix una quantitat vàlida.");
            return;
        }

        Producte producte = botiga[nom];
        if (producte == null)
        {
            Console.WriteLine("Producte no trobat a la botiga.");
            return;
        }

        cistella.ComprarProducte(producte, quantitat);
    }

    static void MostrarCistella(Cistella cistella)
    {
        Console.WriteLine("\nContingut de la Cistella:");
        cistella.Mostra();
    }
    static void Diners(Cistella cistella)
    {
        Console.WriteLine("Quants diners vols que tingui la cistella?");
        double diners = Convert.ToDouble(Console.ReadLine());
        cistella.AfegirDiners(diners);
    }
}