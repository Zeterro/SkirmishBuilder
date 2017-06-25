using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    [Header("Warband")]
    public Warband _warband;

    [Header("Database")]
    public List<Warscroll> _warscrolls = new List<Warscroll>();
    public List<WarscrollDatabase> _warscrollDB = new List<WarscrollDatabase>();
    public List<string> _factionOptions = new List<string>();

    [Header("Miscellaneous")]
    public Alliance _currentAlliance;
    public string _currentFaction;
    public int _spentRenown;

    [Header("UI")]
    public Dropdown _allianceDrop;
    public Dropdown _factionDrop;
    public InputField _maxRenownInput;

    private void Start()
    {
        UpdateMaxRenown(25);
    }

    public void ChangeAlliance()
    {
        _currentAlliance = (Alliance)_allianceDrop.value;
        _warband._allegiance = _currentAlliance;
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

    public void UpdateMaxRenown()
    {
        if (_maxRenownInput.text != "")
        {
            _warband._maxRenown = int.Parse(_maxRenownInput.text);
        }
        else _warband._maxRenown = 0;
        Debug.Log("Changed max renown to: " + _warband._maxRenown);
    }

    public void UpdateMaxRenown(int value = -1)
    {
        if (value == -1)
        {
            if (_maxRenownInput.text != "")
            {
                _warband._maxRenown = int.Parse(_maxRenownInput.text);
            }
            else _warband._maxRenown = 0;
        }
        else
        {
            _warband._maxRenown = value;
            _maxRenownInput.text = value.ToString();
        }
        Debug.Log("Changed max renown to: " + _warband._maxRenown);
    }
}