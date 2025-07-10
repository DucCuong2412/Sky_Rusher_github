using UnityEngine;

public class RotateOnce : MonoBehaviour
{
	public float m_MinAngle;

	public float m_MaxAngle = 360f;

	private void Awake()
	{
		Transform transform = base.transform;
		transform.RotateAround(transform.position, Vector3.forward, UnityEngine.Random.Range(m_MinAngle, m_MaxAngle));
	}
}
