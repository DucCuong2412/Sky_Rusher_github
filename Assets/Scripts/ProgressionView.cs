using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionView : SingletonMB<ProgressionView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Text m_Text;

	public Image m_Bar;

	public Image m_Fill;

	public Image m_PrevLvl;

	public Text m_PrevLvlText;

	public Image m_NextLvl;

	public Text m_NextLvlText;

	private CanvasGroup m_Group;

	private GameManager m_GameManager;

	private void Awake()
	{
		m_Group = GetComponent<CanvasGroup>();
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	public void SetColor(Color _Color)
	{
		m_Text.color = _Color;
		m_Fill.color = _Color;
		m_Bar.color = _Color;
		m_PrevLvl.color = _Color;
		m_PrevLvlText.color = _Color;
		m_NextLvl.color = _Color;
		m_NextLvlText.color = _Color;
	}

	public void SetProgression(float _RemainingDistance, float _TotalDistance)
	{
		m_Text.text = Mathf.RoundToInt(_RemainingDistance).ToString() + "m";
		m_Fill.fillAmount = 1f - _RemainingDistance / _TotalDistance;
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.GAME:
		case GamePhase.SAVE_ME:
		case GamePhase.CONTINUE_GAME:
			break;
		case GamePhase.FAILED:
			break;
		case GamePhase.MAIN_MENU:
			m_Group.alpha = 0f;
			break;
		case GamePhase.INTRO:
			SetUiIntro();
			break;
		case GamePhase.SUCCESS:
			StartCoroutine(Appear(_Appear: false));
			break;
		}
	}

	private IEnumerator Appear(bool _Appear)
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime / 0.5f;
			m_Group.alpha = Mathf.Lerp((!_Appear) ? 1f : 0f, (!_Appear) ? 0f : 1f, time);
			yield return null;
		}
	}

	private void EnableLevelText(bool _Value)
	{
		m_PrevLvl.enabled = _Value;
		m_PrevLvlText.enabled = _Value;
		m_NextLvlText.enabled = _Value;
		m_NextLvl.enabled = _Value;
	}

	private void EnableProgressionBar(bool _Value)
	{
		m_Text.enabled = _Value;
		m_Bar.enabled = _Value;
		m_Fill.enabled = _Value;
	}

	private void SetUiIntro()
	{
		if (m_GameManager.m_HasTimeToSurvive)
		{
			EnableLevelText(_Value: false);
			EnableProgressionBar(_Value: false);
		}
		else if (m_GameManager.m_SpeedChallenge || m_GameManager.m_HasTimeToBeat || m_GameManager.m_IsBonusStage)
		{
			EnableLevelText(_Value: false);
			EnableProgressionBar(_Value: true);
		}
		else
		{
			EnableLevelText(_Value: true);
			EnableProgressionBar(_Value: true);
			int level = m_GameManager.GetLevel();
			m_PrevLvlText.text = level.ToString();
			m_NextLvlText.text = (level + 1).ToString();
		}
		StartCoroutine(Appear(_Appear: true));
	}
}
