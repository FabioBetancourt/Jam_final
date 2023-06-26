using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumen : MonoBehaviour
{
    public Slider controlVolumenAudio;       //La barra que representa la cantidad de volumen en el juego.
    public Slider controlVolumenEffects;

    public GameObject[] audios;         //Lista para ubicar todos los objetos que encuentre.
    public GameObject[] effects; 

    private void Start()
    {
        audios = GameObject.FindGameObjectsWithTag("audio");  //Busca todos los objetos con el tag audio.
        effects = GameObject.FindGameObjectsWithTag("sfx");
        controlVolumenAudio.value = PlayerPrefs.GetFloat("volumenSave", 1f); //Carga la informaci� preguardada de no tenerla lo deja con valor 1.
        controlVolumenEffects.value = PlayerPrefs.GetFloat("volumenSave", 1f); 
    }

    private void Update()
    {
        foreach (GameObject au in audios)
            au.GetComponent<AudioSource>().volume = controlVolumenAudio.value;    //Carga en todos los objetos con el tag audio el valor que corresponda.
        foreach (GameObject au in effects)
            au.GetComponent<AudioSource>().volume = controlVolumenEffects.value;
    }

    public void guardarVolumen()
    {
        PlayerPrefs.SetFloat("volumenSave", controlVolumenAudio.value);     //Guarda la informaci�n cuando cambiamos el slider.
        PlayerPrefs.SetFloat("volumenSave", controlVolumenEffects.value);
    }
} 