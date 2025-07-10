using UnityEngine;

namespace EmptyGame.Misc
{
	public class FPSDisplayer : MonoBehaviour
	{
		private float deltaTime;

		private void Update()
		{
			deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		}

		private void OnGUI()
		{
			int width = Screen.width;
			int height = Screen.height;
			GUIStyle gUIStyle = new GUIStyle();
			Rect position = new Rect((float)width * -0.02f, (float)height * 0.96f, width, (float)height * 0.02f);
			gUIStyle.alignment = TextAnchor.LowerRight;
			gUIStyle.fontSize = height * 2 / 100;
			gUIStyle.normal.textColor = Color.white;
			float num = deltaTime * 1000f;
			float num2 = 1f / deltaTime;
			string text = $"{num:0.0} ms ({num2:0.} fps)";
			GUI.Label(position, text, gUIStyle);
		}
	}
}
