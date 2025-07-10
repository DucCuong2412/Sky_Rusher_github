using UnityEngine;

namespace Battlehub.HorizonBending
{
	[ExecuteInEditMode]
	public class HBCamera : MonoBehaviour
	{
		[HideInInspector]
		public bool SceneViewCamera;

		private Camera m_camera;

		private float m_currentFieldOfView;

		private float m_currentOrthographicSize;

		private void Awake()
		{
			m_camera = GetComponent<Camera>();
		}

		private void OnEnable()
		{
			m_currentFieldOfView = m_camera.fieldOfView;
		}

		private void OnPreCull()
		{
			HB instance = HB.Instance;
			if (instance == null)
			{
				UnityEngine.Debug.LogWarning("HB is null");
			}
			Vector4 value = m_camera.transform.position;
			value.w = 1f;
			Shader.SetGlobalVector("_HBWorldSpaceCameraPos", value);
			if (m_camera.orthographic)
			{
				m_currentOrthographicSize = m_camera.orthographicSize;
				m_camera.orthographicSize += instance.FixOrthographicSize;
			}
			else
			{
				m_currentFieldOfView = m_camera.fieldOfView;
				m_camera.fieldOfView += instance.FixFieldOfView;
			}
		}

		private void OnPreRender()
		{
			if (m_camera.orthographic)
			{
				m_camera.orthographicSize = m_currentOrthographicSize;
			}
			else
			{
				m_camera.fieldOfView = m_currentFieldOfView;
			}
		}
	}
}
