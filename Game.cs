public class Game
{
    public IEnumerable<Dice> GameDice { get; set; }

    public List<Dice> StoredDice { get; set; } = new List<Dice>();

    public int Rerolls { get; set; } = 0;

    private object RerollLock = new object();

    public Game(IDiceGame diceGame)
    {
        GameDice = diceGame.GetDice();
        RollDice();
        EnforceRerollState(true);
    }

    public void StoreDice(params string[] types)
    {
        if (types == null)
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

        if (types == null)
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

    private bool EnforceRerollState(bool reset = false)
    {
        if(reset)
        {
            lock(RerollLock)
            {
                Rerolls = 0;
                return true;
            }
        }

        lock (RerollLock)
        {
            if (Rerolls >= 2) return false;
            Rerolls++;
            return true;
        }
    }
}