using System.IO;
using System.Linq;
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
        var database = Resources.LoadAll("WarscrollDatabase", typeof(TextAsset)).Cast<TextAsset>().ToArray();

        for (int i = 0; i < database.Length; i++)
        {
            Debug.Log("Loaded: " + database[i].name);
            var reader = new StringReader(database[i].text);
            DataManager.Instance._warscrollDB.Add(serializer.Deserialize(reader) as WarscrollDatabase);
        }

        DataManager.Instance.AddFactionsOptions();
    }
}