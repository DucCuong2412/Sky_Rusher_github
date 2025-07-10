using UnityEngine;

public class TardisScript : MonoBehaviour
{
	public int m_Speed;

	private void Update()
	{
		base.transform.Rotate(Vector3.up * Time.deltaTime * m_Speed);
	}
}
