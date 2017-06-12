using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    public List<GameObject> _troops = new List<GameObject>();
    public TroopWarscroll[] _troopsOptions;

    [Header("Prefabs")]
    public GameObject _troopsHeaderPrefab;
    public GameObject _troopsOptionsPrefab;
    public GameObject _troopsPrefab;

    [Header("UI")]
    public RectTransform _troopsPanel;
    public RectTransform _troopsTemplatePanel;
    public int _spacing;
    public Vector2 _padding;

    public void AddTroop(int arg)
    {
        GameObject go = Instantiate(_troopsPrefab, _troopsPanel);

        Item item = go.GetComponent<Item>();
        item._warscroll = _troopsOptions[arg];

        _troops.Add(go);
        UpdateTroops();
    }

    public void UpdateTroops()
    {
        int count = _troops.Count;
        float elementHeight = 0;

        for (int i = 0; i < _troops.Count; i++)
        {
            RectTransform rt = _troops[i].GetComponent<RectTransform>();
            rt.localPosition = new Vector2(_padding.x, -(rt.sizeDelta.y * i) - (_spacing * i));
            if (elementHeight == 0) elementHeight = rt.sizeDelta.y;
        }

        _troopsPanel.sizeDelta = new Vector2(_troopsPanel.sizeDelta.x, (count * elementHeight) + (_spacing * (count - 1)));
    }

    private void Start()
    {
        _troopsOptions = new TroopWarscroll[2];
        _troopsOptions[0] = new TroopWarscroll("Chaos Warrior", 4);
        _troopsOptions[1] = new TroopWarscroll("Chaos Chosen", 8);

        for (int i = 0; i < _troopsOptions.Length; i++)
        {
            AddTroopsOption(_troopsOptions[i]._name, i, _troopsTemplatePanel);
        }

        GameObject go = Instantiate(_troopsHeaderPrefab, _troopsPanel);
        _troops.Add(go);
        UpdateTroops();

        AddTroop(0);
        AddTroop(1);
        AddTroop(0);
    }

    public void AddTroopsOption(string name, int id, RectTransform parent)
    {
        GameObject go = Instantiate(_troopsOptionsPrefab);
        go.transform.SetParent(parent);
        go.GetComponentInChildren<Text>().text = name;
        go.GetComponent<Button>().onClick.AddListener(() => AddTroop(id));
    }
}