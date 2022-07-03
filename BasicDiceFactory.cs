public class BasicDiceFactory : DiceFactory
{
    public override Dice GetDice(string type)
    {
        switch(type)
        {
            case "d6":
                return GetD6();
            default:
                throw new ArgumentException($"Type unsupported: {type}", "type");
        }
    }

    public override Dice GetDice()
    {
        return GetDice("d6");
    }

    private Dice GetD6()
    {
        return new Dice("d6",
            new DiceFace[] {
                new DiceFace("1", 1),
                new DiceFace("2", 2),
                new DiceFace("3", 3),
                new DiceFace("4", 4),
                new DiceFace("5", 5),
                new DiceFace("6", 6)
            }.AsEnumerable()
        );
    }
}