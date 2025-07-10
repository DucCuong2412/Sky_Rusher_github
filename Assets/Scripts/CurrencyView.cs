using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : SingletonMB<CurrencyView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Image m_Image;

	public Text m_Text;

	private CanvasGroup m_Group;

	private GameManager m_GameManager;

	private void Awake()
	{
		m_Group = GetComponent<CanvasGroup>();
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	public void SetCurrency(int _Count)
	{
		m_Text.text = _Count.ToString();
	}

	private void OnGamePhaseChanged(GamePhase _Phase)
	{
		switch (_Phase)
		{
		case GamePhase.MAIN_MENU:
			break;
		case GamePhase.INTRO:
			break;
		case GamePhase.GAME:
		case GamePhase.SAVE_ME:
		case GamePhase.CONTINUE_GAME:
			break;
		case GamePhase.FAILED:
			break;
		case GamePhase.SUCCESS:
			break;
		}
	}
}
