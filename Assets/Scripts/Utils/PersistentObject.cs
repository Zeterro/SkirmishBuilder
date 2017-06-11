using UnityEngine;

/// <summary>
/// Stores GameObject that are activated across all scenes and should never be destroyed.
/// </summary>
public class PersistentObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
