using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		if (SceneManager.sceneCount == 1)
		{
			SceneManager.LoadScene(1);
		}
	}
}
