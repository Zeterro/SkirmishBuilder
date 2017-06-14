using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
public class WarscrollDatabase
{
    [XmlAttribute("Faction")]
    public string _faction;
    [XmlAttribute("Alliance")]
    public string _alliance;

    [XmlArray("Warscrolls")]
    public List<Warscroll> _list = new List<Warscroll>();
}