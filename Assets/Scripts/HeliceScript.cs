using UnityEngine;

public class HeliceScript : MonoBehaviour
{
	public int m_Speed;

	private void Update()
	{
		base.transform.Rotate(Vector3.right * Time.deltaTime * m_Speed);
	}
}
