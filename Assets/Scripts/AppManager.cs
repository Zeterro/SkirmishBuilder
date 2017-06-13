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
    public List<Warscroll> _warscrolls = new List<Warscroll>();
    public List<GameObject> _warscrollsGO = new List<GameObject>();
    public List<GameObject> _options = new List<GameObject>();

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
    public GameObject _generalTemplate;
    public GameObject _troopsTemplate;
    public int _spacing;
    public Vector2 _padding;

    public Pool _generalPool;
    public Pool _troopsPool;
    public Pool _optionsPool;

    private void Start()
    {
        _generalPool = new Pool(_generalPrefab, _generalPanel.parent.gameObject, 1);
        _troopsPool = new Pool(_troopsPrefab, _troopsPanel.parent.gameObject, 10);
        _optionsPool = new Pool(_optionsPrefab, _generalTemplate.transform.parent.gameObject, 10);

        _warscrolls.Add(new GeneralWarscroll("Chaos Sorcerer", 16));
        _warscrolls.Add(new GeneralWarscroll("Chaos Lord", 28));
        _warscrolls.Add(new TroopWarscroll("Chaos Warrior", 4));
        _warscrolls.Add(new TroopWarscroll("Chaos Chosen", 8));

        for (int i = 0; i < _warscrolls.Count; i++)
        {
            if (_warscrolls[i] is GeneralWarscroll) AddOption(Type.General, _warscrolls[i]._name, i);
            AddOption(Type.Troop, _warscrolls[i]._name, i);
        }

        InstantiateHeaders();
    }

    private void InstantiateHeaders()
    {
        GameObject go;
        go = Instantiate(_troopsHeaderPrefab, _troopsPanel);
        go.name = "TroopsHeader";
        go = Instantiate(_generalHeaderPrefab, _generalPanel);
        go.name = "GeneralHeader";
        UpdateElementsPositions();
    }

    public void AddElement(Type type, int arg)
    {
        Transform parent;
        GameObject go;
        Item item;
        Warscroll warscroll = _warscrolls[arg];

        if (type == Type.General)
        {
            go = _generalPool.GetGameObject();
            parent = _generalPanel;
        }
        else
        {
            go = _troopsPool.GetGameObject();
            parent = _troopsPanel;
        }

        go.transform.SetParent(parent);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.name = warscroll._name;

        item = go.GetComponent<Item>();
        item._warscroll = warscroll;

        _warscrollsGO.Add(go);

        if (type == Type.General)
        {
            _generalPanel.GetChild(0).GetComponentInChildren<Button>().interactable = false;
            _generalTemplate.SetActive(false);
        }
        else _troopsTemplate.SetActive(false);

        UpdateElementsPositions();
    }

    public void UpdateElementsPositions()
    {
        int generalCount = _generalPanel.childCount, troopsCount = _troopsPanel.childCount;
        float elementHeight = 0;

        for (int i = 0; i < generalCount; i++)
        {
            RectTransform rt = _generalPanel.GetChild(i).GetComponent<RectTransform>();
            rt.localPosition = new Vector2(_padding.x, -(rt.sizeDelta.y * i) - (_spacing * i));
            if (elementHeight == 0) elementHeight = rt.sizeDelta.y;
        }

        for (int i = 0; i < troopsCount; i++)
        {
            RectTransform rt = _troopsPanel.GetChild(i).GetComponent<RectTransform>();
            rt.localPosition = new Vector2(_padding.x, -(rt.sizeDelta.y * i) - (_spacing * i));
            if (elementHeight == 0) elementHeight = rt.sizeDelta.y;
        }

        _generalPanel.sizeDelta = new Vector2(_generalPanel.sizeDelta.x, (generalCount * elementHeight) + (_spacing * (generalCount - 1)));
        _troopsPanel.sizeDelta = new Vector2(_troopsPanel.sizeDelta.x, (troopsCount * elementHeight) + (_spacing * (troopsCount - 1)));
    }

    public void AddOption(Type type, string name, int id)
    {
        RectTransform parent;
        if (type == Type.General) parent = _generalTemplateContent;
        else parent = _troopsTemplateContent;

        GameObject go = _optionsPool.GetGameObject();
        go.transform.SetParent(parent);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponentInChildren<Text>().text = name;
        go.GetComponent<Button>().onClick.AddListener(() => AddElement(type, id));
        go.name = name;

        _options.Add(go);
    }

    public void UpdateOptions(Type type = Type.Both)
    {
        int count = 0;
        for (int i = 0; i < _warscrollsGO.Count; i++)
        {
            for (int j = 0; j < _options.Count; j++)
            {
                if (_options[j].name.Equals(_warscrollsGO[i].name))
                {
                    Debug.Log("Deactivate option " + _options[j].name);
                    _options[j].GetComponent<Button>().interactable = false;
                    count++;
                    if (count >= 2) return;
                }
            }
        }
    }
}