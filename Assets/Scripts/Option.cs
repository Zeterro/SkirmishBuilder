using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Warscroll _warscroll;
    public Button _button;
    public Text _name;
    public Text _cost;

    public void UpdateOption(Type type)
    {
        _name.text = _warscroll._name;
        _cost.text = _warscroll._cost.ToString();
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => AppManager.Instance.AddElement(_warscroll, type));
    }
}