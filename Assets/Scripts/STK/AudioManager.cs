using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float volume;

    public Slider controlVolumenAudio;
    public Slider controlVolumenEffects;

    public List<GameObject> audios;
    public List<GameObject> effects;

    public Button stopButton;
    public AudioSource canvasAudioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audios = new List<GameObject>(GameObject.FindGameObjectsWithTag("audio"));
        effects = new List<GameObject>(GameObject.FindGameObjectsWithTag("sfx"));

        controlVolumenAudio.value = PlayerPrefs.GetFloat("volumenSave", 0.5f);
        controlVolumenEffects.value = PlayerPrefs.GetFloat("volumenSave", 0.5f);

        stopButton.onClick.AddListener(StopMusic);

        controlVolumenAudio.onValueChanged.AddListener(UpdateVolume);
        controlVolumenEffects.onValueChanged.AddListener(UpdateVolume);

        volume = controlVolumenAudio.value;

        UpdateAudioVolumes();
    }

    private void Update()
    {
        UpdateAudioVolumes();
    }

    private void UpdateAudioVolumes()
    {
        foreach (GameObject au in audios.ToArray())
        {
            if (au != null)
            {
                AudioSource audioSource = au.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.volume = controlVolumenAudio.value;
                }
            }
            else
            {
                audios.Remove(au);
            }
        }

        foreach (GameObject au in effects.ToArray())
        {
            if (au != null)
            {
                AudioSource audioSource = au.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.volume = controlVolumenEffects.value;
                }
            }
            else
            {
                effects.Remove(au);
            }
        }
    }

    public void UpdateVolume(float value)
    {
        volume = value;
    }

    public void guardarVolumen()
    {
        PlayerPrefs.SetFloat("volumenSave", controlVolumenAudio.value);
        PlayerPrefs.SetFloat("volumenSave", controlVolumenEffects.value);
    }

    private void StopMusic()
    {
        if (canvasAudioSource.isPlaying)
        {
            canvasAudioSource.Stop();
        }
    }
}
