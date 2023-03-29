using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;
using System;

public class PlayerController : MonoBehaviour
{
    private MeshRenderer mr;

    //Clockwise
    [SerializeField] Material defMaterial;
    [SerializeField] Material upMaterial;
    [SerializeField] Material rightMaterial;
    [SerializeField] Material downMaterial;
    [SerializeField] Material leftMaterial;

    private Rigidbody rb;
    [SerializeField] private float defSpeed = 10f;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        playerInput();

        //Update the UI to show slider %age
        text.text = (slider.value * 100f).ToString("F0") + "%";
    }

    private void playerInput()
    {
        //Stores the WASD/Joystick input
        Vector3 pInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Updates the player's position
        rb.MovePosition(transform.position + pInput * Time.deltaTime * (defSpeed * slider.value));

        materialSwitch();

        //Change slider value
        if (Input.GetKeyDown(KeyCode.Equals)) //increase
        {
            slider.value += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.Minus)) //decrease
        {
            slider.value -= 0.1f;
        }
    }

    //change material of player object based in input values
    private void materialSwitch()
    {
        //reset material
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            mr.material = defMaterial;

        //Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            mr.material = rightMaterial;

            //Respawn - moved out of view
            if (transform.position.x > 11f)
                transform.position = new Vector3(-11f, 0.5f, transform.position.z);
        }

        //Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            mr.material = leftMaterial;

            //Respawn - moved out of view
            if (transform.position.x < -11f)
                transform.position = new Vector3(11f, 0.5f, transform.position.z);
        }

        //Up
        if (Input.GetAxis("Vertical") > 0)
        {
            mr.material = upMaterial;

            //Respawn - moved out of view
            if (transform.position.z > 6f)
                transform.position = new Vector3(transform.position.x, 0.5f, -6f);
        }

        //Down
        if (Input.GetAxis("Vertical") < 0)
        {
            mr.material = downMaterial;

            //Reset - moved out of view
            if (transform.position.z < -6f)
                transform.position = new Vector3(transform.position.x, 0.5f, 6f);
        }
    }
}
