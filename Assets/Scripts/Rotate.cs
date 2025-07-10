using UnityEngine;

public class Rotate : LevelBasedMovement
{
	public bool m_CanBeNegative = true;

	public float m_MinSpeed = 1f;

	public float m_MaxSpeed = 10f;

	public float m_MinRand = -1f;

	public float m_MaxRand = 1f;

	private float m_Speed;

	protected override void AwakeSpecific()
	{
		m_Speed = m_MinSpeed + m_LevelPower * (m_MaxSpeed - m_MinSpeed);
		m_Speed += Random.Range(m_MinRand, m_MaxRand);
		if (m_CanBeNegative && Random.Range(0, 2) == 0)
		{
			m_Speed *= -1f;
		}
	}

	protected override void Move()
	{
		m_Transform.RotateAround(m_Transform.position, Vector3.forward, m_Speed * Time.deltaTime);
	}
}
