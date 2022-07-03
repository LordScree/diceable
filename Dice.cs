public class Dice : IComparable<Dice>
{
    public string DiceType { get; set; }
    public IEnumerable<DiceFace> Faces { get; set; }
    public DiceFace CurrentFace { get; set; } = null!;

    public Dice(string diceType, IEnumerable<DiceFace> faces)
    {
        DiceType = diceType;
        Faces = faces;

        RollDice();
    }

    public void RollDice()
    {
        // Update CurrentFace.
        CurrentFace = Faces.ElementAt(
            Globals.RNG.Next(Faces.Count())
        );
    }

    public int CompareTo(Dice? other)
    {
        return this.DiceType.CompareTo(other?.DiceType);
    }
}