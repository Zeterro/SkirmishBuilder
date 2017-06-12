using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private RectTransform _rectTrans;

    public Warscroll _warscroll;
    public Text _name, _cost;
    public Button _deleteButton;

    void Start()
    {
        _rectTrans = GetComponent<RectTransform>();
        _name.text = _warscroll._name;
        _cost.text = _warscroll._cost.ToString();
    }

    public void Delete()
    {
        AppManager.Instance._troops.Remove(gameObject);
        AppManager.Instance.UpdateTroops();
        Destroy(gameObject);
    }
}
