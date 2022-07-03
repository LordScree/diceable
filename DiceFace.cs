public class DiceFace
{
    public string FaceText { get; set; }
    public int FaceValue { get; set; }

    public DiceFace(string faceText, int faceValue)
    {
        FaceText = faceText;
        FaceValue = faceValue;
    }
}