using System;
using System.Collections.Generic;
using UnityEngine;

public class Tweener_Rotation : Tweener
{
	[Serializable]
	public class State
	{
		public float m_Duration;

		public bool m_HasCurve;

		public AnimationCurve m_Curve;

		public Quaternion m_Rotation;
	}

	public List<State> m_States;

	protected override void AwakeSpecific()
	{
		if (m_IsUI)
		{
			m_UITransform = GetComponent<RectTransform>();
		}
		else
		{
			m_Transform = GetComponent<Transform>();
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
				m_UITransform.localRotation = Quaternion.Lerp(state.m_Rotation, state2.m_Rotation, num);
			}
			else
			{
				m_Transform.localRotation = Quaternion.Lerp(state.m_Rotation, state2.m_Rotation, num);
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
