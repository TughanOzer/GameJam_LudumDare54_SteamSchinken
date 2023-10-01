using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

//This script is to be attached to the MainMenuCanvas in the MainMenu Scene
internal class MainMenu : MonoBehaviour
{
    #region Fields and Properties

    private Canvas _menuCanvas;

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _settingsButton;
    [SerializeField] private GameObject _creditsButton;

    #endregion

    #region Functions

    private void Awake()
    {
        _menuCanvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        MenuEvents.OnMainMenuOpened += OnMainMenuOpened;
        MenuEvents.OnSettingsMenuOpened += OnOtherMenuOpened;
        MenuEvents.OnCreditsMenuOpened += OnOtherMenuOpened;
    }

    private void Start()
    {
        _startButton.GetComponent<Button>().onClick.AddListener(NewGame);
        _settingsButton.GetComponent<Button>().onClick.AddListener(OpenSettings);
        _creditsButton.GetComponent<Button>().onClick.AddListener(OpenCredits);

        AudioManager.Instance.PlayMenuMusic();
    }

    private void OnDisable()
    {
        MenuEvents.OnMainMenuOpened -= OnMainMenuOpened;
        MenuEvents.OnSettingsMenuOpened -= OnOtherMenuOpened;
        MenuEvents.OnCreditsMenuOpened -= OnOtherMenuOpened;
    }

    private void OnMainMenuOpened()
    {
        _menuCanvas.enabled = true;
    }

    private void OnOtherMenuOpened()
    {
        _menuCanvas.enabled = false;
    }

    public void OpenSettings()
    {
        PlayButtonClick();
        MenuEvents.RaiseSettingsMenuOpened();
    }

    public void OpenCredits()
    {
        PlayButtonClick();
        MenuEvents.RaiseCreditsOpened();
    }

    public void NewGame()
    {
        PlayButtonClick();
        AudioManager.Instance.StopMenuMusic();
        AudioManager.Instance.PlayLevelMusic();
        SceneManager.LoadSceneAsync(1); //1 should be first level; 0 should be main menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void PlayButtonClick()
    {
        //AudioManager.Instance.PlaySoundEffectOnce(SFX._0001_ButtonClick);
    }

    #endregion
}

