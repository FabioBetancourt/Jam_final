using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioMixerGroup Master;
    [SerializeField] private AudioMixerGroup Music;
    [SerializeField] private AudioMixerGroup SoundFX;
    
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSoundFX;

    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundFXVolumeKey = "SoundFXVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        sliderMaster.onValueChanged.AddListener(SetMaster);
        sliderMusic.onValueChanged.AddListener(SetMusic);
        sliderSoundFX.onValueChanged.AddListener(SetSoundFX);
    }

    private void Start()
    {
        LoadVolumes();
    }

    public void MuteMaster()
    {
        Master.audioMixer.SetFloat("Master", -80);
        PlayerPrefs.SetFloat(MasterVolumeKey, -80);
    }

    public void MuteMusic()
    {
        Music.audioMixer.SetFloat("Music", -80);
        PlayerPrefs.SetFloat(MusicVolumeKey, -80);
    }
    
    public void MuteSoundFX()
    {
        SoundFX.audioMixer.SetFloat("SoundFX", -80);
        PlayerPrefs.SetFloat(SoundFXVolumeKey, -80);
    }
    
    private void SetMaster(float value)
    {
        Master.audioMixer.SetFloat("Master", value);
        PlayerPrefs.SetFloat(MasterVolumeKey, value);
    }
    
    private void SetMusic(float value)
    {
        Music.audioMixer.SetFloat("Music", value);
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
    }
      
    private void SetSoundFX(float value)
    {
        SoundFX.audioMixer.SetFloat("SoundFX", value);
        PlayerPrefs.SetFloat(SoundFXVolumeKey, value);
    }

    private void LoadVolumes()
    {
        if (PlayerPrefs.HasKey(MasterVolumeKey))
        {
            float masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey);
            sliderMaster.value = masterVolume;
            SetMaster(masterVolume);
        }

        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            sliderMusic.value = musicVolume;
            SetMusic(musicVolume);
        }

        if (PlayerPrefs.HasKey(SoundFXVolumeKey))
        {
            float soundFXVolume = PlayerPrefs.GetFloat(SoundFXVolumeKey);
            sliderSoundFX.value = soundFXVolume;
            SetSoundFX(soundFXVolume);
        }
    }
   
}
