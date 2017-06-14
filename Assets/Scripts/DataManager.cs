using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    public Alliance _alliance;

    public enum Alliance
    {
        Chaos = 0,
        Order = 1,
        Destruction = 2,
        Death = 3
    }

    [Header("Data")]
    public List<Warscroll> _warscrolls = new List<Warscroll>();
    public List<WarscrollDatabase> _warscrollDB = new List<WarscrollDatabase>();
    public List<string> _factionOptions = new List<string>();

    [Header("UI")]
    public Dropdown _allianceDrop;
    public Dropdown _factionDrop;

    public void ChangeAlliance()
    {
        _alliance = (Alliance)_allianceDrop.value;
        _factionDrop.ClearOptions();
        AddFactionsOptions();
        _factionDrop.value = 0;

        AppManager.Instance.ClearWarscrollsOptions();
    }

    public void AddFactionsOptions()
    {
        _factionOptions.Clear();
        _factionOptions.Add("Select Faction");
        for (int i = 0; i < _warscrollDB.Count; i++)
        {
            if (_warscrollDB[i]._alliance == _alliance.ToString()) _factionOptions.Add(_warscrollDB[i]._faction);
        }
        _factionDrop.AddOptions(_factionOptions);
    }

    public void SelectFaction(int index)
    {
        if (index != 0)
        {
            //_warscrolls.Clear();
            _warscrolls = _warscrollDB.Where(arg => arg._faction == _factionOptions[index]).SingleOrDefault()._list;
            AppManager.Instance.AddWarscrollOptions(_warscrolls); 
        }
    }
}