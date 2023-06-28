using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEscena : MonoBehaviour
{
    public float velocidadRotacion;
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * velocidadRotacion);
    }
}
