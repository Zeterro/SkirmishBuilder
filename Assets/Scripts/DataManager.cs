using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    [Header("Warband")]
    public Alliance _currentAlliance;
    public string _currentFaction;
    public Warscroll[] _currentGeneral;
    public List<Warscroll> _currentWarscrolls = new List<Warscroll>();

    [Header("Database")]
    public List<Warscroll> _warscrolls = new List<Warscroll>();
    public List<WarscrollDatabase> _warscrollDB = new List<WarscrollDatabase>();
    public List<string> _factionOptions = new List<string>();

    [Header("UI")]
    public Dropdown _allianceDrop;
    public Dropdown _factionDrop;

    public void ChangeAlliance()
    {
        _currentAlliance = (Alliance)_allianceDrop.value;
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
            if (_warscrollDB[i]._alliance == _currentAlliance.ToString()) _factionOptions.Add(_warscrollDB[i]._faction);
        }
        _factionDrop.AddOptions(_factionOptions);
    }

    public void SelectFaction(int index)
    {
        if (index != 0)
        {
            //_warscrolls.Clear();
            _warscrolls = _warscrollDB.Where(arg => arg._faction == _factionOptions[index]).SingleOrDefault()._list;
            _currentFaction = _warscrollDB.Where(arg => arg._faction == _factionOptions[index]).SingleOrDefault()._faction;
            AppManager.Instance.AddWarscrollOptions(_warscrolls); 
        }
        else _currentFaction = "";
    }
}