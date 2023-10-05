using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


internal class SettingMenu : MonoBehaviour
{
    #region Fields and Properties

    private Canvas _settingsCanvas;
    private Button _backButton;

    [SerializeField] private Slider _masterVolume;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Slider _sfxVolume;

    #endregion

    #region Functions

    private void Awake()
    {
        _settingsCanvas = GetComponent<Canvas>();
        _settingsCanvas.enabled = false;
    }

    private void OnEnable()
    {
        MenuEvents.OnSettingsMenuOpened += OnSettingsOpened;
        MenuEvents.OnMainMenuOpened += OnOtherMenuOpened;
        MenuEvents.OnCreditsMenuOpened += OnOtherMenuOpened;
    }

    private void Start()
    {
        _backButton = GetComponentInChildren<Button>();

        _backButton.onClick.AddListener(OpenMainMenu);
        
        _masterVolume.onValueChanged.AddListener(SetMasterVolume);
        _musicVolume.onValueChanged.AddListener(SetMusicVolume);
        _sfxVolume.onValueChanged.AddListener(SetSoundVolume);       
    }

    private void OnDisable()
    {
        MenuEvents.OnSettingsMenuOpened -= OnSettingsOpened;
        MenuEvents.OnMainMenuOpened -= OnOtherMenuOpened;
        MenuEvents.OnCreditsMenuOpened -= OnOtherMenuOpened;
    }

    private void OnSettingsOpened()
    {
        _settingsCanvas.enabled = true;

        _masterVolume.value = AudioManager.Instance.MasterVolume;
        _musicVolume.value = AudioManager.Instance.MusicVolume;
        _sfxVolume.value = AudioManager.Instance.SoundVolume;
    }

    private void OnOtherMenuOpened()
    {
        _settingsCanvas.enabled = false;
    }

    private void OpenMainMenu()
    {
        MenuEvents.RaiseMainMenuOpened();
    }

    private void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    private void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    private void SetSoundVolume(float value)
    {
        AudioManager.Instance.SetSoundVolume(value);
    }

    public void ResumeGameButton()
    {
        PauseControl.Instance.ResumeGame();
        _settingsCanvas.enabled = false;
    }

    #endregion
}

