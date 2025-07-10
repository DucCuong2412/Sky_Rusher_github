using UnityEngine;

public class Section : MonoBehaviour
{
	private const float c_SafeDistance = 100f;

	private Transform m_Transform;

	private Transform m_PlayerTr;

	private GameManager m_GameManager;

	private SkinManager m_SkinManager;

	private void Awake()
	{
		m_Transform = base.transform;
		m_PlayerTr = SingletonMB<Player>.Instance.transform;
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_SkinManager = SingletonMB<SkinManager>.Instance;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
		m_SkinManager.onPlayerChanged += OnSkinChanged;
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
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		if (_Phase == GamePhase.MAIN_MENU)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnSkinChanged(Player _Player)
	{
		m_PlayerTr = _Player.transform;
	}

	private void Update()
	{
		if (m_GameManager.currentPhase == GamePhase.GAME)
		{
			Vector3 position = m_PlayerTr.position;
			float z = position.z;
			Vector3 position2 = m_Transform.position;
			if (z - position2.z > 100f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
