public class Game
{
    public IEnumerable<Dice> GameDice { get; set; }

    public List<Dice> StoredDice { get; set; } = new List<Dice>();

    public int Rerolls { get; set; } = 0;

    public IWordsDictionary WordsDictionary { get; set; } = null!;

    public Game(IDiceGame diceGame, IWordsDictionary wordsDictionary)
    {
        WordsDictionary = wordsDictionary;
        GameDice = diceGame.GetDice();
        RollDice();
        EnforceRerollState(true);
    }

    public void StoreDice(params string[] types)
    {
        if (types.Length == 0)
        {
            ResetStoredDice();
            return;
        }

        StoredDice = new List<Dice>();
        foreach (var type in types)
        {
            var diceToStore = GameDice.SingleOrDefault(die => type.Equals(die.DiceType, StringComparison.InvariantCultureIgnoreCase));

            if (diceToStore != null)
            {
                StoredDice.Add(diceToStore);
            }
        }
    }

    private void ResetStoredDice()
    {
        StoredDice = new List<Dice>();
    }

    public bool RollUnstoredDice()
    {
        if (StoredDice == null || StoredDice.Count() == 0)
        {
            return RollDice();
        }

        // TODO: This implementation only works if GameDice only contains dice with unique dice types.
        return RollDice(GameDice
            .Where(die => !StoredDice.Contains(die))
            .Select(die => die.DiceType)
            .ToArray()
        );
    }

    private void RollAllDice()
    {
        // clear any stored dice.
        ResetStoredDice();

        // roll all the dice.
        foreach (Dice die in GameDice)
        {
            die.RollDice();
        }
    }

    /// <summary>
    /// Rolls the specifed dice, or all dice if no types are specified.
    /// </summary>
    /// <param name="types"></param>
    /// <returns>True if the reroll succeeded, otherwise False.</returns>
    public bool RollDice(params string[] types)
    {
        if (!EnforceRerollState()) return false;

        if (types.Length == 0)
        {
            RollAllDice();
            return true;
        }
        // roll just the dice specified.
        foreach (Dice die in GameDice.Where(x => types.Contains(x.DiceType)))
        {
            die.RollDice();
        }
        return true;
    }

    /// <summary>
    /// Calculates the current score of stored dice.
    /// </summary>
    /// <returns>The score.</returns>
    public int CalculateScore()
    {
        int score = 0;
        foreach (Dice die in StoredDice)
        {
            score = score + die.CurrentFace.FaceValue;
        }
        return score * StoredDice.Count();
    }

    public string CheckWorkMeaning()
    {
        string? result;
        WordsDictionary.Words.TryGetValue(StoredWord().ToLower(), out result);

        if (String.IsNullOrWhiteSpace(result))
        {
            return string.Empty;
        }

        return result;
    }

    public string StoredWord()
    {
        return String.Join("", StoredDice.Select(die => die.CurrentFace.FaceText));
    }

    /// <returns>True if the roll is permitted, otherwise False.</returns>
    private bool EnforceRerollState(bool reset = false)
    {
        if (reset)
        {
            Rerolls = 0;
            return true;
        }

        if (Rerolls >= 2) return false;
        Rerolls++;
        return true;
    }
}