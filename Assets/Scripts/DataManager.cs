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

    public Dropdown _allianceDrop;
    public Dropdown _factionDrop;

    public void ChangeAlliance()
    {
        _alliance = (Alliance)_allianceDrop.value;
    }

    public void ChangeFaction()
    {
        _faction = (Faction)_factionDrop.value;
    }
}