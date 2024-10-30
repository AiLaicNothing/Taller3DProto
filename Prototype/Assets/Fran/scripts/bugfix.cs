using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugfix : MonoBehaviour
{
  
    public Color newColor = Color.yellow; // Color a cambiar

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Verificar si se hizo clic izquierdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Realizar el raycast a una distancia de 5 unidades
            if (Physics.Raycast(ray, out hit, 5f))
            {
                ChangeColor(hit.collider.gameObject);
            }
        }
    }
    //---------------------------------------------------------------------------------------------------------------------
    private void ChangeColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Cambiar el color del material del objeto a amarillo
            renderer.material.color = newColor;
            Debug.Log("se ha reparado: " + obj.name);
        }
        else
        {
            Debug.LogWarning("El objeto " + obj.name + " no es reparable.");
        }
    }
}