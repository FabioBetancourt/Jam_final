using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frontline2Lava : MonoBehaviour
{
    public AudioSource audioLava;
    public AudioSource audioDesert;
    private bool reduce;

    private void Update()
    {
        if (reduce)
        {
            audioLava.volume += 0.7f * Time.deltaTime;
            audioDesert.volume += 0.7f * Time.deltaTime;
        }
        else
        {
            audioLava.volume -= 0.5f * Time.deltaTime;
            audioDesert.volume -= 0.5f * Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioLava.Play();
            audioDesert.Play();
            audioLava.enabled = true;
            audioDesert.enabled = true;
            reduce = !reduce;
        }
    }
}