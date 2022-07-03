public class LetterDiceFactory : DiceFactory
{
    public override Dice GetDice(string type)
    {
        switch(type)
        {
            case "?":
                return GetWildDice();
            case "Y":
                return GetYDice();
            case "J":
                return GetJDice();
            case "K":
                return GetKDice();
            case "Q":
                return GetQDice();
            case "V":
                return GetVDice();
            case "W":
                return GetWDice();
            case "X":
                return GetXDice();
            case "Z":
                return GetZDice();
            default:
                throw new NotImplementedException($"Type {type} not supported");
        }
    }

    private Dice GetZDice()
    {
        return new Dice("Z",
            new DiceFace[] {
                new DiceFace("Z", 8),
                new DiceFace("E", 1),
                new DiceFace("G", 2),
                new DiceFace("M", 3),
                new DiceFace("P", 3),
                new DiceFace("T", 1)
            }.AsEnumerable()
        );
    }

    private Dice GetXDice()
    {
        return new Dice("X",
            new DiceFace[] {
                new DiceFace("X", 7),
                new DiceFace("I", 1),
                new DiceFace("B", 3),
                new DiceFace("C", 3),
                new DiceFace("D", 2),
                new DiceFace("T", 1)
            }.AsEnumerable()
        );
    }

    private Dice GetWDice()
    {
        return new Dice("W",
            new DiceFace[] {
                new DiceFace("W", 3),
                new DiceFace("Y", 2),
                new DiceFace("B", 3),
                new DiceFace("N", 1),
                new DiceFace("P", 3),
                new DiceFace("S", 1)
            }.AsEnumerable()
        );
    }

    private Dice GetVDice()
    {
        return new Dice("V",
            new DiceFace[] {
                new DiceFace("V", 4),
                new DiceFace("U", 1),
                new DiceFace("C", 3),
                new DiceFace("F", 3),
                new DiceFace("G", 2),
                new DiceFace("S", 1)
            }.AsEnumerable()
        );
    }

    private Dice GetQDice()
    {
        return new Dice("Q",
            new DiceFace[] {
                new DiceFace("Q", 9),
                new DiceFace("O", 1),
                new DiceFace("L", 1),
                new DiceFace("R", 1),
                new DiceFace("S", 1),
                new DiceFace("T", 1)
            }.AsEnumerable()
        );
    }

    private Dice GetKDice()
    {
        return new Dice("K",
            new DiceFace[] {
                new DiceFace("K", 5),
                new DiceFace("E", 1),
                new DiceFace("D", 2),
                new DiceFace("F", 3),
                new DiceFace("H", 2),
                new DiceFace("L", 1)
            }.AsEnumerable()
        );
    }

    private Dice GetJDice()
    {
        return new Dice("J",
            new DiceFace[] {
                new DiceFace("J", 6),
                new DiceFace("A", 1),
                new DiceFace("H", 2),
                new DiceFace("M", 3),
                new DiceFace("N", 1),
                new DiceFace("R", 1)
            }.AsEnumerable()
        );
    }

    private Dice GetYDice()
    {
        return new Dice("Y",
            new DiceFace[] {
                new DiceFace("Y", 2),
                new DiceFace("A", 1),
                new DiceFace("E", 1),
                new DiceFace("I", 1),
                new DiceFace("O", 1),
                new DiceFace("U", 1)
            }.AsEnumerable()
        );
    }

    public override Dice GetDice()
    {
        return GetDice("?");
    }

    private Dice GetWildDice()
    {
        return new Dice("?",
            new DiceFace[] {
                new DiceFace("?", -2),
                new DiceFace("A", 1),
                new DiceFace("E", 1),
                new DiceFace("I", 1),
                new DiceFace("O", 1),
                new DiceFace("U", 1)
            }.AsEnumerable()
        );
    }
}