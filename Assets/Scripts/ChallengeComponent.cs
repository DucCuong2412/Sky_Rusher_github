using UnityEngine;
using UnityEngine.UI;

public class ChallengeComponent : MonoBehaviour
{
	public Text m_TextDescription;

	public Image m_CheckBox;

	private ChallengeData m_ChallengeData;

	private ChallengeView m_ChallengeView;

	private Image m_Img;

	private void Awake()
	{
		m_ChallengeView = SingletonMB<ChallengeView>.Instance;
		m_Img = GetComponent<Image>();
	}

	public void Init(ChallengeData _Challenge)
	{
		m_TextDescription.text = _Challenge.m_DescriptionId;
		m_ChallengeData = _Challenge;
	}

	public void OnChallengeSelected()
	{
		m_ChallengeView.OnChallengeSelected(m_ChallengeData);
	}

	public void SetColor(Color _Color)
	{
		m_Img.color = _Color;
		m_CheckBox.color = _Color;
	}
}
