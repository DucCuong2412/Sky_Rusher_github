using Battlehub.HorizonBending;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoningManager : SingletonMB<ZoningManager>
{
	private const float c_SuccessDuration = 0.5f;

	public List<Color> m_Colors;

	public Material m_ObstacleMat;

	private bool m_ZoningEnabled;

	private GameManager m_GameManager;

	private Camera m_MainCamera;

	private Player m_Player;

	private Generator m_Generator;

	private StartRamp m_StartRamp;

	private SkinManager m_SkinManager;

	private ChallengeManager m_ChallengeManager;

	private MainMenuView m_MainMenuView;

	private LevelView m_LevelView;

	private ProgressionView m_ProgressionView;

	private SuccessView m_SuccessView;

	private FailedView m_FailedView;

	private ChallengeView m_ChallengeView;

	private RewardVideoView m_RewardVidéoView;

	private TimeAttackView m_TimeAttackView;

	private SurvivalView m_SurvivalView;

	private Pattern m_Pattern;

	private float m_StartTime;

	private int m_CurrId;

	private int m_NextId;

	public Color zoningColor => m_Colors[m_CurrId];

	private void Awake()
	{
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_MainCamera = SingletonMB<MainCamera>.Instance.GetComponent<Camera>();
		m_Generator = SingletonMB<Generator>.Instance;
		m_StartRamp = SingletonMB<StartRamp>.Instance;
		m_SkinManager = SingletonMB<SkinManager>.Instance;
		m_ChallengeManager = SingletonMB<ChallengeManager>.Instance;
		m_MainMenuView = SingletonMB<MainMenuView>.Instance;
		m_LevelView = SingletonMB<LevelView>.Instance;
		m_ProgressionView = SingletonMB<ProgressionView>.Instance;
		m_SuccessView = SingletonMB<SuccessView>.Instance;
		m_FailedView = SingletonMB<FailedView>.Instance;
		m_ChallengeView = SingletonMB<ChallengeView>.Instance;
		m_RewardVidéoView = SingletonMB<RewardVideoView>.Instance;
		m_TimeAttackView = SingletonMB<TimeAttackView>.Instance;
		m_SurvivalView = SingletonMB<SurvivalView>.Instance;
		m_SkinManager.onPlayerChanged += OnSkinChanged;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	private void SetZoning()
	{
		HB.ApplyCurvature(0f);
		Color color = m_Colors[m_CurrId];
		SetColor(color);
		m_MainMenuView.SetColor(color);
		m_LevelView.SetColor(color);
		m_ProgressionView.SetColor(color);
		m_SuccessView.SetColor(color);
		m_FailedView.SetColor(color);
		m_ChallengeView.SetColor(color);
		m_RewardVidéoView.SetColor(color);
		m_SurvivalView.SetColor(color);
	}

	private void Update()
	{
		if (m_ZoningEnabled)
		{
			HB.ApplyCurvature(Mathf.Sin((Time.time - m_StartTime) * 0.5f) * 2f);
		}
	}

	private void SetColor(Color _Color)
	{
		m_Player.SetColor(_Color);
		m_Generator.SetColor(_Color);
		m_StartRamp.SetColor(_Color);
		m_SkinManager.SetColor(_Color);
		m_ChallengeManager.SetColor(_Color);
		RenderSettings.fogColor = _Color;
		m_MainCamera.backgroundColor = _Color;
		m_ObstacleMat.color = new Color(0.17f, 0.17f, 0.17f, 1f);
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.INTRO:
		case GamePhase.SAVE_ME:
		case GamePhase.CONTINUE_GAME:
			break;
		case GamePhase.MAIN_MENU:
		{
			int level = m_GameManager.GetLevel();
			m_CurrId = (level - 1) % m_Colors.Count;
			m_NextId = level % m_Colors.Count;
			m_ZoningEnabled = false;
			m_Player = SingletonMB<Player>.Instance;
			SetZoning();
			break;
		}
		case GamePhase.GAME:
			m_ZoningEnabled = true;
			m_StartTime = Time.time;
			break;
		case GamePhase.SUCCESS:
			m_ZoningEnabled = false;
			StartCoroutine(SuccessCoroutine());
			break;
		case GamePhase.FAILED:
			m_ZoningEnabled = false;
			break;
		}
	}

	private IEnumerator SuccessCoroutine()
	{
		Color currColor = m_Colors[m_CurrId];
		Color nextColor = m_Colors[m_NextId];
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime / 0.5f;
			Color lerped = Color.Lerp(currColor, nextColor, time);
			SetColor(lerped);
			m_SuccessView.SetColor(lerped);
			yield return null;
		}
	}

	private void OnSkinChanged(Player _Player)
	{
		m_Player = _Player;
	}
}
