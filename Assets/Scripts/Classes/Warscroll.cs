[System.Serializable]
public class Warscroll
{
    public string _name;
    public string _id;
    public int _cost;

    public Warscroll(string name, int cost)
    {
        _name = name;
        _id = name.ToLower().Replace(" ", string.Empty);
        _cost = cost;
    }
}