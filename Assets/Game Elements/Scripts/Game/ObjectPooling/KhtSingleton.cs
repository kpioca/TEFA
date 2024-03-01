using UnityEngine;

public class KhtSingleton<T> : MonoBehaviour where T : KhtSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get => _instance;
    }

    public static bool IsInstantiated
    {
        get => _instance != null;
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("[" + typeof(T) + "] '" + transform.name + "' trying to instantiate a second instance of singleton class previously created in '" + _instance.transform.name + "'");
        }
        else
        {
            _instance = (T)this;
        }
    }

    protected void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
