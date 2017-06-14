using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    [Header("General")]
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

    public void AddElement(Warscroll warscroll, Type type)
    {
        Transform parent;
        GameObject go;
        Item item;

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
        item.UpdateItem();

        _warscrollsGO.Add(go);

        if (type == Type.General)
        {
            _generalPanel.GetChild(0).GetComponentInChildren<Button>().interactable = false;
            _generalTemplate.SetActive(false);
        }
        else _troopsTemplate.SetActive(false);

        UpdateElementsPositions();
        Debug.Log("Add warscroll: " + warscroll._name);
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

    public void AddWarscrollOptions(List<Warscroll> warscrolls)
    {
        ClearWarscrollsOptions();

        for (int i = 0; i < warscrolls.Count; i++)
        {
            if (warscrolls[i]._type == Type.General) AddWarscrollOption(warscrolls[i], _generalTemplateContent, Type.General);
            AddWarscrollOption(warscrolls[i], _troopsTemplateContent, Type.Warscroll);
        }
    }

    public void ClearWarscrollsOptions()
    {
        for (int i = 0; i < _options.Count; i++)
        {
            _optionsPool.ReturnGameObject(_options[i]);
        }
        _options.Clear();
    }

    public void AddWarscrollOption(Warscroll warscroll, Transform parent, Type type)
    {
        GameObject go = _optionsPool.GetGameObject();
        Button button = go.GetComponent<Button>();
        go.transform.SetParent(parent);
        go.transform.localScale = new Vector3(1, 1, 1);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => AddElement(warscroll, type));
        button.interactable = true;
        go.GetComponentInChildren<Text>().text = warscroll._name;
        go.name = warscroll._name;

        _options.Add(go);
    }

    public void UpdateWarscrollOptionsAvailability()
    {
        for (int i = 0; i < _warscrollsGO.Count; i++)
        {
            for (int j = 0; j < _options.Count; j++)
            {
                if (_options[j].name.Equals(_warscrollsGO[i].name))
                {
                    //Debug.Log("Deactivate option " + _options[j].name);
                    _options[j].GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}