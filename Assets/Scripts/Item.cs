using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private RectTransform _rectTrans;

    public enum Type
    {
        General,
        Warscroll
    }

    public Type type;
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
        if (type == Type.General)
        {
            AppManager.Instance._generalPool.ReturnGameObject(gameObject);
            AppManager.Instance._generalPanel.GetChild(0).GetComponentInChildren<Button>().interactable = true;
        }
        else
        {
            AppManager.Instance._troopsPool.ReturnGameObject(gameObject);
        }
        AppManager.Instance._warscrollsGO.Remove(gameObject);
        AppManager.Instance._usedWarscrolls.Remove(_warscroll);
        AppManager.Instance.UpdateElementsPositions();
        Debug.Log("Remove element");
    }
}
