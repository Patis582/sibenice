
class Program
{
    static void Main(String[] args)
    {
        PlayGame();
        

    }   
    static string PickWord()
    {
        try
        {
            string[] slova = File.ReadAllLines("slova.txt");
            Random random = new Random();
            return slova[random.Next(slova.Length)];
        }
        catch (Exception ex)
        {
            Console.WriteLine("Chyba při čtení souboru: " + ex.Message);
            return string.Empty;
        }
    }

    static void PlayGame()
    {   
        Console.WriteLine("Zadejte počet možných pokusů: ");
        int possibleTries;
        while (true)
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out possibleTries) && possibleTries > 0)
            {
                break; // Vstup je platný, ukonči cyklus
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Prosím zadejte platné kladné celé číslo.");
            }
        }
        
        int tries = 0;


        char[] wordLetters = PickWord().ToCharArray();
        char[] guessedLetters = new string('_', wordLetters.Length).ToCharArray();
        bool valid = false;
        while (!valid)
        {   
            Console.Clear();
            Console.WriteLine("Pokusy: " + tries + "/" + possibleTries);
            Console.WriteLine("Počet písmen: " + wordLetters.Length);
            Console.WriteLine("Napiš písmeno: ");
            Console.WriteLine(guessedLetters);
            
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                continue;
            }
            
            input = input.ToLower();
            bool found = false;

            if (input.Length != 1)
            {
                Console.WriteLine("Zadej prosím pouze jedno písmeno.");
                continue;
            }

            for (int i = 0; i < wordLetters.Length; i++)
            {
                if (wordLetters[i] == input[0])
                {
                    guessedLetters[i] = input[0];
                    found = true;
                }
            }

            if (!found)
            {
                tries ++;
            }

            
            if (tries >= possibleTries)
            {   
                Console.Clear();
                Console.WriteLine("Hádané slovo: " + new string(wordLetters));
                Console.WriteLine("Jsi oběšen ×_×");
                valid = true;
                Console.WriteLine("Pro pokračování stiskni enter.");
                Console.WriteLine("Pro otevreni webu s vyznamem slova stiskněte 1.");
                string inputW = Console.ReadLine();
                if (inputW == "1")
                {
                    OpenLink();
                }
                AskToPlayAgain();
            }else if (guessedLetters.SequenceEqual(wordLetters))
            {   
                Console.Clear();
                Console.WriteLine("Vyhrál jsii!!");
                Console.WriteLine("Uhodnuté slovo: " + new string(guessedLetters));
                valid = true;

                Console.WriteLine("Pro pokračování stiskni enter.");
                Console.WriteLine("Pro otevreni webu s vyznamem slova stiskněte 1.");
                string inputW = Console.ReadLine();
                if (inputW == "1")
                {
                    OpenLink();
                }
                AskToPlayAgain();
            }
        }
        

    }

    static void AskToPlayAgain()
    {
        Console.Clear();
        Console.WriteLine("Chcete hrát znovu? ano/ne");
        string input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            AskToPlayAgain();
            return;
        }
        else if (input == "ano")
        {
            PlayGame();
            return;
        }
        else if (input == "ne")
        {
            Console.Clear();
        }
        else
        {
            Console.Clear();
            AskToPlayAgain();
        }
    }

    static void OpenLink()
    {
        // Otevření odkazu ve webovém prohlížeči
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "https://cestina20.cz/pismeno/a/",
            UseShellExecute = true
        });
    }
    
}

