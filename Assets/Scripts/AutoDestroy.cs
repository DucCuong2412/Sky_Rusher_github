using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
	public float m_Duration = 1f;

	private float m_StartTime;

	private void Awake()
	{
		m_StartTime = Time.time;
	}

	private void Update()
	{
		if (Time.time - m_StartTime > m_Duration)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
