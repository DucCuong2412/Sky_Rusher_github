using UnityEngine;

public class SpeedLines : SingletonMB<SpeedLines>
{
	private Transform m_Transform;

	private Transform m_PlayerTr;

	private Vector3 m_PosBuffer;

	private void Awake()
	{
		m_Transform = base.transform;
		m_PlayerTr = SingletonMB<Player>.Instance.transform;
		m_PosBuffer = m_Transform.position;
	}

	private void Update()
	{
		ref Vector3 posBuffer = ref m_PosBuffer;
		Vector3 position = m_PlayerTr.position;
		posBuffer.z = position.z;
		m_Transform.position = m_PosBuffer;
	}
}
