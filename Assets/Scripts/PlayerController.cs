using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

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
    private float speed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
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

        materialSwitch();
    }

    private void materialSwitch()
    {
        //reset material
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            mr.material = defMaterial;

        //Right
        if (Input.GetAxis("Horizontal") > 0)
            mr.material = rightMaterial;

        //Left
        if (Input.GetAxis("Horizontal") < 0)
            mr.material = leftMaterial;

        //Up
        if (Input.GetAxis("Vertical") > 0)
            mr.material = upMaterial;

        //Down
        if (Input.GetAxis("Vertical") < 0)
            mr.material = downMaterial;
    }
}
