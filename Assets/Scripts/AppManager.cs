using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    [Header("General")]
    public List<Warscroll> _usedWarscrolls = new List<Warscroll>();
    public List<GameObject> _warscrollsGO = new List<GameObject>();
    public List<GameObject> _options = new List<GameObject>();

    [Header("Prefabs")]
    public GameObject _generalPrefab;
    public GameObject _generalHeaderPrefab;
    public GameObject _troopsPrefab;
    public GameObject _troopsHeaderPrefab;
    public GameObject _optionsPrefab;

    [Header("UI")]
    public RectTransform _generalPanel;
    public RectTransform _generalTemplateContent;
    public RectTransform _troopsPanel;
    public RectTransform _troopsTemplateContent;
    public GameObject _generalTemplate;
    public GameObject _troopsTemplate;
    public Text _totalText;
    public InputField _maxRenownInput;
    public int _spacing;
    public Vector2 _padding;

    public Pool _generalPool;
    public Pool _troopsPool;
    public Pool _optionsPool;

    private void Awake()
    {
        Color color = HexColor(33, 46, 58, 255);
        SetupAndroidTheme(ToARGB(color), ToARGB(color));
    }

    private void Start()
    {
        _generalPool = new Pool(_generalPrefab, _generalPanel.parent.gameObject, 1);
        _troopsPool = new Pool(_troopsPrefab, _troopsPanel.parent.gameObject, 10);
        _optionsPool = new Pool(_optionsPrefab, _generalTemplate.transform.parent.gameObject, 10);

        InstantiateHeaders();
        UpdateTotalRenown();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Application.platform == RuntimePlatform.Android) Global.MoveTaskToBack();
        }
    }

    private void InstantiateHeaders()
    {
        GameObject go;
        go = Instantiate(_troopsHeaderPrefab, _troopsPanel);
        go.name = "TroopsHeader";
        go = Instantiate(_generalHeaderPrefab, _generalPanel);
        go.name = "GeneralHeader";
        UpdateElementsPositions();
    }

    public void AddElement(Warscroll warscroll, Type type)
    {
        Transform parent;
        GameObject go;
        Item item;

        if (type == Type.General)
        {
            go = _generalPool.GetGameObject();
            parent = _generalPanel;
            DataManager.Instance._warband._general = new List<Warscroll>() { warscroll };
        }
        else
        {
            go = _troopsPool.GetGameObject();
            parent = _troopsPanel;
            DataManager.Instance._warband._warscrolls.Add(warscroll);
        }

        go.transform.SetParent(parent);
        go.transform.localScale = new Vector3(1, 1, 1);
        go.name = warscroll._name;

        item = go.GetComponent<Item>();
        item._warscroll = warscroll;
        item.UpdateItem();

        _warscrollsGO.Add(go);

        if (type == Type.General)
        {
            _generalPanel.GetChild(0).GetComponentInChildren<Button>().interactable = false;
            _generalTemplate.SetActive(false);
        }
        else _troopsTemplate.SetActive(false);

        _usedWarscrolls.Add(warscroll);

        UpdateElementsPositions();
        UpdateTotalRenown();

        Debug.Log("Add warscroll: " + warscroll._name);
    }

    public void ClearAllElements()
    {
        for (int i = _warscrollsGO.Count - 1; i >= 0; i--)
        {
            _warscrollsGO[i].GetComponent<Item>().Delete();
        }
    }

    public void UpdateElementsPositions()
    {
        int generalCount = _generalPanel.childCount, troopsCount = _troopsPanel.childCount;
        float elementHeight = 0;

        for (int i = 0; i < generalCount; i++)
        {
            RectTransform rt = _generalPanel.GetChild(i).GetComponent<RectTransform>();
            rt.localPosition = new Vector2(_padding.x, -(rt.sizeDelta.y * i) - (_spacing * i));
            if (elementHeight == 0) elementHeight = rt.sizeDelta.y;
        }

        for (int i = 0; i < troopsCount; i++)
        {
            RectTransform rt = _troopsPanel.GetChild(i).GetComponent<RectTransform>();
            rt.localPosition = new Vector2(_padding.x, -(rt.sizeDelta.y * i) - (_spacing * i));
            if (elementHeight == 0) elementHeight = rt.sizeDelta.y;
        }

        _generalPanel.sizeDelta = new Vector2(_generalPanel.sizeDelta.x, (generalCount * elementHeight) + (_spacing * (generalCount - 1)));
        _troopsPanel.sizeDelta = new Vector2(_troopsPanel.sizeDelta.x, (troopsCount * elementHeight) + (_spacing * (troopsCount - 1)));
    }

    public void AddWarscrollOptions(List<Warscroll> warscrolls)
    {
        ClearWarscrollsOptions();

        for (int i = 0; i < warscrolls.Count; i++)
        {
            if (warscrolls[i]._type == Type.General) AddWarscrollOption(warscrolls[i], _generalTemplateContent, Type.General);
            AddWarscrollOption(warscrolls[i], _troopsTemplateContent, Type.Warscroll);
        }
    }

    public void ClearWarscrollsOptions()
    {
        for (int i = 0; i < _options.Count; i++)
        {
            _optionsPool.ReturnGameObject(_options[i]);
        }
        _options.Clear();
    }

    public void AddWarscrollOption(Warscroll warscroll, Transform parent, Type type)
    {
        GameObject go = _optionsPool.GetGameObject();
        Option option = go.GetComponent<Option>();
        option._warscroll = warscroll;
        option.UpdateOption(type);

        go.name = warscroll._name;
        go.transform.SetParent(parent);
        go.transform.localScale = new Vector3(1, 1, 1);

        _options.Add(go);
    }

    public void UpdateWarscrollOptionsAvailability()
    {
        for (int i = 0; i < _options.Count; i++)
        {
            _options[i].GetComponent<Option>().UpdateOptionAvailability();
        }
    }

    public void UpdateTotalRenown()
    {
        int total = 0;

        for (int i = 0; i < _warscrollsGO.Count; i++)
        {
            Item item = _warscrollsGO[i].GetComponent<Item>();
            total += (item._warscroll._cost * item._warscroll._number);
        }

        DataManager.Instance._spentRenown = total;
        _totalText.text = DataManager.Instance._spentRenown + "/";
    }

    private void SetupAndroidTheme(int primaryARGB, int darkARGB, string label = null)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        label = label ?? Application.productName;
        Screen.fullScreen = false;
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaClass layoutParamsClass = new AndroidJavaClass("android.view.WindowManager$LayoutParams");
            int flagFullscreen = layoutParamsClass.GetStatic<int>("FLAG_FULLSCREEN");
            int flagNotFullscreen = layoutParamsClass.GetStatic<int>("FLAG_FORCE_NOT_FULLSCREEN");
            int flagDrawsSystemBarBackgrounds = layoutParamsClass.GetStatic<int>("FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS");
            AndroidJavaObject windowObject = activity.Call<AndroidJavaObject>("getWindow");
            windowObject.Call("clearFlags", flagFullscreen);
            windowObject.Call("addFlags", flagNotFullscreen);
            windowObject.Call("addFlags", flagDrawsSystemBarBackgrounds);
            int sdkInt = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");
            int lollipop = 21;
            if (sdkInt > lollipop)
            {
                windowObject.Call("setStatusBarColor", darkARGB);
                string myName = activity.Call<string>("getPackageName");
                AndroidJavaObject packageManager = activity.Call<AndroidJavaObject>("getPackageManager");
                AndroidJavaObject drawable = packageManager.Call<AndroidJavaObject>("getApplicationIcon", myName);
                AndroidJavaObject taskDescription = new AndroidJavaObject("android.app.ActivityManager$TaskDescription", label, drawable.Call<AndroidJavaObject>("getBitmap"), primaryARGB);
                activity.Call("setTaskDescription", taskDescription);
            }
        }));
#endif
    }

    private int ToARGB(Color color)
    {
        Color32 c = (Color32)color;
        byte[] b = new byte[] { c.b, c.g, c.r, c.a };
        return System.BitConverter.ToInt32(b, 0);
    }

    private Vector4 HexColor(float r, float g, float b, float a)
    {
        Vector4 color = new Vector4(r / 255, g / 255, b / 255, a / 255);
        return color;
    }
}