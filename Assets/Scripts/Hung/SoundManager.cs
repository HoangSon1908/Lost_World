using System;
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

    [Header("Audio Clips (Background and SFX)")]
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField] private AudioClip CardSwipeSFX;
    [SerializeField] private AudioClip OpenUISFX;
    [SerializeField] private AudioClip CloseUISFX;
    [SerializeField] private AudioClip SuccessBuySFX;

    public enum SFXType
    {
        CardSwipe,
        OpenUI,
        CloseUI,
        SuccessBuy
    }

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float backgroundMusicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.5f;
    public float volumeChangeThreshold = 0.1f;

    private float lastBackgroundMusicVolume;
    private float lastSFXVolume;

    [Header("UI Elements")]
    [SerializeField] private Slider backgroundMusicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle backgroundMusicMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;

    private bool isBackgroundMusicMuted = false;
    private bool isSfxMuted = false;

    private const string BG_MUSIC_VOLUME_KEY = "BG music volume";
    private const string SFX_VOLUME_KEY = "SFX Volume";
    private const string BG_MUSIC_MUTE_KEY = "BG Music Mute";
    private const string SFX_MUTE_KEY = "SFX Mute";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        LoadPreferences();

        backgroundMusicSource.volume = backgroundMusicVolume;
        sfxSource.volume = sfxVolume;

        if (backgroundMusicSlider != null)
        {
            backgroundMusicSlider.value = backgroundMusicVolume*2;
            backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        if (backgroundMusicMuteToggle != null)
        {
            backgroundMusicMuteToggle.isOn = !isBackgroundMusicMuted;
            backgroundMusicMuteToggle.onValueChanged.AddListener(ToggleBackgroundMusicMute);
        }
        if (sfxMuteToggle != null)
        {
            sfxMuteToggle.isOn = !isSfxMuted;
            sfxMuteToggle.onValueChanged.AddListener(ToggleSFXMute);
        }

        lastBackgroundMusicVolume = backgroundMusicVolume;
        lastSFXVolume = sfxVolume;

        PlayBackgroundMusic();
    }

    public void PlaySFX(SFXType sfx)
    {
        AudioClip clipToPlay = null;

        switch (sfx)
        {
            case SFXType.CardSwipe:
                clipToPlay = CardSwipeSFX;
                break;
            case SFXType.OpenUI:
                clipToPlay = OpenUISFX;
                break;
            case SFXType.CloseUI:
                clipToPlay = CloseUISFX;
                break;
            case SFXType.SuccessBuy:
                clipToPlay = SuccessBuySFX;
                break;
        }

        if (clipToPlay != null)
        {
            sfxSource.PlayOneShot(clipToPlay, sfxVolume);
        }
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusicSource.clip = backgroundMusicClip;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        volume = Mathf.Round(volume * 10f) / 10f;

        if (Mathf.Abs(volume - lastBackgroundMusicVolume) >= volumeChangeThreshold)
        {
            backgroundMusicVolume = volume * 0.5f;
            backgroundMusicSource.volume = backgroundMusicVolume;
            lastBackgroundMusicVolume = volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Round(volume * 10f) / 10f;

        if (Mathf.Abs(volume - lastSFXVolume) >= volumeChangeThreshold)
        {
            sfxVolume = volume;
            sfxSource.volume = sfxVolume;
            lastSFXVolume = volume;
        }
    }

    public void SaveVolume()
    {
        Debug.Log("Saving volume settings");
        PlayerPrefs.SetInt(BG_MUSIC_VOLUME_KEY, Mathf.RoundToInt(backgroundMusicVolume * 10));
        PlayerPrefs.SetInt(SFX_VOLUME_KEY, Mathf.RoundToInt(sfxVolume * 10));
    }

    public void ToggleBackgroundMusicMute(bool isMuted)
    {
        isBackgroundMusicMuted = isMuted;
        backgroundMusicSource.mute = isMuted;

        PlayerPrefs.SetInt(BG_MUSIC_MUTE_KEY, isMuted ? 1 : 0);
    }

    public void ToggleSFXMute(bool isMuted)
    {
        isSfxMuted = isMuted;
        sfxSource.mute = isMuted;

        PlayerPrefs.SetInt(SFX_MUTE_KEY, isMuted ? 1 : 0);
    }

    private void LoadPreferences()
    {
        // Load background music volume (chia 10 để đưa về giá trị float)
        backgroundMusicVolume = PlayerPrefs.GetInt(BG_MUSIC_VOLUME_KEY, 5) / 10f;

        // Load SFX volume (chia 10 để đưa về giá trị float)
        sfxVolume = PlayerPrefs.GetInt(SFX_VOLUME_KEY, 10) / 10f;

        // Load mute states
        isBackgroundMusicMuted = PlayerPrefs.GetInt(BG_MUSIC_MUTE_KEY, 0) == 1;
        isSfxMuted = PlayerPrefs.GetInt(SFX_MUTE_KEY, 0) == 1;
    }
}
