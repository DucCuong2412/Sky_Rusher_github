using UnityEngine;

public class Tweener : MonoBehaviour
{
	public bool m_IsUI;

	public bool m_PlayAtStart;

	public bool m_Loop;

	public bool m_PingPong;

	public bool m_OverrideStartState;

	public bool m_HasDelay;

	public bool m_RepeatableDelay;

	public float m_Delay;

	protected int m_CurrentStateId;

	protected int m_NextStateId = 1;

	protected bool m_IsDelayed;

	private bool m_StartToEnd = true;

	protected bool m_IsPlaying;

	protected float m_Time;

	protected RectTransform m_UITransform;

	protected Transform m_Transform;

	private void Awake()
	{
		AwakeSpecific();
	}

	protected virtual void AwakeSpecific()
	{
	}

	private void OnEnable()
	{
		if (m_PlayAtStart)
		{
			Play();
		}
	}

	private void OnDisable()
	{
		Stop();
	}

	public virtual void Play()
	{
		m_IsPlaying = true;
		m_Time = 0f;
		m_StartToEnd = true;
		m_CurrentStateId = 0;
		m_NextStateId = 1;
		if (m_HasDelay)
		{
			m_IsDelayed = true;
		}
	}

	public virtual void Stop()
	{
		m_IsPlaying = false;
		m_Time = 0f;
		m_StartToEnd = true;
		m_CurrentStateId = 0;
		m_NextStateId = 1;
	}

	private void Update()
	{
		if (!m_IsPlaying)
		{
			return;
		}
		float num = (!m_IsUI) ? Time.deltaTime : Time.unscaledDeltaTime;
		if (m_IsDelayed)
		{
			if (m_Time < 1f)
			{
				m_Time += num / m_Delay;
				return;
			}
			m_IsDelayed = false;
			m_Time = 0f;
		}
		else
		{
			UpdateSpecific(num);
		}
	}

	protected virtual void UpdateSpecific(float _Dt)
	{
	}

	protected virtual int GetStateCount()
	{
		return 0;
	}

	protected virtual void GoToNextState()
	{
		m_Time = 0f;
		if (m_PingPong)
		{
			if (m_StartToEnd)
			{
				m_CurrentStateId++;
				m_NextStateId++;
				if (m_NextStateId < GetStateCount())
				{
					return;
				}
				if (m_Loop)
				{
					if (m_HasDelay)
					{
						m_IsDelayed = true;
					}
					m_StartToEnd = false;
					m_CurrentStateId = GetStateCount() - 1;
					m_NextStateId = m_CurrentStateId - 1;
				}
				else
				{
					Stop();
				}
				return;
			}
			m_CurrentStateId--;
			m_NextStateId--;
			if (m_NextStateId < 0)
			{
				if (m_Loop)
				{
					m_StartToEnd = true;
					m_CurrentStateId = 0;
					m_NextStateId = 1;
				}
				else
				{
					Stop();
				}
			}
			return;
		}
		m_CurrentStateId++;
		m_NextStateId++;
		if (m_NextStateId < GetStateCount())
		{
			return;
		}
		if (m_Loop)
		{
			if (m_HasDelay)
			{
				m_IsDelayed = true;
			}
			m_CurrentStateId = 0;
			m_NextStateId = 1;
		}
		else
		{
			Stop();
		}
	}
}
