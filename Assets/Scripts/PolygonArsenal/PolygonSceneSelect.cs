using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolygonArsenal
{
	public class PolygonSceneSelect : MonoBehaviour
	{
		public bool GUIHide;

		public bool GUIHide2;

		public bool GUIHide3;

		public void LoadSceneDemoMissiles()
		{
			SceneManager.LoadScene("PolygonDemoMissiles");
		}

		public void LoadSceneDemo01()
		{
			SceneManager.LoadScene("PolygonDemo01");
		}

		public void LoadSceneDemo02()
		{
			SceneManager.LoadScene("PolygonDemo02");
		}

		public void LoadSceneDemo03()
		{
			SceneManager.LoadScene("PolygonDemo03");
		}

		public void LoadSceneDemo04()
		{
			SceneManager.LoadScene("PolygonDemo04");
		}

		public void LoadSceneDemo05()
		{
			SceneManager.LoadScene("PolygonDemo05");
		}

		public void LoadSceneDemo06()
		{
			SceneManager.LoadScene("PolygonDemo06");
		}

		public void LoadSceneDemo07()
		{
			SceneManager.LoadScene("PolygonDemo07");
		}

		public void LoadSceneDemo08()
		{
			SceneManager.LoadScene("PolygonDemo08");
		}

		public void LoadSceneDemo09()
		{
			SceneManager.LoadScene("PolygonDemo09");
		}

		public void LoadSceneDemo10()
		{
			SceneManager.LoadScene("PolygonDemo10");
		}

		public void LoadSceneDemo11()
		{
			SceneManager.LoadScene("PolygonDemo11");
		}

		public void LoadSceneDemo12()
		{
			SceneManager.LoadScene("PolygonDemo12");
		}

		public void LoadSceneDemo13()
		{
			SceneManager.LoadScene("PolygonDemo13");
		}

		public void LoadSceneDemo14()
		{
			SceneManager.LoadScene("PolygonDemo14");
		}

		public void LoadSceneDemo15()
		{
			SceneManager.LoadScene("PolygonDemo15");
		}

		public void LoadSceneDemo16()
		{
			SceneManager.LoadScene("PolygonDemo16");
		}

		public void LoadSceneDemo17()
		{
			SceneManager.LoadScene("PolygonDemo17");
		}

		public void LoadSceneDemo18()
		{
			SceneManager.LoadScene("PolygonDemo18");
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.L))
			{
				GUIHide = !GUIHide;
				if (GUIHide)
				{
					GameObject.Find("CanvasSceneSelect").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("CanvasSceneSelect").GetComponent<Canvas>().enabled = true;
				}
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.J))
			{
				GUIHide2 = !GUIHide2;
				if (GUIHide2)
				{
					GameObject.Find("CanvasMissiles").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("CanvasMissiles").GetComponent<Canvas>().enabled = true;
				}
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.H))
			{
				GUIHide3 = !GUIHide3;
				if (GUIHide3)
				{
					GameObject.Find("CanvasTips").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("CanvasTips").GetComponent<Canvas>().enabled = true;
				}
			}
		}
	}
}
