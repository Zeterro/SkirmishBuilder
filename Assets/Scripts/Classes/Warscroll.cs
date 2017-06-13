[System.Serializable]
public class Warscroll
{
    public string _name;
    public string _id;
    public int _cost;
    public int _maxNumber;

    public Warscroll(string name, int cost, int maxNumber = 1)
    {
        _name = name;
        _id = name.ToLower().Replace(" ", string.Empty);
        _cost = cost;
        _maxNumber = maxNumber;
    }
}