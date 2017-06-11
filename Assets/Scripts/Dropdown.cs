using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public GameObject _template;

    public void Awake()
    {
        _template.SetActive(false);
    }

    public void ToggleTemplate()
    {
        if (_template.activeSelf) _template.SetActive(false);
        else _template.SetActive(true);
    }
}