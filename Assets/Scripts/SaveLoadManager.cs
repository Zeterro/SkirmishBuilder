using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviourSingleton<SaveLoadManager>
{
    public GameObject _savePromptPanel;
    public InputField _textInput;
    public string _saveName = "";

    private void Awake()
    {
        string path = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path); 
    }

    private void Start()
    {
        _savePromptPanel.SetActive(false);
    }

    public void Save()
    {
        if (!_saveName.Equals(""))
        {
            string path = Application.persistentDataPath + "/saves/";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path + _saveName + ".skir", FileMode.Create);

            Warband data = new Warband();

            bf.Serialize(stream, data);
            stream.Close();

            UpdateSaveName();
            TogglePromptPanel();
            Debug.Log(path); 
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

    public void UpdateSaveName()
    {
        _saveName = _textInput.text;
    }

    public void Load()
    {

    }
}
