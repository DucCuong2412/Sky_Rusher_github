using UnityEngine;

public class SingletonMB<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new object();

	private static bool _isQuitting = false;

	public static T Instance
	{
		get
		{
			lock (_lock)
			{
				if ((Object)_instance == (Object)null)
				{
					_instance = (T)UnityEngine.Object.FindObjectOfType(typeof(T));
					if (UnityEngine.Object.FindObjectsOfType(typeof(T)).Length > 1)
					{
						UnityEngine.Debug.LogWarning("Il y a plusieurs managers du meme type sur la scène : " + typeof(T).Name);
						return _instance;
					}
					if ((Object)_instance == (Object)null && !_isQuitting)
					{
						return (T)null;
					}
				}
				return _instance;
			}
		}
	}

	public static void SetActive(bool _Active)
	{
		T instance = Instance;
		instance.gameObject.SetActive(_Active);
	}

	private void Awake()
	{
		AwakeSpecific();
	}

	protected virtual void AwakeSpecific()
	{
	}

	private void OnDestroy()
	{
		OnDestroySpecific();
	}

	private void OnApplicationQuit()
	{
		_isQuitting = true;
	}

	protected virtual void OnDestroySpecific()
	{
	}
}
