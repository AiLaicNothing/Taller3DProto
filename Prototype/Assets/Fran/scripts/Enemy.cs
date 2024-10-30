using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 
    [SerializeField] private string enemyName;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float healthPoint;
    [SerializeField] private float maxHealthPoint;

    private Transform target; // nuestro target es nuestro player
 

    private void Start()
    {
        healthPoint = maxHealthPoint;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Introduction();
    }

    private void Update()
    {
        Move();
        return;
    }

    protected virtual void Introduction()
    {
        UnityEngine.Debug.Log("My Name is " + enemyName + ", HP: " + healthPoint + ", moveSpeed: " + moveSpeed);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}