using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private EventInstance _menuMusicEventInstance;
    private EventInstance _levelMusicEventInstance;

    [field: SerializeField] public EventReference _menuMusic { get; private set; }
    [field: SerializeField] public EventReference _levelMusic { get; private set; }

    [Header("Volume")]
    [Range(0, 1)] public float MasterVolume = 1;
    [Range(0, 1)] public float MusicVolume = 1;
    [Range(0, 1)] public float SoundVolume = 1;

    private Bus _masterBus;
    private Bus _soundBus;
    private Bus _musicBus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _masterBus = RuntimeManager.GetBus("bus:/");
        _musicBus = RuntimeManager.GetBus("bus:/Music");
        _soundBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Update()
    {
        _masterBus.setVolume(MasterVolume);
        _musicBus.setVolume(MusicVolume);
        _soundBus.setVolume(SoundVolume);
    }

    private void Start()
    {
        _menuMusicEventInstance = CreateEventInstance(_menuMusic);
        _levelMusicEventInstance = CreateEventInstance(_levelMusic);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    public void PlayMenuMusic()
    {
        PLAYBACK_STATE playbackState;
        _menuMusicEventInstance.getPlaybackState(out playbackState);
        
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            _menuMusicEventInstance.start();
    }

    public void PlayLevelMusic()
    {
        PLAYBACK_STATE playbackState;
        _levelMusicEventInstance.getPlaybackState(out playbackState);

        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            _levelMusicEventInstance.start();
    }

    public void StopMenuMusic()
    {
        _menuMusicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopLevelMusic()
    {
        _levelMusicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
