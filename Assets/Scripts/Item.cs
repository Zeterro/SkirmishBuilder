using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum Type
    {
        General,
        Warscroll
    }

    public Type type;
    public Warscroll _warscroll;
    public Text _name, _cost;
    public Button _deleteButton;

    public void UpdateItem()
    {
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

        for (int i = 0; i < AppManager.Instance._options.Count; i++)
        {
            if (AppManager.Instance._options[i].name.Equals(gameObject.name)) AppManager.Instance._options[i].GetComponent<Button>().interactable = true;
        }

        AppManager.Instance._warscrollsGO.Remove(gameObject);
        AppManager.Instance.UpdateElementsPositions();
        Debug.Log("Remove warscroll: " + _warscroll._name);
    }
}
