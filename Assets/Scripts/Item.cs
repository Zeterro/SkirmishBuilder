using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("Variables")]
    public Type type;
    public Warscroll _warscroll;

    [Header("UI")]
    public Text _name;
    public Text _cost;
    public Button _deleteButton;
    public Text _numberText;
    public Button[] _numberButtons;

    private void OnEnable()
    {
        _warscroll._number = 0;
        UpdateItem();
    }

    public void UpdateItem()
    {
        _name.text = _warscroll._name;
        _cost.text = _warscroll._cost.ToString();
        if (_numberText != null) _numberText.text = _warscroll._number + "/" + _warscroll._maxNumber.ToString();
    }

    public void Delete()
    {
        AppManager am = AppManager.Instance;

        if (type == Type.General)
        {
            am._generalPool.ReturnGameObject(gameObject);
            am._generalPanel.GetChild(0).GetComponentInChildren<Button>().interactable = true;
            DataManager.Instance._currentGeneral = null;
        }
        else
        {
            am._troopsPool.ReturnGameObject(gameObject);
            DataManager.Instance._currentWarscrolls.Remove(_warscroll);
        }

        am._warscrollsGO.Remove(gameObject);
        am._usedWarscrolls.Remove(_warscroll);
        am.UpdateElementsPositions();
        am.UpdateTotalRenown();
        Debug.Log("Remove warscroll: " + _warscroll._name);
    }

    public void Add()
    {
        if (_warscroll._number < _warscroll._maxNumber && _warscroll._cost + AppManager.Instance._spentRenown <= AppManager.Instance._maxRenown)
        {
            _warscroll._number++;
            UpdateItem();
            AppManager.Instance.UpdateTotalRenown();
        }
    }

    public void Remove()
    {
        if (_warscroll._number > 1)
        {
            _warscroll._number--;
            UpdateItem();
            AppManager.Instance.UpdateTotalRenown();
        }
    }
}
