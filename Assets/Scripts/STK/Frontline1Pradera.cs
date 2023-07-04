using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frontline1Pradera : MonoBehaviour
{
    public AudioSource audioFrontline;
    public AudioSource audioMorning;
    private bool reduce;

    private void Update()
    {
        if (reduce)
        {
            audioFrontline.volume -= 0.5f * Time.deltaTime;
            audioMorning.volume -= 0.5f * Time.deltaTime;
        }
        else
        {
            audioFrontline.volume += 0.7f * Time.deltaTime;
            audioMorning.volume += 0.7f * Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            reduce = !reduce;
        }
    }
}