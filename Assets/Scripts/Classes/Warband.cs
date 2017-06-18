[System.Serializable]
public class Warband
{
    public Alliance _alliance;
    public string _faction;
    public int _maxRenown;
    public Warscroll[] _general;
    public Warscroll[] _warscrolls;

    public Warband()
    {
        DataManager dm = DataManager.Instance;
        AppManager am = AppManager.Instance;
        _alliance = dm._currentAlliance;
        _faction = dm._currentFaction;
        _maxRenown = am._maxRenown;
        _general = dm._currentGeneral;
        _warscrolls = dm._currentWarscrolls.ToArray();
    }
}