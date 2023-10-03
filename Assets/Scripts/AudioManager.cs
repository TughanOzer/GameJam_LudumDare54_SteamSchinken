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
    }

    private void Update()
    {
        RuntimeManager.GetBus("bus:/").setVolume(MasterVolume);
        RuntimeManager.GetBus("bus:/Music").setVolume(MusicVolume);
        RuntimeManager.GetBus("bus:/SFX").setVolume(SoundVolume);
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
        _menuMusicEventInstance.release();
        _menuMusicEventInstance = CreateEventInstance(_menuMusic);
        _menuMusicEventInstance.getPlaybackState(out playbackState);
        
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            _menuMusicEventInstance.start();
    }

    public void PlayLevelMusic()
    {
        PLAYBACK_STATE playbackState;
        _levelMusicEventInstance.release();
        _levelMusicEventInstance = CreateEventInstance(_levelMusic);
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
