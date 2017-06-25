using System.Collections.Generic;

[System.Serializable]
public class Warband
{
    public Alliance _allegiance;
    public int _maxRenown;
    public List<Warscroll> _general = new List<Warscroll>();
    public List<Warscroll> _warscrolls = new List<Warscroll>();

    public Warband()
    {
        
    }
}