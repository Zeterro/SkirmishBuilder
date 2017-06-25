using UnityEngine;

public class Template : MonoBehaviour
{
    private RectTransform _anchor;
    private RectTransform _rt;

    public RectTransform _header;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _rt.position = new Vector2(_header.position.x, _header.position.y);

    }

    private void Update()
    {
        _rt.position = new Vector2(_header.position.x, _header.position.y);
    }
}