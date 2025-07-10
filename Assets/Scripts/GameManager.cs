using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMB<GameManager>
{
	public delegate void OnGamePhaseChanged(GamePhase _GamePhase);

	private GamePhase m_CurrentPhase;

	private float m_Score;

	private float m_RemainingDistance;

	private float m_TotalDistance;

	private bool m_IsChallenge;

	public bool m_HasTimeToBeat;

	private float m_TimeToBeat;

	public bool m_HasTimeToSurvive;

	private float m_TimeToSurvive;

	public bool m_SpeedChallenge;

	public bool m_IsBonusStage;

	public int m_LevelBeforeBonusStage;

	private float m_BestScore;

	private float m_StartTime;

	private int m_CurrencyEarned;

	private Player m_Player;

	private Transform m_PlayerTr;

	private ProgressionView m_ProgressionView;

	private CurrencyView m_CurrencyView;

	private SkinManager m_SkinManager;

	private BonusStage m_BonusStage;

	private Dictionary<string, object> m_CustomEvent;

	public GamePhase currentPhase => m_CurrentPhase;

	public float score => m_Score;

	public float remainingDistance => m_RemainingDistance;

	public float totalDistance
	{
		get
		{
			return m_TotalDistance;
		}
		set
		{
			m_TotalDistance = value;
		}
	}

	public bool isChallenge
	{
		get
		{
			return m_IsChallenge;
		}
		set
		{
			m_IsChallenge = value;
		}
	}

	public event OnGamePhaseChanged onGamePhaseChanged;

	private void Awake()
	{
		Application.targetFrameRate = 60;
		Input.multiTouchEnabled = false;
		m_ProgressionView = SingletonMB<ProgressionView>.Instance;
		m_CurrencyView = SingletonMB<CurrencyView>.Instance;
		m_SkinManager = SingletonMB<SkinManager>.Instance;
		m_BonusStage = SingletonMB<BonusStage>.Instance;
		m_CustomEvent = new Dictionary<string, object>();
		m_SkinManager.onPlayerChanged += OnSkinChanged;
	}

	private void Start()
	{
		ChangePhase(GamePhase.MAIN_MENU);
	}

	private void Update()
	{
		if (m_CurrentPhase == GamePhase.GAME || m_CurrentPhase == GamePhase.CONTINUE_GAME)
		{
			float totalDistance = m_TotalDistance;
			Vector3 position = m_PlayerTr.position;
			m_RemainingDistance = Mathf.Clamp(totalDistance - position.z, 0f, m_TotalDistance);
			m_ProgressionView.SetProgression(m_RemainingDistance, m_TotalDistance);
		}
	}

	private void OnBannerShown()
	{
		UnityEngine.Debug.Log("On Banner Shown");
	}

	private void OnInterstitialShown()
	{
		UnityEngine.Debug.Log("On Interstitial Shown");
	}

	public void ChangePhase(GamePhase _GamePhase)
	{
		switch (_GamePhase)
		{
		case GamePhase.MAIN_MENU:
			if (!isChallenge && !m_IsBonusStage)
			{
				m_TotalDistance = 1000f + (float)(GetLevel() - 1) * 50f;
			}
			m_CurrencyView.SetCurrency(GetCurrency());
			break;
		case GamePhase.INTRO:
			m_RemainingDistance = m_TotalDistance;
			m_ProgressionView.SetProgression(m_RemainingDistance, m_TotalDistance);
			SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.INTRO);
			break;
		case GamePhase.GAME:
			m_StartTime = Time.time;
			SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.STARTAMBIANCE);
			break;
		case GamePhase.SAVE_ME:
			SingletonMB<SoundManager>.Instance.StopSound(ESoundType.STARTAMBIANCE);
			break;
		case GamePhase.CONTINUE_GAME:
			break;
		case GamePhase.FAILED:
			StartCoroutine(TimerInterstitial());
			SingletonMB<SoundManager>.Instance.StopSound(ESoundType.STARTAMBIANCE);
			SendCustomEvent(_Won: false);
			break;
		case GamePhase.SUCCESS:
			m_RemainingDistance = 0f;
			SingletonMB<SoundManager>.Instance.StopSound(ESoundType.STARTAMBIANCE);
			SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.ENDWOOSH);
			SendCustomEvent(_Won: true);
			m_ProgressionView.SetProgression(m_RemainingDistance, m_TotalDistance);
			break;
		}
		m_CurrentPhase = _GamePhase;
		if (this.onGamePhaseChanged != null)
		{
			this.onGamePhaseChanged(_GamePhase);
		}
	}

	private void SendCustomEvent(bool _Won)
	{
		m_CustomEvent.Clear();
		m_CustomEvent.Add("Level", GetLevel());
		m_CustomEvent.Add("Won", _Won);
		m_CustomEvent.Add("Currency", GetCurrencyEarned());
		m_CurrencyEarned = 0;
	}

	public bool HasBestScore()
	{
		return PlayerPrefs.HasKey(Constants.c_BestScoreSave);
	}

	public float GetBestScore()
	{
		if (PlayerPrefs.HasKey(Constants.c_BestScoreSave))
		{
			return PlayerPrefs.GetFloat(Constants.c_BestScoreSave);
		}
		return 0f;
	}

	public void GoToNextLevel()
	{
		PlayerPrefs.SetInt(Constants.c_LevelSave, GetLevel() + 1);
	}

	public void CheckLevel()
	{
		UnityEngine.Debug.Log("ICI");
		int level = GetLevel();
		if (level % m_LevelBeforeBonusStage == 0 && !m_IsBonusStage)
		{
			m_IsBonusStage = true;
			m_BonusStage.Init();
		}
		else
		{
			m_IsBonusStage = false;
			GoToNextLevel();
		}
	}

	private void GotoNextLevel(int _Lvl)
	{
		PlayerPrefs.SetInt(Constants.c_LevelSave, _Lvl);
	}

	public int GetLevel()////////////curent level
	{
		if (PlayerPrefs.HasKey(Constants.c_LevelSave))
		{
			return PlayerPrefs.GetInt(Constants.c_LevelSave);
		}
		return 1;
	}

	public void AddCurrency(int _Currency)
	{
		AddCurrencyEarned(_Currency);
		PlayerPrefs.SetInt(Constants.c_CurrencySave, PlayerPrefs.GetInt(Constants.c_CurrencySave) + _Currency);
		m_CurrencyView.SetCurrency(GetCurrency());
	}

	private void AddCurrencyEarned(int __Currency)
	{
		m_CurrencyEarned += __Currency;
	}

	private int GetCurrencyEarned()
	{
		return m_CurrencyEarned;
	}

	public int GetCurrency()
	{
		if (PlayerPrefs.HasKey(Constants.c_CurrencySave))
		{
			return PlayerPrefs.GetInt(Constants.c_CurrencySave);
		}
		return 0;
	}

	public bool GetVibrations()
	{
		if (PlayerPrefs.HasKey(Constants.c_VibrationSave))
		{
			return PlayerPrefs.GetInt(Constants.c_VibrationSave) == 1;
		}
		return true;
	}

	public void ToggleVibrations()
	{
		PlayerPrefs.SetInt(Constants.c_VibrationSave, (!GetVibrations()) ? 1 : 0);
	}

	public bool GetSound()
	{
		if (PlayerPrefs.HasKey(Constants.c_SoundSave))
		{
			return PlayerPrefs.GetInt(Constants.c_SoundSave) == 1;
		}
		return true;
	}

	public void ToggleSound()
	{
		PlayerPrefs.SetInt(Constants.c_SoundSave, (!GetSound()) ? 1 : 0);
	}

	private IEnumerator TimerInterstitial()
	{
		yield return new WaitForSeconds(0.5f);
	}

	private void OnSkinChanged(Player _Player)
	{
		m_Player = _Player;
		m_PlayerTr = m_Player.transform;
	}

	public void SetTimeToBeat(bool _HasTime, float _Time = 0f)
	{
		m_HasTimeToBeat = _HasTime;
		m_TimeToBeat = _Time;
	}

	public void SetTimeToSurvive(bool _HasTime, float _Time = 0f)
	{
		m_HasTimeToSurvive = _HasTime;
		m_TimeToSurvive = _Time;
	}

	public void SetSpeedAttackChallenge(bool _Value)
	{
		m_SpeedChallenge = _Value;
	}

	public float GetTimeToBeat()
	{
		return m_TimeToBeat;
	}

	public float GetTimeToSurvive()
	{
		return m_TimeToSurvive;
	}
}
