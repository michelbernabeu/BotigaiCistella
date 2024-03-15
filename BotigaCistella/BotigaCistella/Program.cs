using System;

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

    // Métodos
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

    public bool AfegirProducte(Producte producte)
    {
        int posicio = EsapiLliure();
        if (posicio != -1)
        {
            productes[posicio] = producte;
            nElements++;
            return true;
        }
        else
        {
            Console.WriteLine("No es pot afegir més productes. Vols ampliar la tenda?");
            return false;
        }
    }

    public bool AfegirProducte(Producte[] productesNous)
    {
        if (nElements + productesNous.Length <= productes.Length)
        {
            Array.Copy(productesNous, 0, productes, nElements, productesNous.Length);
            nElements += productesNous.Length;
            return true;
        }
        else
        {
            Console.WriteLine("No hi ha prou espai per afegir tots els productes. Vols ampliar la tenda?");
            return false;
        }
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
                return true;
            }
        }
        Console.WriteLine("Producte no trobat.");
        return false;
    }

    public bool BuscarProducte(Producte producte)
    {
        for (int i = 0; i < nElements; i++)
        {
            if (productes[i] == producte)
                return true;
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
                productes[i].PreuSenseIVA = nouPreu;
                productes[i].Quantitat = novaQuantitat;
                return true;
            }
        }
        Console.WriteLine("Producte no trobat.");
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
                return true;
            }
        }
        Console.WriteLine("Producte no trobat.");
        return false;
    }

    public void Mostrar()
    {
        Console.WriteLine($"Botiga: {nomBotiga}");
        for (int i = 0; i < nElements; i++)
        {
            Console.WriteLine(productes[i]);
        }
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
    public void ComprarProducte(Producte producte, int quantitat)
    {
        if (botiga == null)
        {
            Console.WriteLine("No s'ha assignat cap botiga a la cistella.");
            return;
        }

        if (nElements >= productes.Length)
        {
            Console.WriteLine("No hi ha prou espai a la cistella. Amplia-la abans de continuar.");
            return;
        }

        if (producte == null)
        {
            Console.WriteLine("El producte no existeix.");
            return;
        }

        if (producte.Quantitat < quantitat)
        {
            Console.WriteLine("No hi ha prou stock del producte.");
            return;
        }

        double costTotal = producte.Preu() * quantitat;
        if (diners < costTotal)
        {
            Console.WriteLine("No tens prou diners per comprar aquest producte.");
            return;
        }

        // Añadir producto a la cesta
        productes[nElements] = producte;
        this.quantitat[nElements] = quantitat;
        nElements++;

        // Actualizar dinero
        diners -= costTotal;

        // Actualizar stock en la botiga
        producte.Quantitat -= quantitat;

        // Actualizar fecha
        data = DateTime.Now;
    }

    public void ComprarProducte(Producte[] productes, int[] quantitats)
    {
        if (botiga == null)
        {
            Console.WriteLine("No s'ha assignat cap botiga a la cistella.");
            return;
        }

        if (nElements + productes.Length > this.productes.Length)
        {
            Console.WriteLine("No hi ha prou espai a la cistella. Amplia-la abans de continuar.");
            return;
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
    }

    public double CostTotal()
    {
        double costTotal = 0;
        for (int i = 0; i < nElements; i++)
        {
            costTotal += productes[i].Preu() * quantitat[i];
        }
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
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Botiga botiga = new Botiga("Supermercat XYZ", 10);
        Cistella cistella = new Cistella();

        while (true)
        {
            Console.WriteLine("Benvingut a la botiga virtual!");
            Console.WriteLine("Escull una opció:");
            Console.WriteLine("1. Gestionar Botiga");
            Console.WriteLine("2. Gestionar Cistella");
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
                    Console.WriteLine("Opció no vàlida. Si us plau, introdueix un nombre vàlid.");
                    break;
            }
        }
    }

    static void GestionarBotiga(Botiga botiga)
    {
        while (true)
        {
            Console.WriteLine("\nGestionar Botiga:");
            Console.WriteLine("Escull una opció:");
            Console.WriteLine("1. Afegir Producte");
            Console.WriteLine("2. Mostrar Productes");
            Console.WriteLine("3. Tornar al menú principal");

            int opcio;
            if (!int.TryParse(Console.ReadLine(), out opcio))
            {
                Console.WriteLine("Opció no vàlida. Si us plau, introdueix un nombre vàlid.");
                continue;
            }

            switch (opcio)
            {
                case 1:
                    AfegirProducte(botiga);
                    break;
                case 2:
                    MostrarProductes(botiga);
                    break;
                case 3:
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

    static void GestionarCistella(Botiga botiga, Cistella cistella)
    {
        while (true)
        {
            Console.WriteLine("\nGestionar Cistella:");
            Console.WriteLine("Escull una opció:");
            Console.WriteLine("1. Comprar Producte");
            Console.WriteLine("2. Mostrar Cistella");
            Console.WriteLine("3. Tornar al menú principal");

            int opcio;
            if (!int.TryParse(Console.ReadLine(), out opcio))
            {
                Console.WriteLine("Opció no vàlida. Si us plau, introdueix un nombre vàlid.");
                continue;
            }

            switch (opcio)
            {
                case 1:
                    ComprarProducte(botiga, cistella);
                    break;
                case 2:
                    MostrarCistella(cistella);
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("Opció no vàlida. Si us plau, introdueix un nombre vàlid.");
                    break;
            }
        }
    }

    static void ComprarProducte(Botiga botiga, Cistella cistella)
    {
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
}
