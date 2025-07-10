using UnityEngine;

public class Currency : MonoBehaviour
{
	private Transform m_Transform;

	private Transform m_Parent;

	private void Awake()
	{
		m_Transform = base.transform;
		m_Parent = m_Transform.parent;
	}

	private void Update()
	{
		Transform transform = m_Transform;
		Vector3 eulerAngles = m_Parent.eulerAngles;
		transform.localRotation = Quaternion.Euler(0f, 0f, 0f - eulerAngles.z);
	}
}
