using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    [SerializeField] private string m_objectName;

    [SerializeField] private bool m_dontDestroyOnLoad;

    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance != null)
                return m_instance;

            m_instance = FindObjectOfType<T>();
            if (m_instance == null)
                CreateSingleton();
            (m_instance as Singleton<T>)?.Initialize();
            
            return m_instance;
        }
    }

    static void CreateSingleton()
    {
        GameObject singletonObject = new GameObject();

        m_instance = singletonObject.AddComponent<T>();
    }

    protected void Initialize()
    {
        if (!string.IsNullOrWhiteSpace(m_objectName))
        {
            gameObject.name = m_objectName;
        }
        
        if (m_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}