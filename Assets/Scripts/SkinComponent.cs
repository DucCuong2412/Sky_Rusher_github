using UnityEngine;
using UnityEngine.UI;

public class SkinComponent : MonoBehaviour
{
	private SkinData m_SkinData;

	private SkinView m_SkinView;

	private GameManager m_GameManager;

	private Image m_Img;

	private void Awake()
	{
		m_SkinView = SingletonMB<SkinView>.Instance;
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_Img = GetComponent<Image>();
	}

	public void InitSkin(SkinData _SkinData)
	{
		m_SkinData = _SkinData;
	}

	public void OnSkinSelected()
	{
		m_SkinView.OnSkinSelected(m_SkinData);
	}

	public void SetColor(Color _Color)
	{
		m_Img.color = _Color;
	}
}
