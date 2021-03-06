
Globals.RNG = new Random();
DiceFactory factory = new LetterDiceFactory();
IDiceGame diceGame = new DiceableGame(factory);
IWordsDictionary words = new BritishDictionary();
words.LoadDictionaryData();

Game g = new Game(diceGame, words);
PrintDice(g);
RequestInput();

void RequestInput()
{
    Console.WriteLine("What would you like to do? Type \"help\" if unsure!");
    var userAction = Console.ReadLine();
    while (ResolveUserInput(userAction))
    {
        userAction = Console.ReadLine();
    }
}

bool ResolveUserInput(string? userAction)
{
    if (userAction == null) return true;
    if (userAction.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
    {
        return false;
    }

    // check for help
    if (userAction.Equals("help", StringComparison.InvariantCultureIgnoreCase))
    {
        HandleHelpCommand();
    }
    else if (userAction.StartsWith("store ", StringComparison.InvariantCultureIgnoreCase))
    {
        var diceToStore = userAction.Substring(6);
        if (ValidateDiceToStore(diceToStore))
            HandleStoreDiceCommand(diceToStore);
    }
    else if (userAction.Equals("reroll all", StringComparison.InvariantCultureIgnoreCase))
    {
        HandleRerollAllCommand();
    }
    else if (userAction.Equals("roll", StringComparison.InvariantCultureIgnoreCase))
    {
        HandleRollCommand();
    }
    else if (userAction.Equals("print", StringComparison.InvariantCultureIgnoreCase))
    {
        HandlePrintCommand();
    }
    else if(userAction.Equals("score", StringComparison.InvariantCultureIgnoreCase))
    {
        HandleScoreCommand();
    }
    else if(userAction.Equals("check", StringComparison.InvariantCultureIgnoreCase))
    {
        HandleCheckCommand();
    }
    else if (userAction.Equals("newgame", StringComparison.InvariantCultureIgnoreCase))
    {
        HandleNewGameCommand();
    }
    else
    {
        // Unrecognised command.
        HandleUnrecognisedCommand();
    }

    return true;
}

void HandleCheckCommand()
{
    var wordMeaning = g.CheckWorkMeaning();
    if (string.IsNullOrWhiteSpace(wordMeaning))
    {
        Console.WriteLine($"Your current word ({g.StoredWord()}) does not appear in the dictionary.");
    }
    else
    {
        Console.WriteLine($"Your current word ({g.StoredWord()}) appears in the dictionary, with the following meaning:");
        Console.WriteLine(wordMeaning);
    }
}

void HandleScoreCommand()
{
    Console.WriteLine($"Your current score is:\t{g.CalculateScore()} points!");
}

void HandleNewGameCommand()
{
    g = new Game(diceGame, words);
    PrintDice(g);
    Console.WriteLine("What would you like to do? Type \"help\" if unsure!");
}

void HandlePrintCommand()
{
    PrintDice(g);
}

bool ValidateDiceToStore(string diceToStore)
{
    // must be non-empty.
    if (string.IsNullOrWhiteSpace(diceToStore))
    {
        Console.WriteLine("No dice specified.");
        return false;
    }

    if (diceToStore.IndexOf(",") == 0)
    {
        // Either there is only one dice to store, or the input is invalid.
        if (!g.GameDice.Any(x => x.DiceType.Equals(diceToStore, StringComparison.InvariantCultureIgnoreCase)))
        {
            Console.WriteLine("Dice not found. Please separate dice names using commas.");
            return false;
        }
    }

    // each die must exist in the game.
    foreach (string type in diceToStore.Split(","))
    {
        if (!g.GameDice.Any(x => x.DiceType.Equals(type, StringComparison.InvariantCultureIgnoreCase)))
        {
            Console.WriteLine($"Dice \"{type}\" not found.");
            return false;
        }
    }

    return true;
}

void HandleStoreDiceCommand(string diceToStore)
{
    Console.Write($"Storing {diceToStore}...");
    g.StoreDice(diceToStore.Split(","));
    Console.WriteLine("Done!");
}

void HandleRerollAllCommand()
{
    Console.Write("Rolling all dice...");
    if (!g.RollDice())
    {
        Console.WriteLine("You have already used your rolls!");
        return;
    }
    Console.WriteLine("Done!");
}

void HandleRollCommand()
{
    Console.Write("Rolling all unstored dice...");
    if (!g.RollUnstoredDice())
    {
        Console.WriteLine("You have already used your rolls!");
        return;
    }
    Console.WriteLine("Done!");
}

void HandleUnrecognisedCommand()
{
    Console.WriteLine("Sorry, try something else - type \"help\" if stuck");
}

void HandleHelpCommand()
{
    Console.WriteLine("Usage:");
    Console.WriteLine("help          Show this help.");
    Console.WriteLine("store X,Y,Z   Store the specified dice.");
    Console.WriteLine("              NOTE: Use the dice name, not the current face!");
    Console.WriteLine("reroll all    Re-roll ALL the dice, clearing your stored dice.");
    Console.WriteLine("roll          Roll any unstored dice.");
    Console.WriteLine("              NOTE: You only have two rerolls/rolls per game!");
    Console.WriteLine("print         Show the dice.");
    Console.WriteLine("score         Show the current score of the stored word.");
    Console.WriteLine("              NOTE: Score is the sum of the dice multiplied by the number of letters.");
    Console.WriteLine("check         Check your word appears in the dictionary.");
    Console.WriteLine("newgame       Start a new game.");
    Console.WriteLine("exit          Exit the game.");
}

void PrintDice(Game g)
{
    Console.WriteLine("The current dice are: ");

    foreach (var die in g.GameDice)
    {
        Console.Write($"{die.CurrentFace.FaceText} ");
    }
    Console.WriteLine();

    foreach (var die in g.StoredDice)
    {
        Console.Write($"{die.CurrentFace.FaceText} ");
    }
    Console.WriteLine();
    foreach (var die in g.GameDice.Where(die => !g.StoredDice.Contains(die)))
    {
        Console.Write($"{die.CurrentFace.FaceText} ");
    }
    Console.WriteLine();

    foreach (var die in g.GameDice)
    {
        Console.WriteLine($"{die.CurrentFace.FaceText} - {die.CurrentFace.FaceValue} points ({die.DiceType} die :: Possible faces: {string.Join(",", die.Faces.Select(x => x.FaceText))})");
    }
}