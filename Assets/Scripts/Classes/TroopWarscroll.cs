[System.Serializable]
public class TroopWarscroll : Warscroll
{
    int _number;

    public TroopWarscroll(string name, int cost, int number = 1) : base(name, cost)
    {
        _number = number;
    }
}