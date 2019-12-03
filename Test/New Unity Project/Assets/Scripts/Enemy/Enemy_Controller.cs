using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Rigidbody rb;

    public Transform player;
    public float moveSpeed;
    public float range;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Look at player target
        transform.LookAt(player);

        Vector3 playerDirection = transform.forward * moveSpeed * Time.fixedDeltaTime;

        // if distance between enemy position and player position is less than the range
        //      move enemy position to player position
        if (Vector3.Distance(transform.position, player.position) <= range)
        {
            rb.MovePosition(transform.position + playerDirection);
        }
    }
}
