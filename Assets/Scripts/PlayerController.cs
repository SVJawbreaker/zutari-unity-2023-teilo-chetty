using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerInput();
    }

    private void playerInput()
    {
        //Stores the WASD/Joystick input
        Vector3 pInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Updates the player's position
        rb.MovePosition(transform.position + pInput * Time.deltaTime * speed);
    }
}
