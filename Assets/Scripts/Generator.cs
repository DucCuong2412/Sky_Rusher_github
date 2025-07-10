using System.Collections.Generic;
using UnityEngine;

public class Generator : SingletonMB<Generator>
{
	public delegate void OnResetGame();

	private const float c_GenDistance = 200f;

	private const float c_SectionLength = 40f;

	private const float c_StartPatternOffset = 120f;

	public GameObject[] m_SectionPrefab;

	public GameObject m_EndPrefab;

	public int m_NbPatternBeforeChange;

	private bool m_IsGenerating;

	private List<PatternData> m_Patterns;

	private List<int> m_PatternIds;

	private int m_LastPatternId;

	private float m_LastSectionZ;

	private float m_LastPatternZ;

	private GameObject m_EndInstance;

	private Transform m_PlayerTr;

	private GameManager m_GameManager;

	private SkinManager m_SkinManager;

	private int m_Level;

	private float m_TotalDistance;

	private bool m_FromSkinMenu;

	private Vector3 m_PosBuffer;

	public event OnResetGame onResetGame;

	private void Awake()
	{
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_SkinManager = SingletonMB<SkinManager>.Instance;
		m_PosBuffer = Vector3.zero;
		m_EndInstance = Object.Instantiate(m_EndPrefab, Vector3.zero, Quaternion.identity);
		m_LastPatternId = -1;
		m_Patterns = new List<PatternData>();
		m_PatternIds = new List<int>();
		m_Patterns.AddRange(Resources.LoadAll<PatternData>("Patterns"));
		m_SkinManager.onPlayerChanged += OnSkinChanged;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	public void SetColor(Color _Color)
	{
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.GAME:
		case GamePhase.SAVE_ME:
		case GamePhase.CONTINUE_GAME:
			break;
		case GamePhase.MAIN_MENU:
			if (!m_GameManager.isChallenge && !m_GameManager.m_IsBonusStage)
			{
				ChangePatternPath("Patterns");
			}
			Init();
			break;
		case GamePhase.INTRO:
			m_PosBuffer.z = m_TotalDistance;
			m_EndInstance.transform.position = m_PosBuffer;
			m_EndInstance.SetActive(value: true);
			break;
		case GamePhase.SUCCESS:
			m_IsGenerating = false;
			break;
		case GamePhase.FAILED:
			m_IsGenerating = false;
			break;
		}
	}

	private void Init()
	{
		if (this.onResetGame != null)
		{
			this.onResetGame();
		}
		m_LastSectionZ = 0f;
		m_LastPatternZ = 120f;
		m_IsGenerating = true;
		m_EndInstance.SetActive(value: false);
		m_Level = m_GameManager.GetLevel();
		m_TotalDistance = m_GameManager.totalDistance;
		m_PlayerTr = SingletonMB<Player>.Instance.transform;
		Generate();
	}

	private GameObject PickPattern(float _MaxLength, out float _Length)
	{
		m_PatternIds.Clear();
		for (int i = 0; i < m_Patterns.Count; i++)
		{
			if (i != m_LastPatternId)
			{
				PatternData patternData = m_Patterns[i];
				if ((patternData.m_MinLevel == -1 || m_Level >= patternData.m_MinLevel) && (patternData.m_MaxLevel == -1 || m_Level <= patternData.m_MaxLevel) && !(patternData.m_PatternLength > _MaxLength))
				{
					m_PatternIds.Add(i);
				}
			}
		}
		if (m_PatternIds.Count == 0)
		{
			_Length = 1000f;
			return null;
		}
		m_LastPatternId = m_PatternIds[Random.Range(0, m_PatternIds.Count)];
		PatternData patternData2 = m_Patterns[m_LastPatternId];
		_Length = patternData2.m_PatternLength;
		return patternData2.m_Prefab;
	}

	private void Update()
	{
		if (m_IsGenerating)
		{
			Generate();
		}
	}

	private void Generate()
	{
		Vector3 position = m_PlayerTr.position;
		float num = position.z + 200f;
		if (num < m_GameManager.totalDistance)
		{
			while (num > m_LastSectionZ)
			{
				m_PosBuffer.z = m_LastSectionZ;
				InstantiateCorridor();
				m_LastSectionZ += 40f;
			}
			while (num > m_LastPatternZ)
			{
				float maxLength = m_TotalDistance - m_LastPatternZ;
				float _Length;
				GameObject gameObject = PickPattern(maxLength, out _Length);
				if (gameObject != null)
				{
					m_PosBuffer.z = m_LastPatternZ;
					Object.Instantiate(gameObject, m_PosBuffer, Quaternion.identity);
				}
				m_LastPatternZ += _Length;
			}
		}
		else
		{
			m_PosBuffer.z = m_LastSectionZ;
			InstantiateCorridor();
			m_LastSectionZ += 40f;
		}
	}

	private void OnSkinChanged(Player _Player)
	{
		m_PlayerTr = _Player.transform;
	}

	private void InstantiateCorridor()
	{
		//if (VoodooSauce.GetPlayerCohort() == "Corridor_Form")
		{
			int num = m_Level / m_NbPatternBeforeChange % m_SectionPrefab.Length;
			Object.Instantiate(m_SectionPrefab[num], m_PosBuffer, Quaternion.identity);
		}
		//else
		//{
		//	Object.Instantiate(m_SectionPrefab[0], m_PosBuffer, Quaternion.identity);
		//}
	}

	public void ChangePatternPath(string _Path)
	{
		m_Patterns.Clear();
		m_Patterns.AddRange(Resources.LoadAll<PatternData>(_Path));
		Init();
	}
}
