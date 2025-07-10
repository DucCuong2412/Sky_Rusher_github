using UnityEngine;
using UnityEngine.UI;

public class InfoView : SingletonMB<InfoView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Text m_InfoText;

	private CanvasGroup m_Group;

	private GameManager m_GameManager;

	private void Awake()
	{
		m_Group = GetComponent<CanvasGroup>();
		m_GameManager = SingletonMB<GameManager>.Instance;
		Enable(_Enable: false);
	}

	private void Enable(bool _Enable)
	{
		m_Group.alpha = ((!_Enable) ? 0f : 1f);
		m_Group.interactable = _Enable;
		m_Group.blocksRaycasts = _Enable;
	}

	public void DisableView()
	{
		Enable(_Enable: false);
	}

	public void SetDescription(string _Descritption)
	{
		m_InfoText.text = _Descritption;
		Enable(_Enable: true);
	}
}
