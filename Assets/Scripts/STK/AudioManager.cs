
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource effectsSourse;
    public AudioSource musicSourse;

    public AudioClip menuMusic;
    public AudioClip levelMusic;
    public AudioClip effectsSound;

    public Slider sliderMusic, sliderSFX;
    
    public void PlayEffect()
    {
        effectsSourse.PlayOneShot(effectsSound);
    }
    
    public void PlaySong(AudioClip audioClip)
    {
        musicSourse.clip = audioClip;
        musicSourse.Play();
    }

    public void OnMusicVolumeUpdate()
    {
        musicSourse.volume = sliderMusic.value;
    }
    
    public void OnSFXVolumeUpdate()
    {
        effectsSourse.volume = sliderSFX.value;
    }
} 
