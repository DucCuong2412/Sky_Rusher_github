using UnityEngine;

public class LevelBasedMovement : MonoBehaviour
{
	public int m_MinLevel;

	public int m_MaxLevel;

	public float m_DistToActivate;

	private bool m_Activable;

	protected Transform m_Transform;

	private Transform m_PlayerTr;

	private SkinManager m_SkinManager;

	private bool m_Activated;

	protected float m_LevelPower;

	private void Awake()
	{
		int level = SingletonMB<GameManager>.Instance.GetLevel();
		m_Activable = (level >= m_MinLevel);
		m_Transform = base.transform;
		m_PlayerTr = SingletonMB<Player>.Instance.transform;
		m_SkinManager = SingletonMB<SkinManager>.Instance;
		if (m_Activable)
		{
			m_Activated = false;
			m_LevelPower = (float)(Mathf.Clamp(level, m_MinLevel, m_MaxLevel) - m_MinLevel) / (float)m_MaxLevel;
		}
		AwakeSpecific();
		m_SkinManager.onPlayerChanged += OnSkinChanged;
	}

	protected virtual void AwakeSpecific()
	{
	}

	private void OnDestroy()
	{
		if (m_SkinManager != null)
		{
			m_SkinManager.onPlayerChanged -= OnSkinChanged;
		}
	}

	private void Update()
	{
		if (!m_Activable)
		{
			return;
		}
		if (m_Activated)
		{
			Move();
			return;
		}
		Vector3 position = m_Transform.position;
		float z = position.z;
		Vector3 position2 = m_PlayerTr.position;
		if (z - position2.z < m_DistToActivate)
		{
			Activate();
		}
	}

	protected virtual void Move()
	{
	}

	protected virtual void Activate()
	{
		m_Activated = true;
	}

	private void OnSkinChanged(Player _Player)
	{
		m_PlayerTr = _Player.transform;
	}
}
