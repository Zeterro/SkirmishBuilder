using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviourSingleton<SaveLoadManager>
{
    private DirectoryInfo _dirInfo;

    [Header("General")]
    public List<string> _saveNames = new List<string>();
    public List<GameObject> _saveObjects = new List<GameObject>();
    public string _saveName = "";
    public GameObject _loadPanel;

    [Header("Prefabs")]
    public GameObject _loadPrefab;

    [Header("UI")]
    public GameObject _savePromptPanel;
    public GameObject _loadPromptPanel;
    public InputField _textInput;

    [HideInInspector]
    public string _savesPath = "";
    public Pool _loadOptionsPool;

    private void Awake()
    {
        _savesPath = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(_savesPath)) Directory.CreateDirectory(_savesPath);
    }

    private void Start()
    {
        _dirInfo = new DirectoryInfo(_savesPath);
        _loadOptionsPool = new Pool(_loadPrefab, _loadPanel.transform.parent.gameObject, 10);

        _savePromptPanel.SetActive(false);
        _loadPromptPanel.SetActive(false);
    }

    public void Save()
    {
        if (!_saveName.Equals(""))
        {
            string rawPath = _savesPath + "/" + _saveName + ".skir";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(rawPath, FileMode.Create);

            Warband data = DataManager.Instance._warband;

            bf.Serialize(stream, data);
            stream.Close();

            UpdateSaveName();
            TogglePromptPanel();

            Debug.Log("Warband saved: " + rawPath);
        }
        else
        {
            Debug.Log("Please enter a valid name");
        }
    }

    public void OpenSavePromptPanel()
    {
        _savePromptPanel.SetActive(true);
        _textInput.text = _saveName;
    }

    public void TogglePromptPanel()
    {
        _savePromptPanel.SetActive(!_savePromptPanel.activeSelf);
    }

    public void OpenLoadPanel()
    {
        _loadPromptPanel.SetActive(true);
        UpdateLoadList();
    }

    public void UpdateLoadList()
    {
        FileInfo[] fileInfo = _dirInfo.GetFiles();

        foreach (FileInfo file in fileInfo)
        {
            string rawFileName = file.Name;
            string fileName = Path.GetFileNameWithoutExtension(file.Name);

            if (!_saveNames.Contains(fileName))
            {
                GameObject go = _loadOptionsPool.GetGameObject();
                go.transform.SetParent(_loadPanel.transform);
                go.transform.localScale = new Vector3(1, 1, 1);
                go.name = fileName;

                LoadItem li = go.GetComponent<LoadItem>();
                li._rawFileName = rawFileName;
                li.UpdateOption();

                _saveObjects.Add(go);
                _saveNames.Add(fileName);

                Debug.Log("Save in memory: " + Path.GetFileNameWithoutExtension(file.Name));
            }
        }
    }

    public void UpdateSaveName()
    {
        _saveName = _textInput.text;
    }

    public void Load(string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(_savesPath + "/" + fileName, FileMode.Open);

        Warband data = bf.Deserialize(stream) as Warband;
        stream.Close();

        _loadPromptPanel.SetActive(false);

        DataManager.Instance.UpdateMaxRenown(data._maxRenown);

        for (int i = 0; i < data._general.Count; i++)
        {
            AppManager.Instance.AddElement(data._general[i], Type.General);
        }

        for (int i = 0; i < data._warscrolls.Count; i++)
        {
            AppManager.Instance.AddElement(data._warscrolls[i], Type.Warscroll);
        }

        Debug.Log("Loaded save: " + fileName);
    }
}