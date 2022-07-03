public class DiceableGame : IDiceGame
{
    public DiceFactory Factory { get; set; }

    public DiceableGame(DiceFactory factory)
    {
        Factory = factory;
    }

    public IEnumerable<Dice> GetDice()
    {
        return new Dice[] {
            Factory.GetDice("?"),
            Factory.GetDice("Y"),
            Factory.GetDice("J"),
            Factory.GetDice("K"),
            Factory.GetDice("Q"),
            Factory.GetDice("V"),
            Factory.GetDice("W"),
            Factory.GetDice("X"),
            Factory.GetDice("Z")            
        }.AsEnumerable();
    }
}