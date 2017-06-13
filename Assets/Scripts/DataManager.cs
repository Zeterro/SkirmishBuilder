using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    [SerializeField]
    private Alliance _alliance;

    [SerializeField]
    private Faction _faction;

    public enum Alliance
    {
        Chaos = 0,
        Order = 1,
        Destruction = 2,
        Death = 3
    }

    public enum Faction
    {
        None = 0
    }

    public Alliance CurrentAlliance
    {
        get { return _alliance; }
        set
        {
            _alliance = value;
        }
    }

    public Faction CurrentFaction
    {
        get { return _faction; }
        set
        {
            _faction = value;
        }
    }

    public Dictionary<string, Warscroll> _warscrolls = new Dictionary<string, Warscroll>();
    public Dropdown _allianceDrop;
    public Dropdown _factionDrop;

    private void Start()
    {
        GeneralWarscroll gw = new GeneralWarscroll("Chaos Sorcerer", 16);
        _warscrolls.Add(gw._id, gw);

        gw = new GeneralWarscroll("Chaos Lord", 28);
        _warscrolls.Add(gw._id, gw);

        TroopWarscroll tw = new TroopWarscroll("Chaos Warrior", 4, 10);
        _warscrolls.Add(tw._id, tw);

        tw = new TroopWarscroll("Chaos Chosen", 8, 10);
        _warscrolls.Add(tw._id, tw);
    }

    public void ChangeAlliance()
    {
        _alliance = (Alliance)_allianceDrop.value;
    }

    public void ChangeFaction()
    {
        _faction = (Faction)_factionDrop.value;
    }
}