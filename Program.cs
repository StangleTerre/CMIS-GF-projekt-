using System; 
using System.ComponentModel; 
using System.IO; 

namespace Informationsstander 
{
    class Program 
    {
        // En todimensionel array til at gemme brugerinformationsfelter: fornavn, efternavn, alder, adresse osv.
        static string[,] users = new string[100, 9]; // Maksimalt 100 brugere med 9 felter pr. bruger
        static int userCount = 0; // Tæller hvor mange brugere der er oprettet
        static string adminPassword = "admin123"; // Admin adgangskoden til beskyttet adgang
        static string filePath = "C:\\Users\\jola42\\Desktop\\Projekt CMIS\\user.txt"; // Stien til filen, hvor brugere gemmes

        static void Main(string[] args) 
        {
            // Henter eksisterende brugere fra filen
            LoadUsers(); // Kalder metoden LoadUsers for at indlæse brugere fra fil
            Menu(); // Kalder Menu metoden for at vise brugermenuen
        }

        static void Menu() // Metode til at vise menuen for programmet
        {
            bool keepRunning = true; // En boolean for at holde programmet kørende
            while (keepRunning) // Loop, der fortsætter så længe keepRunning er true
            {
                Console.Clear(); // Rydder konsollen hver gang menuen vises for at gøre det overskueligt
                Console.Title = "CMIS Informationsstander"; // Sætter titlen for vinduet
                Console.ForegroundColor = ConsoleColor.Cyan; // Sætter konsoltekstfarven til cyan
                Console.WriteLine("***********************************"); // Viser en dekorativ linje
                Console.WriteLine("*      CMIS Informationsstander   *"); // Viser programmets navn
                Console.WriteLine("***********************************\n"); // Viser en dekorativ linje
                Console.ResetColor(); // Nulstiller farven til standard

                Console.ForegroundColor = ConsoleColor.Yellow; // Sætter farven til gul
                Console.WriteLine("\n== Hovedmenu =="); // Viser overskriften for hovedmenuen
                Console.ResetColor(); // Nulstiller farven til standard
                Console.WriteLine("1. Opret bruger"); // Indikerer valgmulighed for at oprette en bruger
                Console.WriteLine("2. Admin login"); // Indikerer valgmulighed for admin login
                Console.WriteLine("3. Afslut program\n"); // Indikerer valgmulighed for at afslutte programmet
                Console.Write("Vælg en mulighed: "); // Beder brugeren om at vælge en mulighed

                string choice = Console.ReadLine(); // Læser brugerens valg fra konsollen

                // Switch for at håndtere brugerens valg
                switch (choice) // Skifter baseret på brugerens valg
                {
                    case "1": // Hvis valg er 1
                        CreateUser(); // Kalder funktionen for at oprette en ny bruger
                        break; // Afbryder switch statement
                    case "2": // Hvis valg er 2
                        AdminMenu(); // Kalder funktionen for at tilgå admin-menuen
                        break; // Afbryder switch statement
                    case "3": // Hvis valg er 3
                        Console.ForegroundColor = ConsoleColor.Green; // Sætter farven til grøn
                        Console.WriteLine("\nProgrammet afsluttes..."); // Informerer brugeren om at programmet lukker
                        Console.ResetColor(); // Nulstiller farven til standard
                        keepRunning = false; // Sætter keepRunning til false for at afslutte loopet
                        break; // Afbryder switch statement
                    default: // Hvis brugeren indtaster et ugyldigt valg
                        Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                        Console.WriteLine("Ugyldigt valg. Prøv igen."); // Informerer brugeren om ugyldigt valg
                        Console.ResetColor(); // Nulstiller farven til standard
                        break; // Afbryder switch statement
                }
            }
        }

        // Funktion til at indlæse eksisterende brugere fra en fil
        static void LoadUsers() // Metode til at indlæse brugere
        {
            if (File.Exists(filePath)) // Tjekker om filen med brugere eksisterer
            {
                var lines = File.ReadAllLines(filePath); // Læser alle linjer fra filen ind i et array
                foreach (var line in lines) // Itererer over hver linje i arrayet
                {
                    if (userCount >= users.GetLength(0)) // Tjekker om arrayet er fuldt
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                        Console.WriteLine("Brugerlisten er fuld. Kan ikke indlæse flere brugere."); // Informerer om fuld brugerliste
                        Console.ResetColor(); // Nulstiller farven til standard
                        break; // Afbryder loopet
                    }

                    var data = line.Split('|'); // Splitter linjen ind i datafelter ved '|'
                    if (data.Length == 9 && userCount < users.GetLength(0)) // Tjekker om dataene har korrekt længde og der er plads til flere brugere
                    {
                        for (int i = 0; i < data.Length; i++) // Loop igennem datafelterne
                        {
                            users[userCount, i] = data[i]; // Gemmer dataene i users arrayet
                        }
                        userCount++; // Øger tælleren for brugere
                    }
                    else // Hvis dataene ikke er gyldige
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                        Console.WriteLine($"Ugyldig data fundet i filen: {line}"); // Informerer om ugyldige data
                        Console.ResetColor(); // Nulstiller farven til standard
                    }
                }
            }
            else // Hvis filen ikke findes
            {
                Console.ForegroundColor = ConsoleColor.Magenta; // Sætter farven til magenta
                Console.WriteLine("Ingen eksisterende brugerdata fundet."); // Informerer om, at der ikke findes brugerdata
                Console.ResetColor(); // Nulstiller farven til standard
            }
        }

        // Funktion til at oprette en ny bruger
        static void CreateUser() // Metode til at oprette en bruger
        {
            Console.Clear(); // Rydder konsollen for at give et rent udseende
            string tlf; // Variabel til at gemme telefonnummeret
            bool validInput = false; // Boolean til at sikre gyldigt input

            // Telefonnummer validerings loop
            do // Loop, der fortsætter indtil der gives gyldigt input
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Sætter farven til gul
                Console.Write("\nIndtast 8-cifret telefonnummer (eller skriv 'N' for at springe over eller 'A' for at annullere): "); // Beder brugeren om telefonnummer
                Console.ResetColor(); // Nulstiller farven til standard
                tlf = Console.ReadLine(); // Læser telefonnummeret fra konsollen

                // Hvis brugeren vil springe over at indtaste telefonnummer
                if (tlf.Equals("N", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren vil springe over
                {
                    tlf = ""; // Sætter telefonnummer til en tom streng
                    validInput = true; // Sætter validInput til true, da brugeren har valgt at springe over
                }
                // Hvis brugeren ønsker at annullere oprettelsen
                else if (tlf.Equals("A", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren vil annullere
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                    Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                    Console.ResetColor(); // Nulstiller farven til standard
                    Menu(); // Går tilbage til hovedmenuen
                }
                // Tjekker om telefonnummeret er gyldigt (8 cifre og kun tal)
                else if (tlf.Length == 8 && long.TryParse(tlf, out _)) // Tjekker længden og om det er numerisk
                {
                    bool exists = false; // Variabel til at tjekke om nummeret allerede findes
                    // Loop igennem eksisterende brugere for at se, om nummeret allerede findes
                    for (int i = 0; i < userCount; i++) // Itererer over eksisterende brugere
                    {
                        if (users[i, 8] == tlf) // Tjekker om telefonnummeret allerede findes
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                            Console.WriteLine("Telefonnummeret findes allerede."); // Informerer om, at nummeret findes
                            Console.ResetColor(); // Nulstiller farven til standard
                            exists = true; // Sætter exists til true
                            break; // Afbryder loopet
                        }
                    }
                    // Hvis nummeret ikke findes, accepteres det
                    if (!exists) // Hvis nummeret ikke findes
                    {
                        validInput = true; // Sætter validInput til true for at acceptere input
                    }
                }
                else // Hvis input ikke er gyldigt
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                    Console.WriteLine("Indtast venligst et gyldigt 8-cifret telefonnummer eller 'N' for at springe over eller 'A' for at annullere."); // Informerer om, at input er ugyldigt
                    Console.ResetColor(); // Nulstiller farven til standard
                }
            } while (!validInput); // Loopet kører indtil input er gyldigt

            // Indsamler brugerens oplysninger med mulighed for annullering undervejs
            string fornavn = PromptForString("Indtast fornavn (eller 'A' for at annullere): "); // Kalder en metode for at indsamle fornavn
            if (fornavn.Equals("A", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren vil annullere
            {
                Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                Menu(); // Går tilbage til hovedmenuen
            }

            string efternavn = PromptForString("Indtast efternavn (eller 'A' for at annullere): "); // Kalder en metode for at indsamle efternavn
            if (efternavn.Equals("A", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren vil annullere
            {
                Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                Menu(); // Går tilbage til hovedmenuen
            }

            int alder = PromptForInt("Indtast alder (eller 'A' for at annullere): ", 0, 100); // Kalder en metode for at indsamle alder
            if (alder == -1) // Tjekker om annullering er valgt
            {
                Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                Menu(); // Går tilbage til hovedmenuen
            }

            string adresse = PromptForString("Indtast adresse og Hus nr. (eller 'A' for at annullere): "); // Kalder en metode for at indsamle adresse
            if (adresse.Equals("A", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren vil annullere
            {
                Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                Menu(); // Går tilbage til hovedmenuen
            }

            string postnummer = PromptForString("Indtast postnummer (eller 'A' for at annullere): "); // Kalder en metode for at indsamle postnummer
            if (postnummer.Equals("A", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren vil annullere
            {
                Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                Menu(); // Går tilbage til hovedmenuen
            }

            string by = PromptForString("Indtast by (eller 'A' for at annullere): "); // Kalder en metode for at indsamle by
            if (by.Equals("A", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren vil annullere
            {
                Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                Menu(); // Går tilbage til hovedmenuen
            }

            string email = PromptForEmail("Indtast email (eller 'A' for at annullere): "); // Kalder en metode for at indsamle email
            if (email.Equals("A", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren vil annullere
            {
                Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                Menu(); // Går tilbage til hovedmenuen
            }

            int frekvens = PromptForInt("Nyhedsbrev frekvens (12 for månedligt, 4 for kvartal, 1 for årligt, eller 'A' for at annullere): ", 1, 12); // Kalder en metode for at indsamle nyhedsbrev frekvens
            if (frekvens == -1) // Tjekker om annullering er valgt
            {
                Console.WriteLine("Oprettelse af bruger annulleret."); // Informerer om annullering
                Menu(); // Går tilbage til hovedmenuen
            }

            if (userCount < users.GetLength(0)) // Tjekker om der er plads til en ny bruger
            {
                // Gemmer brugerdata i arrayet
                users[userCount, 0] = fornavn; // Gemmer fornavn i arrayet
                users[userCount, 1] = efternavn; // Gemmer efternavn i arrayet
                users[userCount, 2] = alder.ToString(); // Gemmer alder i arrayet
                users[userCount, 3] = adresse; // Gemmer adresse i arrayet
                users[userCount, 4] = postnummer; // Gemmer postnummer i arrayet
                users[userCount, 5] = by; // Gemmer by i arrayet
                users[userCount, 6] = email; // Gemmer email i arrayet
                users[userCount, 7] = frekvens.ToString(); // Gemmer nyhedsbrev frekvens i arrayet
                users[userCount, 8] = tlf; // Gemmer telefonnummer i arrayet
                userCount++; // Øger antallet af brugere med 1

                SaveUsers(); // Kalder metoden til at gemme brugere til fil

                Console.ForegroundColor = ConsoleColor.Green; // Sætter farven til grøn
                Console.WriteLine("\nBruger oprettet succesfuldt."); // Informerer om, at brugeren er oprettet
                Console.ResetColor(); // Nulstiller farven til standard
            }
            else // Hvis brugerliste er fuld
            {
                // Gemmer den nye bruger til filen
                Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                Console.WriteLine("Brugerlisten er fuld. Kan ikke gemme nye brugere."); // Informerer om, at brugerliste er fuld
                Console.ResetColor(); // Nulstiller farven til standard
            }
        }

        // Hjælpefunktion til at anmode om streng input med en besked
        static string PromptForString(string message)
        {
            string input; // Variabel til at gemme brugerens input
            bool validInput = false; // Flag for at holde styr på, om input er gyldigt

            // Validerer at input ikke er tomt
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Sætter konsolens farve til gul
                Console.Write(message); // Viser besked til brugeren
                Console.ResetColor(); // Nulstiller farven til standard
                input = Console.ReadLine(); // Læser input fra brugeren
                if (string.IsNullOrWhiteSpace(input)) // Tjekker om input er tomt eller kun indeholder hvide tegn
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                    Console.WriteLine("Input kan ikke være tomt. Prøv venligst igen."); // Informerer om ugyldigt input
                    Console.ResetColor(); // Nulstiller farven til standard
                }
                else
                {
                    validInput = true; // Sætter validInput til true, hvis input er gyldigt
                }
            } while (!validInput); // Gentager indtil validInput er true
            return input; // Returnerer det gyldige input
        }

        // Funktion til at indhente tal og sikre det er i det ønskede interval
        static int PromptForInt(string message, int min, int max)
        {
            string input; // Variabel til at gemme brugerens input
            bool validInput = false; // Flag for at holde styr på, om input er gyldigt
            int result = -1; // Variabel til at gemme det konverterede tal

            // Læser og validerer input som tal og kontrollerer om det er inden for intervallet
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Sætter farven til gul
                Console.Write(message); // Viser besked til brugeren
                Console.ResetColor(); // Nulstiller farven til standard
                input = Console.ReadLine(); // Læser input fra brugeren
                if (input.Equals("A", StringComparison.OrdinalIgnoreCase)) // Tjekker om brugeren ønsker at annullere
                {
                    return -1; // Returnerer -1 for at indikere annullering
                }
                if (int.TryParse(input, out result) && result >= min && result <= max) // Tjekker om input kan konverteres til et tal og er inden for intervallet
                {
                    validInput = true; // Sætter validInput til true, hvis input er gyldigt
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                    Console.WriteLine($"Indtast venligst et gyldigt tal mellem {min} og {max}."); // Informerer om ugyldigt input
                    Console.ResetColor(); // Nulstiller farven til standard
                }
            } while (!validInput); // Gentager indtil validInput er true

            return result; // Returnerer det gyldige tal
        }

        // Hjælpefunktion til at anmode om en email
        static string PromptForEmail(string message)
        {
            string email; // Variabel til at gemme brugerens email
            bool validInput = false; // Flag for at holde styr på, om input er gyldigt

            // Validerer email formatet
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Sætter farven til gul
                Console.Write(message); // Viser besked til brugeren
                Console.ResetColor(); // Nulstiller farven til standard
                email = Console.ReadLine(); // Læser input fra brugeren
                if (string.IsNullOrWhiteSpace(email)) // Tjekker om input er tomt eller kun indeholder hvide tegn
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                    Console.WriteLine("Email kan ikke være tom. Prøv igen."); // Informerer om ugyldigt input
                    Console.ResetColor(); // Nulstiller farven til standard
                }
                else
                {
                    validInput = true; // Sætter validInput til true, hvis input er gyldigt
                }
            } while (!validInput); // Gentager indtil validInput er true

            return email; // Returnerer den gyldige email
        }

        // Admin menu for at få adgang til admin funktioner
        static void AdminMenu()
        {
            Console.Clear(); // Rydder konsollen
            Console.ForegroundColor = ConsoleColor.Yellow; // Sætter farven til gul
            Console.Write("Indtast adgangskode: "); // Anmoder om adgangskode
            Console.ResetColor(); // Nulstiller farven til standard
            string inputPassword = Console.ReadLine(); // Læser adgangskoden fra brugeren

            if (inputPassword == adminPassword) // Tjekker om den indtastede adgangskode er korrekt
            {
                Console.ForegroundColor = ConsoleColor.Green; // Sætter farven til grøn
                Console.WriteLine("Adgangskode korrekt!\nVelkommen Admin"); // Informerer om succes
                Console.ResetColor(); // Nulstiller farven til standard
                                      // Mulighed for at udvide med flere funktioner
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                Console.WriteLine("Forkert adgangskode."); // Informerer om forkert adgangskode
                Console.ResetColor(); // Nulstiller farven til standard
            }

            bool validInput = false; // Flag for at styre menu-loopet
                                     // Adminmenu med forskellige valgmuligheder
            while (!validInput)
            {
                Console.WriteLine("Admin menu:"); // Viser menuoverskrift
                Console.WriteLine("1. Find bruger"); // Valgmulighed for at finde en bruger
                Console.WriteLine("2. Vis alle brugere"); // Valgmulighed for at vise alle brugere
                Console.WriteLine("3. Beregn gennemsnitsalder"); // Valgmulighed for at beregne gennemsnitsalderen
                Console.WriteLine("4. Log ud"); // Valgmulighed for at logge ud

                string choice = Console.ReadLine(); // Læser brugerens valg

                // Switch for at vælge en af de tilgængelige admin-funktioner
                switch (choice)
                {
                    case "1":
                        FindUser(); // Finder en bestemt bruger
                        break;
                    case "2":
                        ShowAllUsers(); // Viser alle brugere
                        break;
                    case "3":
                        Console.Clear(); // Rydder konsollen
                        int sum = 0; // Variabel til at gemme summen af alderen
                        for (int i = 0; i < userCount; i++)
                        {
                            sum += int.Parse(users[i, 2]); // Lægger alle brugeres alder sammen
                        }

                        // Beregner gennemsnitsalderen
                        double average = (double)sum / userCount; // Gennemsnitsalder
                        Console.WriteLine($"Gennemsnitsalder: {average}"); // Viser gennemsnitsalderen
                        break;
                    case "4":
                        return; // Logger ud af admin-menuen
                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen."); // Informerer om ugyldigt valg
                        break;
                }
            }
        }

        // Gemmer alle brugere til filen

        /*StreamWriter bruges til at skrive tekst til filer.
          I dit program bruges det til at gemme brugernes data til en fil efter oprettelse af nye brugere.
          Ved hjælp af StreamWriter gemmer du dataen som en række strenge adskilt af en separator (|), som du senere kan læse og behandle.
          StreamWriter sikrer, at filen åbnes, dataene skrives effektivt, og filen lukkes korrekt bagefter.  */
        static void SaveUsers()
        {
            // Opretter en instans af StreamWriter til at skrive til filen
            using (StreamWriter writer = new StreamWriter(filePath, true)) // Åbner filen for at tilføje data
            {
                for (int i = 0; i < userCount; i++)
                {
                    // Skriver brugerdataen tilbage til filen, adskilt med '|'
                    writer.WriteLine(string.Join("|", users[i, 0], users[i, 1], users[i, 2], users[i, 3], users[i, 4], users[i, 5], users[i, 6], users[i, 7], users[i, 8])); // Gemmer alle brugerens data som en linje
                }
            } // StreamWriter lukker automatisk ved afslutningen af using-blokken
        }

        // Funktion til at finde en bestemt bruger baseret på telefonnummer eller navn
        static void FindUser()
        {
            Console.Clear(); // Rydder konsollen
            Console.ForegroundColor = ConsoleColor.Cyan; // Sætter farven til cyan
            Console.WriteLine("==== SØG EFTER BRUGER ===="); // Viser søgeoverskrift
            Console.ResetColor(); // Nulstiller farven til standard

            Console.Write("Indtast telefonnummer eller navn: "); // Anmoder om telefonnummer eller navn
            string search = Console.ReadLine(); // Læser brugerens input
            bool found = false; // Flag for at angive, om brugeren er fundet

            Console.ForegroundColor = ConsoleColor.Yellow; // Sætter farven til gul
            Console.WriteLine("\nResultater:"); // Viser resultater
            Console.ResetColor(); // Nulstiller farven til standard

            // Søger igennem alle brugere for at finde et match
            for (int i = 0; i < userCount; i++)
            {
                if (users[i, 7] == search || users[i, 0].Equals(search, StringComparison.OrdinalIgnoreCase) || users[i, 1].Equals(search, StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Sætter farven til grøn
                    Console.WriteLine($"Bruger fundet: {users[i, 0]} {users[i, 1]}"); // Viser den fundne bruger
                    Console.ResetColor(); // Nulstiller farven til standard
                    Console.WriteLine($"Alder: {users[i, 2]}"); // Viser brugerens alder
                    Console.WriteLine($"Adresse: {users[i, 3]}"); // Viser brugerens adresse
                    Console.WriteLine($"Postnummer: {users[i, 4]}"); // Viser brugerens postnummer
                    Console.WriteLine($"By: {users[i, 5]}"); // Viser brugerens by
                    Console.WriteLine($"Email: {users[i, 6]}"); // Viser brugerens email
                    Console.WriteLine($"Frekvens: {users[i, 7]}"); // Viser brugerens frekvens
                    Console.WriteLine($"Tlf: {users[i, 8]}\n"); // Viser brugerens telefonnummer
                    found = true; // Sætter found til true, da brugeren er fundet
                }
            }

            if (!found) // Hvis ingen bruger blev fundet
            {
                Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                Console.WriteLine("Ingen bruger fundet."); // Informerer om, at ingen brugere blev fundet
                Console.ResetColor(); // Nulstiller farven til standard
            }

            Console.WriteLine("\nTryk på en vilkårlig tast for at vende tilbage til hovedmenuen..."); // Viser besked om at vende tilbage
            Console.ReadKey(); // Venter på brugerens input
        }

        // Funktion til at vise alle brugere, med paginering hvis der er mange brugere
        static void ShowAllUsers()
        {
            Console.Clear(); // Rydder konsollen
            Console.ForegroundColor = ConsoleColor.Cyan; // Sætter farven til cyan
            Console.WriteLine("==== VIS ALLE BRUGERE ===="); // Viser overskrift for at vise alle brugere
            Console.ResetColor(); // Nulstiller farven til standard

            if (userCount == 0) // Tjekker om der er nogen brugere
            {
                Console.ForegroundColor = ConsoleColor.Red; // Sætter farven til rød
                Console.WriteLine("Ingen brugere fundet."); // Informerer om, at ingen brugere er fundet
                Console.ResetColor(); // Nulstiller farven til standard
                Console.WriteLine("\nTryk på en vilkårlig tast for at vende tilbage til hovedmenuen..."); // Viser besked om at vende tilbage
                Console.ReadKey(); // Venter på brugerens input
                return; // Afslutter funktionen
            }

            int usersPerPage = 12; // Variabel til at angive, hvor mange brugere der vises per side
            for (int i = 0; i < userCount; i += usersPerPage) // Løber igennem brugerne i intervaller
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Sætter farven til gul
                Console.WriteLine($"\nViser brugere {i + 1} til {Math.Min(i + usersPerPage, userCount)} af {userCount}:"); // Viser hvilke brugere der vises
                Console.ResetColor(); // Nulstiller farven til standard

                // Viser en side af brugere ad gangen
                for (int j = i; j < i + usersPerPage && j < userCount; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Sætter farven til grøn
                    Console.WriteLine($"\nBruger #{j + 1}:"); // Viser brugerens nummer
                    Console.ResetColor(); // Nulstiller farven til standard
                    Console.WriteLine($"Navn: {users[j, 0]} {users[j, 1]}"); // Viser brugerens navn
                    Console.WriteLine($"Alder: {users[j, 2]}"); // Viser brugerens alder
                    Console.WriteLine($"Adresse: {users[j, 3]}"); // Viser brugerens adresse
                    Console.WriteLine($"Postnummer: {users[j, 4]}"); // Viser brugerens postnummer
                    Console.WriteLine($"By: {users[j, 5]}"); // Viser brugerens by
                    Console.WriteLine($"Email: {users[j, 6]}"); // Viser brugerens email
                    Console.WriteLine($"Frekvens: {users[j, 7]}"); // Viser brugerens frekvens
                    Console.WriteLine($"Tlf: {users[j, 8]}"); // Viser brugerens telefonnummer
                    Console.WriteLine("------------------------------------"); // Laver en streg imellem brugernes data
                }

                // Hvis der er flere brugere end det, der kan vises på én side
                if (i + usersPerPage < userCount) // Tjekker om der er flere brugere at vise
                {
                    Console.WriteLine("Tryk på enter for at se næste side..."); // Viser besked om at se næste side
                    Console.ReadKey(); // Venter på brugerens input
                    Console.Clear(); // Rydder konsollen
                }
            }
        }
    }
}