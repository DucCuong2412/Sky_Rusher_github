using UnityEngine;

public class Flip : MonoBehaviour
{
	public bool m_CanXFlip;

	public bool m_CanYFlip;

	public bool m_CanZFlip;

	private void Awake()
	{
		base.transform.Rotate((!m_CanXFlip || UnityEngine.Random.Range(0, 2) != 0) ? 0f : 180f, (!m_CanYFlip || UnityEngine.Random.Range(0, 2) != 0) ? 0f : 180f, (!m_CanZFlip || UnityEngine.Random.Range(0, 2) != 0) ? 0f : 180f);
	}
}
