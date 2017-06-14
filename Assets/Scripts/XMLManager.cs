using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XMLManager : MonoBehaviour
{
    private void Start()
    {
        LoadWarscrolls();
    }

    public void LoadWarscrolls()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(WarscrollDatabase));

        string filePath = Application.streamingAssetsPath;
        DirectoryInfo dirInfo = new DirectoryInfo(filePath);
        FileStream stream;
        foreach (var file in dirInfo.GetFiles("*.xml"))
        {
            stream = new FileStream(Application.streamingAssetsPath + "/" + file.Name, FileMode.Open);
            DataManager.Instance._warscrollDB.Add(serializer.Deserialize(stream) as WarscrollDatabase);
            stream.Close();
        }

        DataManager.Instance.AddFactionsOptions();
    }
}