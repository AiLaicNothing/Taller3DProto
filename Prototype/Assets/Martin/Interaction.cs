using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform holdPos;

    private bool inRange;
    private bool holdingItem = false;
    private GameObject itemInHand;
    private GameObject item;

    private PlayerMove playerMove;

    public bool holdItem
    {
        get { return holdingItem; }
        set { holdingItem = value; }
    }
    private void Start()
    {
        playerMove = gameObject.GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        if (holdingItem)
        {
            itemInHand.transform.position = holdPos.position;
            itemInHand.transform.rotation = holdPos.rotation;
        }
        if (inRange && !holdingItem)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Grab Item");
                HoldItem(item);
            }
        }
        else if (holdingItem)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("DropItem");
                DropItem(item);
            }
        }
    }
    private void HoldItem(GameObject Object)
    {
        holdingItem = true;
        itemInHand = Object;

        Rigidbody rb = Object.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

    }

    private void DropItem(GameObject Object)
    {
        holdingItem = false;
        itemInHand = null;
        Object.GetComponent<Collider>().enabled = true; // Enable collider when dropped

        Rigidbody rb = Object.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            Debug.Log("Hay una canica");
            item = other.gameObject;
            inRange = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            item = null;
            Debug.Log("salio una canica");
            inRange = false;
        }
    }

}
