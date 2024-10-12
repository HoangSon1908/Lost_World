using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private List<AudioClip> backgroundMusicClips;
    [SerializeField] private List<AudioClip> sfxClips;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float backgroundMusicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.5f;

    [Header("UI Elements")]
    [SerializeField] private Slider backgroundMusicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle backgroundMusicMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;

    private bool isBackgroundMusicMuted = false;
    private bool isSfxMuted = false;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Set initial volume values and UI states
        backgroundMusicSource.volume = backgroundMusicVolume;
        sfxSource.volume = sfxVolume;

        // Initialize Sliders and Toggles
        if (backgroundMusicSlider != null)
        {
            backgroundMusicSlider.value = backgroundMusicVolume;
            backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        if (backgroundMusicMuteToggle != null)
        {
            backgroundMusicMuteToggle.isOn = isBackgroundMusicMuted;
            backgroundMusicMuteToggle.onValueChanged.AddListener(ToggleBackgroundMusicMute);
        }
        if (sfxMuteToggle != null)
        {
            sfxMuteToggle.isOn = isSfxMuted;
            sfxMuteToggle.onValueChanged.AddListener(ToggleSFXMute);
        }

        // Play the first background music
        if (backgroundMusicClips.Count > 0)
        {
            PlayBackgroundMusic(0);
        }
    }

    // Play background music
    public void PlayBackgroundMusic(int index)
    {
        if (index >= 0 && index < backgroundMusicClips.Count)
        {
            backgroundMusicSource.clip = backgroundMusicClips[index];
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    // Play SFX
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Count)
        {
            sfxSource.PlayOneShot(sfxClips[index], sfxVolume);
        }
    }

    // Adjust the background music volume
    public void SetBackgroundMusicVolume(float volume)
    {
        if (!isBackgroundMusicMuted)
        {
            backgroundMusicVolume = volume;
            backgroundMusicSource.volume = backgroundMusicVolume;
        }
    }

    // Adjust the SFX volume
    public void SetSFXVolume(float volume)
    {
        if (!isSfxMuted)
        {
            sfxVolume = volume;
            sfxSource.volume = sfxVolume;
        }
    }

    // Toggle mute for background music
    public void ToggleBackgroundMusicMute(bool isMuted)
    {
        isBackgroundMusicMuted = isMuted;
        backgroundMusicSource.mute = isBackgroundMusicMuted;
    }

    // Toggle mute for SFX
    public void ToggleSFXMute(bool isMuted)
    {
        isSfxMuted = isMuted;
        sfxSource.mute = isSfxMuted;
    }

    // Stop background music
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }
}
