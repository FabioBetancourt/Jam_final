using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public float volume;

    public Slider controlVolumenAudio;
    public Slider controlVolumenEffects;

    private List<AudioSource> audioSources;
    private List<AudioSource> effectSources;

    public Button stopButton;
    public AudioSource canvasAudioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSources = new List<AudioSource>();
        effectSources = new List<AudioSource>();

        GameObject[] audioObjects = GameObject.FindGameObjectsWithTag("audio");
        foreach (GameObject audioObject in audioObjects)
        {
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSources.Add(audioSource);
            }
        }

        GameObject[] effectObjects = GameObject.FindGameObjectsWithTag("sfx");
        foreach (GameObject effectObject in effectObjects)
        {
            AudioSource effectSource = effectObject.GetComponent<AudioSource>();
            if (effectSource != null)
            {
                effectSources.Add(effectSource);
            }
        }

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
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = controlVolumenAudio.value;
            }
        }

        foreach (AudioSource effectSource in effectSources)
        {
            if (effectSource != null)
            {
                effectSource.volume = controlVolumenEffects.value;
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
