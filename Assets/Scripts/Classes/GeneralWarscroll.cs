[System.Serializable]
public class GeneralWarscroll : Warscroll
{
    string[] _abilities;
    string[] _artefacts;

    public GeneralWarscroll(string name, int cost, string[] abilities = null, string[] artefacts = null) : base (name, cost)
    {
        _abilities = abilities;
        _artefacts = artefacts;
    }
}