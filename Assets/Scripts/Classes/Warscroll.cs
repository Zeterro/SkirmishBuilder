[System.Serializable]
public class Warscroll
{
    public string _name;
    public Type _type;
    public string _id;
    public int _cost;
    public int _maxNumber;

    public Warscroll() { }

    public Warscroll(string name, Type type, int cost, int maxNumber = 1)
    {
        _name = name;
        _type = type;
        _id = name.ToLower().Replace(" ", string.Empty);
        _cost = cost;
        _maxNumber = maxNumber;
    }
}