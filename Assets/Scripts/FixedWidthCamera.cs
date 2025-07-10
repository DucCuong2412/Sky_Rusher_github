using UnityEngine;

public class FixedWidthCamera : MonoBehaviour
{
	private const float c_DesiredAspectRatio = 0.5625f;

	public float m_CameraHeight = 5.33f;

	private Camera m_Camera;

	private void Awake()
	{
		Refresh();
	}

	public void Refresh()
	{
		if (m_Camera == null)
		{
			m_Camera = GetComponent<Camera>();
		}
		float aspect = m_Camera.aspect;
		float num = 0.5625f / aspect;
		m_Camera.orthographicSize = m_CameraHeight * num;
	}
}
