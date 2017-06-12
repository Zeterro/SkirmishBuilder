using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    public enum Type
    {
        General,
        Troop,
        Both
    }

    [Header("General")]
    public List<GameObject> _general = new List<GameObject>();
    public List<GameObject> _troops = new List<GameObject>();
    public GeneralWarscroll[] _generalOptions;
    public TroopWarscroll[] _troopsOptions;

    [Header("Prefabs")]
    public GameObject _generalPrefab;
    public GameObject _generalHeaderPrefab;
    public GameObject _troopsPrefab;
    public GameObject _troopsHeaderPrefab;
    public GameObject _optionsPrefab;

    [Header("UI")]
    public RectTransform _generalPanel;
    public RectTransform _generalTemplateContent;
    public RectTransform _troopsPanel;
    public RectTransform _troopsTemplateContent;
    public GameObject _generalTemplate, _troopsTemplate;
    public int _spacing;
    public Vector2 _padding;

    private void Start()
    {
        _generalOptions = new GeneralWarscroll[2];
        _generalOptions[0] = new GeneralWarscroll("Chaos Sorcerer", 16);
        _generalOptions[1] = new GeneralWarscroll("Chaos Lord", 28);

        _troopsOptions = new TroopWarscroll[4];
        _troopsOptions[0] = new TroopWarscroll("Chaos Warrior", 4);
        _troopsOptions[1] = new TroopWarscroll("Chaos Chosen", 8);
        _troopsOptions[2] = new TroopWarscroll("Chaos Sorcerer", 16);
        _troopsOptions[3] = new TroopWarscroll("Chaos Lord", 28);

        for (int i = 0; i < _generalOptions.Length; i++)
        {
            AddOption(Type.General, _generalOptions[i]._name, i);
        }

        for (int i = 0; i < _troopsOptions.Length; i++)
        {
            AddOption(Type.Troop, _troopsOptions[i]._name, i);
        }

        InstantiateHeaders();
    }

    private void InstantiateHeaders()
    {
        GameObject go = Instantiate(_troopsHeaderPrefab, _troopsPanel);
        _troops.Add(go);

        go = Instantiate(_generalHeaderPrefab, _generalPanel);
        _general.Add(go);

        UpdateElementsPositions();
    }

    public void AddElement(Type type, int arg)
    {
        GameObject prefab;
        Transform parent;
        Warscroll warscroll;

        if (type == Type.General)
        {
            prefab = _generalPrefab;
            parent = _generalPanel;
            warscroll = _generalOptions[arg];
        }
        else
        {
            prefab = _troopsPrefab;
            parent = _troopsPanel;
            warscroll = _troopsOptions[arg];
        }

        GameObject go = Instantiate(prefab, parent);

        Item item = go.GetComponent<Item>();
        item._warscroll = warscroll;

        if (type == Type.General)
        {
            _general.Add(go);
            _generalTemplate.SetActive(false);
        }
        else
        {
            _troops.Add(go);
            _troopsTemplate.SetActive(false);
        }

        UpdateElementsPositions();
    }

    public void UpdateElementsPositions(Type type = Type.Both)
    {
        int generalCount = _general.Count, troopsCount = _troops.Count;
        float elementHeight = 0;

        if (type == Type.General || type == Type.Both)
        {
            for (int i = 0; i < generalCount; i++)
            {
                RectTransform rt = _general[i].GetComponent<RectTransform>();
                rt.localPosition = new Vector2(_padding.x, -(rt.sizeDelta.y * i) - (_spacing * i));
                if (elementHeight == 0) elementHeight = rt.sizeDelta.y;
            }
            _generalPanel.sizeDelta = new Vector2(_generalPanel.sizeDelta.x, (generalCount * elementHeight) + (_spacing * (generalCount - 1)));
        }

        if (type == Type.Troop || type == Type.Both)
        {
            for (int i = 0; i < troopsCount; i++)
            {
                RectTransform rt = _troops[i].GetComponent<RectTransform>();
                rt.localPosition = new Vector2(_padding.x, -(rt.sizeDelta.y * i) - (_spacing * i));
                if (elementHeight == 0) elementHeight = rt.sizeDelta.y;
            }
            _troopsPanel.sizeDelta = new Vector2(_troopsPanel.sizeDelta.x, (troopsCount * elementHeight) + (_spacing * (troopsCount - 1)));
        }
    }

    public void AddOption(Type type, string name, int id)
    {
        RectTransform parent;
        if (type == Type.General) parent = _generalTemplateContent;
        else parent = _troopsTemplateContent;

        GameObject go = Instantiate(_optionsPrefab);
        go.transform.SetParent(parent);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponentInChildren<Text>().text = name;
        go.GetComponent<Button>().onClick.AddListener(() => AddElement(type, id));
    }
}