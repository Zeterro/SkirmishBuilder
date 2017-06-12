using UnityEngine;

public class Header : MonoBehaviour
{
    private GameObject _template;

    public enum Type
    {
        General,
        Troops
    }

    public Type _type;

    private void Start()
    {
        if(_type == Type.Troops) _template = AppManager.Instance._troopsTemplate;
        else _template = AppManager.Instance._generalTemplate;
        _template.SetActive(false);
    }

    public void ToggleTemplate()
    {
        if (_type == Type.General) AppManager.Instance._troopsTemplate.SetActive(false);
        else AppManager.Instance._generalTemplate.SetActive(false);

        _template.SetActive(!_template.activeSelf);
    }
}