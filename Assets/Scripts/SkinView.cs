using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkinView : SingletonMB<SkinView>
{
	private const float c_PlayAnimDuration = 0.5f;

	public Text m_Price;

	public Button m_PurchaseButton;

	public GameObject m_PricePanel;

	private CanvasGroup m_Group;

	private GameManager m_GameManager;

	private MainMenuView m_MainMenuView;

	private MainCamera m_MainCamera;

	private SkinManager m_SkinManager;

	private SkinData m_SelectedSkin;

	private Image m_PurchaseButtonImage;

	private void Awake()
	{
		m_Group = GetComponent<CanvasGroup>();
		m_PurchaseButtonImage = m_PurchaseButton.GetComponent<Image>();
		m_GameManager = SingletonMB<GameManager>.Instance;
		m_MainCamera = SingletonMB<MainCamera>.Instance;
		m_SkinManager = SingletonMB<SkinManager>.Instance;
		Enable(_Enable: false);
	}

	private void Enable(bool _Enable)
	{
		m_Group.alpha = ((!_Enable) ? 0f : 1f);
		m_Group.interactable = _Enable;
		m_Group.blocksRaycasts = _Enable;
	}

	public void GoToSkinView()
	{
		DisablePrice();
		StartCoroutine(Appear());
		//VoodooSauce.HideCrossPromo();
	}

	public void ReturnMainMenu()
	{
		Enable(_Enable: false);
		m_SkinManager.ReturnMainMenu();
		m_MainCamera.GoToMainView();
		//VoodooSauce.ShowCrossPromo();
	}

	private IEnumerator Appear()
	{
		float time = 0f;
		while (time < 1f)
		{
			time += Time.deltaTime / 0.5f;
			m_Group.alpha = Mathf.Lerp(0f, 1f, time);
			yield return null;
		}
		Enable(_Enable: true);
		yield return null;
	}

	public void InitPrice(int _Price)
	{
		m_PricePanel.SetActive(value: true);
		m_Price.text = _Price.ToString();
		m_PurchaseButton.gameObject.SetActive(value: true);
		m_PurchaseButton.interactable = (m_GameManager.GetCurrency() >= _Price);
	}

	public void DisablePrice()
	{
		m_PricePanel.SetActive(value: false);
		m_PurchaseButton.gameObject.SetActive(value: false);
	}

	public void OnPurchaseSkin()
	{
		if (m_SkinManager.BuySkin(m_SelectedSkin))
		{
			m_SkinManager.OnSkinSelected(m_SelectedSkin);
		}
	}

	public void OnSkinSelected(SkinData _SkinData)
	{
		m_SelectedSkin = _SkinData;
		m_SkinManager.OnSkinSelected(_SkinData);
	}

	public void SetColor(Color _Color)
	{
		m_PurchaseButtonImage.color = _Color;
	}
}
