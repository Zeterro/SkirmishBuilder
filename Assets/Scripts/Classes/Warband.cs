[System.Serializable]
public class Warband
{
    public Alliance _alliance;
    public string _faction;
    public Warscroll _general;
    public Warscroll[] _warscrolls;

    public Warband()
    {
        DataManager dm = DataManager.Instance;
        _alliance = dm._currentAlliance;
        _faction = dm._currentFaction;
        _general = dm._currentGeneral;
        _warscrolls = dm._currentWarscrolls.ToArray();
    }
}