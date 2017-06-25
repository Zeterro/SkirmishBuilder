using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviourSingleton<ScenesManager>
{
    [SerializeField]
    private AppState _appState;

    public AppState CurrentAppState
    {
        get { return _appState; }
        set
        {
            _appState = value;
            switch (value)
            {
                case AppState.Home:
                    break;

                default:
                    break;
            }
            Debug.Log("Changed Appstate to: " + CurrentAppState);
        }
    }

    private void Start()
    {
        CurrentAppState = AppState.Home;
        SceneManager.LoadScene("header", LoadSceneMode.Additive);
    }
}