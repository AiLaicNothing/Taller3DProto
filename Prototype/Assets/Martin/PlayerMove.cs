using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rb;
    private Interaction interaction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();         
        interaction = gameObject.GetComponentInChildren<Interaction>();
    }

    void Update()
    {
        float Hori = Input.GetAxisRaw("Horizontal");
        float Vert = Input.GetAxisRaw("Vertical");

        if (!interaction.holdItem)
        {
            rb.velocity = new Vector3(Hori, 0f, Vert).normalized * speed;
        }
        else
        {
            rb.velocity = new Vector3(Hori, 0f, Vert).normalized * speed/1.5f;
        }

    }
}
