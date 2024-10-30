using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Carga la escena "Muerte"
            SceneManager.LoadScene("Muerte");
        }
    }
}