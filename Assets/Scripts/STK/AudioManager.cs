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
    public void MuteMaster()
    {
        Master.audioMixer.SetFloat("Master", -80);
    }

    public void MuteMusic()
    {
        Music.audioMixer.SetFloat("Music", -80);
    }
    
    public void MuteSoundFX()
    {
        SoundFX.audioMixer.SetFloat("SoundFX", -80);
    }
    private void SetMaster(float value)
    {
        Master.audioMixer.SetFloat("Master", value);
    }
    private void SetMusic(float value)
    {
        Music.audioMixer.SetFloat("Music", value);
    }
      
    private void SetSoundFX(float value)
    {
        SoundFX.audioMixer.SetFloat("SoundFX", value);
    }
   
}