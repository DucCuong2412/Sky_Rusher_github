using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttackView : SingletonMB<TimeAttackView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Text m_TimerText;

	private GameManager m_GameManager;

	private ChallengeManager m_ChallengeManager;

	private CanvasGroup m_Group;

	private float m_Timer;

	private void Awake()
	{
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_ChallengeManager = SingletonMB<ChallengeManager>.Instance;
		m_Group = GetComponent<CanvasGroup>();
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.MAIN_MENU:
			Enable(_Enable: false);
			break;
		case GamePhase.FAILED:
			StartCoroutine(Appear(_Appear: false));
			if (m_GameManager.isChallenge)
			{
				m_ChallengeManager.ChallengeHasCompleted(_Won: false);
			}
			break;
		case GamePhase.SUCCESS:
			StartCoroutine(Appear(_Appear: false));
			if (m_GameManager.isChallenge)
			{
				m_ChallengeManager.ChallengeHasCompleted(_Won: true);
			}
			break;
		}
	}

	private void Enable(bool _Enable)
	{
		m_Group.alpha = ((!_Enable) ? 0f : 1f);
		m_Group.interactable = _Enable;
		m_Group.blocksRaycasts = _Enable;
	}

	private IEnumerator Appear(bool _Appear)
	{
		Enable(_Enable: false);
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime / 0.5f;
			m_Group.alpha = Mathf.Lerp((!_Appear) ? 1f : 0f, (!_Appear) ? 0f : 1f, time);
			yield return null;
		}
		if (_Appear)
		{
			Enable(_Enable: true);
		}
	}

	public void Init(float _TimeToBeat)
	{
		m_Timer = _TimeToBeat;
		m_TimerText.text = m_Timer.ToString();
		StartCoroutine(Appear(_Appear: true));
	}

	public void TimeAttack()
	{
		if (m_Timer > 0f)
		{
			m_Timer -= Time.deltaTime;
			UnityEngine.Debug.Log(Time.timeScale);
			m_TimerText.text = m_Timer.ToString("N0");
		}
		else
		{
			m_GameManager.ChangePhase(GamePhase.FAILED);
		}
	}

	public void SetColor(Color _Color)
	{
		m_TimerText.color = _Color;
	}
}
