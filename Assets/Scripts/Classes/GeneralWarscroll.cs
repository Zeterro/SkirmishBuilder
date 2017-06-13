[System.Serializable]
public class GeneralWarscroll : Warscroll
{
    string[] _abilities;
    string[] _artefacts;

    public GeneralWarscroll(string name, int cost, int maxNumber = 1, string[] abilities = null, string[] artefacts = null) : base (name, cost, maxNumber)
    {
        _abilities = abilities;
        _artefacts = artefacts;
    }
}