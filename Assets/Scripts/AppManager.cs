using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    [Header("General")]
    public GeneralWarscroll _general;
    public TroopWarscroll[] _troops;

    [Header("Options")]
    public GeneralWarscroll[] _generalOptions;
    public TroopWarscroll[] _troopsOptions;

    [Header("Prefabs")]
    public GameObject _generalUIPrefab;
    public GameObject _troopUIPrefab;
    public GameObject _itemPrefab;

    [Header("Parents")]
    public Transform _generalParent;
    public Transform _troopsParent;
    public Transform _generalDropdownParent;
    public Transform _troopsDropdownParent;

    [Header("Templates")]
    public GameObject _generalTemplate;
    public GameObject _troopTemplate;

    [Header("UI")]
    public Button _generalButton;
    public GameObject _prefabs;

    private void Awake()
    {
        _prefabs.SetActive(false);

        //int index = 0;
        for (int i = 0; i < _generalOptions.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(_itemPrefab);
            go.transform.SetParent(_generalDropdownParent);
            go.GetComponentInChildren<Text>().text = _generalOptions[i]._name;
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponentInChildren<Button>().onClick.AddListener(() => AddGeneral(index));
        }
    }

    private void Start()
    {
        //_general = new GeneralWarscroll("Chaos Sorcerer", 16);

        _troops = new TroopWarscroll[2];
        _troops[0] = new TroopWarscroll("Chaos Warrior", 4);
        _troops[1] = new TroopWarscroll("Chaos Chosen", 8);

        //GameObject gen = Instantiate(_generalUIPrefab);
        //gen.transform.SetParent(_generalParent, false);
        //gen.transform.GetChild(0).GetComponent<Text>().text = _general._name;
        //gen.transform.GetChild(1).GetComponent<Text>().text = _general._cost.ToString();

        GameObject troop;
        for (int i = 0; i < _troops.Length; i++)
        {
            troop = Instantiate(_troopUIPrefab);
            troop.transform.SetParent(_troopsParent, false);
            troop.transform.GetChild(0).GetComponent<Text>().text = _troops[i]._name;
            troop.transform.GetChild(2).GetComponent<Text>().text = _troops[i]._cost.ToString();
        }
    }

    public void AddGeneral(int i)
    {
        Debug.Log("Option: " + i);
        _general = _generalOptions[i];
        GameObject gen = Instantiate(_generalUIPrefab);
        gen.transform.SetParent(_generalParent, false);
        gen.transform.GetChild(0).GetComponent<Text>().text = _general._name;
        gen.transform.GetChild(1).GetComponent<Text>().text = _general._cost.ToString();
        _generalTemplate.SetActive(false);
        _generalButton.interactable = false;
    }
}