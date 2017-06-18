using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadItem : MonoBehaviour
{
    public string _rawFileName;
    public Text _text;
    public Button _loadButton;
    public Button _deleteButton;

    public void DeleteSave()
    {
        string path = SaveLoadManager.Instance._savesPath + "/" + _rawFileName;
        if (File.Exists(path))
        {
            File.Delete(path);
            SaveLoadManager slm = SaveLoadManager.Instance;
            slm._saveNames.Remove(_text.text);
            slm._saveObjects.Remove(gameObject);
            slm._loadOptionsPool.ReturnGameObject(gameObject);
            slm.UpdateLoadList();
            Debug.Log("Deleted: " + path);
        }
    }

    public void UpdateOption()
    {
        _text.text = gameObject.name;
        _loadButton.onClick.AddListener(() => SaveLoadManager.Instance.Load(_rawFileName));
    }
}