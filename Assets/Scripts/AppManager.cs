using UnityEngine;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    [Header("General")]
    public GeneralWarscroll _general;
    public TroopWarscroll[] _troops;

    [Header("UI")]
    public GameObject _prefabs;

    private void Awake()
    {
        _prefabs.SetActive(false);
    }

    private void Start()
    {
        _general = new GeneralWarscroll("Chaos Sorcerer", 16);
        _troops = new TroopWarscroll[2];

        _troops[0] = new TroopWarscroll("Chaos Warrior", 4);
        _troops[1] = new TroopWarscroll("Chaos Chosen", 8);
    }
}