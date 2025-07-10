using System;
using System.Collections.Generic;
using UnityEngine;

public class Tweener_Position : Tweener
{
	[Serializable]
	public class State
	{
		public float m_Duration;

		public bool m_HasCurve;

		public AnimationCurve m_Curve;

		public Vector3 m_Position;
	}

	public List<State> m_States;

	protected override void AwakeSpecific()
	{
		if (m_IsUI)
		{
			m_UITransform = GetComponent<RectTransform>();
			if (!m_OverrideStartState)
			{
				m_States[0].m_Position = m_UITransform.anchoredPosition;
			}
		}
		else
		{
			m_Transform = GetComponent<Transform>();
			if (!m_OverrideStartState)
			{
				m_States[0].m_Position = m_Transform.localPosition;
			}
		}
	}

	protected override void UpdateSpecific(float _Dt)
	{
		State state = m_States[m_CurrentStateId];
		State state2 = m_States[m_NextStateId];
		if (m_Time < 1f)
		{
			m_Time += _Dt / state2.m_Duration;
			float num = 0f;
			num = ((!state2.m_HasCurve) ? m_Time : state2.m_Curve.Evaluate(m_Time));
			if (m_IsUI)
			{
				m_UITransform.anchoredPosition = Vector2.Lerp(state.m_Position, state2.m_Position, num);
			}
			else
			{
				m_Transform.localPosition = Vector3.Lerp(state.m_Position, state2.m_Position, num);
			}
		}
		else
		{
			GoToNextState();
		}
	}

	protected override int GetStateCount()
	{
		return m_States.Count;
	}
}
