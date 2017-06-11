[System.Serializable]
public class Warscroll
{
    public string _name;
    public int _cost;

    public Warscroll(string name, int cost)
    {
        _name = name;
        _cost = cost;
    }
}