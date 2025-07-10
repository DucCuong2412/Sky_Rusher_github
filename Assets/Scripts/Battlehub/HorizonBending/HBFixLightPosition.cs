using UnityEngine;

namespace Battlehub.HorizonBending
{
	[ExecuteInEditMode]
	public class HBFixLightPosition : MonoBehaviour
	{
		private Vector3 m_prevCameraPosition;

		private Vector3 m_prevPosition;

		[HideInInspector]
		[SerializeField]
		private Transform m_transform;

		private void Start()
		{
			if (HB.Instance == null)
			{
				UnityEngine.Debug.LogWarning("HB instance not found");
			}
			else if (HB.Instance.FixLightsPositionCamera == null)
			{
				UnityEngine.Debug.LogError("Set FixLightPositionCamera field of HB script");
			}
			Light component = GetComponent<Light>();
			if (component != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(base.gameObject);
				m_transform = gameObject.transform;
				m_transform.SetParent(base.transform, worldPositionStays: false);
				m_transform.rotation = Quaternion.identity;
				m_transform.localScale = Vector3.one;
				m_transform.position = Vector3.zero;
				Component[] components = gameObject.GetComponents<Component>();
				foreach (Component component2 in components)
				{
					if (!(component2 is Light) && !(component2 is Transform))
					{
						if (Application.isPlaying)
						{
							UnityEngine.Object.Destroy(component2);
						}
						else
						{
							UnityEngine.Object.DestroyImmediate(component2);
						}
					}
				}
				int childCount = gameObject.transform.childCount;
				for (int num = childCount - 1; num >= 0; num--)
				{
					if (Application.isPlaying)
					{
						UnityEngine.Object.Destroy(gameObject.transform.GetChild(num).gameObject);
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(gameObject.transform.GetChild(num).gameObject);
					}
				}
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(component);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(component);
				}
			}
			if (m_transform == null)
			{
				base.enabled = false;
				UnityEngine.Debug.LogWarningFormat("HBFixLightPosition {0} disabled (Light is not found)", base.gameObject.name);
			}
		}

		private void OnBecameVisible()
		{
			base.enabled = true;
		}

		private void OnBecameInvisible()
		{
			base.enabled = false;
		}

		private void Update()
		{
			if (HB.Instance == null || HB.Instance.FixLightsPositionCamera == null)
			{
				return;
			}
			Transform transform = HB.Instance.FixLightsPositionCamera.transform;
			if (transform.position != m_prevCameraPosition || base.transform.position != m_prevPosition)
			{
				Vector3 offset = HB.GetOffset(base.transform.position, transform);
				if (offset.magnitude != float.PositiveInfinity)
				{
					m_transform.localPosition = m_transform.InverseTransformVector(-offset);
				}
				m_prevCameraPosition = transform.position;
				m_prevPosition = base.transform.position;
			}
		}
	}
}
