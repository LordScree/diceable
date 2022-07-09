public class Game
{
    public IEnumerable<Dice> GameDice { get; set; }

    public List<Dice> StoredDice { get; set; } = new List<Dice>();

    public Game(IDiceGame diceGame)
    {
        GameDice = diceGame.GetDice();
        RollDice();
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

    public void RollUnstoredDice()
    {
        if (StoredDice == null || StoredDice.Count() == 0)
        {
            RollDice();
            return;
        }

        // TODO: This implementation only works if GameDice only contains dice with unique dice types.
        RollDice(GameDice
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

    public void RollDice(params string[] types)
    {
        if (types == null)
        {
            RollAllDice();
            return;
        }
        // roll just the dice specified.
        foreach (Dice die in GameDice.Where(x => types.Contains(x.DiceType)))
        {
            die.RollDice();
        }
    }
}