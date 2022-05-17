using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static bool m_ShuttingDown = false;
	private static object m_Lock = new object();
	private static T m_Instance;

	public static T Instance
	{
		get
		{
			if (m_ShuttingDown)
			{
				return null;
			}

			lock (m_Lock)
			{
				if (m_Instance == null)
				{
					m_Instance = (T)FindObjectOfType(typeof(T));

					GameObject singletonObject = null;
					if (m_Instance == null)
					{
						singletonObject = new GameObject();
						m_Instance = singletonObject.AddComponent<T>();
					}
					else
					{
						singletonObject = m_Instance.gameObject;
					}

					DontDestroyOnLoad(singletonObject);
					singletonObject.name = typeof(T).ToString() + " (Singleton)";
				}

				return m_Instance;
			}
		}
	}

	private void OnApplicationQuit()
	{
		m_ShuttingDown = true;
	}

	private void OnDestroy()
	{
		m_ShuttingDown = true;
	}
}