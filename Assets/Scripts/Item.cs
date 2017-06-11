using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public void Destroy()
    {
        AppManager.Instance._general = null;
        AppManager.Instance._generalButton.interactable = true;
        Destroy(gameObject);
    }
}