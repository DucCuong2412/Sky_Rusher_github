using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : SingletonMB<MainMenuView>
{
    private const float c_PlayAnimDuration = 0.5f;

    public GameObject m_PlayButton;

    public Text m_TitleImage;

    public Image m_LevelImage;

    public Text m_LevelText;

    public Image m_SkinImage;

    public Image m_Challenge;

    public Image m_SettingsImage;

    //public Image m_NoAdsImage;

    //public Image m_RestorePurchaseImage;

    //public Image m_PrivatePolicy;

    //public Image m_VibrationImage;
    public Image moregame;

    public Image infore;

    public Image m_SoundImage;

    public Sprite m_VibrationOn;

    public Sprite m_VibrationOff;

    public Sprite m_SoundOn;

    public Sprite m_SoundOff;

    public GameObject m_SettingsMenu;

    public GameObject m_NoAdsButton;

    private CanvasGroup m_Group;

    private GameManager m_GameManager;

    private MainCamera m_MainCamera;

    private SkinView m_SkinView;

    private ChallengeView m_ChallengeView;

    private PurchaseDelegate m_PurchaseDelegate;

    private SoundManager m_SoundManager;
    public GameObject panelInfor;
    public TextMeshProUGUI textVersion;

    private bool m_SettingMenuEnable;

    private void Awake()
    {
        m_Group = GetComponent<CanvasGroup>();
        m_GameManager = SingletonMB<GameManager>.Instance;
        m_MainCamera = SingletonMB<MainCamera>.Instance;
        m_SkinView = SingletonMB<SkinView>.Instance;
        m_ChallengeView = SingletonMB<ChallengeView>.Instance;
        m_PurchaseDelegate = SingletonMB<PurchaseDelegate>.Instance;
        m_SoundManager = SingletonMB<SoundManager>.Instance;
        m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
    }

    private void Start()
    {
        RefreshNoAds();
        textVersion.text = Application.version;

    }

    private void OnGamePhaseChanged(GamePhase _Phase)
    {
        switch (_Phase)
        {
            case GamePhase.MAIN_MENU:
                if (!m_GameManager.isChallenge && !m_GameManager.m_IsBonusStage)
                {
                    m_LevelText.text = "LVL " + m_GameManager.GetLevel().ToString();
                    StartCoroutine(Appear(_Appear: true));
                }
                else if (m_GameManager.m_IsBonusStage)
                {
                    m_LevelText.text = "BONUS";
                    StartCoroutine(Appear(_Appear: true));
                }
                break;
            case GamePhase.INTRO:
                if (!m_GameManager.isChallenge)
                {
                    StartCoroutine(Appear(_Appear: false));
                }
                break;
        }
    }

    public void SetColor(Color _Color)
    {
        m_TitleImage.color = _Color;
        m_LevelImage.color = _Color;
        m_LevelText.color = _Color;
        m_SkinImage.color = _Color;
        m_Challenge.color = _Color;
        //m_VibrationImage.color = _Color;
        //m_PrivatePolicy.color = _Color;
        m_SettingsImage.color = _Color;
        //m_NoAdsImage.color = _Color;
        //m_RestorePurchaseImage.color = _Color;
        //m_VibrationImage.color = _Color;
        infore.color = _Color;
        moregame.color = _Color;

        m_SoundImage.color = _Color;
    }

    public void OnPlayButton()
    {
        if (m_GameManager.currentPhase == GamePhase.MAIN_MENU)
        {
            m_GameManager.ChangePhase(GamePhase.INTRO);
            SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
        }
    }

    public void OnSkinButton()
    {
        if (m_GameManager.currentPhase == GamePhase.MAIN_MENU)
        {
            m_MainCamera.GoToSkinView();
            m_SkinView.GoToSkinView();
            StartCoroutine(Appear(_Appear: false));
            SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
        }
    }
    public void infor()
    {
        if (m_GameManager.currentPhase == GamePhase.MAIN_MENU)
        {
            panelInfor.SetActive(true);
        }
    }
    public void close()
    {
        panelInfor.SetActive(false);
    }
    public void OnChallengeButton()
    {
        if (m_GameManager.currentPhase == GamePhase.MAIN_MENU)
        {
            m_ChallengeView.GotoChallengeView();
            StartCoroutine(Appear(_Appear: false));
            SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
        }
    }

    public void OnVibrationButton()
    {
        if (m_GameManager.currentPhase == GamePhase.MAIN_MENU)
        {
            m_GameManager.ToggleVibrations();
            if (m_GameManager.GetVibrations())
            {
                //m_VibrationImage.sprite = m_VibrationOn;
                //Handheld.Vibrate();
            }
            else
            {
                //m_VibrationImage.sprite = m_VibrationOff;
            }
            SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
        }
    }

    public void OnSoundButton()
    {
        if (m_GameManager.currentPhase == GamePhase.MAIN_MENU)
        {
            m_GameManager.ToggleSound();
            if (m_GameManager.GetSound())
            {
                m_SoundImage.sprite = m_SoundOn;
                m_SoundManager.ActivateSounds();
            }
            else
            {
                m_SoundImage.sprite = m_SoundOff;
                m_SoundManager.DeactivateSounds();
            }
            SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
        }
    }

    public void OnPurchaseNoAds()
    {
        m_PurchaseDelegate.Purchase(Constants.c_NoAdsBundleID);
        SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
    }

    public void OnRestorePurchases()
    {
        m_PurchaseDelegate.RestorePurchase();
        SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
    }

    public void SettingsMenu()
    {
        m_SettingMenuEnable = !m_SettingMenuEnable;
        if (m_SettingMenuEnable)
        {
            m_SettingsMenu.gameObject.SetActive(value: true);
        }
        else
        {
            m_SettingsMenu.gameObject.SetActive(value: false);
        }
        SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(Appear(_Appear: true));
        SingletonMB<SoundManager>.Instance.PlaySound(ESoundType.CLICK);
    }

    private void Enable(bool _Enable)
    {
        m_Group.interactable = _Enable;
        m_Group.blocksRaycasts = _Enable;
        m_PlayButton.SetActive(_Enable);
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

    public void RefreshNoAds()
    {
        //m_NoAdsButton.SetActive(!VoodooSauce.IsPremium());
    }

    public void PrivatePolicy()
    {
        //VoodooSauce.RequestGdprApplicability(ShowPrivacySettings);
    }

    public void ShowPrivacySettings(bool _isApplicable)
    {
        if (_isApplicable)
        {
            UnityEngine.Debug.Log("Showing privacy settings ...");
            //VoodooSauce.ShowGDPRSettings();
        }
        else
        {
            UnityEngine.Debug.Log("GDPR doesn't apply, hiding privacy settings button.");
            base.gameObject.SetActive(value: false);
        }
    }
}
