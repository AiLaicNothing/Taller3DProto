using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugfix : MonoBehaviour
{
    public Color newColor = Color.yellow; // Color a cambiar
    public LayerMask bugLayer; // LayerMask para la capa "bug"

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Comprobar si el objeto está en la capa "bug"
                if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & bugLayer) != 0)
                {
                    ChangeColor(hit.collider.gameObject);
                }
            }
        }
    }

    private void ChangeColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Cambiar el color del material del objeto
            renderer.material.color = newColor;
            Debug.Log("Cambiado el color de " + obj.name + " a " + newColor);
        }
    }
}