using UnityEngine;

public class Pattern : MonoBehaviour
{
	public float m_SafeDistance = 60f;

	private Transform m_Transform;

	private Transform m_PlayerTr;

	private GameManager m_GameManager;

	private SkinManager m_SkinManager;

	private HapticFeedback m_HapticManager;

	private Generator m_Generator;

	private bool m_FromSkinMenu;

	private bool m_HapticDone;

	private void Awake()
	{
		m_Transform = base.transform;
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_SkinManager = SingletonMB<SkinManager>.Instance;
		m_HapticManager = SingletonMB<HapticFeedback>.Instance;
		m_Generator = SingletonMB<Generator>.Instance;
		m_HapticDone = false;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
		m_SkinManager.onPlayerChanged += OnSkinChanged;
		m_Generator.onResetGame += OnResetGame;
	}

	private void Start()
	{
		m_PlayerTr = SingletonMB<Player>.Instance.transform;
	}

	private void OnDestroy()
	{
		if (m_GameManager != null)
		{
			m_GameManager.onGamePhaseChanged -= OnGamePhaseChanged;
		}
		if (m_SkinManager != null)
		{
			m_SkinManager.onPlayerChanged -= OnSkinChanged;
		}
		if (m_Generator != null)
		{
			m_Generator.onResetGame -= OnResetGame;
		}
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		if (_Phase == GamePhase.CONTINUE_GAME)
		{
			OnRevive();
		}
	}

	private void OnSkinChanged(Player _Player)
	{
		m_PlayerTr = _Player.transform;
	}

	private void OnRevive()
	{
		Vector3 position = m_Transform.position;
		float z = position.z;
		Vector3 position2 = m_PlayerTr.position;
		if (z - position2.z < m_SafeDistance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnResetGame()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Update()
	{
		if (!m_HapticDone)
		{
			Vector3 position = m_PlayerTr.position;
			float z = position.z;
			Vector3 position2 = m_Transform.position;
			if (z - position2.z > 0f)
			{
				m_HapticDone = true;
				if (m_GameManager.GetVibrations())
				{
					m_HapticManager.DoLightImapactHaptic();
				}
			}
		}
		Vector3 position3 = m_PlayerTr.position;
		float z2 = position3.z;
		Vector3 position4 = m_Transform.position;
		if (z2 - position4.z > m_SafeDistance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
