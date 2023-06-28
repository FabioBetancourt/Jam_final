using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumen : MonoBehaviour
{
    public Slider controlVolumenAudio; //La barra que representa la cantidad de volumen en el juego.
    public Slider controlVolumenEffects; //La barra que representa la cantidad de volumen en el juego.

    public GameObject[] audios; //Lista para ubicar todos los objetos que encuentre.
    public GameObject[] effects; //Lista para ubicar todos los objetos que encuentre.

    public Button stopButton;
    public AudioSource canvasAudioSource;

    private void Start()
    {
        audios = GameObject.FindGameObjectsWithTag("audio"); //Busca todos los objetos con el tag "audio".
        effects = GameObject.FindGameObjectsWithTag("sfx"); //Busca todos los objetos con el tag "sfx".
        controlVolumenAudio.value = PlayerPrefs.GetFloat("volumenSave", 1f); //Carga la información preguardada, si no tiene valor preguardado, lo deja con valor 1.
        controlVolumenEffects.value = PlayerPrefs.GetFloat("volumenSave", 1f); //Carga la información preguardada, si no tiene valor preguardado, lo deja con valor 1.

        stopButton.onClick.AddListener(StopMusic);
    }

    private void Update()
    {
        foreach (GameObject au in audios)
            au.GetComponent<AudioSource>().volume = controlVolumenAudio.value; //Carga en todos los objetos con el tag "audio" el valor que corresponda.
        foreach (GameObject au in effects)
            au.GetComponent<AudioSource>().volume = controlVolumenEffects.value; //Carga en todos los objetos con el tag "sfx" el valor que corresponda.
    }

    public void guardarVolumen()
    {
        PlayerPrefs.SetFloat("volumenSave", controlVolumenAudio.value); //Guarda la información cuando cambiamos el slider.
        PlayerPrefs.SetFloat("volumenSave", controlVolumenEffects.value); //Guarda la información cuando cambiamos el slider.
    }

    private void StopMusic()
    {
        if (canvasAudioSource.isPlaying)
        {
            canvasAudioSource.Stop();
        }
    }
}