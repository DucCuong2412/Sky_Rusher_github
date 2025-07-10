using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : SingletonMB<ChallengeManager>
{
	public GameObject m_ChallengeSlotPrefab;

	private ChallengeView m_ChallengeView;

	private MainMenuView m_MainMenuView;

	private TimeAttackView m_TimeAttackView;

	private SurvivalView m_SurvivalView;

	private GameManager m_GameManager;

	private List<ChallengeData> m_Challenge;

	private List<ChallengeComponent> m_ChallengeComponent;

	private ChallengeData m_ActualChallenge;

	private Dictionary<string, object> m_CustomEvent;

	private void Awake()
	{
		m_ChallengeView = SingletonMB<ChallengeView>.Instance;
		m_MainMenuView = SingletonMB<MainMenuView>.Instance;
		m_TimeAttackView = SingletonMB<TimeAttackView>.Instance;
		m_SurvivalView = SingletonMB<SurvivalView>.Instance;
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_Challenge = new List<ChallengeData>();
		m_Challenge.AddRange(Resources.LoadAll<ChallengeData>(Constants.c_ChallengePath));
		m_ChallengeComponent = new List<ChallengeComponent>();
		for (int i = 0; i < m_Challenge.Count; i++)
		{
			GameObject gameObject = Object.Instantiate(m_ChallengeSlotPrefab, m_ChallengeView.m_Content);
			gameObject.GetComponent<ChallengeComponent>().Init(m_Challenge[i]);
			m_ChallengeComponent.Add(gameObject.GetComponent<ChallengeComponent>());
		}
		CheckChallengeCompleted();
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
		m_CustomEvent = new Dictionary<string, object>();
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.MAIN_MENU:
			CheckChallengeCompleted();
			break;
		}
	}

	private void Update()
	{
		if ((m_GameManager.currentPhase == GamePhase.GAME || m_GameManager.currentPhase == GamePhase.CONTINUE_GAME) && m_GameManager.isChallenge)
		{
			if (m_GameManager.m_HasTimeToBeat)
			{
				m_TimeAttackView.TimeAttack();
			}
			else if (m_GameManager.m_HasTimeToSurvive)
			{
				m_SurvivalView.SurvivalTime();
			}
		}
	}

	public void OnChallengeSelected(ChallengeData _ChallengeData)
	{
		if (HasCompletedChallenge(_ChallengeData))
		{
			_ChallengeData.m_Reward = (int)((float)_ChallengeData.m_Reward * 0.3f);
		}
		SetChallenge(_ChallengeData);
		m_ActualChallenge = _ChallengeData;
	}

	private bool HasCompletedChallenge(ChallengeData _ChallengeData)
	{
		if (PlayerPrefs.HasKey(_ChallengeData.m_SaveId))
		{
			return PlayerPrefs.GetInt(_ChallengeData.m_SaveId) == 1;
		}
		return false;
	}

	private void SetChallenge(ChallengeData _ChallengeData)
	{
		if (!m_GameManager.isChallenge)
		{
			m_GameManager.ChangePhase(GamePhase.INTRO);
			return;
		}
		_ChallengeData.Init();
		if (m_GameManager.m_HasTimeToBeat)
		{
			float timeToBeat = m_GameManager.GetTimeToBeat();
			m_TimeAttackView.Init(timeToBeat);
		}
		else if (m_GameManager.m_HasTimeToSurvive)
		{
			float timeToSurvive = m_GameManager.GetTimeToSurvive();
			m_SurvivalView.Init(timeToSurvive);
		}
	}

	public void ChallengeHasCompleted(bool _Won)
	{
		SendCustomEvent(_Won);
		m_ActualChallenge.Reset();
		if (_Won)
		{
			CompleteChallenge();
		}
		else
		{
			FailedChallenge();
		}
	}

	private void CompleteChallenge()
	{
		PlayerPrefs.SetInt(m_ActualChallenge.m_SaveId, 1);
	}

	private void FailedChallenge()
	{
		m_ActualChallenge.Reset();
	}

	public void SetColor(Color _color)
	{
		for (int i = 0; i < m_ChallengeComponent.Count; i++)
		{
			m_ChallengeComponent[i].SetColor(_color);
		}
	}

	public void RestartChallenge()
	{
		SetChallenge(m_ActualChallenge);
	}

	private void CheckChallengeCompleted()
	{
		for (int i = 0; i < m_Challenge.Count; i++)
		{
			if (HasCompletedChallenge(m_Challenge[i]))
			{
				m_ChallengeComponent[i].m_CheckBox.gameObject.SetActive(value: true);
			}
		}
	}

	private void SendCustomEvent(bool _Won)
	{
		m_CustomEvent.Clear();
		m_CustomEvent.Add("Challenge", m_ActualChallenge);
		m_CustomEvent.Add("Won", _Won);
	}
}
