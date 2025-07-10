using System.Collections;
using UnityEngine;

public class Translate : LevelBasedMovement
{
	public Transform m_Start;

	public Transform m_End;

	public float m_MinDuration = 0.3f;

	public float m_MaxDuration = 1f;

	public float m_MinRand = 0.3f;

	public float m_MaxRand = 1f;

	public AnimationCurve m_Curve;

	private float m_Duration;

	protected override void AwakeSpecific()
	{
		m_Duration = m_MinDuration + Random.Range(0f, m_MaxDuration - m_MinDuration) + Random.Range(m_MinRand, m_MaxRand);
	}

	protected override void Activate()
	{
		base.Activate();
		StartCoroutine(MoveCoroutine());
	}

	private IEnumerator MoveCoroutine()
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime / m_Duration;
			m_Transform.position = Vector3.Lerp(m_Start.position, m_End.position, m_Curve.Evaluate(time));
			yield return null;
		}
	}
}
