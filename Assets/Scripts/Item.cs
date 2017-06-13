using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum Type
    {
        General,
        Warscroll
    }

    [Header("Variables")]
    public Type type;
    public Warscroll _warscroll;
    public int _number = 1;

    [Header("UI")]
    public Text _name;
    public Text _cost;
    public Button _deleteButton;
    public Text _numberText;
    public Button[] _numberButtons;

    private void OnEnable()
    {
        _number = 1;
        UpdateItem();
    }

    public void UpdateItem()
    {
        _name.text = _warscroll._name;
        _cost.text = _warscroll._cost.ToString();
        if (_numberText != null) _numberText.text = _number + "/" + _warscroll._maxNumber.ToString();
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

    public void Add()
    {
        if (_number < _warscroll._maxNumber)
        {
            _number++;
            UpdateItem(); 
        }
    }

    public void Remove()
    {
        if (_number > 1)
        {
            _number--;
            UpdateItem(); 
        }
    }
}
